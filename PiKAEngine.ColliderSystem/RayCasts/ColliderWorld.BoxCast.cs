using PiKAEngine.Mathematics;

namespace PiKAEngine.ColliderSystem;

public partial class ColliderWorld<T>
{
    public List<RectCollider<T>> BoxCast(RectColliderTransform transform, bool targetingInactiveCollider = false)
    {
        RayCastContactingColliders.Clear();

        var cellIndex = MortonOrder.GetIndex(new AABB(transform), WorldTransform);

        for (var i = cellIndex; i > 0; i = (i - 1) / 4)
        {
            var cell = _colliderCells[i];
            if (cell.Colliders is null) continue;
            foreach (var collider in cell.Colliders)
            {
                if (!targetingInactiveCollider && !collider.IsActive) continue;
                if (collider.Detect(transform)) RayCastContactingColliders.Add(collider);
            }
        }

        return RayCastContactingColliders;
    }

    public List<RectCollider<T>> BoxCast(FixVector2 position, FixVector2 size, bool targetingInactiveCollider = false)
    {
        return BoxCast(position, size, Fix64.Zero, targetingInactiveCollider);
    }

    public List<RectCollider<T>> BoxCast(FixVector2 position, FixVector2 size, Fix64 angle,
        bool targetingInactiveCollider = false)
    {
        return BoxCast(new RectColliderTransform(position, size, angle), targetingInactiveCollider);
    }
}