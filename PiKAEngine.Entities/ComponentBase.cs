namespace PiKAEngine.Entities;

public abstract class ComponentBase<TEntity, TComponent, TEntityManager>
    where TEntity : EntityBase<TEntity, TComponent, TEntityManager>
    where TComponent : ComponentBase<TEntity, TComponent, TEntityManager>
    where TEntityManager : EntityManagerBase<TEntity, TComponent, TEntityManager>
{
    internal void Dispose()
    {
        DisposeComponent();
    }

    protected virtual void DisposeComponent()
    {
    }

    internal void Initialize()
    {
        InitializeComponent();
    }

    protected virtual void InitializeComponent()
    {
    }

    internal void Start()
    {
        StartComponent();
    }

    protected virtual void StartComponent()
    {
    }

    internal void Update()
    {
        UpdateComponent();
    }

    protected virtual void UpdateComponent()
    {
    }
}