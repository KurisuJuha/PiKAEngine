using System;
using UniRx;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Entities
{
    public abstract class EntityComponent : IComponent<Entity, EntityComponent>
    {
        private readonly ComponentBase<Entity, EntityComponent> componentBase;
        public IObservable<EntityComponent> onStarted => componentBase.onStarted;
        public IObservable<EntityComponent> onUpdated => componentBase.onUpdated;
        public IObservable<EntityComponent> onChanged => componentBase.onChanged;
        public Entity entity => componentBase.entity;
        public bool isActive { get; set; }

        public EntityComponent()
        {
            componentBase = new(this);
        }

        public abstract EntityComponent Copy();

        public void Dispose()
        {
            DisposeComponent();
            componentBase.Dispose();
        }

        public void Initialize(Entity entity)
        {
            componentBase.Initialize(entity);
            onStarted.Subscribe(_ => ComponentStart());
        }

        public void Start()
        {
            componentBase.Start();
            ComponentStart();
        }

        protected virtual void ComponentStart() { }

        public void Update()
        {
            componentBase.Update();
            ComponentUpdate();
        }

        protected virtual void ComponentUpdate() { }

        protected abstract void DisposeComponent();

        public void NotifyChanged()
            => componentBase.NotifyChanged();
    }
}