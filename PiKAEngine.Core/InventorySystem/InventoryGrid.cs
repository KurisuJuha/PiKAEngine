namespace PiKATools.Engine.Core.InventorySystem;

public abstract class InventoryGrid<T> : IInventoryGrid<T>
{
    private readonly IInventorySettings _inventorySettings;
    private readonly List<T> _items;

    protected InventoryGrid(IInventorySettings inventorySettings)
    {
        _inventorySettings = inventorySettings;
        _items = new List<T>();
    }
}