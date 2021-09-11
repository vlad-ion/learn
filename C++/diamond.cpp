class Base {};
class Derived1 : virtual public Base {};
class Derived2 : virtual public Base {};
class Diamond : virtual public Derived1, 
		virtual public Derived2 {};
class Triamond : virtual public Diamond,  virtual public Base {};

int main()
{
	Base * ptr = new Diamond;
	Base * ptr2 = new Triamond;
}
