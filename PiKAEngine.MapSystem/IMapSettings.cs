namespace PiKAEngine.MapSystem;

public interface IMapSettings<T>
{
    (int width, int height) ChunkSize { get; }
    T GetEmptyTile();
}