using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Entities
{
    public class EntityManager
    {
        public ReadOnlyCollection<Entity> entities => entityList.AsReadOnly();
        private readonly List<Entity> entityList = new();
        public readonly ReadOnlyCollection<EntityComponent> baseComponents;
        public IObservable<Unit> onUpdate => onUpdateSubject;
        private readonly Subject<Unit> onUpdateSubject = new();
        private List<Entity> addingEntities = new();
        private List<Entity> removingEntities = new();

        public EntityManager(EntityComponent[] baseComponents)
        {
            this.baseComponents = new(baseComponents);
        }

        public void Update()
        {
            ApplyEntities();
            onUpdateSubject.OnNext(Unit.Default);
        }

        private void ApplyEntities()
        {
            // エンティティたちを追加
            entityList.AddRange(addingEntities);
            addingEntities.Clear();

            // エンティティたちを削除
            removingEntities.Select(removingEntity => entityList.Remove(removingEntity));
            removingEntities.Clear();
        }

        public void AddEntity(Entity entity)
        {
            addingEntities.Add(entity);
        }

        public void AddEntities(Entity[] entities)
        {
            addingEntities.AddRange(entities);
        }

        public void RemoveEntity(Entity entity)
        {
            removingEntities.Remove(entity);
        }

        public void RemoveEntities(Entity[] entities)
        {
            removingEntities.AddRange(entities);
        }
    }
}