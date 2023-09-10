using PiKAEngine.Mathematics;

namespace PiKAEngine.ColliderSystem;

public partial class ColliderWorld<T>
{
    public List<RectCollider<T>> LineCast(FixVector2 startPosition, FixVector2 endPosition,
        bool targetingInactiveCollider = false)
    {
        var scaledStartPosition = (startPosition - WorldTransform.LeftBottomPosition) * WorldTransform.Scale;
        var scaledEndPosition = (endPosition - WorldTransform.LeftBottomPosition) * WorldTransform.Scale;

        var aabbPosition = (scaledStartPosition + scaledEndPosition) / new Fix64(2);
        var aabbSize = new FixVector2(Fix64.Abs(scaledStartPosition.X - scaledEndPosition.X),
            Fix64.Abs(scaledStartPosition.Y - scaledEndPosition.Y));

        var cellIndex = MortonOrder.GetIndex(new AABB(new RectColliderTransform(aabbPosition, aabbSize, Fix64.Zero)),
            WorldTransform);

        for (var i = cellIndex;; i = (i - 1) / 4)
        {
            var cell = _colliderCells[i];
            if (cell.Colliders is null) continue;
            foreach (var collider in cell.Colliders)
            {
                if (!targetingInactiveCollider && !collider.IsActive) continue;
                if (!collider.Detect(startPosition, endPosition)) continue;
                if (!collider.Detect(startPosition)) continue;
                if (!collider.Detect(endPosition)) continue;
                RayCastContactingColliders.Add(collider);
            }

            if (i == 0) break;
        }

        return RayCastContactingColliders;
    }
}