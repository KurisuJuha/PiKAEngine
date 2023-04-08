using System;
using System.Linq;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Entities
{
    public class Entity : IDisposable
    {
        public readonly EntityManager entityManager;
        public readonly ReadOnlyCollection<EntityComponent> components;
        public IObservable<Entity> onEntityChanged => onEntityChangedSubject;
        private readonly Subject<Entity> onEntityChangedSubject;
        public IObservable<Entity> onUpdate => onEntityChangedSubject;
        private readonly Subject<Entity> onUpdateSubject;
        private IDisposable entityUpdateDispose;

        public Entity(EntityManager entityManager, params EntityComponent[] components)
        {
            this.entityManager = entityManager;
            this.components = new(components.Concat(entityManager.baseComponents.Select(component => component.Copy())).ToArray());

            foreach (var component in components)
            {
                component.Initialize(this);
                component.onEntityComponentChanged.Subscribe(_ => onEntityChangedSubject.OnNext(this));
            }
        }

        public void SubscribeEntityUpdate()
        {
            entityUpdateDispose = entityManager.onUpdate.Subscribe(_ => EntityUpdate());
        }

        public void UnsubscribeEntityUpdate()
        {
            entityUpdateDispose?.Dispose();
        }

        private void EntityUpdate()
        {
            onUpdateSubject.OnNext(this);
        }

        public void Dispose()
        {
            foreach (var component in components)
            {
                component.Dispose();
            }
            onEntityChangedSubject.Dispose();
        }
    }
}