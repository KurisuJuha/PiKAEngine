using System;
using UnityEngine;

namespace JuhaKurisu.PiKAEngine.Logics.TileComponents
{
    [Serializable]
    public class TestTileComponent : TileComponent
    {
        [SerializeField] private int testFloat;

        public override TileComponent Copy() => new TestTileComponent();

        public override void Update() { }
    }
}