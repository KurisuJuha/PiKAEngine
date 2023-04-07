using System.Linq;
using UnityEngine;
using AnnulusGames.LucidTools.Inspector;

namespace JuhaKurisu.PiKAEngine.Logics.Items
{
    [HideMonoScript, CreateAssetMenu(menuName = "PiKAEngine/Item", fileName = "newItem")]
    public class ItemContents : ScriptableObject
    {
        [SerializeField, SerializeReference] private ItemComponent[] components;

        public ItemContents(ItemComponent[] components)
        {
            this.components = components;
        }

        public Item GenerateItem()
        {
            return new Item(
                components.Select(component => component.Copy()).ToArray()
            );
        }
    }
}