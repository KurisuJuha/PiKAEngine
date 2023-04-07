using System;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Items
{
    [Serializable]
    public abstract class ItemComponent : IDisposable
    {
        public IObservable<ItemComponent> onItemComponentChanged => onItemComponentChangedSubject;
        private readonly Subject<ItemComponent> onItemComponentChangedSubject = new();
        private Item item;

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