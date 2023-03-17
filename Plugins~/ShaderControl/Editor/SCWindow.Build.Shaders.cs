using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shader.Control.Editor
{
    /// <summary>
    /// Shader Control - (C) Copyright 2016-2020 Ramiro Oliva (Kronnect)
    /// </summary>
    public partial class SCWindow
    {
        private class BuildKeywordView
        {
            public string keyword;
            public List<ShaderBuildInfo> shaders;
            public bool foldout, isInternal;
        }

        private ShadersBuildInfo shadersBuildInfo;
        private int totalBuildKeywords, totalBuildIncludedKeywords, totalBuildShadersWithKeywords, totalBuildShaders, totalBuildIncludedShaders, totalBuildIncludedShadersWithKeywords;
        private int maxBuildKeywordsCountFound = 0;
        private bool nextQuickBuild;
        private Dictionary<string, List<ShaderBuildInfo>> uniqueBuildKeywords, uniqueIncludedBuildKeywords;
        private List<BuildKeywordView> buildKeywordView;
        public static int issueRefresh;

        private void RefreshBuildStats(bool quick)
        {
            issueRefresh = 1;
            nextQuickBuild = GetEditorPrefBool("QUICK_BUILD", false);
            shadersBuildInfo = ShaderDebugBuildProcessor.CheckShadersBuildStore(shadersBuildInfo);
            totalBuildKeywords = totalBuildIncludedKeywords = totalBuildShadersWithKeywords = totalBuildShaders = totalBuildIncludedShaders = totalBuildIncludedShadersWithKeywords = 0;
            shadersBuildInfo = Resources.Load<ShadersBuildInfo>("BuiltShaders");
            if (shadersBuildInfo == null || shadersBuildInfo.shaders == null) return;

            if (uniqueBuildKeywords == null)
            {
                uniqueBuildKeywords = new Dictionary<string, List<ShaderBuildInfo>>();
            }
            else
            {
                uniqueBuildKeywords.Clear();
            }

            if (uniqueIncludedBuildKeywords == null)
            {
                uniqueIncludedBuildKeywords = new Dictionary<string, List<ShaderBuildInfo>>();
            }
            else
            {
                uniqueIncludedBuildKeywords.Clear();
            }

            var count = shadersBuildInfo.shaders.Count;
            totalBuildShaders = 0;
            maxBuildKeywordsCountFound = 0;

            for (var k = 0; k < count; k++)
            {
                var shader = shadersBuildInfo.shaders[k];
                var kwCount = shader.keywords?.Count ?? 0;
                totalBuildShaders++;

                // Check shaders exist
                if (!quick && UnityEngine.Shader.Find(shader.name) == null)
                {
                    shadersBuildInfo.shaders.RemoveAt(k);
                    k--;
                    totalBuildShaders--;
                    count--;
                    continue;
                }

                if (shader.includeInBuild)
                {
                    totalBuildIncludedShaders++;
                }

                shader.isReadOnly = !string.IsNullOrEmpty(shader.path) && (shader.path.Contains("Packages/com.unity") || IsFileReadonly(shader.path));

                if (kwCount > 0)
                {
                    if (kwCount > maxBuildKeywordsCountFound)
                    {
                        maxBuildKeywordsCountFound = kwCount;
                    }

                    totalBuildShadersWithKeywords++;
                    if (shader.includeInBuild)
                    {
                        totalBuildIncludedShadersWithKeywords++;
                        for (var j = 0; j < kwCount; j++)
                        {
                            if (!uniqueBuildKeywords.TryGetValue(shader.keywords[j].keyword, out var shaderList))
                            {
                                totalBuildKeywords++;
                                shaderList = new List<ShaderBuildInfo>();
                                uniqueBuildKeywords[shader.keywords[j].keyword] = shaderList;
                            }

                            shaderList.Add(shader);
                            if (shader.keywords[j].includeInBuild)
                            {
                                if (!uniqueIncludedBuildKeywords.TryGetValue(shader.keywords[j].keyword, out var includedList))
                                {
                                    totalBuildIncludedKeywords++;
                                    includedList = new List<ShaderBuildInfo>();
                                    uniqueIncludedBuildKeywords[shader.keywords[j].keyword] = includedList;
                                }

                                includedList.Add(shader);
                            }
                        }
                    }
                }
            }

            if (buildKeywordView == null)
            {
                buildKeywordView = new List<BuildKeywordView>();
            }
            else
            {
                buildKeywordView.Clear();
            }

            foreach (var kv in uniqueBuildKeywords.Select(kvp => new BuildKeywordView { keyword = kvp.Key, shaders = kvp.Value }))
            {
                buildKeywordView.Add(kv);
            }

            buildKeywordView.Sort((x, y) => y.shaders.Count.CompareTo(x.shaders.Count));
            // Annotate which keywords are used in project
            var bkwCount = buildKeywordView.Count;
            for (var k = 0; k < bkwCount; k++)
            {
                var bkv = buildKeywordView[k];
                var isInternal = true;
                var shadersCount = bkv.shaders.Count;
                for (var j = 0; j < shadersCount; j++)
                {
                    if (!bkv.shaders[j].isInternal)
                    {
                        isInternal = false;
                        break;
                    }
                }

                bkv.isInternal = isInternal;
            }

            UpdateProjectStats();
        }

        private void ClearBuildData()
        {
            shadersBuildInfo = ShaderDebugBuildProcessor.CheckShadersBuildStore(shadersBuildInfo);
            if (shadersBuildInfo != null)
            {
                shadersBuildInfo.Clear();
            }
        }

        private void BuildUpdateShaderKeywordsState(SCShader shader)
        {
            if (shader?.keywords == null) return;
            if (shadersBuildInfo == null) return;
            var shadersCount = shadersBuildInfo.shaders.Count;
            for (var s = 0; s < shadersCount; s++)
            {
                var info = shadersBuildInfo.shaders[s];
                if (info != null && info.name.Equals(shader.fullName))
                {
                    foreach (var keyword in shader.keywords)
                    {
                        info.ToggleIncludeKeyword(keyword.name, keyword.enabled);
                    }
                }
            }

            shadersBuildInfo.requiresBuild = true;
        }
    }
}