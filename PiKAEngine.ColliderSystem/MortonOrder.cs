using System.Runtime.CompilerServices;

namespace PiKAEngine.ColliderSystem;

public static class MortonOrder
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint BitSeparate(uint n)
    {
        n = (n | (n << 8)) & 0x00ff00ff;
        n = (n | (n << 4)) & 0x0f0f0f0f;
        n = (n | (n << 2)) & 0x33333333;
        return (n | (n << 1)) & 0x55555555;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetMortonNumber(ushort x, ushort y)
    {
        return BitSeparate(x) | (BitSeparate(y) << 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetIndex(uint a, uint b, uint level)
    {
        var highLevel = 0;

        var def = a ^ b;

        for (var i = 0; i < level; i++)
        {
            var check = (def >> (i * 2)) & 0x3;
            if (check != 0) highLevel = i + 1;
        }

        var space = a >> (highLevel * 2);
        var startIndex = (uint)GetLevelStartIndex(level - highLevel);

        return space + startIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetLevelStartIndex(long level)
    {
        return ((int)Math.Pow(4, level) - 1) / 3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetIndex(AABB aabb, WorldTransform worldTransform)
    {
        var rescaledAabb = aabb.Rescale(worldTransform);
        var leftTopMortonNumber = GetMortonNumber((ushort)rescaledAabb.LeftTopPosition.X,
            (ushort)rescaledAabb.LeftTopPosition.Y);
        var rightBottomMortonNumber = GetMortonNumber((ushort)rescaledAabb.RightBottomPosition.X,
            (ushort)rescaledAabb.RightBottomPosition.Y);

        return GetIndex(leftTopMortonNumber, rightBottomMortonNumber,
            worldTransform.Level);
    }
}