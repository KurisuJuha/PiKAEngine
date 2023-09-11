using PiKAEngine.Mathematics;

namespace PiKAEngine.ColliderSystem;

public partial class ColliderWorld<T>
{
    private readonly Stack<long> _indexStack = new();

    public List<RectCollider<T>> BoxCast(RectColliderTransform transform, bool targetingInactiveCollider = false)
    {
        _indexStack.Clear();
        RayCastContactingColliders.Clear();

        var cellIndex = MortonOrder.GetIndex(new AABB(transform), WorldTransform);

        // 自分以上のレベルとの当たり判定
        _indexStack.Push(cellIndex);
        while (_indexStack.TryPop(out var currentCellIndex))
        {
            if (!_colliderCells[currentCellIndex].HasChild) continue;
            BoxCastInCell(currentCellIndex, transform, targetingInactiveCollider);

            // 上限までいったならさらに深い部分まで行かずに次に行く
            var nextRootIndex = currentCellIndex * 4 + 1;
            if (_colliderCells.Length <= nextRootIndex) continue;
            for (var i = 0; i < 4; i++)
                _indexStack.Push(nextRootIndex + i);
        }

        // 自分よりも低いレベルとの当たり判定
        for (var i = (cellIndex - 1) / 4;; i = (i - 1) / 4)
        {
            BoxCastInCell(i, transform, targetingInactiveCollider);

            if (i == 0) break;
        }

        return RayCastContactingColliders;
    }

    private void BoxCastInCell(long cellIndex, RectColliderTransform transform, bool targetingInactiveCollider)
    {
        var cell = _colliderCells[cellIndex];
        if (cell.Colliders is null) return;
        foreach (var collider in cell.Colliders)
        {
            if (!targetingInactiveCollider && !collider.IsActive) continue;
            if (collider.Detect(transform)) RayCastContactingColliders.Add(collider);
        }
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