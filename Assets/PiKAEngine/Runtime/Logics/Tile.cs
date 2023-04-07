using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;

namespace JuhaKurisu.PiKAEngine.Logics
{
    public class Tile : IDisposable
    {
        public string id { get; private set; }
        public Position position;
        public ReadOnlyCollection<TileComponent> components => new(_components);
        public IObservable<Tile> onTileChanged => onTileChangedSubject;
        private readonly Subject<Tile> onTileChangedSubject = new();
        private List<TileComponent> _components = new();

        public Tile(string id, Position position, TileComponent[] components)
        {
            this.id = id;
            this.position = position;
            this._components = components.ToList();
            foreach (var component in _components)
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