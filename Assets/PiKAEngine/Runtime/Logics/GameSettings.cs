using System.Collections.ObjectModel;
using UnityEngine;
using AnnulusGames.LucidTools.Inspector;

namespace JuhaKurisu.PiKAEngine.Logics
{
    [HideMonoScript, CreateAssetMenu(menuName = "PiKAEngine/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public Vector2Int chunkSize => _chunkSize;
        [SerializeField, DisableInPlayMode] private Vector2Int _chunkSize;
        public TileContents emptyTile => _emptyTile;
        [SerializeField, LabelText("Empty Tile"), Required] private TileContents _emptyTile;
        public ReadOnlyCollection<TileContents> tileContentsList => new(_tileContentsList);
        [SerializeField, LabelText("Tiles")] private TileContents[] _tileContentsList;
    }
}