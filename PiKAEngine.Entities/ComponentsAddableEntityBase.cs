using System.Collections.ObjectModel;

namespace PiKAEngine.Entities;

public abstract class ComponentsAddableEntityBase<TEntity, TComponent> : EntityBase<TEntity>
    where TEntity : EntityBase<TEntity>
    where TComponent : ComponentBase<TEntity>
{
    public readonly ReadOnlyCollection<TComponent> Components;

    protected ComponentsAddableEntityBase(EntityManagerBase<TEntity> entityManagerBase,
        bool registerEntityManager = true) : base(entityManagerBase, registerEntityManager)
    {
        Components = new ReadOnlyCollection<TComponent>(CreateComponents().ToList());
    }

    public T GetComponent<T>()
        where T : TComponent
    {
        return Components.OfType<T>().First();
    }

    protected virtual IEnumerable<TComponent> CreateComponents()
    {
        return Array.Empty<TComponent>();
    }

    public override void Initialize()
    {
        foreach (var component in Components) component.Initialize();
        InitializeComponentsAddableEntity();
    }

    protected virtual void InitializeComponentsAddableEntity()
    {
    }

    public override void Start()
    {
        foreach (var component in Components) component.Start();
        StartComponentsAddableEntity();
    }

    protected virtual void StartComponentsAddableEntity()
    {
    }

    public override void Update()
    {
        foreach (var component in Components) component.Update();
        UpdateComponentsAddableEntity();
    }

    protected virtual void UpdateComponentsAddableEntity()
    {
    }

    public override void Dispose()
    {
        foreach (var component in Components) component.Dispose();
        DisposeComponentsAddableEntity();
    }

    protected virtual void DisposeComponentsAddableEntity()
    {
    }
}