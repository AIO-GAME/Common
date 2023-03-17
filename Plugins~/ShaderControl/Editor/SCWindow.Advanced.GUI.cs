/*
Shader Control - (C) Copyright 2016-2020 Ramiro Oliva (Kronnect)
*/

using UnityEngine;
using UnityEditor;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;

namespace Shader.Control.Editor
{
    public class SCWindowAdvanced : EditorWindow
    {
        private ShadersBuildInfo shadersBuildInfo;
        private Vector2 scrollViewPosProject;
        private GUIStyle titleStyle;
        private StringBuilder sb = new StringBuilder();
        public ShaderBuildInfo shader;

        public static void ShowWindow(ShaderBuildInfo shader)
        {
            var window = GetWindow<SCWindowAdvanced>(true, "Advanced Build Options", true);
            window.shader = shader;
        }


        private void OnGUI()
        {
            if (titleStyle == null)
            {
                titleStyle = new GUIStyle(GUI.skin.box);
                titleStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
                titleStyle.richText = true;
                titleStyle.alignment = TextAnchor.MiddleLeft;
            }

            if (shadersBuildInfo == null)
            {
                shadersBuildInfo = ShaderDebugBuildProcessor.CheckShadersBuildStore(shadersBuildInfo);
            }

            DrawAdvancedGUI();
        }

        private void DrawAdvancedGUI()
        {
            GUILayout.Box(new GUIContent("This window let you specify which shader variants are allowed during build."), titleStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.Separator();

            if (shader == null)
            {
                Close();
                return;
            }

            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Shader:", shader.name);
            EditorGUILayout.Separator();

            var kwCount = shader.keywords?.Count ?? 0;
            if (kwCount == 0)
            {
                EditorGUILayout.LabelField("No keywords.");
                return;
            }

            GUILayout.Label("Select a keyword set:");
            EditorGUI.indentLevel++;
            for (var j = 0; j < kwCount; j++)
            {
                var kw = shader.keywords[j];
                EditorGUILayout.BeginHorizontal();
                kw.includeInVariant = EditorGUILayout.ToggleLeft(kw.keyword, kw.includeInVariant);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Variant", GUILayout.Width(140)))
            {
                AddVariant(shader);
                GUIUtility.ExitGUI();
            }

            if (GUILayout.Button("Add Permutations", GUILayout.Width(140)))
            {
                AddPermutations(shader);
                GUIUtility.ExitGUI();
            }

            EditorGUILayout.EndHorizontal();
            GUI.enabled = shader.variants.Count > 0;
            if (GUILayout.Button("Create Shader Variant Collection Asset", GUILayout.Width(280)))
            {
                CreateShaderVariantCollection(shader);
                GUIUtility.ExitGUI();
            }

            GUI.enabled = true;

            EditorGUILayout.Separator();

            // Show current variants
            if (shader.variants != null)
            {
                var keywordSetsCount = shader.variants.Count;
                if (keywordSetsCount > 0)
                {
                    GUILayout.Label("Current allowed keywords combinations:");
                }

                scrollViewPosProject = EditorGUILayout.BeginScrollView(scrollViewPosProject);

                for (var k = 0; k < keywordSetsCount; k++)
                {
                    EditorGUILayout.Separator();
                    var keywordSet = shader.variants[k];
                    sb.Length = 0;
                    sb.Append("Variant ");
                    sb.Append(k + 1);
                    sb.Append(": ");
                    foreach (var keyword in keywordSet.keywords)
                    {
                        sb.Append(keyword);
                        sb.Append(" ");
                    }

                    GUILayout.Label(sb.ToString());
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    if (GUILayout.Button("Remove", GUILayout.Width(100)))
                    {
                        shader.variants.RemoveAt(k);
                        RefreshShadersBuildInfo();
                        GUIUtility.ExitGUI();
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndScrollView();
            }
        }

        private void AddVariant(ShaderBuildInfo shader)
        {
            var kwCount = shader.keywords.Count;
            var ks = new KeywordSet();
            for (var j = 0; j < kwCount; j++)
            {
                if (shader.keywords[j].includeInVariant) ks.keywords.Add(shader.keywords[j].keyword);
            }

            AddKeywordSet(shader, ks);
        }

        private void AddPermutations(ShaderBuildInfo shader)
        {
            var kwCount = shader.keywords.Count;
            var all = new List<string>();
            for (var j = 0; j < kwCount; j++)
            {
                if (shader.keywords[j].includeInVariant) all.Add(shader.keywords[j].keyword);
            }

            foreach (var variant in StringPerm.GetCombinations(all))
            {
                var ks = new KeywordSet();
                foreach (var keyword in variant)
                {
                    ks.keywords.Add(keyword);
                }

                AddKeywordSet(shader, ks);
            }
        }

        private void AddKeywordSet(ShaderBuildInfo shader, KeywordSet ks)
        {
            // check uniqueness
            var repeated = shader.variants.Any(existingVariant => ks.keywords.All(existingVariant.keywords.Contains) && ks.keywords.Count == existingVariant.keywords.Count);

            if (!repeated)
            {
                shader.variants.Add(ks);
                RefreshShadersBuildInfo();
            }
        }


        private void CreateShaderVariantCollection(ShaderBuildInfo shader)
        {
            var unityShader = UnityEngine.Shader.Find(shader.name);
            if (unityShader == null)
            {
                Debug.LogError("Shader not found! " + shader.name);
                return;
            }

            var svc = new ShaderVariantCollection();
            foreach (var variant in shader.variants)
            {
                var sv = new ShaderVariantCollection.ShaderVariant();
                sv.shader = unityShader;
                sv.passType = UnityEngine.Rendering.PassType.Normal;
                sv.keywords = variant.keywords.ToArray();
                svc.Add(sv);
            }

            AssetDatabase.CreateAsset(svc, "Assets/ShaderVariantCollection.asset");
            AssetDatabase.Refresh();
            Selection.activeObject = svc;
            EditorGUIUtility.PingObject(svc);
        }

        private void RefreshShadersBuildInfo()
        {
            EditorUtility.SetDirty(shadersBuildInfo);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            InternalEditorUtility.RepaintAllViews();
        }
    }
}