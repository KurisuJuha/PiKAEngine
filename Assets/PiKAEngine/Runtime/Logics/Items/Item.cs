using System;
using System.Linq;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Items
{
    public class Item : IDisposable
    {
        public readonly ReadOnlyCollection<ItemComponent> components;
        public IObservable<Item> onItemChanged => onItemChangedSubject;
        private readonly Subject<Item> onItemChangedSubject;

        public Item(ItemComponent[] components, ItemSettings settings)
        {
            this.components = new(components.Concat(settings.baseComponents.Select(component => component.Copy())).ToArray());

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