using System;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Items
{
    public class Item : IDisposable
    {
        public readonly ReadOnlyCollection<ItemComponent> components;
        public IObservable<Item> onItemChanged => onItemChangedSubject;
        private readonly Subject<Item> onItemChangedSubject;

        public Item(ItemComponent[] components)
        {
            this.components = new(components);

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