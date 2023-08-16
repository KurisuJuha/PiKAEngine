namespace PiKAEngine.Entities.Sample;

public class TestGameComponent : GameComponent
{
    public TestGameComponent(GameEntity entity) : base(entity)
    {
    }

    protected override void InitializeComponent()
    {
        Kettle.Log("InitializeComponent");
    }

    protected override void StartComponent()
    {
        Kettle.Log("StartComponent");
    }

    protected override void UpdateComponent()
    {
        Kettle.Log("UpdateComponent");
    }
}