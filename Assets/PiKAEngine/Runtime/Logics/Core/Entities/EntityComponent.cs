using System;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    [Serializable]
    public abstract class EntityComponent : IDisposable
    {
        public IObservable<EntityComponent> onEntityComponentChanged => onEntityComponentChangedSubject;
        private readonly Subject<EntityComponent> onEntityComponentChangedSubject = new();
        protected Entity entity { get; private set; }
        private IDisposable componentUpdateDisposable;
        private IDisposable componentStartDisposable;

        public void Initialize(Entity entity)
        {
            if (this.entity != null) return;
            this.entity = entity;
            componentStartDisposable = entity.onStart.Subscribe(_ => ComponentStart());
        }

        public void SubscribeComponentUpdate()
        {
            componentUpdateDisposable = entity.onUpdate.Subscribe(_ => ComponentUpdate());
        }

        public void UnsubscribeComponentUpdate()
        {
            componentUpdateDisposable?.Dispose();
        }

        protected virtual void ComponentStart() { }
        protected virtual void ComponentUpdate() { }
        public abstract EntityComponent Copy();

        public void Dispose()
        {
            onEntityComponentChangedSubject.Dispose();
            componentUpdateDisposable?.Dispose();
            componentStartDisposable?.Dispose();
        }
    }
}