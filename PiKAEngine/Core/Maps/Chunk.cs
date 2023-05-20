using System.Collections.ObjectModel;

namespace PiKAEngine.Core.Maps
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Chunk : IDisposable
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly TileManager tileManager;
        public readonly ChunkPosition position;
        public ReadOnlyCollection<Tile> tileList => new ReadOnlyCollection<Tile>(tiles);
        private readonly Tile[] tiles;

        public Chunk(TileManager tileManager, ChunkPosition position, Tile[] tiles)
        {
            if (tiles.Length != tileManager.chunkSize * tileManager.chunkSize) throw new System.Exception("sizeが設定と異なっています");
            this.tileManager = tileManager;
            this.position = position;
            this.tiles = tiles;
        }

        public Chunk(TileManager tileManager, ChunkPosition position)
        {
            this.tileManager = tileManager;
            this.position = position;
            tiles = new Tile[tileManager.chunkSize * tileManager.chunkSize];
            for (int i = 0; i < tiles.Length; i++)
                tiles[i] = tileManager.getEmptyTile();
        }

        internal void SetTile(Tile tile, TilePosition position)
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

        public void Dispose()
        {
            foreach (var tile in tiles)
            {
                tile.Dispose();
            }
        }
    }
}