using PiKAEngine.Mathematics;

namespace PiKAEngine.ColliderSystem;

public partial class ColliderWorld<T>
{
    public List<RectCollider<T>> LineCast(FixVector2 startPosition, FixVector2 endPosition,
        bool targetingInactiveCollider = false)
    {
        var scaledStartPosition = (startPosition - WorldTransform.LeftBottomPosition) * WorldTransform.Scale;
        var scaledEndPosition = (startPosition - WorldTransform.LeftBottomPosition) * WorldTransform.Scale;

        var aabbPosition = (scaledStartPosition + scaledEndPosition) / new Fix64(2);
        var aabbSize = new FixVector2(Fix64.Abs(scaledStartPosition.X - scaledEndPosition.X),
            Fix64.Abs(scaledStartPosition.Y - scaledEndPosition.Y));

        var cellIndex = MortonOrder.GetIndex(new AABB(new RectColliderTransform(aabbPosition, aabbSize, Fix64.Zero)),
            WorldTransform);

        return RayCastContactingColliders;
    }
}