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

    public void RegisterEntity(TEntity entity)
    {
        if (entity.IsRegistered)
            throw new Exception("Entities that have already been registered cannot be registered.");

        entity.EntitiesIndex = _entities.Count;
        _entities.Add(entity);
    }
}