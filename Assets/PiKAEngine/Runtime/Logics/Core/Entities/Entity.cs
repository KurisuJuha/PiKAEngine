namespace PiKAEngine.Logics.Core.Entities
{
    public abstract class Entity<T>
        where T : Entity<T>
    {
        public readonly IEntityManager<T> entityManager;

        public Entity(IEntityManager<T> entityManager)
        {
            this.entityManager = entityManager;
        }

        public abstract void Initialize();
        public abstract void Start();
        public abstract void Update();
    }
}