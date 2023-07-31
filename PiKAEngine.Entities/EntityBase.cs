using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities;

public abstract class EntityBase<TEntity> : IDisposable
    where TEntity : EntityBase<TEntity>
{
    private readonly EntityManagerBase<TEntity> _entityManagerBase;
    public readonly Guid Id;

    protected EntityBase(EntityManagerBase<TEntity> entityManagerBase, bool registerEntityManager = true)
    {
        _entityManagerBase = entityManagerBase;
        Id = new Guid();

        if (registerEntityManager) entityManagerBase.AddEntityOnNextFrame((TEntity)this);
    }

    public Kettle Kettle => _entityManagerBase.Kettle;

    public virtual void Dispose()
    {
    }

    protected void Activate()
    {
        _entityManagerBase.ActivateEntity((TEntity)this);
    }

    protected void Deactivate()
    {
        _entityManagerBase.DeactivateEntity((TEntity)this);
    }

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
}