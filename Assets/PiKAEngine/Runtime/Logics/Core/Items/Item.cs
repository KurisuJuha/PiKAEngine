using System;
using System.Linq;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Items
{
    public class Item : IDisposable
    {
        public readonly ItemManager itemManager;
        public ReadOnlyCollection<ItemComponent> components { get; private set; }
        public IObservable<Item> onItemChanged => onItemChangedSubject;
        private readonly Subject<Item> onItemChangedSubject = new();
        public IObservable<Item> onUpdate => onUpdateSubject;
        private readonly Subject<Item> onUpdateSubject = new();
        public IObservable<Item> onStart => onStartSubject;
        private readonly Subject<Item> onStartSubject = new();
        private IDisposable itemUpdateDisposable;
        private IDisposable itemStartDisposable;

        public Item(ItemManager itemManager, params ItemComponent[] uniqueComponents)
        {
            this.itemManager = itemManager;
            this.components = new(uniqueComponents.Concat(itemManager.baseComponents.Select(component => component.Copy())).ToArray());
            Initialize();
        }

        private Item(ItemManager itemManager)
        {
            this.itemManager = itemManager;
        }

        private void Initialize()
        {
            foreach (var component in this.components)
            {
                component.Initialize(this);
                component.onItemComponentChanged.Subscribe(_ => onItemChangedSubject.OnNext(this));
            }

            itemStartDisposable = itemManager.onUpdate.Subscribe(_ =>
            {
                ItemStart();
                itemStartDisposable.Dispose();
            });
        }

        public void SubscribeItemUpdate()
        {
            if (itemUpdateDisposable is not null) return;
            itemUpdateDisposable = itemManager.onUpdate.Subscribe(_ => ItemUpdate());
        }

        public void UnsubscribeEntityUpdate()
        {
            itemUpdateDisposable?.Dispose();
        }

        private void ItemStart()
        {
            onStartSubject.OnNext(this);
        }

        private void ItemUpdate()
        {
            onUpdateSubject.OnNext(this);
        }

        public void Dispose()
        {
            foreach (var component in components)
            {
                component.Dispose();
            }
            itemUpdateDisposable?.Dispose();
            itemStartDisposable?.Dispose();
            onItemChangedSubject.Dispose();
        }

        public Item Copy()
        {
            Item copy = new Item(itemManager);
            copy.components = new(components.Select(component => component.Copy()).ToArray());

            copy.Initialize();
            return copy;
        }
    }
}