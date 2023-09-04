using PiKAEngine.Mathematics;

namespace PiKAEngine.ColliderSystem;

public readonly struct RectColliderTransform : IEquatable<RectColliderTransform>
{
    public readonly FixVector2 Position;
    public readonly FixVector2 Size;
    public readonly Fix64 Angle;
    public readonly FixVector2 LeftBottomPosition;
    public readonly FixVector2 LeftTopPosition;
    public readonly FixVector2 RightTopPosition;
    public readonly FixVector2 RightBottomPosition;

    public RectColliderTransform(FixVector2 position, FixVector2 size, Fix64 angle)
    {
        Position = position;
        Size = size;
        Angle = angle;

        var p = Size / Fix64.Two;
        var m = -p;

        LeftBottomPosition = new FixVector2(m.X, m.Y);
        LeftTopPosition = new FixVector2(m.X, p.Y);
        RightTopPosition = new FixVector2(p.X, p.Y);
        RightBottomPosition = new FixVector2(p.X, m.Y);

        if (Angle != Fix64.Zero)
        {
            LeftBottomPosition = AABB.RotatePoint(LeftBottomPosition, Angle);
            LeftTopPosition = AABB.RotatePoint(LeftTopPosition, Angle);
            RightTopPosition = AABB.RotatePoint(RightTopPosition, Angle);
            RightBottomPosition = AABB.RotatePoint(RightBottomPosition, Angle);
        }

        LeftBottomPosition += Position;
        LeftTopPosition += Position;
        RightTopPosition += Position;
        RightBottomPosition += Position;
    }

    public bool Detect(RectColliderTransform transform)
    {
        if (Detect(transform.LeftBottomPosition)) return true;
        if (Detect(transform.LeftTopPosition)) return true;
        if (Detect(transform.RightTopPosition)) return true;
        if (Detect(transform.RightBottomPosition)) return true;

        if (transform.Detect(LeftBottomPosition)) return true;
        if (transform.Detect(LeftTopPosition)) return true;
        if (transform.Detect(RightTopPosition)) return true;
        if (transform.Detect(RightBottomPosition)) return true;

        if (Detect(transform.LeftBottomPosition, transform.LeftTopPosition))
            return true;
        if (Detect(transform.LeftTopPosition, transform.RightTopPosition))
            return true;
        if (Detect(transform.RightTopPosition, transform.RightBottomPosition))
            return true;
        if (Detect(transform.RightBottomPosition, transform.LeftBottomPosition))
            return true;

        return false;
    }

    public bool Detect(FixVector2 startPosition, FixVector2 endPosition)
    {
        return IsLineCrossing(startPosition, endPosition, LeftBottomPosition, LeftTopPosition) ||
               IsLineCrossing(startPosition, endPosition, LeftTopPosition, RightTopPosition) ||
               IsLineCrossing(startPosition, endPosition, RightTopPosition, RightBottomPosition) ||
               IsLineCrossing(startPosition, endPosition, RightBottomPosition, LeftBottomPosition);
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

    public bool Detect(FixVector2 point)
    {
        return IsRight(LeftBottomPosition, LeftTopPosition, point) &&
               IsRight(LeftTopPosition, RightTopPosition, point) &&
               IsRight(RightTopPosition, RightBottomPosition, point) &&
               IsRight(RightBottomPosition, LeftBottomPosition, point);
    }

    private static bool IsRight(FixVector2 a, FixVector2 b, FixVector2 point)
    {
        var f = (b.X - a.X) * (point.Y - a.Y) - (point.X - a.X) * (b.Y - a.Y);
        return f <= Fix64.Zero;
    }

    public bool Equals(RectColliderTransform other)
    {
        return Position.Equals(other.Position) && Size.Equals(other.Size) && Angle.Equals(other.Angle);
    }

    public override bool Equals(object obj)
    {
        return obj is RectColliderTransform other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Position, Size, Angle);
    }

    public override string ToString()
    {
        return $"Position: {Position} Size: {Size} Angle: {Angle}";
    }
}