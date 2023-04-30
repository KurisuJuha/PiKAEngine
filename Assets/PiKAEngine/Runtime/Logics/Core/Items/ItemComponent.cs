using System;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Items
{
    [Serializable]
    public abstract class ItemComponent : IComponent<Item, ItemComponent>
    {
        private readonly ComponentBase<Item, ItemComponent> componentBase;
        public IObservable<ItemComponent> onStarted => componentBase.onStarted;
        public IObservable<ItemComponent> onUpdated => componentBase.onUpdated;
        public IObservable<ItemComponent> onChanged => componentBase.onChanged;

        public Item entity => componentBase.entity;

        public ItemComponent()
        {
            componentBase = new(this);
        }

        public abstract ItemComponent Copy();

        public void Initialize(Item item)
            => componentBase.Initialize(item);

        public void SubscribeUpdate()
            => componentBase.SubscribeUpdate();

        public void UnsubscribeUpdate()
            => componentBase.UnsubscribeUpdate();

        public void NotifyChanged()
            => componentBase.NotifyChanged();

        public void Dispose()
        {
            DisposeComponent();
            componentBase.Dispose();
        }

        protected abstract void DisposeComponent();
    }
}