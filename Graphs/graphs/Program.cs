using DotNetty.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace graphs
{
    class Program
    {
        static void Main()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {1, new List<int> { 2, 3, 4} },
                {2, new List<int> { 4 } },
                {3, new List<int> { 4, 2 } },
                {4, new List<int> {3} } //with cycles
                //{4, new List<int> {} } //no cycles
            };

            var dfs = new Topo();
            Console.WriteLine(" ====== DFS and topological sort =====");
            dfs.DFS(graph, 1);

            Console.WriteLine();

            var weightedGraph = new Dictionary<int, List<Edge>>
            {
                {1, new List<Edge> {
                    new Edge { next = 2, weight = 10 },
                    new Edge { next = 3, weight = 20 },
                    new Edge{ next = 4, weight = 15 } } },
                {2, new List<Edge> {
                    new Edge{ next = 4, weight = 2} } },
                {3, new List<Edge> { 
                    new Edge { next = 4, weight = 15 },
                    new Edge{ next = 2, weight = 20 } } },
                //{4, new List<(int next, int weight)> {3} } //with cycles
                {4, new List<Edge> {} } //no cycles
            };

            var weightedGraph2 = new Dictionary<int, List<Edge>>
            {
                {1, new List<Edge> { new Edge { next = 2, weight = 10 }, new Edge { next = 3, weight = 21 } } },
                {2, new List<Edge> { new Edge { next = 3, weight = 10 } } },
                {3, new List<Edge> { new Edge { next = 1, weight = 10 }, new Edge { next = 3, weight = 10 } } }
            };

            var djk = new DijkstraAlg();
            Console.WriteLine("====== Dijkstra shortest paths =======");
            djk.Dijkstra(weightedGraph, 1);

            Console.ReadKey();
        }
    }

    class Edge
    {
        public int next;
        public int weight;
    }

    class Node
    {
        public int id;
        public int? parent;
        public int distance;
    }

    class DijkstraAlg
    {
        public void Dijkstra(Dictionary<int, List<Edge>> graph, int start)
        {
            //initialize stuff
            var state = new Dictionary<int, Node>();
            PriorityQueue<Node> prio = new PriorityQueue<Node>(
                Comparer<Node>.Create((x,y) => x.distance.CompareTo(y.distance)));

            foreach (int node in graph.Keys)
            {
                state[node] = new Node { id = node, parent = null, distance = int.MaxValue };
            }
            state[start].distance = 0;

            prio.Enqueue(state[start]);

            //run
            DijkstraVisit(graph, prio, state);
        }

        private void DijkstraVisit(Dictionary<int, List<Edge>> graph, PriorityQueue<Node> prio,
            Dictionary<int, Node> state)
        {
            while (prio.Count > 0)
            {
                Node closestNode = prio.Dequeue();

                string parent = closestNode.parent.HasValue ? closestNode.parent.Value.ToString() : "nil";
                Console.WriteLine($"Picked node {closestNode.id} with distance {closestNode.distance} and parent {parent}");

                foreach (Edge edge in graph[closestNode.id])
                {
                    Relax(prio, state, closestNode, edge);
                }
            }
        }

        private static void Relax(PriorityQueue<Node> prio, Dictionary<int, Node> state, Node closestNode, Edge edge)
        {
            int node = edge.next;
            int newDistance = closestNode.distance + edge.weight;
            if (newDistance < state[node].distance) //shorter path found
            {
                if (state.TryGetValue(node, out Node prevEntry))
                {
                    prio.Remove(prevEntry);
                }

                state[node].parent = closestNode.id;
                state[node].distance = newDistance;

                prio.Enqueue(state[node]);
            }
        }
    }

    class Topo
    {
        public void DFS(Dictionary<int, List<int>> graph, int start)
        {
            //initialize stuff
            int time = 0;
            var state = new Dictionary<int, (int parent, bool started, int? start, int? end)>
                { { 1, ( parent: int.MinValue, started: true, start: time, end: null) } };
            var topo = new Stack<int>();

            DFSVisit(graph, start, state, topo, ref time);

            Console.WriteLine($"Topological sort: [{string.Join(", ",topo)}]");
        }

        private void DFSVisit(Dictionary<int, List<int>> graph, int node, 
            Dictionary<int, (int parent, bool started, int? start, int? end)> state, Stack<int> topo,
            ref int time)
        {
            time++;

            foreach (int next in graph[node])
            {
                if (state.ContainsKey(next) && state[next].started) //next is visited but not finished
                {
                    Console.WriteLine($"Back edge {node} -> {next}");

                    Console.WriteLine("Cycle found, no topological sort possible. ");
                }

                if (state.ContainsKey(next) && !state[next].started) //next is visited and finished
                {
                    if (state[node].start < state[next].start)
                    {
                        Console.WriteLine($"Forward edge {node} -> {next}");
                    }
                    else
                    {
                        Console.WriteLine($"Cross edge {node} -> {next}");
                    }
                }

                if (!state.ContainsKey(next)) //next is not visited
                {
                    Console.WriteLine($"Tree edge {node} -> {next}");

                    state[next]= (parent: node, started: true, start: time, end: null );
                    DFSVisit(graph, next, state, topo, ref time);
                    state[next]= (state[next].parent, false, state[next].start, end: time);
                }
            }

            topo.Push(node);
        }
    }
}
