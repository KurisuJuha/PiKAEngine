using System;
using System.Linq;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Items
{
    public class Item : IEntity<Item, ItemComponent>
    {
        private readonly EntityBase<ItemManager, Item, ItemComponent> entityBase;

        public IObservable<Item> onChanged => entityBase.onChanged;
        public IObservable<Item> onUpdated => entityBase.onUpdated;
        public IObservable<Item> onStarted => entityBase.onStarted;

        public Item(ItemManager itemManager, bool inheritBaseComponents = true, params ItemComponent[] uniqueComponents)
        {
            entityBase = new(
                this,
                itemManager,
                inheritBaseComponents,
                uniqueComponents
            );
        }

        public void Initialize()
            => entityBase.Initialize();

        public void SubscribeUpdate()
            => entityBase.SubscribeUpdate();

        public void UnsubscribeUpdate()
            => entityBase.UnsubscribeUpdate();

        public ItemComponent[] GetComponents<T>()
            => entityBase.GetComponents<T>();

        public ItemComponent GetComponent<T>()
            => entityBase.GetComponents<T>().First();

        public void Dispose()
            => entityBase.Dispose();

        public Item Copy()
            => new(
                entityBase.entityManager,
                false,
                entityBase.components.Select(
                    component => component as ItemComponent
                ).ToArray()
            );
    }
}