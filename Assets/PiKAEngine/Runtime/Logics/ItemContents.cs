using System.Linq;
using UnityEngine;
using AnnulusGames.LucidTools.Inspector;

namespace JuhaKurisu.PiKAEngine.Logics
{
    [HideMonoScript, CreateAssetMenu(menuName = "PiKAEngine/Item", fileName = "newItem")]
    public class ItemContents : ScriptableObject
    {
        public string id => _id;
        [SerializeField, DisableInPlayMode] private string _id;
        [SerializeField, SerializeReference] private ItemComponent[] components;

        public ItemContents(string id, ItemComponent[] components)
        {
            this._id = id;
            this.components = components;
        }

        public Item GenerateItem()
        {
            return new Item(
                id,
                components.Select(component => component.Copy()).ToArray()
            );
        }
    }
}