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