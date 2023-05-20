namespace PiKAEngine.Core.Maps
{
    public abstract class Tile : IDisposable
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly TileManager tileManager;
        public Chunk chunk { get; private set; }
        public MapPosition position { get; private set; }

        protected Tile(TileManager tileManager, Chunk chunk)
        {
            this.tileManager = tileManager;
            this.chunk = chunk;
        }

        public void InitializeBase(MapPosition position)
        {
            this.position = position;
            chunk = tileManager.chunkDictionary[position.chunkPosition];
        }

        public abstract void Initialize();
        public abstract void Start();
        public abstract void Update();
        public abstract void Dispose();
    }
}