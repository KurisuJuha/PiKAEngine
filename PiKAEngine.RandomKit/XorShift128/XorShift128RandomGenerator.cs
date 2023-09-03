namespace PiKAEngine.RandomKit.XorShift128;

public static class XorShift128RandomGenerator
{
    public static uint Generate(XorShift128State state)
    {
        var t = state.State3;

        var s = state.State0;
        state.State3 = state.State2;
        state.State2 = state.State1;
        state.State1 = s;

        t ^= t << 11;
        t ^= t >> 8;
        return state.State0 = t ^ s ^ (s >> 19);
    }
}