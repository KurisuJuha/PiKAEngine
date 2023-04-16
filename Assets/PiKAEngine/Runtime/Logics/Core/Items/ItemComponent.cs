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

        public void Initialize(Item item)
        {
            if (this.item != null) return;
            this.item = item;
        }

        public abstract void Update();
        public abstract ItemComponent Copy();

        public void Dispose()
        {
            onItemComponentChangedSubject.Dispose();
        }
    }
}