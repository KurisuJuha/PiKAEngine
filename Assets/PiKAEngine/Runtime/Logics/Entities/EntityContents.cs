using System.Linq;

namespace JuhaKurisu.PiKAEngine.Logics.Entities
{
    public class EntityContents
    {
        private EntityComponent[] components;
        private EntityManager entityManager;

        public EntityContents(EntityComponent[] components, EntityManager entityManager)
        {
            this.components = components;
            this.entityManager = entityManager;
        }

        public Entity GenerateEntity()
        {
            return new Entity(
                entityManager,
                components.Select(component => component.Copy()).ToArray()
            );
        }
    }
}