using UnityEngine;
using UnityEditor;
using AnnulusGames.LucidTools.Editor;
using JuhaKurisu.PiKAEngine.Logics.TileComponents;

namespace JuhaKurisu.PiKAEngine.Editors
{
    [CustomPropertyDrawer(typeof(TestTileComponent))]
    public class TestTileComponentEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Foldout
            property.isExpanded = EditorGUI.Foldout(
                new(position.x + 10, position.y, position.width, EditorGUIUtility.singleLineHeight),
                property.isExpanded,
                GUIContent.none
            );

            // チェックボックス
            //            property.serializedObject.FindProperty("")
            TestTileComponent ttc = property.managedReferenceValue as TestTileComponent;
            ttc.isEnable = EditorGUI.Toggle(
                new(position.x + 13, position.y, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight),
                ttc.isEnable
            );

            // コンポーネント名
            EditorGUI.LabelField(
                new(position.x + 30, position.y, position.width, EditorGUIUtility.singleLineHeight),
                "TestTileComponentEditor"
            );

            position.y += EditorGUIUtility.singleLineHeight;
            if (property.isExpanded)
            {
                ttc.debugSting = EditorGUI.TextField(new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), ttc.debugSting);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded) return EditorGUIUtility.singleLineHeight * 1f;
            else return EditorGUIUtility.singleLineHeight * 2f;
        }
    }
}