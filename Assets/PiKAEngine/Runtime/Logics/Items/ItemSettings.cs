using System;
using UnityEngine;

namespace JuhaKurisu.PiKAEngine.Logics.Items
{
    [Serializable]
    public class ItemSettings
    {
        public ItemComponent[] baseComponents => _baseComponents;
        [SerializeField] private ItemComponent[] _baseComponents;
    }
}