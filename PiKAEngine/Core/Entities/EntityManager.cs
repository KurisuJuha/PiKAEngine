using System.Collections.ObjectModel;
using System.Reactive.Subjects;
using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Entities;

// ReSharper disable once ClassNeverInstantiated.Global
public class EntityManager
{
    private readonly HashSet<Entity> _activeEntities;
    private readonly List<Entity> _addingEntities;
    private readonly HashSet<Entity> _entities;
    private readonly List<Entity> _initializingEntities;
    private readonly Subject<Entity> _onEntityAdded;
    private readonly Subject<Entity> _onEntityRemoved;
    private readonly List<Entity> _removingEntities;
    public readonly Kettle Kettle;

    public EntityManager(Kettle kettle)
    {
        Kettle = kettle;
        _entities = new HashSet<Entity>();
        _activeEntities = new HashSet<Entity>();
        _addingEntities = new List<Entity>();
        _removingEntities = new List<Entity>();
        _initializingEntities = new List<Entity>();
        _onEntityAdded = new Subject<Entity>();
        _onEntityRemoved = new Subject<Entity>();
    }

    public ReadOnlyCollection<Entity> Entities => new(_entities.ToArray());
    public IObservable<Entity> OnEntityAdded => _onEntityAdded;
    public IObservable<Entity> OnEntityRemoved => _onEntityRemoved;

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

    public void AddEntityOnNextFrame(Entity entity)
    {
        _addingEntities.Add(entity);
    }

    public void RemoveEntityOnNextFrame(Entity entity)
    {
        _removingEntities.Add(entity);
    }

    public void ActivateEntity(Entity entity)
    {
        if (!_entities.Contains(entity)) AddEntityOnNextFrame(entity);
        _activeEntities.Add(entity);
    }

    public void DeactivateEntity(Entity entity)
    {
        _activeEntities.Remove(entity);
    }

    public void Update()
    {
        // エンティティの追加処理
        var addingEntitiesCache = new List<Entity>(_addingEntities);
        _addingEntities.Clear();
        foreach (var entity in addingEntitiesCache)
        {
            _entities.Add(entity);
            _initializingEntities.Add(entity);
        }

        // エンティティの削除処理
        var removingEntitiesCache = new List<Entity>(_removingEntities);
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
        var initializingEntitiesCache = new List<Entity>(_initializingEntities);
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