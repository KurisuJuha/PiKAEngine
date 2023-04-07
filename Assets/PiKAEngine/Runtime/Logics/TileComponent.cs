using System;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics
{
    [Serializable]
    public abstract class TileComponent : IDisposable
    {
        public IObservable<TileComponent> onTileComponentChanged => onTileComponentChangedSubject;
        private readonly Subject<TileComponent> onTileComponentChangedSubject = new();
        private Tile tile;

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