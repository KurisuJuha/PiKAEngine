using System;
using UnityEngine;

namespace JuhaKurisu.PiKAEngine.Logics.TileComponents
{
    [Serializable]
    public class TestTileComponent : TileComponent
    {
        [SerializeField] public string debugSting;

        public override TileComponent Copy() => new TestTileComponent();

        public override void Update()
        {
            Debug.Log(debugSting);
        }
    }
}