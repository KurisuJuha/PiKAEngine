using UnityEngine;
using System;

namespace JuhaKurisu.PiKAEngine.Logics
{
    [Serializable]
    public abstract class TileComponent
    {
        [SerializeField] public bool isEnable = true;

        private Tile tile;
        public void Initialize(Tile tile)
        {
            if (tile != null) return;
            this.tile = tile;
        }
        public abstract void Update();
        public abstract TileComponent Copy();
    }
}