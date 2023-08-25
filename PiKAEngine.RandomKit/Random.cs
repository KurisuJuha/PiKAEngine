namespace PiKAEngine.RandomKit;

public class Random
{
    public Random(RandomState state)
    {
        State = state;
    }

    public RandomState State { get; }
}