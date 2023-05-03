using PiKAEngine.Logics.Core.Entities;

namespace PiKAEngine.Logics.Extentions
{
    public static class EntityExtentions
    {
        public static void Activate<T>(this T self)
            where T : Entity<T>
            => self.entityManager.ActivateEntity(self);

        public static void Deactivate<T>(this T self)
            where T : Entity<T>
            => self.entityManager.DeactivateEntity(self);
    }
}