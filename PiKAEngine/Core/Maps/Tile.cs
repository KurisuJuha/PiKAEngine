namespace PiKAEngine.Core.Maps;

public abstract class Tile : IDisposable
{
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly TileManager tileManager;

    protected Tile(TileManager tileManager)
    {
        this.tileManager = tileManager;
    }

    public Chunk? chunk { get; private set; }
    public MapPosition position { get; private set; }
    public abstract void Dispose();

    public void InitializeBase(MapPosition mapPosition)
    {
        position = mapPosition;
        chunk = tileManager.chunkDictionary[mapPosition.chunkPosition];
    }

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
}