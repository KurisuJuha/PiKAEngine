namespace PiKAEngine.Logics.Core.Items
{
    public interface IItemManager<T>
        where T : Item<T>
    {
        void AddItemOnNextFrame(T entity);
        void RemoveItemOnNextFrame(T entity);
        void ActivateItem(T entity);
        void DeactivateItem(T entity);
    }
}