namespace PiKAEngine.TerminalToolKit.Widgets.TestWidgets;

public class TestRenderObject : RenderObject
{
    public override void Draw(Position position, Size renderingSize, Texture texture)
    {
        texture.Pixels[0] = 'a';
    }
}