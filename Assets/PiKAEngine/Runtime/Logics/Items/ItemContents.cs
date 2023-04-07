using System.Linq;

namespace JuhaKurisu.PiKAEngine.Logics.Items
{
    public class ItemContents
    {
        private ItemComponent[] components;
        private ItemSettings itemSettings;

        public ItemContents(ItemComponent[] components, ItemSettings itemSettings)
        {
            this.components = components;
            this.itemSettings = itemSettings;
        }

        public Item GenerateItem()
        {
            return new Item(
                components.Select(component => component.Copy()).ToArray(),
                itemSettings
            );
        }
    }
}