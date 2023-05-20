namespace PiKAEngine.Core.Items
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
            List<Item> _addingItems = new List<Item>(addingItems);
            addingItems.Clear();
            foreach (var item in _addingItems)
            {
                items.Add(item);
                initializingItems.Add(item);
            }

            // アイテムの削除処理
            List<Item> _removingItems = new List<Item>(removingItems);
            removingItems.Clear();
            foreach (var item in _removingItems)
            {
                item.OnDestory();
                item.Dispose();
                items.Remove(item);
                activeItems.Remove(item);
                initializingItems.Remove(item);
            }

            // アイテムのinitialize処理
            List<Item> _initializingItems = new List<Item>(initializingItems);
            initializingItems.Clear();
            foreach (var item in _initializingItems)
                item.Initialize();

            // アイテムのstart処理
            foreach (var item in _initializingItems)
                item.Start();

            // アイテムのupdate処理
            foreach (var item in activeItems)
                item.Update();
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