using System.Runtime.CompilerServices;
using PiKAEngine.Mathematics;

namespace PiKAEngine.ColliderSystem;

public class RectCollider<T>
{
    private readonly ColliderWorld<T> _world;
    private AABB _aabb;
    public int CellIndex;
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
        if (Detect(otherCollider.InternalTransform.LeftBottomPosition)) return true;
        if (Detect(otherCollider.InternalTransform.LeftTopPosition)) return true;
        if (Detect(otherCollider.InternalTransform.RightTopPosition)) return true;
        if (Detect(otherCollider.InternalTransform.RightBottomPosition)) return true;

        if (otherCollider.Detect(InternalTransform.LeftBottomPosition)) return true;
        if (otherCollider.Detect(InternalTransform.LeftTopPosition)) return true;
        if (otherCollider.Detect(InternalTransform.RightTopPosition)) return true;
        if (otherCollider.Detect(InternalTransform.RightBottomPosition)) return true;

        if (Detect(otherCollider.InternalTransform.LeftBottomPosition, InternalTransform.LeftTopPosition))
            return true;
        if (Detect(otherCollider.InternalTransform.LeftTopPosition, InternalTransform.RightTopPosition))
            return true;
        if (Detect(otherCollider.InternalTransform.RightTopPosition, InternalTransform.RightBottomPosition))
            return true;
        if (Detect(otherCollider.InternalTransform.RightBottomPosition, InternalTransform.LeftBottomPosition))
            return true;

        return false;
    }

    public bool Detect(FixVector2 point)
    {
        return IsRight(InternalTransform.LeftBottomPosition, InternalTransform.LeftTopPosition, point) &&
               IsRight(InternalTransform.LeftTopPosition, InternalTransform.RightTopPosition, point) &&
               IsRight(InternalTransform.RightTopPosition, InternalTransform.RightBottomPosition, point) &&
               IsRight(InternalTransform.RightBottomPosition, InternalTransform.LeftBottomPosition, point);
    }

    public bool Detect(FixVector2 startPosition, FixVector2 endPosition)
    {
        return IsLineCrossing(startPosition, endPosition, InternalTransform.LeftBottomPosition,
                   InternalTransform.LeftTopPosition) ||
               IsLineCrossing(startPosition, endPosition, InternalTransform.LeftTopPosition,
                   InternalTransform.RightTopPosition) ||
               IsLineCrossing(startPosition, endPosition, InternalTransform.RightTopPosition,
                   InternalTransform.RightBottomPosition) ||
               IsLineCrossing(startPosition, endPosition, InternalTransform.RightBottomPosition,
                   InternalTransform.LeftBottomPosition);
    }

    private static bool IsLineCrossing(FixVector2 aStartPosition, FixVector2 aEndPosition, FixVector2 bStartPosition,
        FixVector2 bEndPosition)
    {
        var vector0 = aEndPosition - aStartPosition;
        var vector1 = bEndPosition - bStartPosition;

        return Cross(vector0, bStartPosition - aStartPosition) * Cross(vector0, bEndPosition - aEndPosition) <
               new Fix64(0) &&
               Cross(vector1, aStartPosition - bStartPosition) * Cross(vector1, aEndPosition - bEndPosition) <
               new Fix64(0);
    }

    private static Fix64 Cross(FixVector2 vector0, FixVector2 vector1)
    {
        return vector0.X * vector1.Y - vector0.Y * vector1.X;
    }

    private static bool IsRight(FixVector2 a, FixVector2 b, FixVector2 point)
    {
        var f = (b.X - a.X) * (point.Y - a.Y) - (point.X - a.X) * (b.Y - a.Y);
        return f <= Fix64.Zero;
    }

    public override string ToString()
    {
        return $"({_aabb} {InternalTransform})";
    }
}