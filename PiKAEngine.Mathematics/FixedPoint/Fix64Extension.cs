namespace PiKAEngine.Mathematics;

public static class Fix64Extension
{
    public static int Sign(this Fix64 value)
    {
        return Fix64.Sign(value);
    }

    public static Fix64 Abs(this Fix64 value)
    {
        return Fix64.Abs(value);
    }

    public static Fix64 FastAbs(this Fix64 value)
    {
        return Fix64.FastAbs(value);
    }

    public static Fix64 Floor(this Fix64 value)
    {
        return Fix64.Floor(value);
    }

    public static long FloorToLong(this Fix64 value)
    {
        return Fix64.FloorToLong(value);
    }

    public static Fix64 Ceiling(this Fix64 value)
    {
        return Fix64.Ceiling(value);
    }

    public static Fix64 Round(this Fix64 value)
    {
        return Fix64.Round(value);
    }

    public static Fix64 Ln(this Fix64 value)
    {
        return Fix64.Ln(value);
    }

    public static Fix64 Pow(this Fix64 value, Fix64 exp)
    {
        return Fix64.Pow(value, exp);
    }

    public static Fix64 Sqrt(this Fix64 value)
    {
        return Fix64.Sqrt(value);
    }

    public static Fix64 Sin(this Fix64 value)
    {
        return Fix64.Sin(value);
    }

    public static Fix64 FastSin(this Fix64 value)
    {
        return Fix64.FastSin(value);
    }

    public static Fix64 Cos(this Fix64 value)
    {
        return Fix64.Cos(value);
    }

    public static Fix64 FastCos(this Fix64 value)
    {
        return Fix64.FastCos(value);
    }

    public static Fix64 Tan(this Fix64 value)
    {
        return Fix64.Tan(value);
    }

    public static Fix64 Acos(this Fix64 value)
    {
        return Fix64.Acos(value);
    }

    public static Fix64 Atan(this Fix64 value)
    {
        return Fix64.Atan(value);
    }

    public static Fix64 Clamp(this Fix64 value, Fix64 min, Fix64 max)
    {
        return Fix64.Clamp(value, min, max);
    }

    public static Fix64 Clamp(this Fix64 value, int min, int max)
    {
        return Fix64.Clamp(value, new Fix64(min), new Fix64(max));
    }

    public static Fix64 Clamp01(this Fix64 value)
    {
        return Fix64.Clamp01(value);
    }
}