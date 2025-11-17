/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using UnityEditor;

#if UNITY_EDITOR
namespace AIO.UI
{
    // Custom Editor to order the variables in the Inspector similar to Image component
    [CustomEditor(typeof(CircleGraphic)), CanEditMultipleObjects]
    public class CircleGraphicEditor : Editor
    {
        private SerializedProperty colorProp;

        private void OnEnable() { colorProp = serializedObject.FindProperty("m_Color"); }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(colorProp);
            DrawPropertiesExcluding(serializedObject, "m_Script", "m_Color", "m_OnCullStateChanged");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif