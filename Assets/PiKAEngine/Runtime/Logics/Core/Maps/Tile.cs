using System;
using System.Linq;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Maps
{
    public class Tile : IDisposable
    {
        public readonly Position position;
        public ReadOnlyCollection<TileComponent> components { get; private set; }
        public IObservable<Tile> onTileChanged => onTileChangedSubject;
        private readonly Subject<Tile> onTileChangedSubject = new();
        public IObservable<Tile> onUpdate => onUpdateSubject;
        private readonly Subject<Tile> onUpdateSubject = new();
        public IObservable<Tile> onStart => onStartSubject;
        private readonly Subject<Tile> onStartSubject = new();
        private readonly Map map;
        private IDisposable tileUpdateDisposable;
        private IDisposable tileStartDisposable;

        public Tile(Position position, TileComponent[] components, Map map)
        {
            this.position = position;
            this.components = new(components.Concat(map.baseComponents).ToArray());
            this.map = map;
        }

        private Tile(Map map)
        {
            this.map = map;
        }

        private void Initialize()
        {
            foreach (var component in this.components)
            {
                component.Initialize(this);
                component.onTileComponentChanged.Subscribe(_ => onTileChangedSubject.OnNext(this));
            }

            tileStartDisposable = map.onUpdate.Subscribe(_ =>
            {
                TileStart();
                tileStartDisposable.Dispose();
            });
        }

        public void SubscribeTileUpdate()
        {
            if (tileUpdateDisposable is not null) return;
            tileUpdateDisposable = map.onUpdate.Subscribe(_ => TileUpdate());
        }

        public void UnsubscribeTileUpdate()
        {
            tileUpdateDisposable?.Dispose();
        }

        private void TileStart()
        {
            onStartSubject.OnNext(this);
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
            tileUpdateDisposable?.Dispose();
            tileStartDisposable?.Dispose();
            onTileChangedSubject.Dispose();
        }

        public Tile Copy()
        {
            Tile copy = new Tile(map);
            copy.components = new(components.Select(component => component.Copy()).ToArray());

            copy.Initialize();
            return copy;
        }
    }
}