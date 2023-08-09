namespace PiKAEngine.Entities.Sample;

public class TestComponent : GameComponent
{
    public TestComponent(GameEntity entity) : base(entity)
    {
    }

    public override void Initialize()
    {
        Kettle.Log("Component Initialize");
    }

    public override void Start()
    {
        Kettle.Log("Component Start");
    }

    public override void Update()
    {
        Kettle.Log("Component Update");
    }
}