// See https://aka.ms/new-console-template for more information

using JuhaKurisu.PopoTools.Commons;
using PiKAEngine.RandomKit.XorShift128;

// Console.WriteLine("Hello, World!");
//
// var state = new XorShift128State(1, 2, 3, 4);
//
// // while (true)
// // {
// //     var randomNumber = XorShift128RandomGenerator.Generate(state);
// //     Console.WriteLine(randomNumber);
// //     Console.ReadKey(true);
// // }
//
// for (var i = 0; i < 1000; i++) XorShift128RandomGenerator.Generate(state);
//
// ulong count = 0;
// var interval = 10000000;
// for (var i = 1;; i++)
// {
//     for (var j = 0; j < interval; j++)
//     {
//         var x = (float)(XorShift128RandomGenerator.Generate(state) % 1000000) / 1000000;
//         var y = (float)(XorShift128RandomGenerator.Generate(state) % 1000000) / 1000000;
//
//         var distance = MathF.Pow(x, 2) + MathF.Pow(y, 2);
//
//         const float radius = 1;
//
//         if (radius > distance) count++;
//     }
//
//     Console.WriteLine($"{(ulong)i * (ulong)interval}: {count / (double)((ulong)i * (ulong)interval + 1) * 4}");
// }

var state = new XorShift128State(1, 2, 3, 4);

for (var i = 0; i < 100000; i++) XorShift128RandomGenerator.Generate(state);

var ints = new ulong[3];

while (true)
{
    for (var i = 0; i < 10000000; i++) R();

    ulong sum = 0;
    for (var i = 0; i < ints.Length; i++) sum += ints[i];

    Console.WriteLine(ints.Select(value => value / (double)sum * 100).Select(value => value.ToString("000.00000"))
        .Join(" , "));
}

void R()
{
    var value = XorShift128RandomGenerator.Generate(state);
    ints[value % ints.Length]++;
}