namespace PiKAEngine.ObjectPoolingSystem;

public interface IObjectPoolSettings<T>
{
    bool HasLimit { get; }
    ushort Limit { get; }
    T CreateObject();
    void OnObjectRent(T entity);
    void OnObjectRelease(T entity);
    void OnObjectDestroy(T entity);
}