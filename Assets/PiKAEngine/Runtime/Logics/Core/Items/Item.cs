using System;

namespace PiKAEngine.Logics.Core.Items
{
    public abstract class Item : IDisposable
    {
        public readonly ItemManager itemManager;

        public Item(ItemManager itemManager)
        {
            this.itemManager = itemManager;
        }

        public abstract void Initialize();
        public abstract void Start();
        public abstract void Update();
        public abstract void Dispose();
    }
}