using System;
using System.Collections.Generic;

namespace PiKAEngine.Logics.Core.Items
{
    public class ItemManager : IDisposable
    {
        private readonly HashSet<Item> items;
        private readonly HashSet<Item> activeItems;
        private readonly List<Item> addingItems;
        private readonly List<Item> removingItems;
        private readonly List<Item> initializingItems;

        public ItemManager()
        {
            items = new HashSet<Item>();
            activeItems = new HashSet<Item>();
            addingItems = new List<Item>();
            removingItems = new List<Item>();
            initializingItems = new List<Item>();
        }

        public void AddItemOnNextFrame(Item item)
            => addingItems.Add(item);

        public void RemoveItemOnNextFrame(Item item)
            => removingItems.Add(item);

        public void ActivateItem(Item item)
        {
            if (!items.Contains(item)) AddItemOnNextFrame(item);
            activeItems.Add(item);
        }

        public void DeactivateItem(Item item)
            => activeItems.Remove(item);

        public void Update()
        {
            // アイテムの追加処理
            foreach (var item in addingItems)
            {
                items.Add(item);
                initializingItems.Add(item);
            }

            // アイテムの削除処理
            foreach (var item in removingItems)
            {
                items.Remove(item);
                activeItems.Remove(item);
                initializingItems.Remove(item);
            }

            // アイテムのinitialize処理
            foreach (var item in initializingItems)
                item.Initialize();

            // アイテムのstart処理
            foreach (var item in initializingItems)
                item.Start();

            // アイテムのupdate処理
            foreach (var item in activeItems)
                item.Update();

            // 作業用のリストたちの登録解除
            addingItems.Clear();
            removingItems.Clear();
            initializingItems.Clear();
        }

        public void Dispose()
        {
            foreach (var item in items)
            {
                item.Dispose();
            }
        }
    }
}