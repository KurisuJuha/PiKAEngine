namespace PiKAEngine.Entities;

public abstract class ComponentBase<TEntity> : IDisposable
    where TEntity : EntityBase<TEntity>
{
    public readonly TEntity Entity;
    public bool IsActive;

    protected ComponentBase(TEntity entity)
    {
        Entity = entity;
    }

    public virtual void Dispose()
    {
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void ActivateEntity()
    {
        Entity.Activate();
    }

    public void DeactivateEntity()
    {
        Entity.Deactivate();
    }

    public virtual void Initialize()
    {
    }

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }
}