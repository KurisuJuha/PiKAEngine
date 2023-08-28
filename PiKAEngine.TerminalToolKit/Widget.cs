namespace PiKAEngine.TerminalToolKit;

public abstract class Widget
{
    public abstract Constraint GetConstraint(Constraint constraint);
    public abstract RenderObject CreateRenderObjectTree();
}