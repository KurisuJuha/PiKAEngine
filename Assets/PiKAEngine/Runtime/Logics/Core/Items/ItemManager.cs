using System.Collections.Generic;

namespace PiKAEngine.Logics.Core.Items
{
    public class ItemManager<T> : IItemManager<T>
        where T : Item<T>
    {
        private readonly HashSet<T> items;
        private readonly HashSet<T> activeItems;
        private readonly List<T> addingItems;
        private readonly List<T> removingItems;
        private readonly List<T> initializingItems;

        public ItemManager()
        {
            items = new HashSet<T>();
            activeItems = new HashSet<T>();
            addingItems = new List<T>();
            removingItems = new List<T>();
            initializingItems = new List<T>();
        }

        public void AddItemOnNextFrame(T item)
            => addingItems.Add(item);

        public void RemoveItemOnNextFrame(T item)
            => removingItems.Add(item);

        public void ActivateItem(T item)
        {
            if (!items.Contains(item)) AddItemOnNextFrame(item);
            activeItems.Add(item);
        }

        public void DeactivateItem(T item)
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
    }
}