using System.Buffers;
using System.Collections.ObjectModel;

namespace PiKAEngine.InventorySystem;

public sealed class InventoryGrid<T>
{
    private readonly IInventorySettings<T> _inventorySettings;
    private readonly List<T> _items;

    public InventoryGrid(IInventorySettings<T> inventorySettings, int gridMaxAmount)
    {
        _items = new List<T>();
        Items = _items.AsReadOnly();
        _inventorySettings = inventorySettings;
        GridMaxAmount = gridMaxAmount;
    }

    public int GridMaxAmount { get; }

    public ReadOnlyCollection<T> Items { get; }

    public bool IsAddableItem(T item)
    {
        // 足した場合の最大数のチェック
        if (!CheckAmount(_items.Count + 1)) return false;

        // アイテム数が0ではないなら種類の判別をする
        if (_items.Count != 0)
            // 同じアイテムの種類ではないなら許可しない
            if (!_inventorySettings.AreSameItems(_items[0], item))
                return false;

        return true;
    }

    public bool IsAddableItems(T[] items)
    {
        // 足した場合の最大数のチェック
        if (!CheckAmount(_items.Count + items.Length)) return false;

        // アイテム数が0ではないなら種類の判別をする
        if (_items.Count != 0)
        {
            // 足そうとしている全てのItemがグリッドのアイテムと同じ種類でないならfalseを返す
            var existDifferentTypeItem = false;

            foreach (var item in items)
                if (!_inventorySettings.AreSameItems(_items[0], item))
                    existDifferentTypeItem = true;

            if (existDifferentTypeItem) return false;
        }

        return true;
    }

    public bool TryAddItem(T item)
    {
        if (!IsAddableItem(item)) return false;

        _items.Add(item);

        return true;
    }

    public bool TryAddItems(T[] items)
    {
        if (!IsAddableItems(items)) return false;

        _items.AddRange(items);
        return true;
    }

    public bool IsSubtractableItems(int count)
    {
        // 引いた場合の最小数のチェック
        return CheckAmount(_items.Count - count);
    }

    public bool IsSubtractableItem()
    {
        // 引いた場合の最小数のチェック
        return CheckAmount(_items.Count - 1);
    }

    public bool TrySubtractItem(out T? item)
    {
        if (!IsSubtractableItem())
        {
            item = default;
            return false;
        }

        item = _items[^1];
        _items.RemoveAt(_items.Count - 1);

        return true;
    }

    public bool TrySubtractItems(int count, out T[] items)
    {
        if (!IsSubtractableItems(count))
        {
            items = Array.Empty<T>();
            return false;
        }

        items = new T[count];
        for (var i = count - 1; i >= 0; i--)
        {
            items[i] = _items[^1];
            _items.RemoveAt(_items.Count - 1);
        }

        return true;
    }

    public bool TryExchange(InventoryGrid<T> other)
    {
        if (!CheckAmount(other.Items.Count)) return false;
        if (!other.CheckAmount(_items.Count)) return false;

        var otherItemCount = other.Items.Count;
        var buffer = ArrayPool<T>.Shared.Rent(otherItemCount);

        // otherからbufferに移動
        for (var i = 0; i < otherItemCount; i++) buffer[i] = other.Items[i];

        // thisからotherに移動
        other._items.Clear();
        for (var i = 0; i < _items.Count; i++) other._items.Add(_items[i]);

        // bufferからthisに移動
        _items.Clear();
        for (var i = 0; i < otherItemCount; i++) _items.Add(buffer[i]);

        ArrayPool<T>.Shared.Return(buffer);
        return true;
    }

    private bool CheckAmount(int amount)
    {
        // アイテムを足した結果リストのcountがmaxAmount以上になっているなら許可しない
        if (amount >= GridMaxAmount) return false;

        // 引いて0未満なら許可しない
        if (amount < 0) return false;

        return true;
    }
}