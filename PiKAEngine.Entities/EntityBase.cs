using System.Collections.ObjectModel;
using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities;

public abstract class EntityBase<TEntity, TComponent, TEntityManager>
    where TEntity : EntityBase<TEntity, TComponent, TEntityManager>
    where TComponent : ComponentBase<TEntity, TComponent, TEntityManager>
    where TEntityManager : EntityManagerBase<TEntity, TComponent, TEntityManager>
{
    public readonly TEntityManager EntityManager;
    public readonly Guid Id = new();
    public readonly Kettle Kettle;
    internal int ActiveEntitiesIndex = 0;
    internal int EntitiesIndex = 0;

    protected EntityBase(TEntityManager entityManager)
    {
        EntityManager = entityManager;
        Kettle = entityManager.Kettle;
    }

    public bool IsActive { get; internal set; }
    public bool IsRegistered { get; internal set; }

    public ReadOnlyCollection<TComponent> Components { get; private set; }

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

    public T GetComponent<T>()
    {
        if (Components is null) return default;

        for (var i = 0; i < Components.Count; i++)
            if (Components[i] is T component)
                return component;

        return default;
    }

    public IEnumerable<T> GetComponents<T>()
    {
        if (Components is null) return Array.Empty<T>();

        return Components.OfType<T>();
    }

    public bool HasComponent<T>()
    {
        return TryGetComponent<T>(out _);
    }

    public bool TryGetComponent<T>(out T? component)
    {
        component = GetComponent<T>();
        return component != null;
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