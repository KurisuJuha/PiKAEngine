namespace PiKAEngine.Logics.Core.Entities
{
    public interface IEntityManager
    {
        void AddEntityOnNextFrame(Entity entity);
        void RemoveEntityOnNextFrame(Entity entity);
        void ActivateEntity(Entity entity);
        void DeactivateEntity(Entity entity);
    }
}