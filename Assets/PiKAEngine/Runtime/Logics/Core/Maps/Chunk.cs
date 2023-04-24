using System;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Maps
{
    public class Chunk : IDisposable
    {
        public readonly ChunkPosition position;
        private readonly Map map;
        private Tile[,] tiles;
        public IObservable<Tile> onTileChanged => onTileChangedSubject;
        private readonly Subject<Tile> onTileChangedSubject = new();

        public Chunk(ChunkPosition position, Map map)
        {
            this.position = position;
            this.map = map;

            tiles = new Tile[map.chunkSize.x, map.chunkSize.y];
            for (int y = 0; y < map.chunkSize.y; y++)
            {
                for (int x = 0; x < map.chunkSize.x; x++)
                {
                    tiles[x, y] = map.emptyTile.Copy() as Tile;
                    tiles[x, y].onChanged
                        .Subscribe(tile => onTileChangedSubject.OnNext(tile as Tile));
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public Tile GetTile(TilePosition tilePosition)
            => GetTile(tilePosition.x, tilePosition.y);

        public void SetTile(int x, int y, Tile tile)
        {
            tiles[x, y] = tile.Copy() as Tile;
        }

        public void SetTile(TilePosition tilePosition, Tile tile)
            => SetTile(tilePosition.x, tilePosition.y, tile);

        public void Dispose()
        {
            for (int y = 0; y < map.chunkSize.y; y++)
            {
                for (int x = 0; x < map.chunkSize.x; x++)
                {
                    tiles[x, y].Dispose();
                }
            }
        }
    }
}