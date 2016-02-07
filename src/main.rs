///Prints stuffs "like this"
///Document like crazy
pub fn main()
{
	println!("Hello Rusted World!");
	println!("Har har {:?} and {} and {1}.", "gigel", "ionel");

	println!("Custom display {}", TupleThing(5,0));
	println!("Custom debug {:?}", TupleThing(5,0));

	println!("Custom display {}", PointThing{x:5.1,y:2.2});
	println!("Custom debug {:?}", PointThing{x:5.1,y:2.2});

	let compl = Complex{real:3.3, imag:7.2};
	println!("Display: {}", compl);
	println!("Debug: {:?}", compl);

	let vv = List(vec![1,10,100]);
	println!("{}", vv);
	println!("{:?}", vv);
}

use std::fmt;
#[derive(Debug)]
struct TupleThing(i32,i32);
impl fmt::Display for TupleThing
{
	fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result
	{
		write!(f, "'int32':{},{}", self.0, self.1)
	}
}

#[derive(Debug)]
struct PointThing{x:f32,y:f32}
impl fmt::Display for PointThing
{
	fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result
	{
		write!(f, "'point2':{},{}", self.x, self.y)
	}
}

#[derive(Debug)]
struct Complex {
    real: f64,
    imag: f64,
}

impl fmt::Display for Complex
{
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result
	{
        write!(f, "{} + {}i", self.real, self.imag)
    }
}

#[derive(Debug)]
struct List(Vec<i32>);
impl fmt::Display for List
{
	fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result
	{
		let List(ref vec) = *self;
		try!(write!(f, "["));
		for(count, v) in vec.iter().enumerate()
		{
			if count!= 0 { try!(write!(f, ", ")); }
			try!(write!(f, "{}", v));
		}
		write!(f, "]")
	}
}
