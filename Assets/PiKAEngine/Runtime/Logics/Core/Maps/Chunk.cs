using System.Collections.ObjectModel;

namespace PiKAEngine.Logics.Core.TileMap
{
    public class Chunk<T> where T : Tile<T>
    {
        public readonly TileManager<T> tileManager;
        public readonly ChunkPosition position;
        public ReadOnlyCollection<T> tileList => new ReadOnlyCollection<T>(tiles);
        private T[] tiles;

        public Chunk(TileManager<T> tileManager, ChunkPosition position, T[] tiles)
        {
            if (tiles.Length != tileManager.chunkSize * tileManager.chunkSize) throw new System.Exception("sizeが設定と異なっています");
            this.tileManager = tileManager;
            this.position = position;
            this.tiles = tiles;
        }

        public Chunk(TileManager<T> tileManager, ChunkPosition position)
        {
            this.tileManager = tileManager;
            this.position = position;
            tiles = new T[tileManager.chunkSize * tileManager.chunkSize];
            for (int i = 0; i < tiles.Length; i++)
                tiles[i] = tileManager.getEmptyTile();
        }

        internal void SetTile(T tile, TilePosition position)
        {
            tiles[position.y * tileManager.chunkSize + position.x] = tile;
        }

        private void InitializeTileBase()
        {
            for (int y = 0; y < tileManager.chunkSize; y++)
            {
                for (int x = 0; x < tileManager.chunkSize; x++)
                {
                    tiles[y * tileManager.chunkSize + x].InitializeBase(new MapPosition(position, x, y));
                }
            }
        }
    }
}