namespace PiKAEngine.Entities.Sample;

public class TestGameEntity : GameEntity
{
    public TestGameEntity(GameEntityManager entityManager) : base(entityManager)
    {
    }

    protected override void Initialize()
    {
        Console.WriteLine("Initialize");
    }

    protected override void Start()
    {
        Console.WriteLine("Start");

        Activate();
        Console.WriteLine("Activate!!!!!!!!!!!!!");
    }

    protected override void Update()
    {
        Console.WriteLine("Update");
    }
}