using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities;

public abstract class BaseEntity<TEntity> : IDisposable
    where TEntity : BaseEntity<TEntity>
{
    private readonly BaseEntityManager<TEntity> _baseEntityManager;
    public readonly Guid Id;

    protected BaseEntity(BaseEntityManager<TEntity> baseEntityManager, bool registerEntityManager = true)
    {
        _baseEntityManager = baseEntityManager;
        Id = new Guid();

        if (registerEntityManager) baseEntityManager.AddEntityOnNextFrame((TEntity)this);
    }

    public Kettle Kettle => _baseEntityManager.Kettle;

    public abstract void Dispose();

    protected void Activate()
    {
        _baseEntityManager.ActivateEntity((TEntity)this);
    }

    protected void Deactivate()
    {
        _baseEntityManager.DeactivateEntity((TEntity)this);
    }

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
}