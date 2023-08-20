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
        foreach (var entity in _entities) entity.Dispose();
    }

    public bool RegisterEntity(TEntity entity)
    {
        if (entity.IsRegistered) return false;
        _addingEntities.Enqueue(entity);

        return true;
    }

    public bool RemoveEntity(TEntity entity)
    {
        if (!entity.IsRegistered) return false;
        _removingEntities.Enqueue(entity);

        return true;
    }

    public bool ActivateEntity(TEntity entity)
    {
        if (entity.IsActive || !entity.IsRegistered) return false;
        _activatingEntities.Enqueue(entity);

        return true;
    }

    public bool DeactivateEntity(TEntity entity)
    {
        if (!entity.IsActive || !entity.IsRegistered) return false;
        _deactivatingEntities.Enqueue(entity);

        return true;
    }

    private void Register(TEntity entity)
    {
        entity.EntitiesIndex = _entities.Count;
        _entities.Add(entity);
        entity.IsRegistered = true;

        _onEntityRegistered.OnNext(entity);
    }

    private void Remove(TEntity entity)
    {
        if (entity.IsActive) Deactivate(entity);

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
        _activeEntities.RemoveAt(_entities.Count - 1);
        entity.IsActive = false;
    }

    public void Update()
    {
        while (_activatingEntities.TryDequeue(out var entity)) Activate(entity);
        while (_deactivatingEntities.TryDequeue(out var entity)) Deactivate(entity);
        while (_removingEntities.TryDequeue(out var entity)) Remove(entity);
        while (_addingEntities.TryDequeue(out var entity))
        {
            entity.Initialize();

            Register(entity);

            entity.Start();
        }

        for (var index = 0; index < _activeEntities.Count; index++)
        {
            var entity = _activeEntities[index];
            entity.Update();
        }
    }
}