/*
Shader Control - (C) Copyright 2016-2019 Ramiro Oliva (Kronnect)
*/

using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Shader.Control.Editor
{
    public enum BuildViewSortType
    {
        ShaderName = 0,
        ShaderKeywordCount = 1,
        Keyword = 2
    }

    public partial class SCWindow : EditorWindow
    {
        private string buildShaderNameFilter;
        private StringBuilder sb = new StringBuilder();

        private void DrawBuildGUI()
        {
            GUILayout.Box(
                new GUIContent(
                    "This tab shows all shaders compiled in your last build.\nHere you can exclude any number of shaders or keywords from future compilations. No file is modified, only excluded from the build.\nIf you have exceeded the maximum allowed keywords in your project, use the <b>Project View</b> tab to remove shaders or disable any unwanted keyword from the project."),
                titleStyle, GUILayout.ExpandWidth(true));
            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Quick Build", "Forces a quick compilation to extract all shaders and keywords included in the build.")))
            {
                EditorUtility.DisplayDialog("Ready to analyze!",
                    "Now make a build as normal (select 'File -> Build Settings -> Build').\n\nShader Control will detect the shaders and keywords from the build process and list that information here.\n\nImportant Note!\nTo make this special build faster, shaders won't be compiled (they will show in pink in the build). This is normal. To create a normal build, just build the project again without clicking 'Quick Build'.",
                    "Ok");
                SetEditorPrefBool("QUICK_BUILD", true);
                nextQuickBuild = true;
                ClearBuildData();
            }

            if (GUILayout.Button("Help", GUILayout.Width(40)))
            {
                ShowHelpWindowBuildView();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            if (nextQuickBuild)
            {
                EditorGUILayout.HelpBox("Shader Control is ready to collect data during the next build.", MessageType.Info);
            }

            var shadersCount = shadersBuildInfo == null || shadersBuildInfo.shaders == null ? 0 : shadersBuildInfo.shaders.Count;

            if (shadersBuildInfo != null)
            {
                if (!nextQuickBuild)
                {
                    EditorGUILayout.LabelField(
                        "Last build: " + ((shadersBuildInfo.creationDateTicks != 0) ? shadersBuildInfo.creationDateString : "no data yet. Click 'Quick Build' for more details."),
                        EditorStyles.boldLabel);
                }

                if (shadersBuildInfo.requiresBuild)
                {
                    EditorGUILayout.HelpBox("Project shaders have been modified. Do a 'Quick Build' again to ensure the data shown in this tab is accurate.", MessageType.Warning);
                }
            }

            if (shadersCount == 0) return;

            if (totalBuildShaders == 0 || totalBuildIncludedShaders == 0 || totalBuildKeywords == 0 ||
                (totalBuildKeywords == totalBuildIncludedKeywords && totalBuildShaders == totalBuildIncludedShaders))
            {
                EditorGUILayout.HelpBox(
                    "Total Compiled Shaders: " + totalBuildShaders + "  Shaders Using Keywords: " + totalBuildShadersWithKeywords + "\nTotal Unique Keywords: " + totalBuildKeywords, MessageType.Info);
            }
            else
            {
                var shadersPerc = totalBuildIncludedShaders * 100 / totalBuildShaders;
                var shadersWithKeywordsPerc = totalBuildIncludedShadersWithKeywords * 100 / totalBuildIncludedShaders;
                var keywordsPerc = totalBuildIncludedKeywords * 100 / totalBuildKeywords;
                EditorGUILayout.HelpBox(
                    "Total Compiled Shaders: " + totalBuildIncludedShaders + " of " + totalBuildShaders + " (" + shadersPerc + "%" + "  Shaders Using Keywords: " +
                    totalBuildIncludedShadersWithKeywords + " of " + totalBuildShadersWithKeywords + " (" + shadersWithKeywordsPerc + "%)\nTotal Unique Keywords: " + totalBuildIncludedKeywords +
                    " of " + totalBuildKeywords + " (" + keywordsPerc.ToString() + "%)", MessageType.Info);
            }

            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Sort By", GUILayout.Width(100));
            EditorGUI.BeginChangeCheck();
            shadersBuildInfo.sortType = (BuildViewSortType)EditorGUILayout.EnumPopup(shadersBuildInfo.sortType);
            if (EditorGUI.EndChangeCheck())
            {
                if (shadersBuildInfo != null)
                {
                    shadersBuildInfo.Resort();
                }

                EditorUtility.SetDirty(shadersBuildInfo);
                GUIUtility.ExitGUI();
                return;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Shader Name", GUILayout.Width(100));
            buildShaderNameFilter = EditorGUILayout.TextField(buildShaderNameFilter);
            if (GUILayout.Button(new GUIContent("Clear", "Clear filter."), EditorStyles.miniButton, GUILayout.Width(60)))
            {
                buildShaderNameFilter = "";
                GUIUtility.keyboardControl = 0;
            }

            EditorGUILayout.EndHorizontal();

            if (shadersBuildInfo.sortType != BuildViewSortType.Keyword)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Keywords >=", GUILayout.Width(100));
                minimumKeywordCount = EditorGUILayout.IntSlider(minimumKeywordCount, 0, maxBuildKeywordsCountFound);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Keyword Filter", GUILayout.Width(100));
            keywordFilter = EditorGUILayout.TextField(keywordFilter);
            if (GUILayout.Button(new GUIContent("Clear", "Clear filter."), EditorStyles.miniButton, GUILayout.Width(60)))
            {
                keywordFilter = "";
                GUIUtility.keyboardControl = 0;
            }

            EditorGUILayout.EndHorizontal();

            if (shadersBuildInfo.sortType != BuildViewSortType.Keyword)
            {
                shadersBuildInfo.hideReadOnlyShaders = EditorGUILayout.ToggleLeft("Hide read-only/internal shaders", shadersBuildInfo.hideReadOnlyShaders);
            }

            EditorGUILayout.Separator();

            scrollViewPosProject = EditorGUILayout.BeginScrollView(scrollViewPosProject);

            var requireUpdate = false;
            var needsTitle = true;

            if (shadersBuildInfo.sortType == BuildViewSortType.Keyword)
            {
                if (buildKeywordView != null)
                {
                    var kvCount = buildKeywordView.Count;
                    for (var s = 0; s < kvCount; s++)
                    {
                        var kwv = buildKeywordView[s];
                        var keyword = kwv.keyword;
                        if (!string.IsNullOrEmpty(keywordFilter) && keyword.IndexOf(keywordFilter, StringComparison.InvariantCultureIgnoreCase) < 0) continue;


                        if (needsTitle)
                        {
                            needsTitle = false;
                            GUILayout.Label("Used keywords:");
                        }

                        var kvShadersCount = kwv.shaders.Count;

                        sb.Length = 0;
                        sb.Append("Keyword #");
                        sb.Append(s + 1);
                        sb.Append(" <b>");
                        sb.Append(kwv.keyword);
                        sb.Append("</b> found in ");
                        sb.Append(kvShadersCount);
                        sb.Append(" shader(s)");
                        EditorGUILayout.BeginHorizontal();
                        kwv.foldout = EditorGUILayout.Foldout(kwv.foldout, new GUIContent(sb.ToString()), foldoutRTF);

                        if (!kwv.isInternal && GUILayout.Button("Show In Project View", EditorStyles.miniButton, GUILayout.Width(160)))
                        {
                            sortType = SortType.EnabledKeywordsCount;
                            projectShaderNameFilter = "";
                            keywordFilter = kwv.keyword;
                            keywordScopeFilter = KeywordScopeFilter.Any;
                            pragmaTypeFilter = PragmaTypeFilter.Any;
                            scanAllShaders = true;
                            if (shaders == null) ScanProject();
                            viewMode = ViewMode.Project;
                            GUIUtility.ExitGUI();
                        }

                        EditorGUILayout.EndHorizontal();
                        if (kwv.foldout)
                        {
                            for (var m = 0; m < kvShadersCount; m++)
                            {
                                var shader = kwv.shaders[m];
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("", GUILayout.Width(30));
                                EditorGUILayout.LabelField(shaderIcon, GUILayout.Width(18));
                                EditorGUILayout.LabelField(shader.name);
                                if (shader.isInternal)
                                {
                                    GUILayout.Label("(Internal Shader)");
                                }
                                else
                                {
                                    if (GUILayout.Button("Locate", EditorStyles.miniButton, GUILayout.Width(80)))
                                    {
                                        PingShader(shader.name);
                                    }

                                    if (GUILayout.Button("Show In Project View", EditorStyles.miniButton, GUILayout.Width(160)))
                                    {
                                        projectShaderNameFilter = shader.simpleName;
                                        keywordFilter = "";
                                        keywordScopeFilter = KeywordScopeFilter.Any;
                                        pragmaTypeFilter = PragmaTypeFilter.Any;
                                        scanAllShaders = true;
                                        PingShader(shader.name);
                                        if (shaders == null) ScanProject();
                                        viewMode = ViewMode.Project;
                                        GUIUtility.ExitGUI();
                                    }
                                }

                                EditorGUILayout.EndHorizontal();
                            }
                        }
                    }
                }
            }
            else
            {
                for (var k = 0; k < shadersCount; k++)
                {
                    var shader = shadersBuildInfo.shaders[k];

                    var kwCount = shader.keywords?.Count ?? 0;
                    if (kwCount < minimumKeywordCount && minimumKeywordCount > 0) continue;

                    if ((shader.isReadOnly || shader.isInternal) && shadersBuildInfo.hideReadOnlyShaders) continue;
                    if (!string.IsNullOrEmpty(keywordFilter) && !shader.ContainsKeyword(keywordFilter, false))
                        continue;
                    if (!string.IsNullOrEmpty(buildShaderNameFilter) && shader.name.IndexOf(buildShaderNameFilter, StringComparison.InvariantCultureIgnoreCase) < 0) continue;

                    if (needsTitle)
                    {
                        needsTitle = false;
                        GUILayout.Label("Compiled shaders:");
                    }

                    GUI.enabled = shader.includeInBuild;
                    EditorGUILayout.BeginHorizontal();
                    string shaderName = shader.name;
                    if (shader.isInternal) shaderName += " (internal, ";
                    else if (shader.isReadOnly) shaderName += " (read-only, ";
                    else shaderName += " (";

                    shader.isExpanded = EditorGUILayout.Foldout(shader.isExpanded, shaderName + kwCount + " keyword" + (kwCount != 1 ? "s)" : ")"), shader.isInternal ? foldoutDim : foldoutNormal);
                    GUILayout.FlexibleSpace();
                    GUI.enabled = true;
                    if (shader.name != "Standard")
                    {
                        EditorGUI.BeginChangeCheck();
                        shader.includeInBuild = EditorGUILayout.ToggleLeft("Include", shader.includeInBuild, GUILayout.Width(90));
                        if (EditorGUI.EndChangeCheck())
                        {
                            requireUpdate = true;
                        }
                    }

                    EditorGUILayout.EndHorizontal();
                    if (shader.isExpanded)
                    {
                        GUI.enabled = shader.includeInBuild;
                        EditorGUI.indentLevel++;
                        if (kwCount == 0)
                        {
                            EditorGUILayout.LabelField("No keywords.");
                        }
                        else
                        {
                            if (!shader.isInternal)
                            {
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("", GUILayout.Width(15));
                                if (GUILayout.Button("Locate", EditorStyles.miniButton, GUILayout.Width(80)))
                                {
                                    PingShader(shader.name);
                                }

                                if (!shader.isInternal && GUILayout.Button("Show In Project View", EditorStyles.miniButton, GUILayout.Width(160)))
                                {
                                    projectShaderNameFilter = shader.simpleName;
                                    keywordScopeFilter = KeywordScopeFilter.Any;
                                    pragmaTypeFilter = PragmaTypeFilter.Any;
                                    scanAllShaders = true;
                                    PingShader(shader.name);
                                    if (shaders == null) ScanProject();
                                    viewMode = ViewMode.Project;
                                    GUIUtility.ExitGUI();
                                }

                                EditorGUILayout.EndHorizontal();
                            }

                            for (int j = 0; j < kwCount; j++)
                            {
                                KeywordBuildSettings kw = shader.keywords[j];
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField(kw.keyword);
                                GUILayout.FlexibleSpace();
                                EditorGUI.BeginChangeCheck();
                                kw.includeInBuild = EditorGUILayout.ToggleLeft("Include", kw.includeInBuild, GUILayout.Width(90));
                                if (EditorGUI.EndChangeCheck())
                                {
                                    requireUpdate = true;
                                }

                                EditorGUILayout.EndHorizontal();
                            }
                        }

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(20);
                        if (GUILayout.Button("Advanced...", GUILayout.Width(120)))
                        {
                            SCWindowAdvanced.ShowWindow(shader);
                        }

                        var variantsCount = shader.variants?.Count ?? 0;
                        if (variantsCount > 0)
                        {
                            GUILayout.Label("(Only building " + variantsCount + " variants)");
                        }

                        EditorGUILayout.EndHorizontal();


                        EditorGUI.indentLevel--;
                    }

                    GUI.enabled = true;
                }
            }

            EditorGUILayout.EndScrollView();

            if (requireUpdate)
            {
                RefreshBuildStats(true);
                EditorUtility.SetDirty(shadersBuildInfo);
                AssetDatabase.SaveAssets();
            }
        }
    }
}