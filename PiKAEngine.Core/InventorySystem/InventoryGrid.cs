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

    }
}