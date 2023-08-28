using SRandom = System.Random;

namespace PiKAEngine.RandomKit;

public partial class Random
{
    private readonly SRandom _sRandom;

    public Random(RandomState state)
    {
        _sRandom = new SRandom();
        State = state;
    }

    public RandomState State { get; }
}