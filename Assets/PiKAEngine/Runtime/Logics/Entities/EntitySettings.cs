using System;
using UnityEngine;

namespace JuhaKurisu.PiKAEngine.Logics.Entities
{
    [Serializable]
    public class EntitySettings
    {
        public EntityComponent[] baseComponents => _baseComponents;
        [SerializeField] private EntityComponent[] _baseComponents;
    }
}