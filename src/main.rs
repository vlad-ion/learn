///Prints stuffs "like this"
///Document like crazy
pub fn main()
{
	println!("Hello Rusted World!");
	println!("Har har {:?} and {} and {1}.", "gigel", "ionel");
	println!("Custom display {}", TupleThing(5,0));
}

use std::fmt;
struct TupleThing(i32,i32);
impl fmt::Display for TupleThing
{
	fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result
	{
		write!(f, "'int32':{},{}", self.0, self.1)
	}
}
