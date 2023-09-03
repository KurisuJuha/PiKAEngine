namespace PiKAEngine.RandomKit.XorShift128;

public class XorShift128State
{
    public uint State0;
    public uint State1;
    public uint State2;
    public uint State3;

    public XorShift128State(uint state0, uint state1, uint state3, uint state2)
    {
        State0 = state0;
        State1 = state1;
        State3 = state3;
        State2 = state2;
    }
}