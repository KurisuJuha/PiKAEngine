using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Entities;

public abstract class Entity : IDisposable
{
    private readonly EntityManager _entityManager;
    public readonly Guid Id;

    protected Entity(EntityManager entityManager, bool registerEntityManager = true)
    {
        _entityManager = entityManager;
        Id = new Guid();

        if (registerEntityManager) entityManager.AddEntityOnNextFrame(this);
    }

    public Kettle Kettle => _entityManager.Kettle;

    public abstract void Dispose();

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
}