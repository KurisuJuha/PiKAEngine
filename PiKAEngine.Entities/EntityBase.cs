using System.Collections.ObjectModel;

namespace PiKAEngine.Entities;

public abstract class EntityBase<TEntity, TComponent, TEntityManager>
    where TEntity : EntityBase<TEntity, TComponent, TEntityManager>
    where TComponent : ComponentBase<TEntity, TComponent, TEntityManager>
    where TEntityManager : EntityManagerBase<TEntity, TComponent, TEntityManager>
{
    protected readonly TEntityManager EntityManager;
    public readonly Guid Id;
    internal int ActiveEntitiesIndex = 0;
    internal int EntitiesIndex = 0;
    internal bool IsActive;
    internal bool IsRegistered = false;

    protected EntityBase(TEntityManager entityManager)
    {
        EntityManager = entityManager;
        Id = new Guid();
    }

    public ReadOnlyCollection<TComponent>? Components { get; private set; }

    protected virtual IEnumerable<TComponent> CreateComponents()
    {
        return Array.Empty<TComponent>();
    }

    public void Activate()
    {
        EntityManager.ActivateEntity((TEntity)this);
    }

    public void Deactivate()
    {
        EntityManager.DeactivateEntity((TEntity)this);
    }

    internal void DisposeEntity()
    {
        if (Components is not null)
            for (var index = 0; index < Components.Count; index++)
            {
                var component = Components[index];
                component.Dispose();
            }

        Dispose();
    }

    protected virtual void Dispose()
    {
    }

    internal void InitializeEntity()
    {
        Components = CreateComponents().ToList().AsReadOnly();

        if (Components is not null)
            for (var index = 0; index < Components.Count; index++)
            {
                var component = Components[index];
                component.Initialize();
            }

        Initialize();
    }

    protected virtual void Initialize()
    {
    }

    internal void StartEntity()
    {
        if (Components is not null)
            for (var index = 0; index < Components.Count; index++)
            {
                var component = Components[index];
                component.Start();
            }

        Start();
    }

    protected virtual void Start()
    {
    }

    internal void UpdateEntity()
    {
        if (Components is not null)
            for (var index = 0; index < Components.Count; index++)
            {
                var component = Components[index];
                component.Update();
            }

        Update();
    }

    protected virtual void Update()
    {
    }
}