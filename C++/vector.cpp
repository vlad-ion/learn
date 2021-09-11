#include <vector>
using namespace std;

typedef struct
{
	long lol;
	long lol2[26];
	long ita;
} ceva;

int main()
{
try
{	vector<ceva> v;
	ceva c;
	c.lol = 11;
	c.ita = 22;
	v.push_back(c);
	printf("end: %d\n",(v.end()++)->ita);
	return 0;
}
catch (...)
{
	printf("Hexception caught\n");
}
}
