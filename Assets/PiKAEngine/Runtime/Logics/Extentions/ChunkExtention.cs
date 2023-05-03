using PiKAEngine.Logics.Core.TileMap;

namespace PiKAEngine.Logics.Extentions
{
    public static class ChunkExtention
    {
        public static bool TryChangeTile(this Chunk self, Tile tile, TilePosition position)
            => tile.tileManager.TryChangeTile(tile, new MapPosition(self.position, position));
    }
}