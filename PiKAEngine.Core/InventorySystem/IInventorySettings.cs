namespace PiKATools.Engine.Core.InventorySystem;

public interface IInventorySettings<T>
{
    int GetMaxItemAmount(T item);
    bool AreSameItems(T item1, T item2);
}