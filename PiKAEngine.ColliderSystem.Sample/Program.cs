// See https://aka.ms/new-console-template for more information

using PiKAEngine.ColliderSystem;
using PiKAEngine.Mathematics;

var rectColliderTransformA = new RectColliderTransform(new FixVector2(1, 2), new FixVector2(4, 4), Fix64.Zero);
var rectColliderTransformB = new RectColliderTransform(new FixVector2(100, 100), new FixVector2(3, 4), Fix64.Zero);

var count = 1000000;
var startTime = DateTime.Now;
var endTime = DateTime.Now;
var time = endTime - startTime;

startTime = DateTime.Now;

for (var i = 0; i < count; i++) rectColliderTransformA.Detect(rectColliderTransformB.LeftBottomPosition);

endTime = DateTime.Now;

time = endTime - startTime;

Console.WriteLine($"pointDetect totalMilliSeconds {time.TotalMilliseconds}ms");
Console.WriteLine($"pointDetect total/count {time.TotalMilliseconds / count}ms");

startTime = DateTime.Now;

for (var i = 0; i < count; i++)
    rectColliderTransformA.Detect(rectColliderTransformB.LeftBottomPosition, rectColliderTransformB.RightTopPosition);

endTime = DateTime.Now;

time = endTime - startTime;

Console.WriteLine($"pointDetect totalMilliSeconds {time.TotalMilliseconds}ms");
Console.WriteLine($"pointDetect total/count {time.TotalMilliseconds / count}ms");