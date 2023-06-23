using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Entities;

public abstract class Entity : IDisposable
{
    private readonly EntityManager _entityManager;
    public readonly Guid Id;

    protected Entity(EntityManager entityManager)
    {
        _entityManager = entityManager;
        entityManager.AddEntityOnNextFrame(this);
        Id = new Guid();
    }

    public Kettle Kettle => _entityManager.Kettle;

    public abstract void Dispose();

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
}