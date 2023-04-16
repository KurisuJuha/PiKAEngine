using System.Linq;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Items
{
    public class ItemContents
    {
        private ItemComponent[] components;
        private ItemManager itemManager;

        public ItemContents(ItemComponent[] components, ItemManager itemManager)
        {
            this.components = components;
            this.itemManager = itemManager;
        }

        public Item GenerateItem()
        {
            return new Item(
                itemManager,
                components.Select(component => component.Copy()).ToArray()
            );
        }
    }
}