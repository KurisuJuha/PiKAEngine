namespace PiKAEngine.Core.Entities
{
    public abstract class Entity : IDisposable
    {
        public readonly EntityManager entityManager;

        protected Entity(EntityManager entityManager)
        {
            this.entityManager = entityManager;
            entityManager.AddEntityOnNextFrame(this);
        }

        public abstract void Initialize();
        public abstract void Start();
        public abstract void Update();
        public abstract void OnDestroy();

        public abstract void Dispose();
    }
}