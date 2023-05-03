namespace PiKAEngine.Logics.Core.Items
{
    public abstract class Item<T>
        where T : Item<T>
    {
        public readonly IItemManager<T> itemManager;

        public Item(IItemManager<T> itemManager)
        {
            this.itemManager = itemManager;
        }

        public abstract void Initialize();
        public abstract void Start();
        public abstract void Update();
    }
}