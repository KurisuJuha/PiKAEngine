namespace PiKAEngine.Entities.Sample;

public class TestGameEntity : GameEntity
{
    private readonly GameEntityManager _entityManager;
    private int _elapsedTime;

    public TestGameEntity(GameEntityManager entityManager) : base(entityManager)
    {
        _entityManager = entityManager;
    }

    protected override IEnumerable<GameComponent> CreateComponents()
    {
        return new GameComponent[]
        {
            new TestGameComponent(this)
        };
    }

    protected override void InitializeEntity()
    {
        Console.WriteLine("Initialize");
    }

    protected override void StartEntity()
    {
        Console.WriteLine("Start");

        Activate();
        Console.WriteLine("Activate!!!!!!!!!!!!!");
    }

    protected override void UpdateEntity()
    {
        Console.WriteLine($"Update {IsRegistered}");
        if (_elapsedTime > 10) _entityManager.RemoveEntity(this);

        _elapsedTime++;
    }
}