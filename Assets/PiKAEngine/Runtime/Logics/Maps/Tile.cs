using System;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics.Maps
{
    public class Tile : IDisposable
    {
        public readonly Position position;
        public readonly ReadOnlyCollection<TileComponent> components;
        public IObservable<Tile> onTileChanged => onTileChangedSubject;
        private readonly Subject<Tile> onTileChangedSubject = new();

        public Tile(Position position, TileComponent[] components)
        {
            this.position = position;
            this.components = new(components);
            foreach (var component in components)
            {
                component.Initialize(this);
                component.onTileComponentChanged.Subscribe(_ => onTileChangedSubject.OnNext(this));
            }
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