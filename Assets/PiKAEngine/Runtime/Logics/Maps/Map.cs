using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Maps
{
    public class Map : IDisposable
    {
        public ReadOnlyDictionary<ChunkPosition, Chunk> chunks => new(_chunks);
        private readonly Dictionary<ChunkPosition, Chunk> _chunks;
        private IObservable<Tile> onTileChanged => onTileChangedSubject;
        public readonly Subject<Tile> onTileChangedSubject = new();

        public Map()
        {
            _chunks = new();
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