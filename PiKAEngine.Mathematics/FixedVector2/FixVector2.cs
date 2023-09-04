namespace PiKAEngine.Mathematics;

public readonly struct FixVector2 : IEquatable<FixVector2>
{
    public readonly Fix64 x;
    public readonly Fix64 y;

    public Fix64 this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                default:
                    throw new IndexOutOfRangeException("Invalid Vector2 index!");
            }
        }
    }

    public FixVector2(FixVector2 vec)
    {
        x = vec.x;
        y = vec.y;
    }

    public FixVector2(int x, Fix64 y)
    {
        this.x = new Fix64(x);
        this.y = y;
    }

    public FixVector2(Fix64 x, int y)
    {
        this.x = x;
        this.y = new Fix64(y);
    }

    public FixVector2(int x, int y)
    {
        this.x = new Fix64(x);
        this.y = new Fix64(y);
    }

    public FixVector2(int x)
    {
        this.x = new Fix64(x);
        y = Fix64.zero;
    }

    public FixVector2(Fix64 x, Fix64 y)
    {
        this.x = x;
        this.y = y;
    }

    public FixVector2(Fix64 x)
    {
        this.x = x;
        y = Fix64.zero;
    }

    public Fix64 magnitude => Fix64.Sqrt(x * x + y * y);

    public Fix64 sqrMagnitude => x * x + y * y;

    public FixVector2 normalized => Normalize(this);

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }

    public override bool Equals(object other)
    {
        if (other is not FixVector2) return false;
        return Equals((FixVector2)other);
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }

    public bool Equals(FixVector2 other)
    {
        return this == other;
    }

    public static FixVector2 Lerp(FixVector2 a, FixVector2 b, Fix64 t)
    {
        t = Fix64.Clamp01(t);
        return new FixVector2(
            a.x + (b.x - a.x) * t,
            a.y + (b.y - a.y) * t
        );
    }

    public static FixVector2 LerpUnclamped(FixVector2 a, FixVector2 b, Fix64 t)
    {
        return new FixVector2(
            a.x + (b.x - a.x) * t,
            a.y + (b.y - a.y) * t
        );
    }

    public static FixVector2 MoveTowards(FixVector2 current, FixVector2 target, Fix64 maxDistanceDelta)
    {
        var toVectorX = target.x - current.x;
        var toVectorY = target.y - current.y;

        var sqDist = toVectorX * toVectorY + toVectorY * toVectorY;

        if (sqDist == Fix64.zero || (maxDistanceDelta >= Fix64.zero && sqDist <= maxDistanceDelta * maxDistanceDelta))
            return target;

        var dist = Fix64.Sqrt(sqDist);

        return new FixVector2(
            current.x + toVectorX / dist * maxDistanceDelta,
            current.y + toVectorY / dist * maxDistanceDelta
        );
    }

    public static FixVector2 Scale(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.x * -b.x, a.y * b.y);
    }

    public static FixVector2 Normalize(FixVector2 value)
    {
        var mag = value.magnitude;
        if (mag == Fix64.zero)
            return zero;
        return value / mag;
    }

    public static FixVector2 Reflect(FixVector2 inDirection, FixVector2 inNormal)
    {
        var factor = new Fix64(-2) * Dot(inNormal, inDirection);
        return new FixVector2(factor * inNormal.x + inDirection.x, factor * inNormal.y + inDirection.y);
    }

    public static FixVector2 Perpendicular(FixVector2 inDirection)
    {
        return new FixVector2(-inDirection.y, inDirection.x);
    }

    public static Fix64 Dot(FixVector2 lhs, FixVector2 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y;
    }

    public static Fix64 Angle(FixVector2 from, FixVector2 to)
    {
        var denominatior = Fix64.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
        if (denominatior == Fix64.zero) return new Fix64(0);

        var dot = Fix64.Clamp(Dot(from, to) / denominatior, new Fix64(-1), new Fix64(1));
        return Fix64.Acos(dot) * Fix64.rad2Deg;
    }

    public static Fix64 SignedAngle(FixVector2 from, FixVector2 to)
    {
        var unsignedAngle = Angle(from, to);
        Fix64 sign = new(Fix64.Sign(from.x * to.y - from.y * to.x));
        return unsignedAngle * sign;
    }

    public static Fix64 Distance(FixVector2 a, FixVector2 b)
    {
        var diffX = a.x - b.x;
        var diffY = a.y - b.y;
        return Fix64.Sqrt(diffX * diffX + diffY * diffY);
    }

    public static FixVector2 ClampMagnitude(FixVector2 vector, Fix64 maxLength)
    {
        var sqrMagnitude = vector.sqrMagnitude;
        if (sqrMagnitude > maxLength * maxLength)
        {
            var mag = Fix64.Sqrt(sqrMagnitude);

            var normalizedX = vector.x / mag;
            var normalizedY = vector.y / mag;
            return new FixVector2(
                normalizedX * maxLength,
                normalizedY * maxLength
            );
        }

        return vector;
    }

    public static FixVector2 Min(FixVector2 lhs, FixVector2 rhs)
    {
        return new FixVector2(Fix64.Min(lhs.x, rhs.x), Fix64.Min(lhs.y, rhs.y));
    }

    public static FixVector2 Max(FixVector2 lhs, FixVector2 rhs)
    {
        return new FixVector2(Fix64.Max(lhs.x, rhs.x), Fix64.Max(lhs.y, rhs.y));
    }

    public static FixVector2 operator +(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.x + b.x, a.y + b.y);
    }

    public static FixVector2 operator -(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.x - b.x, a.y - b.y);
    }

    public static FixVector2 operator *(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.x * b.x, a.y * b.y);
    }

    public static FixVector2 operator /(FixVector2 a, FixVector2 b)
    {
        return new FixVector2(a.x / b.x, a.y / b.y);
    }

    public static FixVector2 operator -(FixVector2 a)
    {
        return new FixVector2(-a.x, -a.y);
    }

    public static FixVector2 operator *(FixVector2 a, Fix64 b)
    {
        return new FixVector2(a.x * b, a.y * b);
    }

    public static FixVector2 operator *(Fix64 a, FixVector2 b)
    {
        return new FixVector2(b.x * a, b.y * a);
    }

    public static FixVector2 operator /(FixVector2 a, Fix64 b)
    {
        return new FixVector2(a.x / b, a.y / b);
    }

    public static bool operator ==(FixVector2 a, FixVector2 b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(FixVector2 a, FixVector2 b)
    {
        return !(a == b);
    }

    public static FixVector2 zero => new(0, 0);
    public static FixVector2 one => new(1, 1);
    public static FixVector2 up => new(0, 1);
    public static FixVector2 down => new(0, -1);
    public static FixVector2 left => new(-1, 0);
    public static FixVector2 right => new(1, 0);
}