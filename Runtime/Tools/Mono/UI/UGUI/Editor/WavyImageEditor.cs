#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Custom Editor to order the variables in the Inspector similar to Image component

namespace AIO.UI
{
    [CustomEditor(typeof(WavyImage))] [CanEditMultipleObjects]
    internal class WavyImageEditor : Editor
    {
        private SerializedProperty colorProp, spriteProp, preserveAspectProp, waveSpeedIgnoresTimeScaleProp;
        private GUIContent         spriteLabel;

        private List<WavyImage> wavyImages;
        private bool            inspectingAsset;

        internal static bool previewInEditor;

        private void OnEnable()
        {
            Object[] _targets = targets;
            wavyImages = new List<WavyImage>(_targets.Length);
            for (int i = 0; i < _targets.Length; i++)
            {
                WavyImage wavyImage = _targets[i] as WavyImage;
                if (wavyImage)
                {
                    wavyImages.Add(wavyImage);
                    inspectingAsset |= AssetDatabase.Contains(wavyImage);
                }
            }

            colorProp                     = serializedObject.FindProperty("m_Color");
            spriteProp                    = serializedObject.FindProperty("m_Sprite");
            preserveAspectProp            = serializedObject.FindProperty("m_PreserveAspect");
            waveSpeedIgnoresTimeScaleProp = serializedObject.FindProperty("m_WaveSpeedIgnoresTimeScale");

            spriteLabel = new GUIContent("Source Image");
        }

        private void OnDisable()
        {
            if (previewInEditor)
            {
                previewInEditor = false;
                RefreshWavyImages();
            }

            EditorApplication.update -= PreviewInEditor;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(colorProp);
            EditorGUILayout.PropertyField(spriteProp, spriteLabel);
            if (spriteProp.hasMultipleDifferentValues || spriteProp.objectReferenceValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(preserveAspectProp);
                EditorGUI.indentLevel--;
            }

            DrawPropertiesExcluding(serializedObject, "m_Script", "m_Color", "m_Sprite", "m_PreserveAspect", "m_WaveSpeedIgnoresTimeScale", "m_OnCullStateChanged");

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(waveSpeedIgnoresTimeScaleProp);
            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();

            if (!EditorApplication.isPlayingOrWillChangePlaymode && !inspectingAsset)
            {
                EditorGUILayout.Space();

                EditorGUI.BeginChangeCheck();
                previewInEditor = GUILayout.Toggle(previewInEditor, "Preview In Editor", GUI.skin.button);
                if (EditorGUI.EndChangeCheck())
                {
                    if (previewInEditor)
                    {
                        EditorApplication.update -= PreviewInEditor;
                        EditorApplication.update += PreviewInEditor;
                    }
                    else
                    {
                        EditorApplication.update -= PreviewInEditor;
                        RefreshWavyImages();
                        EditorApplication.delayCall += RefreshWavyImages; // Preview is sometimes not reset immediately without this call
                    }
                }
            }
        }

        private void PreviewInEditor()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                previewInEditor          =  false;
                EditorApplication.update -= PreviewInEditor;
            }
            else
                RefreshWavyImages();
        }

        private void RefreshWavyImages() { EditorApplication.QueuePlayerLoopUpdate(); }
    }
}
#endif