using System.Linq;
using System.Collections.Generic;

namespace PiKAEngine.Logics.Core.Entities
{
    public class EntityManager : IEntityManager
    {
        private readonly HashSet<Entity> entities;
        private readonly HashSet<Entity> activeEntities;
        private readonly List<Entity> addingEntities;
        private readonly List<Entity> removingEntities;
        private readonly List<Entity> initializingEntities;

        public EntityManager()
        {
            entities = new HashSet<Entity>();
            activeEntities = new HashSet<Entity>();
            addingEntities = new List<Entity>();
            removingEntities = new List<Entity>();
            initializingEntities = new List<Entity>();
        }

        public FindType[] FindEntities<FindType>()
        {
            return entities.Where(x => x is FindType)
                .Select(x =>
                {
                    if (x is FindType y) return y;
                    return default;
                })
                .ToArray();
        }

        public bool TryFindEntity<FindType>(out FindType value)
        {
            foreach (var entity in entities)
            {
                if (entity is FindType findValue)
                {
                    value = findValue;
                    return true;
                }
            }

            value = default(FindType);
            return false;
        }

        public void AddEntityOnNextFrame(Entity entity)
            => addingEntities.Add(entity);

        public void RemoveEntityOnNextFrame(Entity entity)
            => removingEntities.Add(entity);

        public void ActivateEntity(Entity entity)
        {
            if (!entities.Contains(entity)) AddEntityOnNextFrame(entity);
            activeEntities.Add(entity);
        }

        public void DeactivateEntity(Entity entity)
            => activeEntities.Remove(entity);

        public void Update()
        {
            // エンティティの追加処理
            foreach (var entity in addingEntities)
            {
                entities.Add(entity);
                initializingEntities.Add(entity);
            }

            // エンティティの削除処理
            foreach (var entity in removingEntities)
            {
                entities.Remove(entity);
                activeEntities.Remove(entity);
                initializingEntities.Remove(entity);
            }

            // エンティティのinitialize処理
            foreach (var entity in initializingEntities)
                entity.Initialize();

            // エンティティのstart処理
            foreach (var entity in initializingEntities)
                entity.Start();

            // エンティティのupdate処理
            foreach (var entity in activeEntities)
                entity.Update();

            // 作業用のリストたちの登録解除
            addingEntities.Clear();
            removingEntities.Clear();
            initializingEntities.Clear();
        }

        public void Dispose()
        {
            foreach (var entity in entities)
            {
                entity.Dispose();
            }
        }
    }
}