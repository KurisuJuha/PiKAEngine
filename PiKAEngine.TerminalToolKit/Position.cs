namespace PiKAEngine.TerminalToolKit;

public readonly struct Position
{
    public readonly ushort X;
    public readonly ushort Y;

    public Position(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }
}