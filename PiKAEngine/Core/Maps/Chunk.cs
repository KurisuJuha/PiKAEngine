using System.Collections.ObjectModel;

namespace PiKAEngine.Core.Maps;

// ReSharper disable once ClassNeverInstantiated.Global
public class Chunk : IDisposable
{
    public readonly ChunkPosition position;

    // ReSharper disable once MemberCanBePrivate.Global
    public readonly TileManager tileManager;
    private readonly Tile[] tiles;

    public Chunk(TileManager tileManager, ChunkPosition position, Tile[] tiles)
    {
        if (tiles.Length != tileManager.chunkSize * tileManager.chunkSize) throw new Exception("sizeが設定と異なっています");
        this.tileManager = tileManager;
        this.position = position;
        this.tiles = tiles;
    }

    public Chunk(TileManager tileManager, ChunkPosition position)
    {
        this.tileManager = tileManager;
        this.position = position;
        tiles = new Tile[tileManager.chunkSize * tileManager.chunkSize];
        for (var i = 0; i < tiles.Length; i++)
            tiles[i] = tileManager.getEmptyTile();
    }

    public ReadOnlyCollection<Tile> tileList => new(tiles);

    public void Dispose()
    {
        foreach (var tile in tiles) tile.Dispose();
    }

    internal void SetTile(Tile tile, TilePosition tilePosition)
    {
        tiles[tilePosition.y * tileManager.chunkSize + tilePosition.x] = tile;
    }

    private void InitializeTileBase()
    {
        for (var y = 0; y < tileManager.chunkSize; y++)
        for (var x = 0; x < tileManager.chunkSize; x++)
            tiles[y * tileManager.chunkSize + x].InitializeBase(new MapPosition(position, x, y));
    }
}