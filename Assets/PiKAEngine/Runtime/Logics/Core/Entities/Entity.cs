using System;

namespace PiKAEngine.Logics.Core.Entities
{
    public abstract class Entity : IDisposable
    {
        public readonly EntityManager entityManager;

        public Entity(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public abstract void Initialize();
        public abstract void Start();
        public abstract void Update();
        public abstract void Dispose();
    }
}