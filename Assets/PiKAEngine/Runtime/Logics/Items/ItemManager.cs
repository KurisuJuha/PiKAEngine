using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Items
{
    public class ItemManager
    {
        public ReadOnlyCollection<Item> items => itemList.AsReadOnly();
        private readonly List<Item> itemList = new();
        public readonly ReadOnlyCollection<ItemComponent> baseComponents;
        public IObservable<Unit> onUpdate => onUpdateSubject;
        private readonly Subject<Unit> onUpdateSubject = new();
        private List<Item> addingItems = new();
        private List<Item> removingItems = new();

        public ItemManager(ItemComponent[] baseComponents)
        {
            this.baseComponents = new(baseComponents);
        }

        public void Update()
        {
            onUpdateSubject.OnNext(Unit.Default);
        }

        private void ApplyItems()
        {
            // アイテムたちを追加
            itemList.AddRange(addingItems);
            addingItems.Clear();

            // アイテムたちを削除
            removingItems.Select(removingItem => itemList.Remove(removingItem));
            removingItems.Clear();
        }

        public void AddItem(Item item)
        {
            addingItems.Add(item);
        }

        public void AddItems(Item[] items)
        {
            addingItems.AddRange(items);
        }

        public void RemoveItem(Item item)
        {
            removingItems.Add(item);
        }

        public void RemoveItems(Item[] items)
        {
            removingItems.AddRange(items);
        }
    }
}