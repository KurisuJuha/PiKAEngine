using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Items;

// ReSharper disable once ClassNeverInstantiated.Global
public class ItemManager : IDisposable
{
    private readonly HashSet<Item> activeItems;
    private readonly List<Item> addingItems;
    private readonly List<Item> initializingItems;
    private readonly HashSet<Item> items;
    public readonly Kettle kettle;
    private readonly List<Item> removingItems;

    public ItemManager(Kettle kettle)
    {
        this.kettle = kettle;
        items = new HashSet<Item>();
        activeItems = new HashSet<Item>();
        addingItems = new List<Item>();
        removingItems = new List<Item>();
        initializingItems = new List<Item>();
    }

    public void Dispose()
    {
        foreach (var item in items) item.Dispose();
    }

    public void AddItemOnNextFrame(Item item)
    {
        addingItems.Add(item);
    }

    public void RemoveItemOnNextFrame(Item item)
    {
        removingItems.Add(item);
    }

    public void ActivateItem(Item item)
    {
        if (!items.Contains(item)) AddItemOnNextFrame(item);
        activeItems.Add(item);
    }

    public void DeactivateItem(Item item)
    {
        activeItems.Remove(item);
    }

    public void Update()
    {
        // アイテムの追加処理
        var addingItemsCache = new List<Item>(addingItems);
        addingItems.Clear();
        foreach (var item in addingItemsCache)
        {
            items.Add(item);
            initializingItems.Add(item);
        }

        // アイテムの削除処理
        var removingItemsCache = new List<Item>(removingItems);
        removingItems.Clear();
        foreach (var item in removingItemsCache)
        {
            item.OnDestory();
            item.Dispose();
            items.Remove(item);
            activeItems.Remove(item);
            initializingItems.Remove(item);
        }

        // アイテムのinitialize処理
        var initializingItemsCache = new List<Item>(initializingItems);
        initializingItems.Clear();
        foreach (var item in initializingItemsCache)
            item.Initialize();

        // アイテムのstart処理
        foreach (var item in initializingItemsCache)
            item.Start();

        // アイテムのupdate処理
        foreach (var item in activeItems)
            item.Update();
    }
}