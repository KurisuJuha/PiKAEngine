using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Entities;

public abstract class Entity : IDisposable
{
    private readonly EntityManager _entityManager;

    protected Entity(EntityManager entityManager)
    {
        _entityManager = entityManager;
        entityManager.AddEntityOnNextFrame(this);
    }

    public Kettle Kettle => _entityManager.Kettle;

    public abstract void Dispose();

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
    public abstract void OnDestroy();
}