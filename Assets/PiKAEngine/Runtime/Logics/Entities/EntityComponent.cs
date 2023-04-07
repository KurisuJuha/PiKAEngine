using System;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Entities
{
    [Serializable]
    public abstract class EntityComponent : IDisposable
    {
        public IObservable<EntityComponent> onEntityComponentChanged => onEntityComponentChangedSubject;
        private readonly Subject<EntityComponent> onEntityComponentChangedSubject = new();
        private Entity entity;

        public void Initialize(Entity entity)
        {
            if (this.entity != null) return;
            this.entity = entity;
        }

        public abstract void Update();
        public abstract EntityComponent Copy();

        public void Dispose()
        {
            onEntityComponentChangedSubject.Dispose();
        }
    }
}