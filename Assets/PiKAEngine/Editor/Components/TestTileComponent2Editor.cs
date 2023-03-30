using UnityEngine;
using UnityEditor;
using JuhaKurisu.PiKAEngine.Logics.TileComponents;

namespace JuhaKurisu.PiKAEngine.Editors
{
    [CustomPropertyDrawer(typeof(TestTileComponent2))]
    public class TestTileComponent2Editor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, "TestTileComponent2Editor");
        }
    }
}