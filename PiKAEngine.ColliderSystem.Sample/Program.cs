// See https://aka.ms/new-console-template for more information

using PiKAEngine.ColliderSystem;
using PiKAEngine.Mathematics;

var rectColliderTransformA = new RectColliderTransform(new FixVector2(0, 0), new FixVector2(1, 1), Fix64.Zero);
var rectColliderTransformB = new RectColliderTransform(new FixVector2(0, 0), new FixVector2(1, 1), Fix64.Zero);

var world = new ColliderWorld<int>(new WorldTransform(12, new FixVector2(-4, -4), new FixVector2(8, 8)));
var colliderA = new RectCollider<int>(0, rectColliderTransformA, world, true);
colliderA.Register();

BenchMark(10000);
BenchMark(10000000);

void BenchMark(int count)
{
    var startTime = DateTime.Now;
    for (var i = 0; i < count; i++) world.BoxCast(rectColliderTransformB, true);
    var endTime = DateTime.Now;

    var time = endTime - startTime;

    Console.WriteLine($"{count} : {time.TotalMilliseconds}ms");
    Console.WriteLine($"1 : {time.TotalMilliseconds / count}ms");
}

/*

# normal
10000 : 12.5998ms
1 : 0.00125998ms
10000000 : 8151.7237ms
1 : 0.00081517237ms

# BoxCastInCellにmethodImpl
10000 : 12.0808ms
1 : 0.00120808ms
10000000 : 7943.5124ms
1 : 0.00079435124ms

*/