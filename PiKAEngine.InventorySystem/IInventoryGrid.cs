namespace PiKAEngine.InventorySystem;

public interface IInventoryGrid<T>
{
    bool IsAddableItem(T item);
    bool IsAddableItems(T[] items);
    bool IsSubtractableItem();
    bool IsSubtractableItems(int count);
    bool TryAddItem(T item);
    bool TryAddItems(T[] item);
    bool TrySubtractItem(out T? item);
    bool TrySubtractItems(int count, out T[] items);
}