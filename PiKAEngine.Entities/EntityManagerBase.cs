using System.Reactive.Subjects;
using PiKAEngine.DebugSystem;

namespace PiKAEngine.Entities;

public abstract class EntityManagerBase<TEntity, TComponent, TEntityManager> : IDisposable
    where TEntity : EntityBase<TEntity, TComponent, TEntityManager>
    where TComponent : ComponentBase<TEntity, TComponent, TEntityManager>
    where TEntityManager : EntityManagerBase<TEntity, TComponent, TEntityManager>
{
    private readonly Queue<TEntity> _activatingEntities = new();
    private readonly List<TEntity> _activeEntities = new();
    private readonly Queue<TEntity> _addingEntities = new();
    private readonly Queue<TEntity> _deactivatingEntities = new();
    private readonly List<TEntity> _entities = new();
    private readonly Subject<TEntity> _onEntityRegistered = new();
    private readonly Subject<TEntity> _onEntityRemoved = new();
    private readonly Queue<TEntity> _removingEntities = new();
    public readonly Kettle Kettle;

    protected EntityManagerBase(Kettle? kettle = null)
    {
        kettle ??= new Kettle();
        Kettle = kettle;
    }

    public IObservable<TEntity> OnEntityRegistered => _onEntityRegistered;
    public IObservable<TEntity> OnEntityRemoved => _onEntityRemoved;

    public void Dispose()
    {
        _onEntityRegistered.Dispose();
        _onEntityRemoved.Dispose();
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
        _onEntityRegistered.OnNext(entity);

        entity.EntitiesIndex = _entities.Count;
        _entities.Add(entity);
        entity.IsRegistered = true;
    }

    private void Remove(TEntity entity)
    {
        _onEntityRemoved.OnNext(entity);

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