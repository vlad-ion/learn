#include <stdio.h>

class A
{
  short a;

public:

  virtual int da();
  virtual int ia();
  const char bla[3];
  static long aaa[2];

};

const int thing(int &a, int&b)
{
  a = b;
  return a;
}

int main()
{
  int a, b;
  //A aaaaaaa;
  thing(a,b);
  printf("class A size %d\n", sizeof(A));
} 
