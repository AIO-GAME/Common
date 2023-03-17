using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AIO.System.Calendar.Editor
{
    [CustomEditor(typeof(CalendarController))]
    [CanEditMultipleObjects]
    public class CalendarControllerEditor : UnityEditor.Editor
    {
        private CalendarController _target;

        SerializedProperty day_prefab;
        SerializedProperty date_header;
        SerializedProperty background_image;
        SerializedProperty time_manager;

        Texture cal_icon;

        void OnEnable()
        {
            _target = (CalendarController)target;

            day_prefab = serializedObject.FindProperty("day_prefab");
            date_header = serializedObject.FindProperty("date_header");
            background_image = serializedObject.FindProperty("background_image");
            time_manager = serializedObject.FindProperty("time_manager");
            //cal_icon = Resources.Load<Texture>("Icons/cal_icon");
        }

        /// <summary>
        /// Custom OnInspectorGUI overload for the camera controller.
        /// </summary>
        public override void OnInspectorGUI()
        {
            //---Inspector header---

            GUIStyle header_style = new GUIStyle(GUI.skin.button);
            header_style.fontSize = 24;
            header_style.fontStyle = FontStyle.Bold;

            GUILayout.Label("Calandar Controller", header_style);
            header_style.fontSize = 16;
            GUILayout.Label(_target.display_date.ToString("Y"), header_style);

            //---Properties---

            // Update
            serializedObject.Update();

            // Time Manager Component
            EditorGUILayout.PropertyField(time_manager);

            // Day Prefab
            EditorGUILayout.PropertyField(day_prefab);

            // GUI elements
            EditorGUILayout.PropertyField(date_header);
            EditorGUILayout.PropertyField(background_image);

            // Apply Properties
            serializedObject.ApplyModifiedProperties();

            //---Bottom Editor Tools---

            // Create Build calendar button
            if (GUILayout.Button("Build Calendar"))
            {
                _target.build_calendar(_target.display_date);
            }

            if (GUILayout.Button("Test Highlight Colors"))
            {
                test_highlight_colors();
            }

            //EditorGUILayout.PropertyField(lookAtPoint);
        }


        internal void test_highlight_colors()
        {
            _target = (CalendarController)target;
            DateTime d = _target.get_first_displayed_date();

            while (d < _target.get_last_displayed_date())
            {
                d = d.AddDays(Random.Range(0, 3));
                _target.highlight_date(_target.get_first_displayed_date().AddDays(Random.Range(1, 35)), _target.pallette.get_by_index(Random.Range(1, 6)));
            }

            _target.enabled = false;
            _target.enabled = true;
        }
    }
}