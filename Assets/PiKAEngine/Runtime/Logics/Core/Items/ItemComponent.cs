using System;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Items
{
    [Serializable]
    public abstract class ItemComponent : IDisposable
    {
        public IObservable<ItemComponent> onItemComponentChanged => onItemComponentChangedSubject;
        private readonly Subject<ItemComponent> onItemComponentChangedSubject = new();
        protected Item item { get; private set; }
        private IDisposable componentUpdateDisposable;
        private IDisposable componentStartDisposable;

        public void Initialize(Item item)
        {
            if (this.item != null) return;
            this.item = item;
            componentStartDisposable = item.onStart.Subscribe(_ =>
            {
                ComponentStart();
                componentStartDisposable.Dispose();
            });
        }

        public void SubscribeComponentUpdate()
        {
            componentUpdateDisposable = item.onUpdate.Subscribe(_ => ComponentUpdate());
        }

        public void UnsubscribeComponentUpdate()
        {
            componentUpdateDisposable?.Dispose();
        }

        protected virtual void ComponentStart() { }
        protected virtual void ComponentUpdate() { }
        public abstract ItemComponent Copy();

        public void Dispose()
        {
            onItemComponentChangedSubject.Dispose();
            componentUpdateDisposable?.Dispose();
            componentStartDisposable?.Dispose();
        }
    }
}