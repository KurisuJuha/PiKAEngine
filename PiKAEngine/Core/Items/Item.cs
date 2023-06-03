using PiKATools.DebugSystem;

namespace PiKATools.Engine.Core.Items;

public abstract class Item : IDisposable
{
    private readonly ItemManager itemManager;

    protected Item(ItemManager itemManager)
    {
        this.itemManager = itemManager;
        itemManager.AddItemOnNextFrame(this);
    }

    public Kettle kettle => itemManager.kettle;

    public abstract void Dispose();

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
    public abstract void OnDestory();
}