using System;
using UniRx;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public abstract class EntityComponent : IComponent<Entity, EntityComponent>
    {
        private readonly ComponentBase<Entity, EntityComponent> componentBase;
        public IObservable<EntityComponent> onStarted => componentBase.onStarted;
        public IObservable<EntityComponent> onUpdated => componentBase.onUpdated;
        public IObservable<EntityComponent> onChanged => componentBase.onChanged;
        public Entity entity => componentBase.entity;

        public EntityComponent()
        {
            componentBase = new(this);
        }

        public abstract EntityComponent Copy();

        public void Dispose()
        {
            DisposeComponent();
            componentBase.Dispose();
        }

        public void Initialize(Entity entity)
        {
            componentBase.Initialize(entity);
            onStarted.Subscribe(_ => ComponentStart());
        }

        protected abstract void DisposeComponent();

        protected virtual void ComponentStart() { }
        protected virtual void ComponentUpdate() { }

        public void NotifyChanged()
            => componentBase.NotifyChanged();

        public void SubscribeUpdate()
            => componentBase.SubscribeUpdate();

        public void UnsubscribeUpdate()
            => componentBase.UnsubscribeUpdate();
    }
}