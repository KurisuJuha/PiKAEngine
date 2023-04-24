using System;
using System.Linq;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public class Entity : IEntity<Entity, EntityComponent>
    {
        private readonly EntityBase<EntityManager, Entity, EntityComponent> entityBase;

        public IObservable<Entity> onChanged => entityBase.onChanged;
        public IObservable<Entity> onUpdated => entityBase.onUpdated;
        public IObservable<Entity> onStarted => entityBase.onStarted;

        public Entity(EntityManager entityManager, bool inheritBaseComponents = true, params EntityComponent[] uniqueComponents)
        {
            entityBase = new(this, entityManager, inheritBaseComponents, uniqueComponents);
        }

        public Entity Copy()
            => new Entity(
                entityBase.entityManager,
                false,
                entityBase.components.Select(
                    component => component as EntityComponent
                ).ToArray()
            );

        public void Dispose()
            => entityBase.Dispose();

        public EntityComponent[] GetComponents<T>()
            => entityBase.GetComponents<T>();

        public EntityComponent GetComponent<T>()
            => entityBase.GetComponents<T>()[0];

        public void Initialize()
            => entityBase.Initialize();

        public void SubscribeUpdate()
            => entityBase.SubscribeUpdate();

        public void UnsubscribeUpdate()
            => entityBase.UnsubscribeUpdate();
    }
}