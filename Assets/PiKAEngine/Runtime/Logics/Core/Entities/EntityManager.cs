using System;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public class EntityManager : IEntityManager
    {
        private readonly EntityManagerBase entityManagerBase;

        public IComponent[] baseComponents => entityManagerBase.baseComponents;
        public IObservable<IEntityManager> onUpdated => entityManagerBase.onUpdated;

        public EntityManager(params IComponent[] baseComponents)
        {
            entityManagerBase = new EntityManagerBase(baseComponents);
        }

        public void Dispose()
            => entityManagerBase.Dispose();

        public void Update()
            => entityManagerBase.Update();
    }
}