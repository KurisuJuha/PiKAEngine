using System.Linq;

namespace JuhaKurisu.PiKAEngine.Logics.Core.Maps
{
    public class TileContents
    {
        private TileComponent[] components;
        private Map map;

        public TileContents(TileComponent[] components, Map map)
        {
            this.components = components;
            this.map = map;
        }

        public Tile GenerateTile(Position position)
        {
            return new Tile(
                position,
                components.Select(component => component.Copy()).ToArray(),
                map
            );
        }
    }
}