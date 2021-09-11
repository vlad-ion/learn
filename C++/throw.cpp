#include <stdio.h>

int main()
{
	try
	{
		throw 0;
	}

	catch (char * s)
	{
	}
	catch(int e)
	{
		throw;
	}
	printf("lol\n");
	catch(int e)
	{
	}
}

