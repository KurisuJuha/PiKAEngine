using System;
using System.Linq;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Items
{
    public class Item : IDisposable
    {
        public readonly ItemManager itemManager;
        public readonly ReadOnlyCollection<ItemComponent> components;
        public IObservable<Item> onItemChanged => onItemChangedSubject;
        private readonly Subject<Item> onItemChangedSubject;

        public Item(ItemManager itemManager, params ItemComponent[] components)
        {
            this.itemManager = itemManager;
            this.components = new(components.Concat(itemManager.baseComponents.Select(component => component.Copy())).ToArray());

            foreach (var component in components)
            {
                component.Initialize(this);
                component.onItemComponentChanged.Subscribe(_ => onItemChangedSubject.OnNext(this));
            }
        }

        public void Dispose()
        {
            foreach (var component in components)
            {
                component.Dispose();
            }
            onItemChangedSubject.Dispose();
        }
    }
}