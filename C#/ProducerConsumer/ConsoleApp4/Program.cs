using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ConsoleApp4
{
    class Program
    {
        const int producerCount = 60;
        const int consumerCount = 30;
        const int maxItems = 500;

        static void Main(string[] args)
        {
            Console.WriteLine("Consume Produce");
            var prog = new Program();


            //Buffer

            var queue = new BufferBlock<int>(new DataflowBlockOptions { BoundedCapacity = 5 });


            //Consumers

            var consumerOpt = new ExecutionDataflowBlockOptions { BoundedCapacity = 1 };
            var linkOpt = new DataflowLinkOptions { PropagateCompletion = true };

            var items = new List<(Task, List<int>)>();
            for (int i = 0; i < consumerCount; i++)
            {
                var results1 = new List<int>();
                var c1 = new ActionBlock<int>(x => results1.Add(x), consumerOpt);
                queue.LinkTo(c1, linkOpt);
                items.Add((c1.Completion, results1));
            }          


            //Producers

            var producers = prog.ProduceAll(queue, maxItems);


            //Result

            var tasks = items.ConvertAll(x => x.Item1);
            tasks.Add(producers);
            Task.WhenAll(tasks).Wait();


            int count = 0;
            items.ForEach(x => count += x.Item2.Count);
            Console.WriteLine(count);

        }
                
        static Random rand = new Random((int)DateTime.Now.Ticks);
        async Task Produce(BufferBlock<int> queue, int maxItems)
        {
            int produced = 0;
            while (produced < maxItems)
            {
                produced++;
                await queue.SendAsync(rand.Next());
            }
        }

        async Task ProduceAll(BufferBlock<int> queue, int maxItems)
        {
            var tasks = new List<Task>();
            for (int i = 0; i < producerCount; i++)
            {
                var p1 = Produce(queue, maxItems);
                tasks.Add(p1);
                
            }
            await Task.WhenAll(tasks);
            queue.Complete();
        }

    }
}
