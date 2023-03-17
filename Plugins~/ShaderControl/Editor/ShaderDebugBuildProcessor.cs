using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shader.Control.Editor
{
    internal class ShaderDebugBuildProcessor : IPreprocessShaders, IPostprocessBuildWithReport
    {
        private ShadersBuildInfo shadersBuildInfo;

        public static ShadersBuildInfo CheckShadersBuildStore(ShadersBuildInfo shadersBuildInfo)
        {
            if (shadersBuildInfo == null)
            {
                var filename = GetStoredDataPath();
                shadersBuildInfo = AssetDatabase.LoadAssetAtPath<ShadersBuildInfo>(filename);
                if (shadersBuildInfo != null)
                {
                    return shadersBuildInfo;
                }
            }

            // Check if scriptable object exists
            var path = GetStoredDataPath();
            if (!File.Exists(path))
            {
                var dir = Path.GetDirectoryName(path);
                Directory.CreateDirectory(dir);
                shadersBuildInfo = ScriptableObject.CreateInstance<ShadersBuildInfo>();
                AssetDatabase.CreateAsset(shadersBuildInfo, path);
                AssetDatabase.SaveAssets();
            }

            return shadersBuildInfo;
        }


        public void OnPostprocessBuild(BuildReport report)
        {
            SaveResults();
        }

        public int callbackOrder
        {
            get { return 0; }
        }

        private static string GetStoredDataPath()
        {
            // Locate shader control path
            var paths = AssetDatabase.GetAllAssetPaths();
            foreach (var t in paths)
            {
                if (t.EndsWith("/ShaderControl/Editor", StringComparison.InvariantCultureIgnoreCase))
                {
                    return t + "/Resources/BuiltShaders.asset";
                }
            }

            return null;
        }

        private void SaveResults()
        {
            SCWindow.SetEditorPrefBool("QUICK_BUILD", false);

            if (shadersBuildInfo != null)
            {
                shadersBuildInfo.creationDateTicks = DateTime.Now.Ticks;
                EditorUtility.SetDirty(shadersBuildInfo);
                var filename = GetStoredDataPath();
                if (filename == null)
                {
                    Debug.LogError("Shader Control path not found.");
                }
                else
                {
                    AssetDatabase.SaveAssets();
                }
            }

            SCWindow.issueRefresh = 0;
        }

        public void OnProcessShader(UnityEngine.Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> shaderCompilerData)
        {
            try
            {
                var skipCompilation = SCWindow.GetEditorPrefBool("QUICK_BUILD", false);

                if (shadersBuildInfo == null)
                {
                    var filename = GetStoredDataPath();
                    shadersBuildInfo = AssetDatabase.LoadAssetAtPath<ShadersBuildInfo>(filename);
                    if (shadersBuildInfo == null)
                    {
                        return;
                    }
                }

                var sb = shadersBuildInfo.GetShader(shader.name);
                if (sb == null)
                {
                    sb = new ShaderBuildInfo();
                    sb.name = shader.name;
                    sb.simpleName = SCShader.GetSimpleName(sb.name);
                    sb.type = snippet.shaderType;
                    sb.path = AssetDatabase.GetAssetPath(shader);
                    sb.isInternal = string.IsNullOrEmpty(sb.path) || !File.Exists(sb.path);
                    shadersBuildInfo.Add(sb);
                    EditorUtility.SetDirty(shadersBuildInfo);
                }
                else if (!sb.includeInBuild)
                {
                    skipCompilation = true;
                }

                var count = shaderCompilerData.Count;
                for (var i = 0; i < count; ++i)
                {
                    var ks = shaderCompilerData[i].shaderKeywordSet;
                    var shaderKeywords = ks.GetShaderKeywords();

                    // Check if variants are allowed
                    if (shaderKeywords.Length > 0 && sb.variants != null && sb.variants.Count > 0)
                    {
                        var includedVariant = false;
                        foreach (var variant in sb.variants)
                        {
                            if (variant.Same(shader, shaderKeywords))
                            {
                                includedVariant = true;
                                break;
                            }
                        }

                        if (!includedVariant)
                        {
                            shaderCompilerData.RemoveAt(i);
                            count--;
                            i--;
                            continue; // for
                        }
                    }

                    // Check if keywords are allowed
                    foreach (var kw in shaderKeywords)
                    {
#if UNITY_2019_3_OR_NEWER
                        var kname = ShaderKeyword.GetKeywordName(shader, kw);
#elif UNITY_2018_4_OR_NEWER
                        var kname = kw.GetKeywordName();
#else
                        var kname = kw.GetName();
#endif
                        if (string.IsNullOrEmpty(kname))
                        {
                            continue;
                        }

                        if (!sb.KeywordsIsIncluded(kname))
                        {
                            shaderCompilerData.RemoveAt(i);
                            count--;
                            i--;
                            break;
                        }

                        EditorUtility.SetDirty(shadersBuildInfo);
                    }
                }

                if (skipCompilation) shaderCompilerData.Clear();
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Shader Control detected an error during compilation of one shader: " + ex);
            }
        }
    }
}