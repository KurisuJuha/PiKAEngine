using System;

namespace PiKAEngine.Logics.Core.Entities
{
    public interface IEntityManager : IDisposable
    {
        FindType[] FindEntities<FindType>();
        bool TryFindEntity<FindType>(out FindType entity);
        void AddEntityOnNextFrame(Entity entity);
        void RemoveEntityOnNextFrame(Entity entity);
        void ActivateEntity(Entity entity);
        void DeactivateEntity(Entity entity);
    }
}