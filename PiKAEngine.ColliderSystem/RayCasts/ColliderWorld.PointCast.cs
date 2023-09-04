using PiKAEngine.Mathematics;

namespace PiKAEngine.ColliderSystem;

public partial class ColliderWorld<T>
{
    public List<RectCollider<T>> PointCast(FixVector2 position, bool targetingInactiveCollider = false)
    {
        RayCastContactingColliders.Clear();

        var scaledPosition = position;
        scaledPosition -= WorldTransform.LeftBottomPosition;
        scaledPosition *= WorldTransform.Scale;

        var cellIndex = MortonOrder.GetLevelStartIndex(WorldTransform.Level) +
                        MortonOrder.GetMortonNumber((ushort)scaledPosition.X, (ushort)scaledPosition.Y);

        for (var i = cellIndex; i > 0; i = (i - 1) / 4)
        {
            var cell = _colliderCells[i];
            if (cell.Colliders is null) continue;
            foreach (var collider in cell.Colliders)
            {
                if (!targetingInactiveCollider && !collider.IsActive) continue;
                if (collider.Detect(position)) RayCastContactingColliders.Add(collider);
            }
        }

        return RayCastContactingColliders;
    }
}