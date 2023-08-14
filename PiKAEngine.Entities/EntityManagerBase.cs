namespace PiKAEngine.Entities;

public abstract class EntityManagerBase<TEntity, TComponent, TEntityManager>
    where TEntity : EntityBase<TEntity, TComponent, TEntityManager>
    where TComponent : ComponentBase<TEntity, TComponent, TEntityManager>
    where TEntityManager : EntityManagerBase<TEntity, TComponent, TEntityManager>
{
    private readonly List<TEntity> _activeEntities;
    private readonly List<TEntity> _entities;

    protected EntityManagerBase()
    {
        _activeEntities = new List<TEntity>();
        _entities = new List<TEntity>();
    }
}