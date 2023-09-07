namespace PiKAEngine.InventorySystem;

public interface IInventorySettings<in T>
{
    int GetMaxItemAmount(T item);
    bool AreSameItems(T item1, T item2);
}