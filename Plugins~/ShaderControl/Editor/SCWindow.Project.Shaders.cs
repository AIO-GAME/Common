using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        private class KeywordView
        {
            public SCKeyword keyword;
            public List<SCShader> shaders;
            public bool foldout;
        }

        private const string PRAGMA_COMMENT_MARK = "// Edited by Shader Control: ";
        private const string PRAGMA_DISABLED_MARK = "// Disabled by Shader Control: ";
        private const string BACKUP_SUFFIX = "_backup";
        private const string PRAGMA_UNDERSCORE = "__ ";

        private List<SCShader> shaders;
        private Dictionary<int, SCShader> shadersDict;
        private int minimumKeywordCount;
        private int totalShaderCount;
        private int maxKeywordsCountFound = 0;
        private int totalKeywords, totalGlobalKeywords, totalVariants, totalUsedKeywords, totalBuildVariants, totalGlobalShaderFeatures, totalGlobalShaderFeaturesNonReadonly;
        private int plusBuildKeywords;
        private Dictionary<string, List<SCShader>> uniqueKeywords, uniqueEnabledKeywords;
        private Dictionary<string, SCKeyword> keywordsDict;
        private List<KeywordView> keywordView;
        private List<BuildKeywordView> keywordViewExtra;
        private StringBuilder convertToLocalLog = new StringBuilder();

        #region Shader handling

        private void ScanProject()
        {
            try
            {
                if (shaders == null)
                {
                    shaders = new List<SCShader>();
                }
                else
                {
                    shaders.Clear();
                }

                // Add shaders from Resources folder
                var guids = AssetDatabase.FindAssets("t:Shader");
                totalShaderCount = guids.Length;
                for (var k = 0; k < totalShaderCount; k++)
                {
                    var guid = guids[k];
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    if (path != null)
                    {
                        var pathUpper = path.ToUpper();
                        if (scanAllShaders || pathUpper.Contains("\\RESOURCES\\") || pathUpper.Contains("/RESOURCES/"))
                        {
                            // this shader will be included in build
                            var unityShader = AssetDatabase.LoadAssetAtPath<UnityEngine.Shader>(path);
                            if (unityShader != null)
                            {
                                var shader = new SCShader();
                                shader.fullName = unityShader.name;
                                shader.name = SCShader.GetSimpleName(shader.fullName); //  Path.GetFileNameWithoutExtension(path);
                                shader.path = path;
                                shader.isReadOnly = path.Contains("Packages/com.unity") || IsFileReadonly(path);
                                shader.GUID = unityShader.GetInstanceID();
                                ScanShader(shader);
                                if (shader.keywords.Count > 0)
                                {
                                    shaders.Add(shader);
                                }
                            }
                        }
                    }
                }

                // Load and reference materials
                if (shadersDict == null)
                {
                    shadersDict = new Dictionary<int, SCShader>(shaders.Count);
                }
                else
                {
                    shadersDict.Clear();
                }

                shaders.ForEach(shader => { shadersDict[shader.GUID] = shader; });
                var matGuids = AssetDatabase.FindAssets("t:Material");
                if (projectMaterials == null)
                {
                    projectMaterials = new List<SCMaterial>();
                }
                else
                {
                    projectMaterials.Clear();
                }

                foreach (var matGUID in matGuids)
                {
                    var matPath = AssetDatabase.GUIDToAssetPath(matGUID);
                    var mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
                    if (mat.shader == null) continue;
                    var scMat = new SCMaterial(mat, matPath, matGUID);
                    scMat.SetKeywords(mat.shaderKeywords);

                    if (mat.shaderKeywords != null && mat.shaderKeywords.Length > 0)
                    {
                        projectMaterials.Add(scMat);
                    }

                    var path = AssetDatabase.GetAssetPath(mat.shader);
                    var shaderGUID = mat.shader.GetInstanceID();
                    if (!shadersDict.TryGetValue(shaderGUID, out var shader))
                    {
                        if (mat.shaderKeywords == null || mat.shaderKeywords.Length == 0) continue;

                        var shad = AssetDatabase.LoadAssetAtPath<UnityEngine.Shader>(path);
                        // add non-sourced shader
                        shader = new SCShader();
                        shader.isReadOnly = path.Contains("Packages/com.unity") || IsFileReadonly(path);
                        shader.GUID = shaderGUID;
                        if (shad != null)
                        {
                            shader.fullName = shad.name;
                            shader.name = SCShader.GetSimpleName(shader.fullName);
                            shader.path = path;
                            ScanShader(shader);
                        }
                        else
                        {
                            shader.fullName = mat.shader.name;
                            shader.name = SCShader.GetSimpleName(shader.fullName);
                        }

                        shaders.Add(shader);
                        shadersDict[shaderGUID] = shader;
                        totalShaderCount++;
                    }

                    shader.materials.Add(scMat);
                    shader.AddKeywordsByName(mat.shaderKeywords);
                }

                // sort materials by name
                projectMaterials.Sort(CompareMaterialsName);

                // refresh variant and keywords count due to potential additional added keywords from materials (rogue keywords) and shader features count
                maxKeywordsCountFound = 0;
                shaders.ForEach(shader =>
                {
                    if (shader.keywordEnabledCount > maxKeywordsCountFound)
                    {
                        maxKeywordsCountFound = shader.keywordEnabledCount;
                    }

                    shader.UpdateVariantCount();
                });

                switch (sortType)
                {
                    case SortType.VariantsCount:
                        shaders.Sort((x, y) => y.actualBuildVariantCount.CompareTo(x.actualBuildVariantCount));
                        break;
                    case SortType.EnabledKeywordsCount:
                        shaders.Sort((x, y) => y.keywordEnabledCount.CompareTo(x.keywordEnabledCount));
                        break;
                    case SortType.ShaderFileName:
                        shaders.Sort((x, y) => string.Compare(x.name, y.name, StringComparison.CurrentCulture));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                UpdateProjectStats();
            }
            catch (Exception ex)
            {
                Debug.LogError("Unexpected exception caught while scanning project: " + ex.Message);
            }
        }

        private static int CompareMaterialsName(SCMaterial m1, SCMaterial m2)
        {
            return string.Compare(m1.unityMaterial.name, m2.unityMaterial.name, StringComparison.CurrentCulture);
        }

        private void ScanShader(SCShader shader)
        {
            // Inits shader
            shader.passes.Clear();
            shader.keywords.Clear();
            shader.hasBackup = File.Exists(shader.path + BACKUP_SUFFIX);
            shader.pendingChanges = false;
            shader.editedByShaderControl = shader.hasBackup;

            if (shader.path.EndsWith(".shadergraph"))
            {
                shader.isShaderGraph = true;
                try
                {
                    ScanShaderGraph(shader);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Couldn't analyze shader graph at " + shader.path + ". Error found: " + ex);
                }
            }
            else
            {
                try
                {
                    ScanShaderNonGraph(shader);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Couldn't analyze shader at " + shader.path + ". Error found: " + ex);
                }
            }
        }


        private void UpdateProjectStats()
        {
            totalKeywords = 0;
            totalGlobalKeywords = 0;
            totalUsedKeywords = 0;
            totalVariants = 0;
            totalBuildVariants = 0;
            totalGlobalShaderFeatures = 0;
            totalGlobalShaderFeaturesNonReadonly = 0;

            if (shaders == null)
                return;

            if (keywordsDict == null)
            {
                keywordsDict = new Dictionary<string, SCKeyword>();
            }
            else
            {
                keywordsDict.Clear();
            }

            if (uniqueKeywords == null)
            {
                uniqueKeywords = new Dictionary<string, List<SCShader>>();
            }
            else
            {
                uniqueKeywords.Clear();
            }

            if (uniqueEnabledKeywords == null)
            {
                uniqueEnabledKeywords = new Dictionary<string, List<SCShader>>();
            }
            else
            {
                uniqueEnabledKeywords.Clear();
            }

            var shadersCount = shaders.Count;
            for (var k = 0; k < shadersCount; k++)
            {
                var shader = shaders[k];
                var keywordsCount = shader.keywords.Count;
                for (var w = 0; w < keywordsCount; w++)
                {
                    var keyword = shader.keywords[w];
                    if (!uniqueKeywords.TryGetValue(keyword.name, out var shadersWithThisKeyword))
                    {
                        shadersWithThisKeyword = new List<SCShader>();
                        uniqueKeywords[keyword.name] = shadersWithThisKeyword;
                        totalKeywords++;
                        if (keyword.isGlobal) totalGlobalKeywords++;
                        if (keyword.isGlobal && !keyword.isMultiCompile && keyword.enabled)
                        {
                            totalGlobalShaderFeatures++;
                            if (!shader.isReadOnly)
                            {
                                totalGlobalShaderFeaturesNonReadonly++;
                            }
                        }

                        keywordsDict[keyword.name] = keyword;
                    }

                    shadersWithThisKeyword.Add(shader);
                    if (keyword.enabled)
                    {
                        if (!uniqueEnabledKeywords.TryGetValue(keyword.name, out var shadersWithThisKeywordEnabled))
                        {
                            shadersWithThisKeywordEnabled = new List<SCShader>();
                            uniqueEnabledKeywords[keyword.name] = shadersWithThisKeywordEnabled;
                            totalUsedKeywords++;
                        }

                        shadersWithThisKeywordEnabled.Add(shader);
                    }

                    if (!shader.isReadOnly)
                    {
                        keyword.canBeModified = true;
                        if (keyword.isGlobal && !keyword.isMultiCompile)
                        {
                            keyword.canBeConvertedToLocal = true;
                        }
                    }
                }

                totalVariants += shader.totalVariantCount;
                totalBuildVariants += shader.actualBuildVariantCount;
            }

            if (keywordView == null)
            {
                keywordView = new List<KeywordView>();
            }
            else
            {
                keywordView.Clear();
            }

            foreach (var kvp in uniqueEnabledKeywords)
            {
                if (!keywordsDict.TryGetValue(kvp.Key, out var kw)) continue;
                var kv = new KeywordView { keyword = kw, shaders = kvp.Value };
                keywordView.Add(kv);
            }

            keywordView.Sort((x, y) => y.shaders.Count.CompareTo(x.shaders.Count));

            // Compute which keywords in build are not present in project
            if (keywordViewExtra == null)
            {
                keywordViewExtra = new List<BuildKeywordView>();
            }
            else
            {
                keywordViewExtra.Clear();
            }

            plusBuildKeywords = 0;
            if (buildKeywordView != null)
            {
                int count = buildKeywordView.Count;
                for (int k = 0; k < count; k++)
                {
                    BuildKeywordView bkv = buildKeywordView[k];
                    if (!uniqueKeywords.ContainsKey(bkv.keyword))
                    {
                        keywordViewExtra.Add(bkv);
                        plusBuildKeywords++;
                    }
                }
            }
        }

        private bool IsFileReadonly(string path)
        {
            FileStream stream = null;

            try
            {
                FileAttributes fileAttributes = File.GetAttributes(path);
                if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    return true;
                }

                FileInfo file = new FileInfo(path);
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        private void MakeBackup(SCShader shader)
        {
            string backupPath = shader.path + BACKUP_SUFFIX;
            if (!File.Exists(backupPath))
            {
                AssetDatabase.CopyAsset(shader.path, backupPath);
                shader.hasBackup = true;
            }
        }

        private void UpdateShader(SCShader shader)
        {
            if (shader.isReadOnly)
            {
                EditorUtility.DisplayDialog("Locked file", "Shader file " + shader.name + " is read-only.", "Ok");
                return;
            }

            try
            {
                // Create backup
                MakeBackup(shader);

                if (shader.isShaderGraph)
                {
                    UpdateShaderGraph(shader);
                }
                else
                {
                    UpdateShaderNonGraph(shader);
                }

                // Also update materials
                CleanMaterials(shader);

                ScanShader(shader); // Rescan shader

                // do not include in build (sync with Build View)
                BuildUpdateShaderKeywordsState(shader);
            }
            catch (Exception ex)
            {
                Debug.LogError("Unexpected exception caught while updating shader: " + ex.Message);
            }
        }

        private void RestoreShader(SCShader shader)
        {
            try
            {
                string shaderBackupPath = shader.path + BACKUP_SUFFIX;
                if (!File.Exists(shaderBackupPath))
                {
                    EditorUtility.DisplayDialog("Restore shader", "Shader backup is missing!", "OK");
                    return;
                }

                File.Copy(shaderBackupPath, shader.path, true);
                File.Delete(shaderBackupPath);
                if (File.Exists(shaderBackupPath + ".meta"))
                    File.Delete(shaderBackupPath + ".meta");
                AssetDatabase.Refresh();

                ScanShader(shader); // Rescan shader
                UpdateProjectStats();
            }
            catch (Exception ex)
            {
                Debug.LogError("Unexpected exception caught while restoring shader: " + ex.Message);
            }
        }


        private void DeleteShader(SCShader shader)
        {
            try
            {
                if (File.Exists(shader.path))
                {
                    File.Delete(shader.path);
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "Shader file was not found at " + shader.path + "!?", "Weird");
                    return;
                }

                File.Delete(shader.path);
                if (File.Exists(shader.path + ".meta"))
                {
                    File.Delete(shader.path + ".meta");
                }

                AssetDatabase.Refresh();
                ScanProject();
            }
            catch (Exception ex)
            {
                Debug.LogError("Unexpected exception caught while deleting shader: " + ex.Message);
            }
        }

        private void ConvertToLocalStarted()
        {
            convertToLocalLog.Length = 0;
        }

        private void ConvertToLocalFinished()
        {
            if (convertToLocalLog.Length > 0)
            {
                EditorUtility.DisplayDialog("Convert To Local Keyword", "The operation finished with the following results:\n\n" + convertToLocalLog, "Ok");
            }
            else
            {
                EditorUtility.DisplayDialog("Convert To Local Keyword", "The operation finished successfully.", "Ok");
            }

            AssetDatabase.Refresh();
        }

        private void ConvertToLocal(SCKeyword keyword)
        {
            if (!uniqueKeywords.TryGetValue(keyword.name, out var list)) return;
            if (list == null) return;
            var builder = new StringBuilder();
            foreach (var shader in list)
            {
                if (shader.isReadOnly)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(", ");
                    }

                    builder.Append(shader.name);
                }
                else
                {
                    ConvertToLocal(keyword, shader);
                }
            }

            if (builder.Length > 0)
            {
                convertToLocalLog.AppendLine("The following shaders couldn't be modified because they're read-only: " + builder);
            }
        }

        private void ConvertToLocal(SCKeyword keyword, SCShader shader)
        {
            // Check total local keyword does not exceed 64 limit
            var potentialCount = 0;
            var kwCount = shader.keywords.Count;
            for (var k = 0; k < kwCount; k++)
            {
                if (!shader.keywords[k].isMultiCompile) potentialCount++;
            }

            if (potentialCount > 64) return;

            if (shader.isReadOnly)
            {
                convertToLocalLog.AppendLine("The keyword " + keyword.name + " can't be converted to local in shader " + shader.name + " at " + shader.path + " because file is readonly.");
                return;
            }

            if (shader.isShaderGraph)
            {
                ConvertToLocalGraph(keyword, shader);
            }
            else
            {
                ConvertToLocalNonGraph(keyword, shader);
            }
        }

        private void ConvertToLocalAll()
        {
            var kvCount = keywordView.Count;
            ConvertToLocalStarted();
            for (var s = 0; s < kvCount; s++)
            {
                var keyword = keywordView[s].keyword;
                if (keyword.isGlobal && !keyword.isMultiCompile)
                {
                    ConvertToLocal(keyword);
                }
            }

            ConvertToLocalFinished();
        }

        #endregion
    }
}