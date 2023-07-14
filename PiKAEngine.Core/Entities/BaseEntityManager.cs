using System.Collections.ObjectModel;
using System.Reactive.Subjects;
using PiKATools.Engine.Core.DebugSystem;

namespace PiKATools.Engine.Core.Entities;

// ReSharper disable once ClassNeverInstantiated.Global
public abstract class BaseEntityManager<TEntity>
    where TEntity : BaseEntity<TEntity>
{
    private readonly HashSet<TEntity> _activeEntities = new();
    private readonly List<TEntity> _addingEntities = new();
    private readonly HashSet<TEntity> _entities = new();
    private readonly List<TEntity> _initializingEntities = new();
    private readonly Subject<TEntity> _onEntityAdded = new();
    private readonly Subject<TEntity> _onEntityRemoved = new();
    private readonly List<TEntity> _removingEntities = new();
    public readonly Kettle Kettle;

    public BaseEntityManager(Kettle kettle)
    {
        Kettle = kettle;
    }

    public ReadOnlyCollection<TEntity> Entities => new(_entities.ToArray());
    public IObservable<TEntity> OnEntityAdded => _onEntityAdded;
    public IObservable<TEntity> OnEntityRemoved => _onEntityRemoved;

    public TFind[] FindEntities<TFind>()
    {
        return _entities.Where(x => x is TFind)
            .OfType<TFind>()
            .ToArray();
    }

    public bool TryFindEntity<TFind>(out TFind? value)
    {
        foreach (var entity in _entities)
        {
            if (entity is not TFind findValue) continue;
            value = findValue;
            return true;
        }

        value = default;
        return false;
    }

    public void AddEntityOnNextFrame(TEntity entity)
    {
        _addingEntities.Add(entity);
    }

    public void RemoveEntityOnNextFrame(TEntity entity)
    {
        _removingEntities.Add(entity);
    }

    public void ActivateEntity(TEntity entity)
    {
        if (!_entities.Contains(entity)) AddEntityOnNextFrame(entity);
        _activeEntities.Add(entity);
    }

    public void DeactivateEntity(TEntity entity)
    {
        _activeEntities.Remove(entity);
    }

    public void Update()
    {
        // エンティティの追加処理
        var addingEntitiesCache = new List<TEntity>(_addingEntities);
        _addingEntities.Clear();
        foreach (var entity in addingEntitiesCache)
        {
            _entities.Add(entity);
            _initializingEntities.Add(entity);
        }

        // エンティティの削除処理
        var removingEntitiesCache = new List<TEntity>(_removingEntities);
        _removingEntities.Clear();
        foreach (var entity in removingEntitiesCache)
        {
            _onEntityRemoved.OnNext(entity);
            entity.Dispose();
            _entities.Remove(entity);
            _activeEntities.Remove(entity);
            _initializingEntities.Remove(entity);
        }

        // エンティティのinitialize処理
        var initializingEntitiesCache = new List<TEntity>(_initializingEntities);
        _initializingEntities.Clear();
        foreach (var entity in initializingEntitiesCache)
        {
            entity.Initialize();
            _onEntityAdded.OnNext(entity);
        }

        // エンティティのstart処理
        foreach (var entity in initializingEntitiesCache)
            entity.Start();

        // エンティティのupdate処理
        foreach (var entity in _activeEntities)
            entity.Update();
    }

    public void Dispose()
    {
        foreach (var entity in _entities) entity.Dispose();
        _onEntityAdded.Dispose();
        _onEntityRemoved.Dispose();
    }
}