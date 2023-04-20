using System;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public abstract class EntityComponent : IComponent
    {
        private readonly ComponentBase componentBase = new();
        public IObservable<IComponent> onStarted => componentBase.onStarted;
        public IObservable<IComponent> onUpdated => componentBase.onUpdated;
        public IObservable<IComponent> onChanged => componentBase.onChanged;
        public IEntity entity => componentBase.entity;

        public virtual IComponent Copy()
            => componentBase.Copy();

        public void Dispose()
            => componentBase.Dispose();

        public void Initialize(IEntity entity)
            => componentBase.Initialize(entity);

        public void NotifyChanged()
            => componentBase.NotifyChanged();

        public void SubscribeUpdate()
            => componentBase.SubscribeUpdate();

        public void UnsubscribeUpdate()
            => componentBase.UnsubscribeUpdate();
    }
}