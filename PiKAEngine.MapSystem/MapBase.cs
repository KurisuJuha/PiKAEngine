namespace PiKAEngine.MapSystem;

public abstract class MapBase<T>
{
    public readonly Dictionary<(int x, int y), ChunkBase<T>> ChunkBases = new();
}