using System.Runtime.CompilerServices;
using PiKAEngine.Mathematics;

namespace PiKAEngine.ColliderSystem;

public partial class ColliderWorld<T>
{
    public List<RectCollider<T>> LineCast(FixVector2 startPosition, FixVector2 endPosition,
        bool targetingInactiveCollider = false)
    {
        _indexStack.Clear();
        RayCastContactingColliders.Clear();

        var scaledStartPosition = (startPosition - WorldTransform.LeftBottomPosition) * WorldTransform.Scale;
        var scaledEndPosition = (endPosition - WorldTransform.LeftBottomPosition) * WorldTransform.Scale;

        var aabbPosition = (scaledStartPosition + scaledEndPosition) / new Fix64(2);
        var aabbSize = new FixVector2(Fix64.Abs(scaledStartPosition.X - scaledEndPosition.X),
            Fix64.Abs(scaledStartPosition.Y - scaledEndPosition.Y));

        var cellIndex = MortonOrder.GetIndex(new AABB(new RectColliderTransform(aabbPosition, aabbSize, Fix64.Zero)),
            WorldTransform);

        // 自分よりも高いレベルとの当たり判定
        for (var i = 0; i < 4; i++) _indexStack.Push(cellIndex * 4 + 1 + i);
        while (_indexStack.TryPop(out var currentCellIndex))
        {
            if (!_colliderCells[currentCellIndex].HasChild) continue;
            LineCastInCell(currentCellIndex, startPosition, endPosition, targetingInactiveCollider);

            // 上限までいったならさらに深い部分まで行かずに次に行く
            var nextRootIndex = currentCellIndex * 4 + 1;
            if (_colliderCells.Length <= nextRootIndex) continue;
            for (var i = 0; i < 4; i++)
                _indexStack.Push(nextRootIndex + i);
        }

        // 自分と同レベルか自分よりも低いレベルとの当たり判定
        for (var i = cellIndex;; i = (i - 1) / 4)
        {
            LineCastInCell(i, startPosition, endPosition, targetingInactiveCollider);
            if (i == 0) break;
        }

        return RayCastContactingColliders;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void LineCastInCell(long cellIndex, FixVector2 startPosition, FixVector2 endPosition,
        bool targetingInactiveCollider)
    {
        var cell = _colliderCells[cellIndex];
        if (cell.Colliders is null) return;
        foreach (var collider in cell.Colliders)
        {
            if (!targetingInactiveCollider && !collider.IsActive) continue;
            if (!collider.Detect(startPosition, endPosition)) continue;
            if (!collider.Detect(startPosition)) continue;
            if (!collider.Detect(endPosition)) continue;
            RayCastContactingColliders.Add(collider);
        }
    }
}