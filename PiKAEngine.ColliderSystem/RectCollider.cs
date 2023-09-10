using System.Runtime.CompilerServices;
using PiKAEngine.Mathematics;

namespace PiKAEngine.ColliderSystem;

public class RectCollider<T>
{
    private readonly ColliderWorld<T> _world;
    private AABB _aabb;
    internal int CellIndex;
    public T Entity;
    internal int Index;
    internal RectColliderTransform InternalTransform;
    public bool IsActive;
    internal bool IsRegistered;

    public RectCollider(T entity, RectColliderTransform transform, ColliderWorld<T> world, bool isActive = false)
    {
        Entity = entity;
        _world = world;
        IsActive = isActive;
        ChangeTransform(transform);
    }

    public RectColliderTransform Transform
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => InternalTransform;
        set => ChangeTransform(value);
    }

    public void ChangeTransform(RectColliderTransform transform)
    {
        var isRegistered = IsRegistered;

        // 登録されているなら一度削除
        if (isRegistered) Remove();

        InternalTransform = transform;
        _aabb = new AABB(InternalTransform);
        CellIndex = (int)MortonOrder.GetIndex(_aabb, _world.WorldTransform);

        // もともと登録されていたならもう一度登録しておく
        if (isRegistered) Register();
    }

    public void Remove()
    {
        _world.Remove(this);
    }

    public void Register()
    {
        _world.Register(this);
    }

    public bool Detect(RectCollider<T> otherCollider)
    {
        return InternalTransform.Detect(otherCollider.Transform);
    }

    public bool Detect(FixVector2 startPosition, FixVector2 endPosition)
    {
        return InternalTransform.Detect(startPosition, endPosition);
    }

    public bool Detect(FixVector2 point)
    {
        return InternalTransform.Detect(point);
    }

    public bool Detect(RectColliderTransform transform)
    {
        return InternalTransform.Detect(transform);
    }

    public override string ToString()
    {
        return $"({_aabb} {InternalTransform})";
    }
}