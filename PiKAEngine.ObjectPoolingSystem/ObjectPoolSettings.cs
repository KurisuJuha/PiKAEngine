namespace PiKAEngine.ObjectPoolingSystem;

public class ObjectPoolSettings<T> : IObjectPoolSettings<T>
{
    private readonly Func<T> _createObject;
    private readonly Func<bool> _hasLimit;
    private readonly Func<ushort> _limit;
    private readonly Action<T> _onObjectDestroy;
    private readonly Action<T> _onObjectRelease;
    private readonly Action<T> _onObjectRent;

    public ObjectPoolSettings(Func<bool> hasLimit, Func<ushort> limit, Func<T> createObject, Action<T> onObjectRent,
        Action<T> onObjectRelease, Action<T> onObjectDestroy, bool autoSet = true)
    {
        _hasLimit = hasLimit;
        _limit = limit;
        _createObject = createObject;
        _onObjectRent = onObjectRent;
        _onObjectRelease = onObjectRelease;
        _onObjectDestroy = onObjectDestroy;

        if (!autoSet) return;
        ObjectPool<T>.Settings = this;
    }

    public bool HasLimit => _hasLimit.Invoke();
    public ushort Limit => _limit.Invoke();

    public T CreateObject()
    {
        return _createObject.Invoke();
    }

    public void OnObjectRent(T entity)
    {
        _onObjectRent.Invoke(entity);
    }

    public void OnObjectRelease(T entity)
    {
        _onObjectRelease.Invoke(entity);
    }

    public void OnObjectDestroy(T entity)
    {
        _onObjectDestroy.Invoke(entity);
    }
}