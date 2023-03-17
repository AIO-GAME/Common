using UnityEditor;
using UnityEngine;

namespace AIO.System.Calendar.Editor
{
    [CustomEditor(typeof(ColorPalette))]
    [CanEditMultipleObjects]
    public class ColorPaletteEditor : UnityEditor.Editor
    {
        private ColorPalette _target;

        SerializedProperty light_color;
        SerializedProperty dark_color;
        SerializedProperty white_color;
        SerializedProperty alt_color_1;
        SerializedProperty alt_color_2;
        SerializedProperty alt_color_3;
        SerializedProperty alt_color_4;
        SerializedProperty alt_color_5;
        SerializedProperty alt_color_6;

        private void Awake()
        {
            TagHelper.AddTag("light_color");
            TagHelper.AddTag("dark_color");
            TagHelper.AddTag("white_color");
            TagHelper.AddTag("alt_color_1");
            TagHelper.AddTag("alt_color_2");
            TagHelper.AddTag("alt_color_3");
            TagHelper.AddTag("alt_color_4");
            TagHelper.AddTag("alt_color_5");
            TagHelper.AddTag("alt_color_6");
        }

        void OnEnable()
        {
            _target = (ColorPalette)target;

            light_color = serializedObject.FindProperty("light_color");
            dark_color = serializedObject.FindProperty("dark_color");
            white_color = serializedObject.FindProperty("white_color");
            alt_color_1 = serializedObject.FindProperty("alt_color_1");
            alt_color_2 = serializedObject.FindProperty("alt_color_2");
            alt_color_3 = serializedObject.FindProperty("alt_color_3");
            alt_color_4 = serializedObject.FindProperty("alt_color_4");
            alt_color_5 = serializedObject.FindProperty("alt_color_5");
            alt_color_6 = serializedObject.FindProperty("alt_color_6");
        }

        /// <summary>
        /// Custom OnInspectorGUI overload for the camera controller.
        /// </summary>
        public override void OnInspectorGUI()
        {
            //---Inspector header---

            GUILayout.Label("Color Pallete");

            // Update
            serializedObject.Update();

            // Time Manager Component
            EditorGUILayout.PropertyField(light_color);
            EditorGUILayout.PropertyField(dark_color);
            EditorGUILayout.PropertyField(white_color);
            EditorGUILayout.PropertyField(alt_color_1);
            EditorGUILayout.PropertyField(alt_color_2);
            EditorGUILayout.PropertyField(alt_color_3);
            EditorGUILayout.PropertyField(alt_color_4);
            EditorGUILayout.PropertyField(alt_color_5);
            EditorGUILayout.PropertyField(alt_color_6);

            if (serializedObject.hasModifiedProperties)
            {
                _target.UpdateUIPallette();
            }

            // Apply Properties
            serializedObject.ApplyModifiedProperties();
        }
    }
}