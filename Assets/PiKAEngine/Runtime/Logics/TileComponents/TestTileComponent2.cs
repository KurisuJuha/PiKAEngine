using System;
using UnityEngine;

namespace JuhaKurisu.PiKAEngine.Logics.TileComponents
{
    [Serializable]
    public class TestTileComponent2 : TileComponent
    {
        [SerializeField] private int testFloat;

        public override TileComponent Copy() => new TestTileComponent2();

        public override void Update() { }
    }
}