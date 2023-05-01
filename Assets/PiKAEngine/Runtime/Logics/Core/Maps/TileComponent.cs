using System;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Maps
{
    [Serializable]
    public abstract class TileComponent : IComponent<Tile, TileComponent>
    {
        private readonly ComponentBase<Tile, TileComponent> componentBase;
        public IObservable<TileComponent> onStarted => componentBase.onStarted;
        public IObservable<TileComponent> onUpdated => componentBase.onUpdated;
        public IObservable<TileComponent> onChanged => componentBase.onChanged;

        public Tile entity => componentBase.entity;
        public bool isActive { get; set; }

        public TileComponent()
        {
            componentBase = new(this);
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

        public abstract TileComponent Copy();

        public void Dispose()
        {
            DisposeComponent();
            componentBase.Dispose();
        }

        protected abstract void DisposeComponent();

        public void Initialize(Tile entity)
            => componentBase.Initialize(entity);

        public void NotifyChanged()
            => componentBase.NotifyChanged();
    }
}