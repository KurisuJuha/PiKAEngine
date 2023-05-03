using PiKAEngine.Logics.Core.Items;

namespace PiKAEngine.Logics.Extentions
{
    public static class ItemExtentions
    {
        public static void Activate(this Item self)
            => self.itemManager.ActivateItem(self);

        public static void Deactivate(this Item self)
            => self.itemManager.DeactivateItem(self);
    }
}