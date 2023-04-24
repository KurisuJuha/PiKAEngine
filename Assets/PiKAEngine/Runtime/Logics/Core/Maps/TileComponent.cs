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

        public TileComponent()
        {
            componentBase = new(this);
        }

        public abstract TileComponent Copy();

        public void Dispose()
            => componentBase.Dispose();

        public void Initialize(Tile entity)
            => componentBase.Initialize(entity);

        public void NotifyChanged()
            => componentBase.NotifyChanged();

        public void SubscribeUpdate()
            => componentBase.SubscribeUpdate();

        public void UnsubscribeUpdate()
            => componentBase.UnsubscribeUpdate();
    }
}