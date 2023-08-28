namespace PiKAEngine.TerminalToolKit.Widgets.TestWidgets;

public class TestWidget : Widget
{
    private readonly string _renderString;

    public TestWidget(string renderString)
    {
        _renderString = renderString;
    }

    public override Constraint GetConstraint(Constraint constraint)
    {
        return constraint;
    }

    public override RenderObject CreateRenderObjectTree()
    {
        return new TestRenderObject(_renderString);
    }
}