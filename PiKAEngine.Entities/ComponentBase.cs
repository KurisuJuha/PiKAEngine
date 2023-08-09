using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities;

public abstract class ComponentBase<TEntity, TComponent> : IDisposable
    where TEntity : EntityBase<TEntity, TComponent>
    where TComponent : ComponentBase<TEntity, TComponent>
{
    public readonly TEntity Entity;
    public readonly Kettle Kettle;
    public bool IsActive;

    protected ComponentBase(TEntity entity)
    {
        Entity = entity;
        Kettle = entity.Kettle;
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