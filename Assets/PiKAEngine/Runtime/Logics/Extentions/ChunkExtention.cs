using PiKAEngine.Logics.Core.TileMap;

namespace PiKAEngine.Logics.Extentions
{
    public static class ChunkExtention
    {
        public static bool TryChangeTile<T>(this Chunk<T> self, T tile, TilePosition position)
            where T : Tile<T>
            => tile.tileManager.TryChangeTile(tile, new MapPosition(self.position, position));
    }
}