using UnityEditor;

namespace AIO.System.Calendar.Editor
{
    [CustomEditor(typeof(AnalogClockGUI))]
    [CanEditMultipleObjects]
    public class AnalogClockEditor : UnityEditor.Editor
    {
        private AnalogClockGUI _target;

        private SerializedProperty time_manager;

        private SerializedProperty hour_hand;
        private SerializedProperty minute_hand;

        private SerializedProperty numeral_prefab;

        void OnEnable()
        {
            _target = (AnalogClockGUI)target;

            time_manager = serializedObject.FindProperty("time_manager");
            numeral_prefab = serializedObject.FindProperty("numeral_prefab");
            hour_hand = serializedObject.FindProperty("hour_hand");
            minute_hand = serializedObject.FindProperty("minute_hand");
        }

        /// <summary>
        /// Custom OnInspectorGUI overload for the camera controller.
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Update the SerializedObject.
            serializedObject.Update();

            // Analog Clock parameters.
            EditorGUILayout.PropertyField(time_manager);
            EditorGUILayout.PropertyField(numeral_prefab);
            EditorGUILayout.PropertyField(hour_hand);
            EditorGUILayout.PropertyField(minute_hand);

            // Apply Properties
            serializedObject.ApplyModifiedProperties();

            // Create build clock button
            // Only works in prefab editing mode

            //if (GUILayout.Button("Build Clock"))
            //{
            //    _target.build_clock_face();
            //}
        }
    }
}