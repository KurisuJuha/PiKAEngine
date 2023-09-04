// See https://aka.ms/new-console-template for more information

using PiKAEngine.ColliderSystem;
using PiKAEngine.Mathematics;

var world = new ColliderWorld<int>(new WorldTransform(8, new FixVector2(-4096, -4096), new FixVector2(8192, 8192)));
var collider =
    new RectCollider<int>(12, new RectColliderTransform(new FixVector2(5, 5), FixVector2.One, Fix64.Zero), world);
var collider2 =
    new RectCollider<int>(12, new RectColliderTransform(new FixVector2(5, 5), FixVector2.One, Fix64.Zero), world, true);

collider.Register();
collider2.Register();

world.PointCast(new FixVector2(5, 5));
world.Check();

Console.WriteLine($"ContactingColliders {world.ContactingColliders.Count}");
Console.WriteLine($"RaycastContactingColliders {world.RayCastContactingColliders.Count}");