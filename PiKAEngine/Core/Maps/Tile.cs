namespace PiKAEngine.Core.Maps
{
    public abstract class Tile : IDisposable
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly TileManager tileManager;
        public Chunk? chunk { get; private set; }
        public MapPosition position { get; private set; }

        protected Tile(TileManager tileManager)
        {
            this.tileManager = tileManager;
        }

        public void InitializeBase(MapPosition mapPosition)
        {
            this.position = mapPosition;
            chunk = tileManager.chunkDictionary[mapPosition.chunkPosition];
        }

        public abstract void Initialize();
        public abstract void Start();
        public abstract void Update();
        public abstract void Dispose();
    }
}