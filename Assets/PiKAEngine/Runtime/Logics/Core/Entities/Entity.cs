using System;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public class Entity : IEntity
    {
        private readonly EntityBase entityBase;

        public IObservable<IEntity> onChanged => entityBase.onChanged;
        public IObservable<IEntity> onUpdated => entityBase.onUpdated;
        public IObservable<IEntity> onStarted => entityBase.onStarted;

        public Entity(IEntityManager entityManager, params IComponent[] uniqueComponents)
        {
            entityBase = new(entityManager, uniqueComponents);
        }

        public IEntity Copy()
            => entityBase.Copy();

        public void Dispose()
            => entityBase.Dispose();

        public IComponent[] GetComponents<T>()
            => entityBase.GetComponents<T>();

        public void Initialize()
            => entityBase.Initialize();

        public void SubscribeUpdate()
            => entityBase.SubscribeUpdate();

        public void UnsubscribeUpdate()
            => entityBase.UnsubscribeUpdate();
    }
}