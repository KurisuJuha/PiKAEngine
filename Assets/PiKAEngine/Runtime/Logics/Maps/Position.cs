namespace JuhaKurisu.PiKAEngine.Logics.Maps
{
    public struct Position
    {
        public readonly ChunkPosition chunkPosition;
        public readonly TilePosition tilePosition;

        public Position(ChunkPosition chunkPosition, TilePosition tilePosition)
        {
            this.chunkPosition = chunkPosition;
            this.tilePosition = tilePosition;
        }

        public Position(int chunkX, int chunkY, int tileX, int tileY)
        {
            this.chunkPosition = new(chunkX, chunkY);
            this.tilePosition = new(tileX, tileY);
        }

        public Position(int chunkX, int chunkY, TilePosition tilePosition)
        {
            this.chunkPosition = new(chunkX, chunkY);
            this.tilePosition = tilePosition;
        }

        public Position(ChunkPosition chunkPosition, int tileX, int tileY)
        {
            this.chunkPosition = chunkPosition;
            this.tilePosition = new(tileX, tileY);
        }
    }
}