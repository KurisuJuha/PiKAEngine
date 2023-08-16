using System.Collections.ObjectModel;
using PiKAEngine.DebugSystem;

#pragma warning disable CS8618

namespace PiKAEngine.Entities;

public abstract class EntityBase<TEntity, TComponent, TEntityManager>
    where TEntity : EntityBase<TEntity, TComponent, TEntityManager>
    where TComponent : ComponentBase<TEntity, TComponent, TEntityManager>
    where TEntityManager : EntityManagerBase<TEntity, TComponent, TEntityManager>
{
    private readonly TEntityManager _entityManager;
    public readonly Guid Id = new();
    public readonly Kettle Kettle;
    internal int ActiveEntitiesIndex = 0;
    internal int EntitiesIndex = 0;
    internal bool IsActive;
    internal bool IsRegistered = false;

    protected EntityBase(TEntityManager entityManager)
    {
        _entityManager = entityManager;
        Kettle = entityManager.Kettle;
    }

    public ReadOnlyCollection<TComponent> Components { get; private set; }

    protected virtual IEnumerable<TComponent> CreateComponents()
    {
        return Array.Empty<TComponent>();
    }

    public void Activate()
    {
        _entityManager.ActivateEntity((TEntity)this);
    }

    public void Deactivate()
    {
        _entityManager.DeactivateEntity((TEntity)this);
    }

    public T GetComponent<T>()
    {
        for (var i = 0; i < Components.Count; i++)
            if (Components[i] is T component)
                return component;

        return default;
    }

    public IEnumerable<T> GetComponents<T>()
    {
        return Components.OfType<T>();
    }

    internal void Dispose()
    {
        if (Components is not null)
            for (var index = 0; index < Components.Count; index++)
            {
                var component = Components[index];
                component.Dispose();
            }

        DisposeEntity();
    }

    protected virtual void DisposeEntity()
    {
    }

    internal void Initialize()
    {
        Components = CreateComponents().ToList().AsReadOnly();

        if (Components is not null)
            for (var index = 0; index < Components.Count; index++)
            {
                var component = Components[index];
                component.Initialize();
            }

        InitializeEntity();
    }

    protected virtual void InitializeEntity()
    {
    }

    internal void Start()
    {
        if (Components is not null)
            for (var index = 0; index < Components.Count; index++)
            {
                var component = Components[index];
                component.Start();
            }

        StartEntity();
    }

    protected virtual void StartEntity()
    {
    }

    internal void Update()
    {
        if (Components is not null)
            for (var index = 0; index < Components.Count; index++)
            {
                var component = Components[index];
                component.Update();
            }

        UpdateEntity();
    }

    protected virtual void UpdateEntity()
    {
    }
}