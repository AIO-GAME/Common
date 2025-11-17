#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AIO.UI
{
    // Custom Editor to order the variables in the Inspector similar to Image component
    [CustomEditor(typeof(PaddingIgnoringImage))] [CanEditMultipleObjects]
    internal class PaddingIgnoringImageEditor : Editor
    {
        private SerializedProperty colorProp, spriteProp, preserveAspectProp, useSpriteMeshProp;
        private GUIContent         spriteLabel;

        private void OnEnable()
        {
            colorProp          = serializedObject.FindProperty("m_Color");
            spriteProp         = serializedObject.FindProperty("m_Sprite");
            useSpriteMeshProp  = serializedObject.FindProperty("m_UseSpriteMesh");
            preserveAspectProp = serializedObject.FindProperty("m_PreserveAspect");

            spriteLabel = new GUIContent("Source Image");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(colorProp);
            EditorGUILayout.PropertyField(spriteProp, spriteLabel);

            bool spriteAssigned = spriteProp.objectReferenceValue || spriteProp.hasMultipleDifferentValues;
            if (spriteAssigned)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(useSpriteMeshProp);
                EditorGUILayout.PropertyField(preserveAspectProp);
                EditorGUI.indentLevel--;
            }

            DrawPropertiesExcluding(serializedObject, "m_Script", "m_Color", "m_Sprite", "m_PreserveAspect", "m_UseSpriteMesh", "m_OnCullStateChanged");

            serializedObject.ApplyModifiedProperties();

            if (spriteAssigned && GUILayout.Button("Set Native Size", EditorStyles.miniButton))
            {
                foreach (var ignoringImage in targets.Select(item => item as PaddingIgnoringImage).Where(image => image))
                {
                    Undo.RecordObject(ignoringImage.rectTransform, "Set Native Size");
                    ignoringImage.SetNativeSize();
                    EditorUtility.SetDirty(ignoringImage);
                }
            }
        }
    }
}
#endif