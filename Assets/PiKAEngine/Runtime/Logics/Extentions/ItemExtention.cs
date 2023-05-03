using PiKAEngine.Logics.Core.Items;

namespace PiKAEngine.Logics.Extentions
{
    public static class ItemExtentions
    {
        public static void Activate<T>(this T self)
            where T : Item<T>
            => self.itemManager.ActivateItem(self);

        public static void Deactivate<T>(this T self)
            where T : Item<T>
            => self.itemManager.DeactivateItem(self);
    }
}