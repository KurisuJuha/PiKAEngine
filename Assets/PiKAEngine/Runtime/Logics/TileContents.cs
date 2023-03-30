using UnityEngine;
using AnnulusGames.LucidTools.Inspector;

namespace JuhaKurisu.PiKAEngine.Logics
{
    [HideMonoScript, CreateAssetMenu(menuName = "PiKAEngine/Tile", fileName = "newTile")]
    public class TileContents : ScriptableObject
    {
        public string id => _id;
        [SerializeField, DisableInPlayMode] private string _id;
        [SerializeField, SerializeReference] private TileComponent[] components;
        [SerializeField] private int[] test;

        public virtual Tile GenerateTile(Position position) => new(id, position, components);
    }
}