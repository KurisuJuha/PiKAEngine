using System;
using System.Linq;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Maps
{
    public class Tile : IDisposable
    {
        public readonly Position position;
        public readonly ReadOnlyCollection<TileComponent> components;
        public IObservable<Tile> onTileChanged => onTileChangedSubject;
        private readonly Subject<Tile> onTileChangedSubject = new();
        public IObservable<Tile> onUpdate => onUpdateSubject;
        private readonly Subject<Tile> onUpdateSubject = new();
        private readonly Map map;
        private IDisposable tileUpdateDisposable;

        public Tile(Position position, TileComponent[] components, Map map)
        {
            this.position = position;
            this.components = new(components.Concat(map.baseComponents).ToArray());
            this.map = map;

            foreach (var component in components)
            {
                component.Initialize(this);
                component.onTileComponentChanged.Subscribe(_ => onTileChangedSubject.OnNext(this));
            }
        }

        public void SubscribeTileUpdate()
        {
            if (tileUpdateDisposable is not null) return;
            tileUpdateDisposable = map.onUpdate.Subscribe(_ => TileUpdate());
        }

        private void TileUpdate()
        {
            onUpdateSubject.OnNext(this);
        }

        public void Dispose()
        {
            foreach (var component in components)
            {
                component.Dispose();
            }
            onTileChangedSubject.Dispose();
        }
    }
}