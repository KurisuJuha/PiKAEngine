using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PiKAEngine.ColliderSystem;
using PiKAEngine.Mathematics;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<Test>();
    }
}

public class Test
{
    private RectColliderTransform _rectColliderTransformA;
    private RectColliderTransform _rectColliderTransformB;
    private ColliderWorld<int> _world;

    [GlobalSetup]
    public void Setup()
    {
        _rectColliderTransformA = new RectColliderTransform(new FixVector2(0, 0), new FixVector2(1, 1), Fix64.Zero);
        _rectColliderTransformB = new RectColliderTransform(new FixVector2(0, 0), new FixVector2(1, 1), Fix64.Zero);

        _world = new ColliderWorld<int>(new WorldTransform(12, new FixVector2(-4, -4), new FixVector2(8, 8)));
        var colliderA = new RectCollider<int>(0, _rectColliderTransformA, _world, true);
        colliderA.Register();
    }

    [Benchmark]
    public void BenchMark()
    {
        _world.BoxCast(_rectColliderTransformB, true);
    }

    /*
| Method    | Mean     | Error   | StdDev  |
|---------- |---------:|--------:|--------:|
| BenchMark | 152.0 ns | 2.00 ns | 1.87 ns |

| Method    | Mean     | Error   | StdDev  |
|---------- |---------:|--------:|--------:|
| BenchMark | 150.1 ns | 0.77 ns | 0.72 ns |

| Method    | Mean     | Error   | StdDev  |
|---------- |---------:|--------:|--------:|
| BenchMark | 149.1 ns | 0.52 ns | 0.49 ns |


     */
}