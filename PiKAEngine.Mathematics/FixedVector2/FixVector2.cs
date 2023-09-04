namespace PiKAEngine.Mathematics;

public readonly struct FixVector2 : IEquatable<FixVector2>
{
    public readonly Fix64 X;
    public readonly Fix64 Y;

    public Fix64 this[int index]
    {
        get
        {
            return index switch
            {
                0 => X,
                1 => Y,
                _ => throw new IndexOutOfRangeException("Invalid Vector2 index!")
            };
        }
    }

    public FixVector2(FixVector2 vec)
    {
        X = vec.X;
        Y = vec.Y;
    }

    public FixVector2(int x, Fix64 y)
    {
        X = new Fix64(x);
        Y = y;
    }

    public FixVector2(Fix64 x, int y)
    {
        X = x;
        Y = new Fix64(y);
    }

    public FixVector2(int x, int y)
    {
        X = new Fix64(x);
        Y = new Fix64(y);
    }

    public FixVector2(int x)
    {
        X = new Fix64(x);
        Y = Fix64.Zero;
    }

    public FixVector2(Fix64 x, Fix64 y)
    {
        X = x;
        Y = y;
    }

    public FixVector2(Fix64 x)
    {
        X = x;
        Y = Fix64.Zero;
    }

    public Fix64 Magnitude => Fix64.Sqrt(X * X + Y * Y);

    public Fix64 SqrMagnitude => X * X + Y * Y;

    public FixVector2 Normalized => Normalize(this);

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public override bool Equals(object other)
    {
        if (other is not FixVector2) return false;
        return Equals((FixVector2)other);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public bool Equals(FixVector2 other)
    {
        return this == other;
    }

    public static FixVector2 Lerp(FixVector2 a, FixVector2 b, Fix64 t)
    {
        t = Fix64.Clamp01(t);
        return new FixVector2(
            a.X + (b.X - a.X) * t,
            a.Y + (b.Y - a.Y) * t
        );
    }

    public static FixVector2 LerpUnclamped(FixVector2 a, FixVector2 b, Fix64 t)
    {
        return new FixVector2(
            a.X + (b.X - a.X) * t,
            a.Y + (b.Y - a.Y) * t
        );
    }

    public static FixVector2 MoveTowards(FixVector2 current, FixVector2 target, Fix64 maxDistanceDelta)
    {
        var toVectorX = target.X - current.X;
        var toVectorY = target.Y - current.Y;

        var sqDist = toVectorX * toVectorY + toVectorY * toVectorY;

        if (sqDist == Fix64.Zero || (maxDistanceDelta >= Fix64.Zero && sqDist <= maxDistanceDelta * maxDistanceDelta))
            return target;

        var dist = Fix64.Sqrt(sqDist);

        return new FixVector2(
            current.X + toVectorX / dist * maxDistanceDelta,
            current.Y + toVectorY / dist * maxDistanceDelta
        );
    }

    public static FixVector2 Scale(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.X * -b.X, a.Y * b.Y);
    }

    public static FixVector2 Normalize(FixVector2 value)
    {
        var mag = value.Magnitude;
        if (mag == Fix64.Zero)
            return Zero;
        return value / mag;
    }

    public static FixVector2 Reflect(FixVector2 inDirection, FixVector2 inNormal)
    {
        var factor = new Fix64(-2) * Dot(inNormal, inDirection);
        return new FixVector2(factor * inNormal.X + inDirection.X, factor * inNormal.Y + inDirection.Y);
    }

    public static FixVector2 Perpendicular(FixVector2 inDirection)
    {
        return new FixVector2(-inDirection.Y, inDirection.X);
    }

    public static Fix64 Dot(FixVector2 lhs, FixVector2 rhs)
    {
        return lhs.X * rhs.X + lhs.Y * rhs.Y;
    }

    public static Fix64 Angle(FixVector2 from, FixVector2 to)
    {
        var denominatior = Fix64.Sqrt(from.SqrMagnitude * to.SqrMagnitude);
        if (denominatior == Fix64.Zero) return new Fix64(0);

        var dot = Fix64.Clamp(Dot(from, to) / denominatior, new Fix64(-1), new Fix64(1));
        return Fix64.Acos(dot) * Fix64.Rad2Deg;
    }

    public static Fix64 SignedAngle(FixVector2 from, FixVector2 to)
    {
        var unsignedAngle = Angle(from, to);
        Fix64 sign = new(Fix64.Sign(from.X * to.Y - from.Y * to.X));
        return unsignedAngle * sign;
    }

    public static Fix64 Distance(FixVector2 a, FixVector2 b)
    {
        var diffX = a.X - b.X;
        var diffY = a.Y - b.Y;
        return Fix64.Sqrt(diffX * diffX + diffY * diffY);
    }

    public static FixVector2 ClampMagnitude(FixVector2 vector, Fix64 maxLength)
    {
        var sqrMagnitude = vector.SqrMagnitude;
        if (sqrMagnitude > maxLength * maxLength)
        {
            var mag = Fix64.Sqrt(sqrMagnitude);

            var normalizedX = vector.X / mag;
            var normalizedY = vector.Y / mag;
            return new FixVector2(
                normalizedX * maxLength,
                normalizedY * maxLength
            );
        }

        return vector;
    }

    public static FixVector2 Min(FixVector2 lhs, FixVector2 rhs)
    {
        return new FixVector2(Fix64.Min(lhs.X, rhs.X), Fix64.Min(lhs.Y, rhs.Y));
    }

    public static FixVector2 Max(FixVector2 lhs, FixVector2 rhs)
    {
        return new FixVector2(Fix64.Max(lhs.X, rhs.X), Fix64.Max(lhs.Y, rhs.Y));
    }

    public static FixVector2 operator +(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.X + b.X, a.Y + b.Y);
    }

    public static FixVector2 operator -(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.X - b.X, a.Y - b.Y);
    }

    public static FixVector2 operator *(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.X * b.X, a.Y * b.Y);
    }

    public static FixVector2 operator /(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.X / b.X, a.Y / b.Y);
    }

    public static FixVector2 operator -(FixVector2 a)
    {
        return new FixVector2(-a.X, -a.Y);
    }

    public static FixVector2 operator *(FixVector2 a, Fix64 b)
    {
        return new FixVector2(a.X * b, a.Y * b);
    }

    public static FixVector2 operator *(Fix64 a, FixVector2 b)
    {
        return new FixVector2(b.X * a, b.Y * a);
    }

    public static FixVector2 operator /(FixVector2 a, Fix64 b)
    {
        return new FixVector2(a.X / b, a.Y / b);
    }

    public static bool operator ==(FixVector2 a, FixVector2 b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(FixVector2 a, FixVector2 b)
    {
        return !(a == b);
    }

    public static FixVector2 Zero => new(0, 0);
    public static FixVector2 One => new(1, 1);
    public static FixVector2 Up => new(0, 1);
    public static FixVector2 Down => new(0, -1);
    public static FixVector2 Left => new(-1, 0);
    public static FixVector2 Right => new(1, 0);
}