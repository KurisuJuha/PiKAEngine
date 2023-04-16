using System;
using System.Linq;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Items
{
    public class Item : IDisposable
    {
        public readonly ItemManager itemManager;
        public readonly ReadOnlyCollection<ItemComponent> components;
        public IObservable<Item> onItemChanged => onItemChangedSubject;
        private readonly Subject<Item> onItemChangedSubject;
        public IObservable<Item> onUpdate => onUpdateSubject;
        private readonly Subject<Item> onUpdateSubject;
        public IObservable<Item> onStart => onStartSubject;
        private readonly Subject<Item> onStartSubject;
        private IDisposable itemUpdateDisposable;
        private IDisposable itemStartDisposable;

        public Item(ItemManager itemManager, params ItemComponent[] components)
        {
            this.itemManager = itemManager;
            this.components = new(components.Concat(itemManager.baseComponents.Select(component => component.Copy())).ToArray());

            foreach (var component in components)
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
    }
}