using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Items;

[Obsolete]
public abstract class Item : IDisposable
{
    private readonly ItemManager _itemManager;

    protected Item(ItemManager itemManager)
    {
        _itemManager = itemManager;
        itemManager.AddItemOnNextFrame(this);
    }

    public Kettle Kettle => _itemManager.Kettle;

    public abstract void Dispose();

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
}