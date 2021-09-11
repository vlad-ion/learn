#include <stdio.h>
#include <string.h>

typedef unsigned char BYTE;

class Base1 {

public:

    virtual void Hi1()
    {    printf("Hi from Base1!\n");  }

    BYTE a1[100];

};

 

class Base2 {

public:

    virtual void Hi2()
    {    printf("Hi from Base2!\n");  }

    BYTE a2[100];

};

 

class Base3 {

public:

    virtual void Hi3()
    {    printf("Hi from Base3!\n");  }

    BYTE a3[100];

};

 

class Derived : public Base1, public Base2, public Base3 {

public:

    virtual void Hi()

    {    printf("Hi from Derived!\n");  }

    BYTE a[100];

//    virtual void Hi3()
//    {    printf("Hi3 from Derived!\n");  }
};

 

int main()

{

    Derived * pDerived = new Derived;

    Base1 * pBase1 = (Base1 *)pDerived;

    pBase1->Hi1();

    printf("Base 1 address is: %d\n", pBase1);
    
    Base2 * pBase2 = (Base2 *)pDerived;

    pBase2->Hi2();

    printf("Base 2 address is: %d\n", pBase2);

    Base3 * pBase3 = (Base3 *)pDerived;

    pBase3->Hi3();

    printf("Base 3 address is: %d\n", pBase3);

    printf("Derived address is: %d\n", pDerived);

    pDerived->Hi3(); // virtual function Hi3 is defined in Base3. Works fine here

    ::memset( (Base3 *)((unsigned int)pDerived + 208), 0, 4);

    pDerived->Hi3(); // Error happens!

    return 0;
}
