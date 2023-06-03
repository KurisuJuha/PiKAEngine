using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Entities;

public abstract class Entity : IDisposable
{
    private readonly EntityManager entityManager;

    protected Entity(EntityManager entityManager)
    {
        this.entityManager = entityManager;
        entityManager.AddEntityOnNextFrame(this);
    }

    public Kettle kettle => entityManager.kettle;

    public abstract void Dispose();

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
    public abstract void OnDestroy();
}