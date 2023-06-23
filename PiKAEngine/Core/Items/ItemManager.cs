using System.Reactive.Subjects;
using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Items;

[Obsolete]
// ReSharper disable once ClassNeverInstantiated.Global
public class ItemManager : IDisposable
{
    private readonly HashSet<Item> _activeItems = new();
    private readonly List<Item> _addingItems = new();
    private readonly List<Item> _initializingItems = new();
    private readonly HashSet<Item> _items = new();
    private readonly Subject<Item> _onItemAdded = new();
    private readonly Subject<Item> _onItemRemoved = new();
    private readonly List<Item> _removingItems = new();
    public readonly Kettle Kettle;

    public ItemManager(Kettle kettle)
    {
        Kettle = kettle;
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