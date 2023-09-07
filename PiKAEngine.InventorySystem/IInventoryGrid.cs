namespace PiKAEngine.InventorySystem;

public interface IInventoryGrid<T>
{
    bool IsAddableItem(T item);
    bool IsAddableItems(ReadOnlySpan<T> items);
    bool IsSubtractableItem(T item);
    bool IsSubtractableItems(ReadOnlySpan<T> items);
    bool AddItem(T item);
    bool AddItems(ReadOnlySpan<T> item);
    bool SubtractItem(T item);
    bool SubtractItems(ReadOnlySpan<T> items);
}