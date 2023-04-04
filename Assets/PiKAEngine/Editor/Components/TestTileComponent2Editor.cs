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
            //            EditorGUI.LabelField(position, "TestTileComponent2Editor");
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, "aiueo");
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUIUtility.singleLineHeight * 1f;
    }
}