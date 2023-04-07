using System.Linq;

namespace JuhaKurisu.PiKAEngine.Logics.Entities
{
    public class EntityContents
    {
        private EntityComponent[] components;
        private EntitySettings entitySettings;

        public EntityContents(EntityComponent[] components, EntitySettings entitySettings)
        {
            this.components = components;
            this.entitySettings = entitySettings;
        }

        public Entity GenerateEntity()
        {
            return new Entity(
                components.Select(component => component.Copy()).ToArray(),
                entitySettings
            );
        }
    }
}