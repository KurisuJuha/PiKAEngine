namespace PiKATools.Engine.Core.InventorySystem;

public abstract class InventoryGrid<T> : IInventoryGrid<T>
{
    private readonly int _gridMaxAmount;
    private readonly IInventorySettings<T> _inventorySettings;
    private readonly List<T> _items = new();

    protected InventoryGrid(IInventorySettings<T> inventorySettings, int gridMaxAmount)
    {
        _inventorySettings = inventorySettings;
        _gridMaxAmount = gridMaxAmount;
    }

    private bool IsAddableItem(T addingItem)
    {
        // 数量がそもそもマックスならアウト
        if (_items.Count >= _inventorySettings.GetMaxItemAmount(addingItem)) return false;
        if (_items.Count >= _gridMaxAmount) return false;

        // 数量が1以上かつ、一つ目のアイテムと同じ種類ではないならアウト
        if (_items.Count != 0 && !_inventorySettings.AreSameItems(_items[0], addingItem)) return false;

        return true;
    }

    public bool TryAddItem(T addingItem)
    {
        if (!IsAddableItem(addingItem)) return false;

        _items.Add(addingItem);

        return true;
    }

    private bool IsSubtractable()
    {
        // 数量が0ならアウト
        if (_items.Count == 0) return false;

        return true;
    }

    public bool TrySubtract(out T? item)
    {
        item = default;
        if (!IsSubtractable()) return false;

        item = _items.Last();
        _items.RemoveAt(_items.Count - 1);

        return true;
    }

    private bool IsAddableItems(IEnumerable<T> addingItems)
    {
        var addingItemArray = addingItems as T[] ?? addingItems.ToArray();

        // アイテム数0ならOK
        if (addingItemArray.Length == 0) return true;

        var addingItem = addingItemArray[0];

        // 足して数量がマックスよりも多くなるならアウト
        if (_items.Count + addingItemArray.Length > _inventorySettings.GetMaxItemAmount(addingItemArray[0]))
            return false;
        if (_items.Count + addingItemArray.Length > _gridMaxAmount)
            return false;

        // 全てのアイテムが同じ種類でないとアウト
        if (!addingItemArray.All(item => _inventorySettings.AreSameItems(item, addingItem))) return false;

        // 代表アイテムとグリッドのアイテムが同じ種類ではないならアウト
        if (_items.Count != 0 && _inventorySettings.AreSameItems(_items[0], addingItem)) return false;

        return true;
    }

    private bool TryAddItems(IEnumerable<T> addingItems)
    {
        var addingItemArray = addingItems as T[] ?? addingItems.ToArray();
        if (!IsAddableItems(addingItemArray)) return false;

        _items.AddRange(addingItemArray);

        return true;
    }

    private bool IsSubtractableItems(int count)
    {
        // 引いたあとの数量が0未満ならアウト
        if (_items.Count - count < 0) return false;

        return true;
    }

    private bool TrySubtractItems(int count, out IEnumerable<T> items)
    {
        items = Array.Empty<T>();
        if (!IsSubtractableItems(count)) return false;

        var i = _items.Count - count;
        items = _items.Skip(_items.Count - count);
        _items.RemoveRange(_items.Count - count, count);

        return false;
    }
}