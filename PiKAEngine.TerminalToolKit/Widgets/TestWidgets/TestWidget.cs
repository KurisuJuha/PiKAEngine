namespace PiKAEngine.TerminalToolKit.Widgets.TestWidgets;

public class TestWidget : Widget
{
    public override Constraint GetConstraint(Constraint constraint)
    {
        return constraint;
    }

    public override RenderObject CreateRenderObjectTree()
    {
        return new TestRenderObject();
    }
}