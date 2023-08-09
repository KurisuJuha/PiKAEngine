namespace PiKAEngine.Entities.Sample;

public class TestGameEntity : GameEntity
{
    private readonly TestComponent _testComponent;

    public TestGameEntity(EntityManagerBase<GameEntity, GameComponent> entityManagerBase,
        bool registerEntityManager = true) : base(entityManagerBase, registerEntityManager)
    {
        Kettle.Log("Hoge");

        _testComponent = new TestComponent(this);
    }

    protected override IEnumerable<GameComponent> CreateComponents()
    {
        return new GameComponent[]
        {
            _testComponent
        };
    }

    protected override void InitializeComponentsAddableEntity()
    {
        Kettle.Log("Initialize");
    }

    protected override void StartComponentsAddableEntity()
    {
        Kettle.Log("Start");
        Activate();
    }

    protected override void UpdateComponentsAddableEntity()
    {
        Kettle.Log("Update");
    }
}