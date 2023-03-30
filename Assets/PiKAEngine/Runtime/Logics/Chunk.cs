namespace JuhaKurisu.PiKAEngine.Logics
{
    public class Chunk
    {
        public readonly ChunkPosition position;
        private readonly GameSettings settings;
        private Tile[,] tiles;
        private Tile[,] groundTiles;

        public Chunk(ChunkPosition position, GameSettings settings)
        {
            this.position = position;
            this.settings = settings;

            tiles = new Tile[settings.chunkSize.x, settings.chunkSize.y];
            groundTiles = new Tile[settings.chunkSize.x, settings.chunkSize.y];
            for (int y = 0; y < settings.chunkSize.y; y++)
            {
                for (int x = 0; x < settings.chunkSize.x; x++)
                {
                    tiles[x, y] = settings.emptyTile.GenerateTile(new(position, x, y));
                    groundTiles[x, y] = settings.emptyTile.GenerateTile(new(position, x, y));
                }
            }
        }
    }
}