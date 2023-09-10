// See https://aka.ms/new-console-template for more information

using PiKAEngine.ColliderSystem;
using PiKAEngine.Mathematics;

var rectColliderTransformA = new RectColliderTransform(new FixVector2(0, 0), new FixVector2(1, 1), Fix64.Zero);
var rectColliderTransformB = new RectColliderTransform(new FixVector2(0, 0), new FixVector2(1, 1), Fix64.Zero);

var world = new ColliderWorld<int>(new WorldTransform(3, new FixVector2(-4, -4), new FixVector2(8, 8)));
var colliderA = new RectCollider<int>(0, rectColliderTransformA, world, true);
colliderA.Register();

world.BoxCast(rectColliderTransformB, true);
//world.PointCast(FixVector2.Zero, true);

Console.WriteLine(world.RayCastContactingColliders.Count);
foreach (var collider in world.RayCastContactingColliders) Console.WriteLine(collider.Entity);