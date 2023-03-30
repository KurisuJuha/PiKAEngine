using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using JuhaKurisu.PiKAEngine.Logics;

namespace JuhaKurisu.PiKAEngine.Editors
{
    [CustomEditor(typeof(TileContents))]
    public class TileContentsEditor : Editor
    {
        private ReorderableList reorderableList;

        private void OnEnable()
        {
            SerializedProperty components = serializedObject.FindProperty("components");

            reorderableList = new ReorderableList(serializedObject, components)
            {
                displayAdd = true,
                displayRemove = true,
                draggable = true,
            };
            reorderableList.showDefaultBackground = false;
            reorderableList.elementHeightCallback += (index) => 50;
            reorderableList.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                SerializedProperty component = components.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, component);
            };
            reorderableList.drawElementBackgroundCallback += (rect, index, isActive, isFocused) =>
            {
                if (isFocused)
                {
                    Texture2D tex = new(1, 1);
                    tex.SetPixel(0, 0, new(1f, 0.5f, 0.5f, 0.5f));
                    tex.Apply();
                    GUI.DrawTexture(rect, tex as Texture);
                }
            };
            reorderableList.drawHeaderCallback += (rect) =>
            {
                EditorGUI.LabelField(rect, "Components");
            };
        }

        public override void OnInspectorGUI()
        {
            reorderableList.DoLayoutList();
        }
    }
}