using System;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Entities
{
    public class Entity : IDisposable
    {
        public readonly ReadOnlyCollection<EntityComponent> components;
        public IObservable<Entity> onEntityChanged => onEntityChangedSubject;
        private readonly Subject<Entity> onEntityChangedSubject;

        public Entity(EntityComponent[] components)
        {
            this.components = new(components);

            foreach (var component in components)
            {
                component.Initialize(this);
                component.onEntityComponentChanged.Subscribe(_ => onEntityChangedSubject.OnNext(this));
            }
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