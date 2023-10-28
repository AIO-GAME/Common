using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AIO.RainbowCore;
using AIO.RainbowFolders.Settings;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace AIO.RainbowFolders
{
    internal static class ProjectWindowAdapter
    {
        private const string EDITOR_WINDOW_TYPE = "UnityEditor.ProjectBrowser";

        private const double EDITOR_WINDOWS_CACHE_TTL = 2.0;

        private const BindingFlags STATIC_PRIVATE = BindingFlags.Static | BindingFlags.NonPublic;

        private const BindingFlags INSTANCE_PRIVATE = BindingFlags.Instance | BindingFlags.NonPublic;

        private const BindingFlags INSTANCE_PUBLIC = BindingFlags.Instance | BindingFlags.Public;

        private static readonly MethodInfo INST_ID_BY_PATH_METHOD;

        private static readonly FieldInfo PROJECT_FOLDER_TREE_FIELD;

        private static readonly FieldInfo PROJECT_ASSET_TREE_FIELD;

        private static readonly PropertyInfo TREE_VIEW_DATA_PROPERTY;

        private static readonly MethodInfo TWO_COLUMN_ITEMS_METHOD;

        private static readonly MethodInfo ONE_COLUMN_ITEMS_METHOD;

        private static readonly FieldInfo PROJECT_OBJECT_LIST_FIELD;

        private static readonly FieldInfo PROJECT_LOCAL_ASSETS_FIELD;

        private static readonly PropertyInfo ASSETS_LIST_MODE_PROPERTY;

        private static readonly FieldInfo LIST_FILTERED_HIERARCHY_FIELD;

        private static readonly PropertyInfo FILTERED_HIERARCHY_RESULTS_METHOD;

        private static readonly FieldInfo FILTER_RESULT_ID_FIELD;

        private static readonly PropertyInfo FILTER_RESULT_ICON_PROPERTY;

        private static readonly object[] ASSET_PATH_PARAM;

        private static double _nextWindowsUpdate;

        private static EditorWindow[] _windowsCache;

        static ProjectWindowAdapter()
        {
            ASSET_PATH_PARAM = new object[1];
            var assembly = Assembly.GetAssembly(typeof(EditorWindow));
            INST_ID_BY_PATH_METHOD = assembly.GetType("UnityEditor.AssetDatabase").GetMethod("GetMainAssetInstanceID", BindingFlags.Static | BindingFlags.NonPublic);
            var type = assembly.GetType("UnityEditor.ProjectBrowser");
            PROJECT_FOLDER_TREE_FIELD = type.GetField("m_AssetTree", BindingFlags.Instance | BindingFlags.NonPublic);
            PROJECT_ASSET_TREE_FIELD = type.GetField("m_FolderTree", BindingFlags.Instance | BindingFlags.NonPublic);
            TREE_VIEW_DATA_PROPERTY = assembly.GetType("UnityEditor.IMGUI.Controls.TreeViewController").GetProperty("data", BindingFlags.Instance | BindingFlags.Public);
            TWO_COLUMN_ITEMS_METHOD = assembly.GetType("UnityEditor.ProjectBrowserColumnOneTreeViewDataSource").GetMethod("GetRows", BindingFlags.Instance | BindingFlags.Public);
            ONE_COLUMN_ITEMS_METHOD = assembly.GetType("UnityEditor.AssetsTreeViewDataSource").GetMethod("GetRows", BindingFlags.Instance | BindingFlags.Public);
            PROJECT_OBJECT_LIST_FIELD = type.GetField("m_ListArea", BindingFlags.Instance | BindingFlags.NonPublic);
            var type2 = assembly.GetType("UnityEditor.ObjectListArea");
            PROJECT_LOCAL_ASSETS_FIELD = type2.GetField("m_LocalAssets", BindingFlags.Instance | BindingFlags.NonPublic);
            var nestedType = type2.GetNestedType("LocalGroup", BindingFlags.Instance | BindingFlags.NonPublic);
            ASSETS_LIST_MODE_PROPERTY = nestedType.GetProperty("ListMode", BindingFlags.Instance | BindingFlags.Public);
            LIST_FILTERED_HIERARCHY_FIELD = nestedType.GetField("m_FilteredHierarchy", BindingFlags.Instance | BindingFlags.NonPublic);
            var type3 = assembly.GetType("UnityEditor.FilteredHierarchy");
            FILTERED_HIERARCHY_RESULTS_METHOD = type3.GetProperty("results", BindingFlags.Instance | BindingFlags.Public);
            var nestedType2 = type3.GetNestedType("FilterResult");
            FILTER_RESULT_ID_FIELD = nestedType2.GetField("instanceID", BindingFlags.Instance | BindingFlags.Public);
            FILTER_RESULT_ICON_PROPERTY = nestedType2.GetProperty("icon", BindingFlags.Instance | BindingFlags.Public);
            ProjectRuleset.OnRulesetChange = (Action)Delegate.Combine(ProjectRuleset.OnRulesetChange, new Action(ApplyDefaultIconsToAll));
        }

        public static EditorWindow GetFirstProjectWindow()
        {
            return GetAllProjectWindows().FirstOrDefault();
        }

        public static EditorWindow[] GetAllProjectWindows(bool forceUpdate = false)
        {
            if (forceUpdate || _nextWindowsUpdate < EditorApplication.timeSinceStartup)
            {
                _nextWindowsUpdate = EditorApplication.timeSinceStartup + 2.0;
                _windowsCache = CoreEditorUtility.GetAllWindowsByType("UnityEditor.ProjectBrowser").ToArray();
            }

            return _windowsCache;
        }

        public static int GetMainAssetInstanceId(string assetPath)
        {
            ASSET_PATH_PARAM[0] = assetPath;
            return (int)INST_ID_BY_PATH_METHOD.Invoke(null, ASSET_PATH_PARAM);
        }

        public static void ApplyIconByPath(int assetId, Texture2D icon, bool isIconSmall)
        {
            var allProjectWindows = GetAllProjectWindows();
            foreach (var window in allProjectWindows)
            {
                if (isIconSmall)
                {
                    var firstColumnItems = GetFirstColumnItems(window);
                    if (firstColumnItems == null) continue;
                    foreach (var item in firstColumnItems)
                    {
                        if (item != null && item.id == assetId)
                        {
                            item.icon = icon;
                            break;
                        }
                    }
                }

                var secondColumnItems = GetSecondColumnItems(window, isIconSmall);
                if (secondColumnItems == null) continue;

                foreach (var item2 in secondColumnItems)
                {
                    if (item2 != null && (int)FILTER_RESULT_ID_FIELD.GetValue(item2) == assetId)
                    {
                        SetIconForListItem(item2, icon);
                        break;
                    }
                }
            }
        }

        public static bool HasChildren(int assetId)
        {
            for (int i = 0; i < 2; i++)
            {
                if (i > 0 || _windowsCache == null)
                {
                    _windowsCache = GetAllProjectWindows(forceUpdate: true);
                }

                EditorWindow[] windowsCache = _windowsCache;
                for (int j = 0; j < windowsCache.Length; j++)
                {
                    IEnumerable<TreeViewItem> firstColumnItems = GetFirstColumnItems(windowsCache[j]);
                    if (firstColumnItems != null)
                    {
                        TreeViewItem treeViewItem = firstColumnItems.FirstOrDefault((TreeViewItem item) => item.id == assetId);
                        if (treeViewItem != null)
                        {
                            return treeViewItem.hasChildren;
                        }
                    }
                }
            }

            return false;
        }

        private static IEnumerable<TreeViewItem> GetFirstColumnItems(EditorWindow window)
        {
            var value = PROJECT_FOLDER_TREE_FIELD.GetValue(window);
            if (value != null)
            {
                var value2 = TREE_VIEW_DATA_PROPERTY.GetValue(value, null);
                return (IEnumerable<TreeViewItem>)ONE_COLUMN_ITEMS_METHOD.Invoke(value2, null);
            }

            var value3 = PROJECT_ASSET_TREE_FIELD.GetValue(window);
            if (value3 != null)
            {
                var value4 = TREE_VIEW_DATA_PROPERTY.GetValue(value3, null);
                return (IEnumerable<TreeViewItem>)TWO_COLUMN_ITEMS_METHOD.Invoke(value4, null);
            }

            return null;
        }

        private static IEnumerable<object> GetSecondColumnItems(EditorWindow window, bool onlyInListMode = false)
        {
            object value = PROJECT_OBJECT_LIST_FIELD.GetValue(window);
            if (value == null)
            {
                return null;
            }

            object value2 = PROJECT_LOCAL_ASSETS_FIELD.GetValue(value);
            if (onlyInListMode && !InListMode(value2))
            {
                return null;
            }

            object value3 = LIST_FILTERED_HIERARCHY_FIELD.GetValue(value2);
            return (IEnumerable<object>)FILTERED_HIERARCHY_RESULTS_METHOD.GetValue(value3, null);
        }

        private static void ApplyDefaultIconsToAll()
        {
            EditorWindow[] allProjectWindows = GetAllProjectWindows(forceUpdate: true);
            foreach (EditorWindow editorWindow in allProjectWindows)
            {
                IEnumerable<TreeViewItem> firstColumnItems = GetFirstColumnItems(editorWindow);
                if (firstColumnItems == null)
                {
                    continue;
                }

                foreach (TreeViewItem item in firstColumnItems)
                {
                    item.icon = null;
                }

                IEnumerable<object> secondColumnItems = GetSecondColumnItems(editorWindow);
                if (secondColumnItems == null)
                {
                    continue;
                }

                foreach (object item2 in secondColumnItems)
                {
                    SetIconForListItem(item2, null);
                }

                editorWindow.Repaint();
            }
        }

        private static bool InListMode(object localAssets)
        {
            return (bool)ASSETS_LIST_MODE_PROPERTY.GetValue(localAssets, null);
        }

        private static void SetIconForListItem(object listItem, Texture2D icon)
        {
            FILTER_RESULT_ICON_PROPERTY.SetValue(listItem, icon, null);
        }
    }
}