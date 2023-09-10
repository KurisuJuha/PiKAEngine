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

        // 自分よりも高いレベルとの当たり判定
        for (var i = 0; i < 4; i++) _indexStack.Push(cellIndex * 4 + 1 + i);
        while (_indexStack.TryPop(out var currentCellIndex))
        {
            BoxCastInCell(currentCellIndex, transform, targetingInactiveCollider);

            // 上限までいったならさらに深い部分まで行かずに次に行く
            if (_colliderCells.Length <= currentCellIndex) continue;
            for (var i = 0; i < 4; i++)
                _indexStack.Push(currentCellIndex * 4 + 1 + i);
        }

        // 自分と同レベルか自分よりも低いレベルとの当たり判定
        for (var i = cellIndex;; i = (i - 1) / 4)
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