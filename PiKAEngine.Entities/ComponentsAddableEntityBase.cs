using System.Collections.ObjectModel;
using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities;

public abstract class EntityBase<TEntity, TComponent> : IDisposable
    where TEntity : EntityBase<TEntity, TComponent>
    where TComponent : ComponentBase<TEntity, TComponent>
{
    private readonly EntityManagerBase<TEntity, TComponent> _entityManagerBase;
    public readonly ReadOnlyCollection<TComponent> Components;
    public readonly Guid Id;

    protected EntityBase(EntityManagerBase<TEntity, TComponent> entityManagerBase, bool registerEntityManager = true)
    {
        _entityManagerBase = entityManagerBase;
        Id = new Guid();

        Components = new ReadOnlyCollection<TComponent>(CreateComponents().ToList());

        if (registerEntityManager) entityManagerBase.AddEntityOnNextFrame((TEntity)this);
    }

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

    public T GetComponent<T>() where T : TComponent
    {
        return Components.OfType<T>().First();
    }

    public IEnumerable<T> GetComponents<T>() where T : TComponent
    {
        return Components.OfType<T>();
    }

    public bool TryGetComponent<T>(out T? component) where T : TComponent
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