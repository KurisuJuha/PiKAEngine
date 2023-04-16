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

        public void Initialize(Tile tile)
        {
            if (this.tile != null) return;
            this.tile = tile;
        }

        public abstract void Update();
        public abstract TileComponent Copy();

        public void Dispose()
        {
            onTileComponentChangedSubject.Dispose();
        }
    }
}