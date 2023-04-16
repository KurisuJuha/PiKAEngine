using System;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    [Serializable]
    public abstract class EntityComponent : IDisposable
    {
        public IObservable<EntityComponent> onEntityComponentChanged => onEntityComponentChangedSubject;
        private readonly Subject<EntityComponent> onEntityComponentChangedSubject = new();
        private Entity entity;
        private IDisposable componentUpdateDispose;

        public void Initialize(Entity entity)
        {
            if (this.entity != null) return;
            this.entity = entity;
        }

        public void SubscribeComponentUpdate()
        {
            componentUpdateDispose = entity.onUpdate.Subscribe(_ => ComponentUpdate());
        }

        public void UnsubscribeComponentUpdate()
        {
            componentUpdateDispose?.Dispose();
        }

        protected abstract void ComponentUpdate();
        public abstract EntityComponent Copy();

        public void Dispose()
        {
            onEntityComponentChangedSubject.Dispose();
        }
    }
}