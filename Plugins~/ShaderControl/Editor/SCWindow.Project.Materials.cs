using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Shader.Control.Editor
{
    /// <summary>
    /// Shader Control - (C) Copyright 2016-2020 Ramiro Oliva (Kronnect)
    /// </summary>
    /// 
    public partial class SCWindow
    {
        private List<SCMaterial> projectMaterials;

        #region Materials handling

        private void CleanMaterials(SCShader shader)
        {
            // Updates any material using this shader
            var shad = AssetDatabase.LoadAssetAtPath<UnityEngine.Shader>(shader.path);
            if (shad != null)
            {
                var requiresSave = false;
                var matGUIDs = AssetDatabase.FindAssets("t:Material");
                foreach (var matGUID in matGUIDs)
                {
                    var matPath = AssetDatabase.GUIDToAssetPath(matGUID);
                    var mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
                    if (mat != null && mat.shader.name.Equals(shad.name))
                    {
                        foreach (var keyword in shader.keywords)
                        {
                            foreach (var matKeyword in mat.shaderKeywords)
                            {
                                if (matKeyword.Equals(keyword.name))
                                {
                                    if (!keyword.enabled && mat.IsKeywordEnabled(keyword.name))
                                    {
                                        mat.DisableKeyword(keyword.name);
                                        EditorUtility.SetDirty(mat);
                                        requiresSave = true;
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }

                if (requiresSave)
                {
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }

        private void CleanAllMaterials()
        {
            if (!EditorUtility.DisplayDialog("Clean All Materials",
                    "This option will scan all materials and will prune any disabled keywords. This option is provided to ensure no materials are referencing a disabled shader keyword.\n\nRemember: to disable keywords, first expand any shader from the list and uncheck the unwanted keywords (press 'Save' to modify the shader file and to clean any existing material that uses that specific shader).\n\nDo you want to continue?",
                    "Yes", "Cancel"))
            {
                return;
            }

            try
            {
                foreach (var t in shaders)
                {
                    CleanMaterials(t);
                }

                ScanProject();
                Debug.Log("Cleaning finished.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Unexpected exception caught while cleaning materials: " + ex.Message);
            }
        }

        private void PruneMaterials(string keywordName)
        {
            try
            {
                var requiresSave = false;
                foreach (var shader in shaders)
                {
                    var materialCount = shader.materials.Count;
                    for (var k = 0; k < materialCount; k++)
                    {
                        var material = shader.materials[k];
                        if (material.ContainsKeyword(keywordName))
                        {
                            var theMaterial = AssetDatabase.LoadAssetAtPath<Material>(shader.materials[k].path);
                            if (theMaterial == null) continue;

                            theMaterial.DisableKeyword(keywordName);
                            EditorUtility.SetDirty(theMaterial);
                            material.RemoveKeyword(keywordName);
                            shader.RemoveKeyword(keywordName);
                            requiresSave = true;
                        }
                    }
                }

                if (requiresSave)
                {
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Unexpected exception caught while pruning materials: " + ex.Message);
            }
        }

        #endregion
    }
}