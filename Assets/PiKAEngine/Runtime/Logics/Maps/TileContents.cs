using System.Linq;

namespace JuhaKurisu.PiKAEngine.Logics.Maps
{
    public class TileContents
    {
        private TileComponent[] components;
        private TileSettings tileSettings;

        public TileContents(TileComponent[] components, TileSettings tileSettings)
        {
            this.components = components;
            this.tileSettings = tileSettings;
        }

        public Tile GenerateTile(Position position)
        {
            return new Tile(
                position,
                components.Select(component => component.Copy()).ToArray(),
                tileSettings
            );
        }
    }
}