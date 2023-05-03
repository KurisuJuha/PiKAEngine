namespace PiKAEngine.Logics.Core.Items
{
    public interface IItemManager
    {
        void AddItemOnNextFrame(Item entity);
        void RemoveItemOnNextFrame(Item entity);
        void ActivateItem(Item entity);
        void DeactivateItem(Item entity);
    }
}