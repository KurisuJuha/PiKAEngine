using PiKAEngine.Mathematics;

// ReSharper disable InconsistentNaming

namespace PiKAEngine.ColliderSystem;

public readonly struct AABB : IEquatable<AABB>
{
    public readonly FixVector2 LeftTopPosition;
    public readonly FixVector2 RightBottomPosition;

    public AABB(RectColliderTransform transform)
    {
        var p = transform.Size / Fix64.Two;
        var m = -p;

        var pos1 = new FixVector2(m.X, m.Y);
        var pos2 = new FixVector2(m.X, p.Y);
        var pos3 = new FixVector2(p.X, p.Y);
        var pos4 = new FixVector2(p.X, m.Y);

        if (transform.Angle != Fix64.Zero)
        {
            pos1 = RotatePoint(pos1, transform.Angle);
            pos2 = RotatePoint(pos2, transform.Angle);
            pos3 = RotatePoint(pos3, transform.Angle);
            pos4 = RotatePoint(pos4, transform.Angle);
        }

        pos1 += transform.Position;
        pos2 += transform.Position;
        pos3 += transform.Position;
        pos4 += transform.Position;

        var leftUpPositionX = transform.Position.X;
        var leftUpPositionY = transform.Position.Y;
        var rightDownPositionX = transform.Position.X;
        var rightDownPositionY = transform.Position.Y;

        // Left Up Position
        if (leftUpPositionX > pos1.X) leftUpPositionX = pos1.X;
        if (leftUpPositionX > pos2.X) leftUpPositionX = pos2.X;
        if (leftUpPositionX > pos3.X) leftUpPositionX = pos3.X;
        if (leftUpPositionX > pos4.X) leftUpPositionX = pos4.X;

        if (leftUpPositionY < pos1.Y) leftUpPositionY = pos1.Y;
        if (leftUpPositionY < pos2.Y) leftUpPositionY = pos2.Y;
        if (leftUpPositionY < pos3.Y) leftUpPositionY = pos3.Y;
        if (leftUpPositionY < pos4.Y) leftUpPositionY = pos4.Y;

        // Right Down Position
        if (rightDownPositionX < pos1.X) rightDownPositionX = pos1.X;
        if (rightDownPositionX < pos2.X) rightDownPositionX = pos2.X;
        if (rightDownPositionX < pos3.X) rightDownPositionX = pos3.X;
        if (rightDownPositionX < pos4.X) rightDownPositionX = pos4.X;

        if (rightDownPositionY > pos1.Y) rightDownPositionY = pos1.Y;
        if (rightDownPositionY > pos2.Y) rightDownPositionY = pos2.Y;
        if (rightDownPositionY > pos3.Y) rightDownPositionY = pos3.Y;
        if (rightDownPositionY > pos4.Y) rightDownPositionY = pos4.Y;

        LeftTopPosition = new FixVector2(leftUpPositionX, leftUpPositionY);
        RightBottomPosition = new FixVector2(rightDownPositionX, rightDownPositionY);
    }

    private AABB(FixVector2 leftTopPosition, FixVector2 rightBottomPosition)
    {
        LeftTopPosition = leftTopPosition;
        RightBottomPosition = rightBottomPosition;
    }

    public static FixVector2 RotatePoint(FixVector2 vec, Fix64 angle)
    {
        var radAngle = Fix64.Deg2Rad * angle;
        var cos = Fix64.Cos(radAngle);
        var sin = Fix64.Sin(radAngle);

        return new FixVector2(
            vec.X * cos - vec.Y * sin,
            vec.X * sin + vec.Y * cos
        );
    }

    public readonly AABB Rescale(WorldTransform worldTransform)
    {
        return new AABB((LeftTopPosition - worldTransform.LeftBottomPosition) * worldTransform.Scale,
            (RightBottomPosition - worldTransform.LeftBottomPosition) * worldTransform.Scale);
    }

    public bool Equals(AABB other)
    {
        return LeftTopPosition.Equals(other.LeftTopPosition) &&
               RightBottomPosition.Equals(other.RightBottomPosition);
    }

    public override bool Equals(object obj)
    {
        return obj is AABB other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(LeftTopPosition, RightBottomPosition);
    }

    public override string ToString()
    {
        return $"LT: {LeftTopPosition} RT: {RightBottomPosition}";
    }
}