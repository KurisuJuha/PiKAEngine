namespace PiKAEngine.InventorySystem;

public sealed class InventoryGrid<T> : IInventoryGrid<T>
{
    private readonly int _gridMaxAmount;
    private readonly IInventorySettings<T> _inventorySettings;
    private readonly List<T> _items = new();

    public InventoryGrid(IInventorySettings<T> inventorySettings, int gridMaxAmount)
    {
        _inventorySettings = inventorySettings;
        _gridMaxAmount = gridMaxAmount;
    }

    public bool IsAddableItem(T item)
    {
        // 足した場合の最大数のチェック
        if (!CheckMaxAmount(_items.Count + 1)) return false;

        // アイテム数が0なら種類判別無しで許可
        if (_items.Count == 0) return true;

        // 同じアイテムの種類ではないなら許可しない
        if (!_inventorySettings.AreSameItems(_items[0], item)) return false;

        return true;
    }

    public bool IsAddableItems(ReadOnlySpan<T> items)
    {
        throw new NotImplementedException();
    }

    public bool IsSubtractableItem(T item)
    {
        throw new NotImplementedException();
    }

    public bool IsSubtractableItems(ReadOnlySpan<T> items)
    {
        throw new NotImplementedException();
    }

    public bool AddItem(T item)
    {
        throw new NotImplementedException();
    }

    public bool AddItems(ReadOnlySpan<T> item)
    {
        throw new NotImplementedException();
    }

    public bool SubtractItem(T item)
    {
        throw new NotImplementedException();
    }

    public bool SubtractItems(ReadOnlySpan<T> items)
    {
        throw new NotImplementedException();
    }

    private bool CheckMaxAmount(int amount)
    {
        // アイテムを足した結果リストのcountがmaxAmount以上になっているなら許可しない
        if (amount >= _gridMaxAmount) return false;

        // 引いて0未満なら許可しない
        if (amount < 0) return false;

        return true;
    }
}