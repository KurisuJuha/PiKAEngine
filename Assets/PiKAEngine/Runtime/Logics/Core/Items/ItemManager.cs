using System;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Items
{
    public class ItemManager : IEntityManager<ItemManager, Item, ItemComponent>
    {
        private readonly EntityManagerBase<ItemManager, Item, ItemComponent> entityManagerBase;

        public ItemComponent[] baseComponents => entityManagerBase.baseComponents;
        public IObservable<ItemManager> onUpdated => entityManagerBase.onUpdated;

        public ItemManager(params ItemComponent[] baseComponents)
        {
            entityManagerBase = new(this, baseComponents);
        }

        public void Dispose()
            => entityManagerBase.Dispose();

        public void Update()
            => entityManagerBase.Update();
    }
}