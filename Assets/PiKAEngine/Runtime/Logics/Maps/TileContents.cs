using System.Linq;
using UnityEngine;
using AnnulusGames.LucidTools.Inspector;

namespace JuhaKurisu.PiKAEngine.Logics.Maps
{
    [HideMonoScript, CreateAssetMenu(menuName = "PiKAEngine/Tile", fileName = "newTile")]
    public class TileContents : ScriptableObject
    {
        [SerializeField, SerializeReference] private TileComponent[] components;

        public TileContents(TileComponent[] components)
        {
            this.components = components;
        }

        public Tile GenerateTile(Position position)
        {
            return new Tile(
                position,
                components.Select(component => component.Copy()).ToArray()
            );
        }
    }
}