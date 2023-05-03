namespace PiKAEngine.Logics.Core.TileMap
{
    public struct MapPosition
    {
        public readonly ChunkPosition chunkPosition;
        public readonly TilePosition tilePosition;

        public MapPosition(ChunkPosition chunkPosition, TilePosition tilePosition)
        {
            this.chunkPosition = chunkPosition;
            this.tilePosition = tilePosition;
        }

        public MapPosition(int chunkX, int chunkY, int tileX, int tileY)
        {
            this.chunkPosition = new(chunkX, chunkY);
            this.tilePosition = new(tileX, tileY);
        }

        public MapPosition(int chunkX, int chunkY, TilePosition tilePosition)
        {
            this.chunkPosition = new(chunkX, chunkY);
            this.tilePosition = tilePosition;
        }

        public MapPosition(ChunkPosition chunkPosition, int tileX, int tileY)
        {
            this.chunkPosition = chunkPosition;
            this.tilePosition = new(tileX, tileY);
        }
    }
}