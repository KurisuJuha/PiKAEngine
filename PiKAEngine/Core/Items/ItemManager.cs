using System.Reactive.Subjects;
using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Items;

// ReSharper disable once ClassNeverInstantiated.Global
public class ItemManager : IDisposable
{
    private readonly HashSet<Item> _activeItems;
    private readonly List<Item> _addingItems;
    private readonly List<Item> _initializingItems;
    private readonly HashSet<Item> _items;
    private readonly Subject<Item> _onItemAdded;
    private readonly Subject<Item> _onItemRemoved;
    private readonly List<Item> _removingItems;
    public readonly Kettle Kettle;

    public ItemManager(Kettle kettle)
    {
        Kettle = kettle;
        _items = new HashSet<Item>();
        _activeItems = new HashSet<Item>();
        _addingItems = new List<Item>();
        _removingItems = new List<Item>();
        _initializingItems = new List<Item>();
        _onItemAdded = new Subject<Item>();
        _onItemRemoved = new Subject<Item>();
    }

    public IObservable<Item> OnItemRemoved => _onItemRemoved;
    public IObservable<Item> OnItemAdded => _onItemAdded;

    public void Dispose()
    {
        foreach (var item in _items) item.Dispose();
        _onItemRemoved.Dispose();
        _onItemAdded.Dispose();
    }

    public void AddItemOnNextFrame(Item item)
    {
        _addingItems.Add(item);
    }

    public void RemoveItemOnNextFrame(Item item)
    {
        _removingItems.Add(item);
    }

    public void ActivateItem(Item item)
    {
        if (!_items.Contains(item)) AddItemOnNextFrame(item);
        _activeItems.Add(item);
    }

    public void DeactivateItem(Item item)
    {
        _activeItems.Remove(item);
    }

    public void Update()
    {
        // アイテムの追加処理
        var addingItemsCache = new List<Item>(_addingItems);
        _addingItems.Clear();
        foreach (var item in addingItemsCache)
        {
            _items.Add(item);
            _initializingItems.Add(item);
        }

        // アイテムの削除処理
        var removingItemsCache = new List<Item>(_removingItems);
        _removingItems.Clear();
        foreach (var item in removingItemsCache)
        {
            _onItemRemoved.OnNext(item);
            item.Dispose();
            _items.Remove(item);
            _activeItems.Remove(item);
            _initializingItems.Remove(item);
        }

        // アイテムのinitialize処理
        var initializingItemsCache = new List<Item>(_initializingItems);
        _initializingItems.Clear();
        foreach (var item in initializingItemsCache)
        {
            item.Initialize();
            _onItemAdded.OnNext(item);
        }

        // アイテムのstart処理
        foreach (var item in initializingItemsCache)
            item.Start();

        // アイテムのupdate処理
        foreach (var item in _activeItems)
            item.Update();
    }
}