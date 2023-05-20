using System.Collections.ObjectModel;

namespace PiKAEngine.Core.Maps
{
    public class TileManager : IDisposable
    {
        public readonly int chunkSize;
        public readonly Func<Tile> getEmptyTile;

        public ReadOnlyDictionary<ChunkPosition, Chunk> chunkDictionary => new(chunks);
        private readonly Dictionary<ChunkPosition, Chunk> chunks;

        private readonly HashSet<Tile> activeTiles;
        private readonly List<Tile> addingTiles;
        private readonly List<Tile> removingTiles;
        private readonly List<Tile> initializingTiles;

        public TileManager(int chunkSize, Func<Tile> getEmptyTile)
        {
            this.chunkSize = chunkSize;
            this.getEmptyTile = getEmptyTile;
            this.chunks = new Dictionary<ChunkPosition, Chunk>();

            activeTiles = new HashSet<Tile>();
            addingTiles = new List<Tile>();
            removingTiles = new List<Tile>();
            initializingTiles = new List<Tile>();
        }

        public bool TryAddChunk(Chunk chunk)
        {
            bool ret = chunks.TryAdd(chunk.position, chunk);

            foreach (var tile in chunk.tileList)
            {
                addingTiles.Add(tile);
            }

            return ret;
        }

        public bool TryChangeTile(Tile tile, MapPosition position)
        {
            if (!(position.tilePosition.x < chunkSize && position.tilePosition.y < chunkSize)) return false;
            if (!chunks.TryGetValue(position.chunkPosition, out Chunk chunk)) return false;

            tile.InitializeBase(position);
            addingTiles.Add(tile);
            removingTiles.Add(chunks[position.chunkPosition].tileList[position.tilePosition.y * chunkSize + position.tilePosition.x]);
            chunks[position.chunkPosition].SetTile(tile, position.tilePosition);

            return true;
        }

        public void Update()
        {
            // タイルの追加処理
            foreach (var tile in addingTiles)
                initializingTiles.Add(tile);

            // タイルの削除処理
            foreach (var tile in removingTiles)
            {
                activeTiles.Remove(tile);
                tile.Dispose();
                initializingTiles.Remove(tile);
            }

            // タイルのinitialize処理
            foreach (var tile in initializingTiles)
                tile.Initialize();

            // タイルのstart処理
            foreach (var tile in initializingTiles)
                tile.Start();

            // タイルのupdate処理
            foreach (var tile in activeTiles)
                tile.Update();

            // 作業用のリストたちの登録解除
            addingTiles.Clear();
            removingTiles.Clear();
            initializingTiles.Clear();
        }

        public void Dispose()
        {
            foreach (var chunk in chunks.Values)
            {
                chunk.Dispose();
            }
        }
    }
}