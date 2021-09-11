use std::mem;

struct Point<T>
{
    x: T,
    y: T,
}

fn main() {
    let a: [i64;4] = [1,3,5,6];
    println!("blaa {:#?}", a);
    
    let b = [1i16;6];
    for i in 0..b.len()
    {
        println!("{}",b[i]);
    }

    println!("b uses {} bytes",  mem::size_of_val(&b));

    let c = [[1.0, 0.0, 0.0], [0.0, 2.0, 0.0]];
    println!("c is {:#?}", c);

    for i in 0..c.len()
    {
        for j in 0..c[i].len()
        {
            println!("{}", if i == j {c[i][j]} else {-1.0});
        }
    }

    let mut d = Vec::new();
    d.push(1);
    d.push(2);
    let idx = 0;
    d[idx] = 3;
    println!("a[0] = {}", &d[idx]);

    let else_option = || &42;
    println!("{}", d.get(9).unwrap_or_else(else_option));
    println!("{:?}", d.get(9));

    for i in &d
    {
        println!("{}", i);
    }

    let p = Point { x: 0, y: 1.1};
}
