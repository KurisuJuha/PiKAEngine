using System;
using System.Linq;
using System.Collections.ObjectModel;
using JuhaKurisu.PopoTools.ComponentSystem;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Maps
{
    public class Tile : IEntity<Tile, TileComponent>
    {
        private readonly EntityBase<Map, Tile, TileComponent> entityBase;

        public readonly Position position;
        public ReadOnlyCollection<TileComponent> components => entityBase.components;
        public IObservable<Tile> onChanged => entityBase.onChanged;
        public IObservable<Tile> onUpdated => entityBase.onUpdated;
        public IObservable<Tile> onStarted => entityBase.onStarted;
        private Map map => entityBase.entityManager;
        private IDisposable tileUpdateDisposable;
        private IDisposable tileStartDisposable;

        public Tile(Map map, Position position, bool inheritBaseComponents = true, params TileComponent[] components)
        {
            this.position = position;
            entityBase = new(this, map, inheritBaseComponents, components);
        }

        public void Initialize()
            => entityBase.Initialize();

        public void SubscribeUpdate()
            => entityBase.SubscribeUpdate();

        public void UnsubscribeUpdate()
            => entityBase.UnsubscribeUpdate();

        public TileComponent[] GetComponents<T>()
            => entityBase.GetComponents<T>();

        public TileComponent GetComponent<T>()
            => entityBase.GetComponents<T>().First();

        public Tile Copy()
            => new(
                map,
                position,
                false,
                components.ToArray()
            );

        public void Dispose()
            => entityBase.Dispose();
    }
}