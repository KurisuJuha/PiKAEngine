using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JuhaKurisu.PiKAEngine.Logics
{
    public class Map
    {
        public ReadOnlyDictionary<ChunkPosition, Chunk> chunks => new(_chunks);
        private readonly Dictionary<ChunkPosition, Chunk> _chunks;

        public Map()
        {
            _chunks = new();
        }

        public bool TryAddChunk(Chunk chunk)
        {
            return _chunks.TryAdd(chunk.position, chunk);
        }
    }
}