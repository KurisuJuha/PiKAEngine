using System;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics
{
    public class Chunk
    {
        public readonly ChunkPosition position;
        private readonly GameSettings settings;
        private Tile[,] tiles;

        public Chunk(ChunkPosition position, GameSettings settings)
        {
            this.position = position;
            this.settings = settings;

            tiles = new Tile[settings.chunkSize.x, settings.chunkSize.y];
            for (int y = 0; y < settings.chunkSize.y; y++)
            {
                for (int x = 0; x < settings.chunkSize.x; x++)
                {
                    tiles[x, y] = settings.emptyTile.GenerateTile(new(position, x, y));
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public void SetTile(int x, int y, TileContents tileContents)
        {
            tiles[x, y] = tileContents.GenerateTile(
                new Position(position, new TilePosition(x, y))
            );
        }
    }
}