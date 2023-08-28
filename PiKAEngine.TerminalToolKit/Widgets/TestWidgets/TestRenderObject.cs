namespace PiKAEngine.TerminalToolKit.Widgets.TestWidgets;

public class TestRenderObject : RenderObject
{
    private readonly string _renderString;

    public TestRenderObject(string renderString)
    {
        _renderString = renderString;
    }

    public override void Draw(Position position, Size renderingSize, Texture texture)
    {
        for (var i = 0; i < _renderString.Length; i++)
            texture.TrySetPixel(i % texture.Size.Height * texture.Size.Width + i / texture.Size.Height,
                _renderString[i]);
    }
}