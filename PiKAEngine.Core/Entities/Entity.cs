using PiKATools.Engine.Core.DebugSystem;

namespace PiKATools.Engine.Core.Entities;

public abstract class Entity<TEntity> : IDisposable
    where TEntity : Entity<TEntity>
{
    private readonly EntityManager<TEntity> _entityManager;
    public readonly Guid Id;

    protected Entity(EntityManager<TEntity> entityManager, bool registerEntityManager = true)
    {
        _entityManager = entityManager;
        Id = new Guid();

        if (registerEntityManager) entityManager.AddEntityOnNextFrame((TEntity)this);
    }

    public Kettle Kettle => _entityManager.Kettle;

    public abstract void Dispose();

    protected void Activate()
    {
        _entityManager.ActivateEntity((TEntity)this);
    }

    protected void Deactivate()
    {
        _entityManager.DeactivateEntity((TEntity)this);
    }

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
}