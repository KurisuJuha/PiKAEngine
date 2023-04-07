using System.Linq;
using UnityEngine;
using AnnulusGames.LucidTools.Inspector;

namespace JuhaKurisu.PiKAEngine.Logics.Entities
{
    [HideMonoScript, CreateAssetMenu(menuName = "PiKAEngine/Entity", fileName = "newEntity")]
    public class EntityContents : ScriptableObject
    {
        [SerializeField, SerializeReference] private EntityComponent[] components;

        public EntityContents(EntityComponent[] components)
        {
            this.components = components;
        }

        public Entity GenerateEntity()
        {
            return new Entity(
                components.Select(component => component.Copy()).ToArray()
            );
        }
    }
}