using System;
using UnityEngine;

namespace JuhaKurisu.PiKAEngine.Logics.Maps
{
    [Serializable]
    public class TileSettings
    {
        public TileComponent[] baseComponents => _baseComponents;
        [SerializeField] private TileComponent[] _baseComponents;
    }
}