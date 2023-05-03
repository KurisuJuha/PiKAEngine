using PiKAEngine.Logics.Core.Entities;

namespace PiKAEngine.Logics.Extentions
{
    public static class EntityExtentions
    {
        public static void Activate(this Entity self)
            => self.entityManager.ActivateEntity(self);

        public static void Deactivate(this Entity self)
            => self.entityManager.DeactivateEntity(self);
    }
}