using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JuhaKurisu.PiKAEngine.Logics
{
    public class Tile
    {
        public string id { get; private set; }
        public Position position;
        public ReadOnlyCollection<TileComponent> components => new(_components);
        private List<TileComponent> _components = new();

        public Tile(string id, Position position, TileComponent[] components)
        {
            this.id = id;
            this.position = position;
            this._components = components.ToList();
            foreach (var component in _components) component.Initialize(this);
        }
    }
}