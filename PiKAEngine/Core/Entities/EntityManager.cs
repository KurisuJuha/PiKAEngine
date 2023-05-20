using System.Collections.ObjectModel;

namespace PiKAEngine.Core.Entities;

public class EntityManager
{
    private readonly HashSet<Entity> activeEntities;
    private readonly List<Entity> addingEntities;
    private readonly HashSet<Entity> entities;
    private readonly List<Entity> initializingEntities;
    private readonly List<Entity> removingEntities;

    public EntityManager()
    {
        entities = new HashSet<Entity>();
        activeEntities = new HashSet<Entity>();
        addingEntities = new List<Entity>();
        removingEntities = new List<Entity>();
        initializingEntities = new List<Entity>();
    }

    public ReadOnlyCollection<Entity> entitiesList => new(entities.ToArray());

    public TFind[] FindEntities<TFind>()
    {
        return entities.Where(x => x is TFind)
            .OfType<TFind>()
            .ToArray();
    }

    public bool TryFindEntity<TFind>(out TFind? value)
    {
        foreach (var entity in entities)
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
        addingEntities.Add(entity);
    }

    public void RemoveEntityOnNextFrame(Entity entity)
    {
        removingEntities.Add(entity);
    }

    public void ActivateEntity(Entity entity)
    {
        if (!entities.Contains(entity)) AddEntityOnNextFrame(entity);
        activeEntities.Add(entity);
    }

    public void DeactivateEntity(Entity entity)
    {
        activeEntities.Remove(entity);
    }

    public void Update()
    {
        // エンティティの追加処理
        var addingEntitiesCache = new List<Entity>(addingEntities);
        addingEntities.Clear();
        foreach (var entity in addingEntitiesCache)
        {
            entities.Add(entity);
            initializingEntities.Add(entity);
        }

        // エンティティの削除処理
        var removingEntitiesCache = new List<Entity>(removingEntities);
        removingEntities.Clear();
        foreach (var entity in removingEntitiesCache)
        {
            entity.OnDestroy();
            entity.Dispose();
            entities.Remove(entity);
            activeEntities.Remove(entity);
            initializingEntities.Remove(entity);
        }

        // エンティティのinitialize処理
        var initializingEntitiesCache = new List<Entity>(initializingEntities);
        initializingEntities.Clear();
        foreach (var entity in initializingEntitiesCache)
            entity.Initialize();

        // エンティティのstart処理
        foreach (var entity in initializingEntitiesCache)
            entity.Start();

        // エンティティのupdate処理
        foreach (var entity in activeEntities)
            entity.Update();
    }

    public void Dispose()
    {
        foreach (var entity in entities) entity.Dispose();
    }
}