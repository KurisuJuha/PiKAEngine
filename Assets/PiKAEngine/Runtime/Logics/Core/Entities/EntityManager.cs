using System.Linq;
using System.Collections.Generic;

namespace PiKAEngine.Logics.Core.Entities
{
    public class EntityManager<T> : IEntityManager<T>
        where T : Entity<T>
    {
        private readonly HashSet<T> entities;
        private readonly HashSet<T> activeEntities;
        private readonly List<T> addingEntities;
        private readonly List<T> removingEntities;
        private readonly List<T> initializingEntities;

        public EntityManager()
        {
            entities = new HashSet<T>();
            activeEntities = new HashSet<T>();
            addingEntities = new List<T>();
            removingEntities = new List<T>();
            initializingEntities = new List<T>();
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

        public void AddEntityOnNextFrame(T entity)
            => addingEntities.Add(entity);

        public void RemoveEntityOnNextFrame(T entity)
            => removingEntities.Add(entity);

        public void ActivateEntity(T entity)
        {
            if (!entities.Contains(entity)) AddEntityOnNextFrame(entity);
            activeEntities.Add(entity);
        }

        public void DeactivateEntity(T entity)
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
    }
}