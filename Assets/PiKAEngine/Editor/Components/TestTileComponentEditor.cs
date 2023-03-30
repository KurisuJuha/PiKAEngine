using UnityEngine;
using UnityEditor;
using JuhaKurisu.PiKAEngine.Logics.TileComponents;

namespace JuhaKurisu.PiKAEngine.Editors
{
    [CustomPropertyDrawer(typeof(TestTileComponent))]
    public class TestTileComponentEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, "TestTileComponentEditor");
        }
    }
}