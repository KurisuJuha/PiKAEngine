using System.Linq;
using UnityEngine;
using AnnulusGames.LucidTools.Inspector;

namespace JuhaKurisu.PiKAEngine.Logics.Maps
{
    [HideMonoScript, CreateAssetMenu(menuName = "PiKAEngine/Tile", fileName = "newTile")]
    public class TileContents : ScriptableObject
    {
        public string id => _id;
        [SerializeField, DisableInPlayMode] private string _id;
        [SerializeField, SerializeReference] private TileComponent[] components;

        public TileContents(string id, TileComponent[] components)
        {
            this._id = id;
            this.components = components;
        }

        public Tile GenerateTile(Position position)
        {
            return new Tile(
                id,
                position,
                components.Select(component => component.Copy()).ToArray()
            );
        }
    }
}