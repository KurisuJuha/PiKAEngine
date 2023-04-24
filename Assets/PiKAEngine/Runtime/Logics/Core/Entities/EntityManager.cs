using System;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public class EntityManager : IEntityManager<EntityManager, Entity, EntityComponent>
    {
        private readonly EntityManagerBase<EntityManager, Entity, EntityComponent> entityManagerBase;

        public EntityComponent[] baseComponents => entityManagerBase.baseComponents;
        public IObservable<EntityManager> onUpdated => entityManagerBase.onUpdated;

        public EntityManager(params EntityComponent[] baseComponents)
        {
            entityManagerBase = new(this, baseComponents);
        }

        public void Dispose()
            => entityManagerBase.Dispose();

        public void Update()
            => entityManagerBase.Update();
    }
}