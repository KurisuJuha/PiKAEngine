using System;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Maps
{
    [Serializable]
    public abstract class TileComponent : IDisposable
    {
        public IObservable<TileComponent> onTileComponentChanged => onTileComponentChangedSubject;
        private readonly Subject<TileComponent> onTileComponentChangedSubject = new();
        protected Tile tile { get; private set; }
        private IDisposable componentUpdateDisposable;
        private IDisposable componentStartDisposable;

        public void Initialize(Tile tile)
        {
            if (this.tile != null) return;
            this.tile = tile;
            componentStartDisposable = tile.onStart.Subscribe(_ =>
            {
                ComponentStart();
                componentStartDisposable.Dispose();
            });
        }

        public void SubscribeComponentUpdate()
        {
            componentUpdateDisposable = tile.onUpdate.Subscribe(_ => ComponentUpdate());
        }

        public void UnsubscribeComponentUpdate()
        {
            componentUpdateDisposable?.Dispose();
        }

        protected virtual void ComponentStart() { }
        protected virtual void ComponentUpdate() { }
        public abstract TileComponent Copy();

        public void Dispose()
        {
            onTileComponentChangedSubject.Dispose();
            componentUpdateDisposable?.Dispose();
            componentStartDisposable?.Dispose();
        }
    }
}