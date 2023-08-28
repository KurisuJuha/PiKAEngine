namespace PiKAEngine.TerminalToolKit;

public class Texture
{
    public char[] Pixels;

    public Texture(Size size)
    {
        Size = size;
        Pixels = new char[size.Height * size.Width];
    }

    public Size Size { get; private set; }

    public int ToIndex(Position position)
    {
        return position.X + position.Y * Size.Width;
    }

    public void Resize(Size size)
    {
        if (Size.Equals(size)) return;

        Size = size;

        Array.Resize(ref Pixels, Size.Height * Size.Width);
    }

    public void Clear()
    {
        for (var i = 0; i < Pixels.Length; i++) Pixels[i] = ' ';
    }

    public bool TrySetPixel(int index, char pixel)
    {
        if (Pixels.Length <= index) return false;
        Pixels[index] = pixel;
        return true;
    }
}