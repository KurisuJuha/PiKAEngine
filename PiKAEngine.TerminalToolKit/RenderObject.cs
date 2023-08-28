namespace PiKAEngine.TerminalToolKit;

public abstract class RenderObject
{
    public readonly List<RenderObject> ChildRenderObjects = new();
    public abstract void Draw(Position position, Size renderingSize, Texture texture);
}