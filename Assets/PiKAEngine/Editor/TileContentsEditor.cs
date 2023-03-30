using System;
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
            reorderableList.onAddDropdownCallback = (rect, target) =>
            {
                GenericMenu menu = new();
                foreach (var type in TypeCache.GetTypesDerivedFrom<TileComponent>())
                {
                    menu.AddItem(new GUIContent(type.Name), false, obj =>
                    {
                        var t = (Type)obj;
                        var index = reorderableList.serializedProperty.arraySize;
                        reorderableList.serializedProperty.arraySize++;
                        var elementProp = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                        elementProp.managedReferenceValue = (TileComponent)Activator.CreateInstance(type);
                        serializedObject.ApplyModifiedProperties();
                    }, type);
                }
                menu.ShowAsContext();
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}