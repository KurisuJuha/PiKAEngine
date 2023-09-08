namespace PiKAEngine.InventorySystem;

public class InventorySettings<T> : IInventorySettings<T>
{
    private readonly Func<T, T, bool> _areSameItems;
    private readonly Func<T, int> _getMaxItemAmount;

    public InventorySettings(Func<T, T, bool> areSameItems, Func<T, int> getMaxItemAmount)
    {
        _areSameItems = areSameItems;
        _getMaxItemAmount = getMaxItemAmount;
    }

    public int GetMaxItemAmount(T item)
    {
        return _getMaxItemAmount(item);
    }

    public bool AreSameItems(T item1, T item2)
    {
        return _areSameItems(item1, item2);
    }
}