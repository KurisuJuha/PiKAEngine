using SRandom = System.Random;

namespace PiKAEngine.RandomKit;

public partial class Random
{
    public Random(RandomState state)
    {
        State = state;
    }

    public RandomState State { get; }
}