namespace PiKAEngine.MapSystem;

public abstract class ChunkBase<T>
{
    private readonly IMapSettings<T> _settings;
    private readonly T[,] _tiles;

    protected ChunkBase(IMapSettings<T> settings)
    {
        _settings = settings;
        _tiles = new T[settings.ChunkSize.width, settings.ChunkSize.height];
        for (var y = 0; y < settings.ChunkSize.height; y++)
        for (var x = 0; x < settings.ChunkSize.width; x++)
            _tiles[x, y] = settings.GetEmptyTile();
    }

    public bool TryGetTile(int x, int y, out T? tile)
    {
        tile = default;
        if (_settings.ChunkSize.width <= x) return false;
        if (_settings.ChunkSize.height <= y) return false;

        tile = _tiles[x, y];

        return true;
    }

    public bool TrySetTile(int x, int y, T tile)
    {
        if (_settings.ChunkSize.width <= x) return false;
        if (_settings.ChunkSize.height <= y) return false;

        _tiles[x, y] = tile;

        return true;
    }
}