/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace AIO.UI
{
    // Custom Editor to order the variables in the Inspector similar to Image component
    [HelpURL("https://gist.github.com/yasirkula/391fa12bc173acdf5ac48c466f180708")]
    [CustomEditor(typeof(SlicedFilledImage)), CanEditMultipleObjects]
    public class SlicedFilledImageEditor : Editor
    {
        private SerializedProperty spriteProp, colorProp;
        private GUIContent         spriteLabel;

        private void OnEnable()
        {
            spriteProp  = serializedObject.FindProperty("m_Sprite");
            colorProp   = serializedObject.FindProperty("m_Color");
            spriteLabel = new GUIContent("Source Image");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(spriteProp, spriteLabel);
            EditorGUILayout.PropertyField(colorProp);
            DrawPropertiesExcluding(serializedObject, "m_Script", "m_Sprite", "m_Color", "m_OnCullStateChanged");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif