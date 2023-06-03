namespace PiKATools.Engine.Core.Items;

public abstract class Item : IDisposable
{
    public readonly ItemManager itemManager;

    protected Item(ItemManager itemManager)
    {
        this.itemManager = itemManager;
        itemManager.AddItemOnNextFrame(this);
    }

    public abstract void Dispose();

    public abstract void Initialize();
    public abstract void Start();
    public abstract void Update();
    public abstract void OnDestory();
}