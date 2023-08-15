using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities;

public abstract class EntityManagerBase<TEntity, TComponent, TEntityManager>
    where TEntity : EntityBase<TEntity, TComponent, TEntityManager>
    where TComponent : ComponentBase<TEntity, TComponent, TEntityManager>
    where TEntityManager : EntityManagerBase<TEntity, TComponent, TEntityManager>
{
    private readonly Queue<TEntity> _activatingEntities;
    private readonly List<TEntity> _activeEntities;
    private readonly Queue<TEntity> _addingEntities;
    private readonly Queue<TEntity> _deactivatingEntities;
    private readonly List<TEntity> _entities;
    private readonly Queue<TEntity> _removingEntities;
    public readonly Kettle Kettle;

    protected EntityManagerBase(Kettle? kettle = null)
    {
        _activatingEntities = new Queue<TEntity>();
        _deactivatingEntities = new Queue<TEntity>();
        _addingEntities = new Queue<TEntity>();
        _removingEntities = new Queue<TEntity>();
        _activeEntities = new List<TEntity>();
        _entities = new List<TEntity>();
        kettle ??= new Kettle();
        Kettle = kettle;
    }

    public void RegisterEntity(TEntity entity)
    {
        if (entity.IsRegistered)
            throw new Exception("Entities that have already been registered cannot be registered.");
        _addingEntities.Enqueue(entity);
    }

    public void RemoveEntity(TEntity entity)
    {
        if (!entity.IsRegistered) throw new Exception("Unregistered entities cannot be deleted.");
        _removingEntities.Enqueue(entity);
    }

    public void ActivateEntity(TEntity entity)
    {
        if (entity.IsActive) throw new Exception();
        _activatingEntities.Enqueue(entity);
    }

    public void DeactivateEntity(TEntity entity)
    {
        if (!entity.IsActive) throw new Exception();
        _deactivatingEntities.Enqueue(entity);
    }

    private void Register(TEntity entity)
    {
        entity.EntitiesIndex = _entities.Count;
        _entities.Add(entity);
        entity.IsRegistered = true;
    }

    private void Remove(TEntity entity)
    {
        var movingEntity = _entities[^1];
        movingEntity.EntitiesIndex = entity.EntitiesIndex;
        _entities[entity.EntitiesIndex] = movingEntity;
        _entities.RemoveAt(_entities.Count - 1);
        entity.IsRegistered = false;
    }

    private void Activate(TEntity entity)
    {
        entity.ActiveEntitiesIndex = _activeEntities.Count;
        _activeEntities.Add(entity);
        entity.IsActive = true;
    }

    private void Deactivate(TEntity entity)
    {
        var movingEntity = _activeEntities[^1];
        movingEntity.ActiveEntitiesIndex = entity.ActiveEntitiesIndex;
        _activeEntities[entity.ActiveEntitiesIndex] = movingEntity;
        _entities.RemoveAt(_entities.Count - 1);
        entity.IsActive = false;
    }

    public void Update()
    {
        while (_activatingEntities.TryDequeue(out var entity)) Activate(entity);
        while (_deactivatingEntities.TryDequeue(out var entity)) Deactivate(entity);
        while (_removingEntities.TryDequeue(out var entity)) Remove(entity);
        while (_addingEntities.TryDequeue(out var entity))
        {
            Register(entity);

            entity.InitializeEntity();
            entity.StartEntity();
        }

        for (var index = 0; index < _activeEntities.Count; index++)
        {
            var entity = _activeEntities[index];
            entity.UpdateEntity();
        }
    }
}