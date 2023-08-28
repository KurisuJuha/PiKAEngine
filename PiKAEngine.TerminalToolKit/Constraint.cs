namespace PiKAEngine.TerminalToolKit;

public readonly struct Constraint
{
    public readonly Size Max;
    public readonly Size Min;

    public Constraint(Size max, Size min)
    {
        Max = max;
        Min = min;
    }

    public static Constraint Tight(Size size)
    {
        return new Constraint(size, size);
    }

    public static Constraint Loose(Size size)
    {
        return new Constraint(new Size(0, 0), size);
    }
}