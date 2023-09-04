﻿namespace PiKAEngine.Mathematics;

public static class FixVector2Extension
{
    public static FixVector2 RotatePoint(this FixVector2 self, FixVector2 origin, Fix64 angle)
    {
        return self.RotatePoint(angle) + origin;
    }

    public static FixVector2 RotatePoint(this FixVector2 self, Fix64 angle)
    {
        var radAngle = Fix64.deg2Rad * angle;
        var cos = Fix64.Cos(radAngle);
        var sin = Fix64.Sin(radAngle);

        return new FixVector2(
            self.x * cos - self.y * sin,
            self.x * sin + self.y * cos
        );
    }
}