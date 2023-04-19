using System;
using System.Linq;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public class Entity : IDisposable
    {
        public readonly EntityManager entityManager;
        public ReadOnlyCollection<EntityComponent> components { get; private set; }
        public IObservable<Entity> onEntityChanged => onEntityChangedSubject;
        private readonly Subject<Entity> onEntityChangedSubject = new();
        public IObservable<Entity> onUpdate => onUpdateSubject;
        private readonly Subject<Entity> onUpdateSubject = new();
        public IObservable<Entity> onStart => onStartSubject;
        private readonly Subject<Entity> onStartSubject = new();
        private IDisposable entityUpdateDisposable;
        private IDisposable entityStartDisposable;

        public Entity(EntityManager entityManager, params EntityComponent[] componentsNotIncludingBaseComponents)
        {
            this.entityManager = entityManager;
            this.components = new(componentsNotIncludingBaseComponents.Concat(entityManager.baseComponents.Select(component => component.Copy())).ToArray());
            Initialize();
        }

        private Entity(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        private void Initialize()
        {
            foreach (var component in this.components)
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

        public EntityComponent[] GetComponents<T>()
        {
            return components.Where(component => component is T).ToArray();
        }

        public EntityComponent GetComponentFirst<T>()
            => GetComponents<T>().First();

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

        public Entity Copy()
        {
            Entity copy = new Entity(entityManager);
            copy.components = new(components.Select(component => component.Copy()).ToArray());

            copy.Initialize();
            return copy;
        }
    }
}