namespace PiKAEngine.Logics.Core.Entities
{
    public interface IEntityManager<T>
        where T : Entity<T>
    {
        FindType[] FindEntities<FindType>();
        bool TryFindEntity<FindType>(out FindType entity);
        void AddEntityOnNextFrame(T entity);
        void RemoveEntityOnNextFrame(T entity);
        void ActivateEntity(T entity);
        void DeactivateEntity(T entity);
    }
}