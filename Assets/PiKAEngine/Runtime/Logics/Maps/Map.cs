using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Maps
{
    public class Map : IDisposable
    {
        public ReadOnlyDictionary<ChunkPosition, Chunk> chunks => new(_chunks);
        private readonly Dictionary<ChunkPosition, Chunk> _chunks;
        public readonly ReadOnlyCollection<TileComponent> baseComponents;
        private IObservable<Tile> onTileChanged => onTileChangedSubject;
        public readonly Subject<Tile> onTileChangedSubject = new();
        public IObservable<Unit> onUpdate => onUpdateSubject;
        private readonly Subject<Unit> onUpdateSubject = new();
        public readonly Vector2Int chunkSize;
        public readonly TileContents emptyTile;

        public Map(Vector2Int chunkSize, TileContents emptyTile, TileComponent[] baseComponents)
        {
            this.chunkSize = chunkSize;
            this.emptyTile = emptyTile;
            this.baseComponents = new(baseComponents);
            _chunks = new();
        }

        public void Update()
        {
            onUpdateSubject.OnNext(Unit.Default);
        }

        public bool TryAddChunk(Chunk chunk)
        {
            bool ret = _chunks.TryAdd(chunk.position, chunk);

            chunk.onTileChanged.Subscribe(tile => onTileChangedSubject.OnNext(tile));

            return ret;
        }

        public void Dispose()
        {
            onTileChangedSubject.Dispose();
            foreach (var chunk in chunks.Values)
            {
                chunk.Dispose();
            }
        }
    }
}