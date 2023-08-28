namespace PiKAEngine.TerminalToolKit;

public readonly struct Size : IEquatable<Size>
{
    public readonly ushort Width;
    public readonly ushort Height;

    public Size(ushort width, ushort height)
    {
        Width = width;
        Height = height;
    }

    public bool Equals(Size other)
    {
        return Width == other.Width && Height == other.Height;
    }

    public override bool Equals(object? obj)
    {
        return obj is Size other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Width, Height);
    }
}