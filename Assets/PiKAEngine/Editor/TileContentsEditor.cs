using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using JuhaKurisu.PiKAEngine.Logics;
using AnnulusGames.LucidTools.Editor;

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
            reorderableList.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                SerializedProperty component = components.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, component);
            };
            reorderableList.drawElementBackgroundCallback += (rect, index, isActive, isFocused) => { };
            reorderableList.elementHeightCallback += (index) => EditorGUI.GetPropertyHeight(components.GetArrayElementAtIndex(index));
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

        private void OnAddComponent()
        {

        }
    }
}