using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UniRx;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Maps
{
    public class Map : IEntityManager<Map, Tile, TileComponent>
    {
        public ReadOnlyDictionary<ChunkPosition, Chunk> chunks => new(_chunks);
        private readonly Dictionary<ChunkPosition, Chunk> _chunks;
        public TileComponent[] baseComponents => _baseComponents;
        private readonly TileComponent[] _baseComponents;
        private IObservable<Tile> onTileChanged => onTileChangedSubject;
        public readonly Subject<Tile> onTileChangedSubject = new();
        public IObservable<Map> onUpdated => onUpdatedSubject;
        private readonly Subject<Map> onUpdatedSubject = new();
        public readonly Vector2Int chunkSize;
        public readonly Tile emptyTile;

        public Map(Vector2Int chunkSize, Tile emptyTile, TileComponent[] baseComponents)
        {
            this.chunkSize = chunkSize;
            this.emptyTile = emptyTile;
            this._baseComponents = baseComponents;
            _chunks = new();
        }

        public void Update()
        {
            onUpdatedSubject.OnNext(this);
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
            onUpdatedSubject.Dispose();
            foreach (var chunk in chunks.Values)
            {
                chunk.Dispose();
            }
        }
    }
}