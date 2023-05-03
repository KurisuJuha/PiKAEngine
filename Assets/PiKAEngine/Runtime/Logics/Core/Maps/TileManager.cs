using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PiKAEngine.Logics.Core.TileMap
{
    public class TileManager<T> where T : Tile<T>
    {
        public readonly int chunkSize;
        public readonly Func<T> getEmptyTile;

        public ReadOnlyDictionary<ChunkPosition, Chunk<T>> chunkDictionary => new(chunks);
        private readonly Dictionary<ChunkPosition, Chunk<T>> chunks;

        private readonly HashSet<T> activeTiles;
        private readonly List<T> addingTiles;
        private readonly List<T> removingTiles;
        private readonly List<T> initializingTiles;

        public TileManager(int chunkSize, Func<T> getEmptyTile)
        {
            this.chunkSize = chunkSize;
            this.getEmptyTile = getEmptyTile;
            this.chunks = new Dictionary<ChunkPosition, Chunk<T>>();

            activeTiles = new HashSet<T>();
            addingTiles = new List<T>();
            removingTiles = new List<T>();
            initializingTiles = new List<T>();
        }

        public bool TryAddChunk(Chunk<T> chunk)
        {
            bool ret = chunks.TryAdd(chunk.position, chunk);

            foreach (var tile in chunk.tileList)
            {
                addingTiles.Add(tile);
            }

            return ret;
        }

        public bool TryChangeTile(T tile, MapPosition position)
        {
            if (!(position.tilePosition.x < chunkSize && position.tilePosition.y < chunkSize)) return false;
            if (!chunks.TryGetValue(position.chunkPosition, out Chunk<T> chunk)) return false;

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
    }
}