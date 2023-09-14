namespace PiKAEngine.ObjectPoolingSystem;

public static class ObjectPool<T>
{
    private static readonly List<T> Entities = new();
    public static IObjectPoolSettings<T>? Settings;

    public static bool TryRent(out T? value)
    {
        try
        {
            value = Rent();
            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }

    public static T Rent()
    {
        if (Settings is null) throw new Exception("settings does not exist.");

        T entity;

        if (Entities.Count == 0)
        {
            entity = Settings.CreateObject();
        }
        else
        {
            entity = Entities[^1];
            Entities.RemoveAt(Entities.Count - 1);
        }

        Settings.OnObjectRent(entity);
        return entity;
    }

    public static void Release(T entity)
    {
        if (Settings is null) throw new Exception("settings does not exist.");

        if (Settings.HasLimit && Entities.Count > Settings.Limit)
        {
            Settings.OnObjectDestroy(entity);
            return;
        }

        Entities.Add(entity);
        Settings.OnObjectRelease(entity);
    }
}