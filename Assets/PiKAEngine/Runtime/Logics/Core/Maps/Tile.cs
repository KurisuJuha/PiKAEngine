namespace PiKAEngine.Logics.Core.TileMap
{
    public abstract class Tile
    {
        public readonly TileManager tileManager;
        public Chunk chunk { get; private set; }
        public MapPosition position { get; private set; }

        public Tile(TileManager tileManager)
        {
            this.tileManager = tileManager;
        }

        public void InitializeBase(MapPosition position)
        {
            this.position = position;
            chunk = tileManager.chunkDictionary[position.chunkPosition];
        }

        public abstract void Initialize();
        public abstract void Start();
        public abstract void Update();
    }
}