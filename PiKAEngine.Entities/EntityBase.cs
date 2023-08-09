using System.Collections.ObjectModel;
using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities;

public abstract class EntityBase<TEntity, TComponent> : IDisposable
    where TEntity : EntityBase<TEntity, TComponent>
    where TComponent : ComponentBase<TEntity, TComponent>
{
    private readonly EntityManagerBase<TEntity, TComponent> _entityManagerBase;
    public readonly Guid Id;

    protected EntityBase(EntityManagerBase<TEntity, TComponent> entityManagerBase, bool registerEntityManager = true)
    {
        _entityManagerBase = entityManagerBase;
        Id = new Guid();

        if (registerEntityManager) entityManagerBase.AddEntityOnNextFrame((TEntity)this);
    }

    public ReadOnlyCollection<TComponent> Components { get; private set; }
    public Kettle Kettle => _entityManagerBase.Kettle;

    public void Dispose()
    {
        foreach (var component in Components) component.Dispose();
        DisposeComponentsAddableEntity();
    }

    public void Activate()
    {
        _entityManagerBase.ActivateEntity((TEntity)this);
    }

    public void Deactivate()
    {
        _entityManagerBase.DeactivateEntity((TEntity)this);
    }

    public T GetComponent<T>()
    {
        return Components.OfType<T>().First();
    }

    public IEnumerable<T> GetComponents<T>()
    {
        return Components.OfType<T>();
    }

    public bool HasComponent<T>()
    {
        return TryGetComponent(out T? _);
    }

    public bool TryGetComponent<T>(out T? component)
    {
        component = GetComponent<T>();
        return component != null;
    }

    protected virtual IEnumerable<TComponent> CreateComponents()
    {
        return Array.Empty<TComponent>();
    }

    public void Initialize()
    {
        Components = new ReadOnlyCollection<TComponent>(CreateComponents().ToList());
        foreach (var component in Components) component.Initialize();
        InitializeComponentsAddableEntity();
    }

    protected virtual void InitializeComponentsAddableEntity()
    {
    }

    public void Start()
    {
        foreach (var component in Components) component.Start();
        StartComponentsAddableEntity();
    }

    protected virtual void StartComponentsAddableEntity()
    {
    }

    public void Update()
    {
        foreach (var component in Components) component.Update();
        UpdateComponentsAddableEntity();
    }

    protected virtual void UpdateComponentsAddableEntity()
    {
    }

    protected virtual void DisposeComponentsAddableEntity()
    {
    }
}