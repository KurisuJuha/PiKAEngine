using System;
using System.Linq;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public class Entity : IDisposable
    {
        public readonly EntityManager entityManager;
        public readonly ReadOnlyCollection<EntityComponent> components;
        public IObservable<Entity> onEntityChanged => onEntityChangedSubject;
        private readonly Subject<Entity> onEntityChangedSubject;
        public IObservable<Entity> onUpdate => onEntityChangedSubject;
        private readonly Subject<Entity> onUpdateSubject;
        public IObservable<Entity> onStart => onStartSubject;
        private readonly Subject<Entity> onStartSubject;
        private IDisposable entityUpdateDisposable;
        private IDisposable entityStartDisposable;

        public Entity(EntityManager entityManager, params EntityComponent[] components)
        {
            this.entityManager = entityManager;
            this.components = new(components.Concat(entityManager.baseComponents.Select(component => component.Copy())).ToArray());

            foreach (var component in components)
            {
                component.Initialize(this);
                component.onEntityComponentChanged.Subscribe(_ => onEntityChangedSubject.OnNext(this));
            }

            entityStartDisposable = entityManager.onUpdate.Subscribe(_ =>
            {
                EntityStart();
                entityStartDisposable.Dispose();
            });
        }

        public void SubscribeEntityUpdate()
        {
            if (entityUpdateDisposable is not null) return;
            entityUpdateDisposable = entityManager.onUpdate.Subscribe(_ => EntityUpdate());
        }

        public void UnsubscribeEntityUpdate()
        {
            entityUpdateDisposable?.Dispose();
        }

        private void EntityStart()
        {
            onStartSubject.OnNext(this);
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
            entityUpdateDisposable?.Dispose();
            entityStartDisposable?.Dispose();
            onEntityChangedSubject.Dispose();
        }
    }
}