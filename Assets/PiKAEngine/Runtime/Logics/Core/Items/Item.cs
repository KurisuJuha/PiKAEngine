using System;

namespace PiKAEngine.Logics.Core.Items
{
    public abstract class Item : IDisposable
    {
        public readonly IItemManager itemManager;

        public Item(IItemManager itemManager)
        {
            this.itemManager = itemManager;
        }

        public abstract void Initialize();
        public abstract void Start();
        public abstract void Update();
        public abstract void Dispose();
    }
}