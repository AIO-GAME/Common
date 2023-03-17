using System;
using System.Collections.Generic;
using MonKey.Editor.Internal;
using MonKey.Settings.Internal;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

[CustomEditor(typeof(MonKeySettings))]
public class MonKeySettingsEditor : Editor
{
    public static readonly string defaultMonKeyInstallFolder = "Assets/Plugins/MonKey Commander/Editor";

    public static MonKeySettings Instance
    {
        get { return !instance ? InitSettings() : instance; }
    }

    private static MonKeySettings instance;

    public static MonKeySettings InitSettings()
    {
        var settingsPaths = AssetDatabase.FindAssets("t:MonKeySettings");
        if (settingsPaths.Length == 0) return CreateNewInstance();

        if (settingsPaths.Length > 1) Debug.LogWarning("MonKey Warning: More than one MonKey Settings were found: this is not allowed, please leave only one");

        instance = AssetDatabase.LoadAssetAtPath<MonKeySettings>(AssetDatabase.GUIDToAssetPath(settingsPaths[0]));

        if (!instance)
        {
            AssetDatabase.DeleteAsset(defaultMonKeyInstallFolder + "/Settings/MonKey Settings.asset");
            return CreateNewInstance();
        }

        SavePrefs();

        CommandManager.FindInstance();
        return instance;
    }

    private static MonKeySettings CreateNewInstance()
    {
        if (!AssetDatabase.IsValidFolder(defaultMonKeyInstallFolder))
            AssetDatabase.CreateFolder("Assets", "/Plugins/MonKey Commander/Editor/Settings");

        instance = CreateInstance<MonKeySettings>();

        AssetDatabase.CreateAsset(instance, defaultMonKeyInstallFolder + "/Settings/MonKey Settings.asset");
        AssetDatabase.SaveAssets();

        SavePrefs();
        return instance;
    }

    private MonKeySettings setting;

    private void OnEnable()
    {
        setting = (MonKeySettings)target;
    }

    private void ScanOnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(setting.PutInvalidCommandAtEndOfSearch ? "关闭扫描设置" : "开启扫描设置");
        EditorGUILayout.Separator();
        setting.PutInvalidCommandAtEndOfSearch = EditorGUILayout.Toggle(setting.PutInvalidCommandAtEndOfSearch);
        EditorGUILayout.EndHorizontal();

        if (!setting.PutInvalidCommandAtEndOfSearch) return;

        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));
        EditorGUILayout.PrefixLabel("扫描设置");

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("包含 MenuItem 属性");
        EditorGUILayout.Separator();
        setting.IncludeMenuItems = EditorGUILayout.Toggle(setting.IncludeMenuItems);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("只包含带有热键的菜单项");
        EditorGUILayout.Separator();
        setting.IncludeOnlyMenuItemsWithHotKeys = EditorGUILayout.Toggle(setting.IncludeOnlyMenuItemsWithHotKeys);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("仅扫描包含目标程序集");
        EditorGUILayout.Separator();
        setting.IncludeModeOnly = EditorGUILayout.Toggle(setting.IncludeModeOnly);
        EditorGUILayout.EndHorizontal();

        if (!setting.IncludeModeOnly)
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("排除的程序集");
            EditorGUILayout.Separator();
            if (GUILayout.Button("+", GUILayout.Width(17), GUILayout.Height(17))) setting.ExcludedAssemblies.Add("");
            EditorGUILayout.EndHorizontal();

            for (var i = 0; i < setting.ExcludedAssemblies.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i + 1).ToString("00"), GUILayout.Width(20));
                setting.ExcludedAssemblies[i] = EditorGUILayout.TextField(setting.ExcludedAssemblies[i]);
                if (GUILayout.Button("-", GUILayout.Width(17), GUILayout.Height(17))) setting.ExcludedAssemblies.RemoveAt(i);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            EditorGUILayout.PrefixLabel("排除的命名空间");
            setting.ExcludedNameSpaces = EditorGUILayout.TextField(setting.ExcludedNameSpaces);
            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }

    private void BasicSettingOnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(new GUIStyle("ChannelStripBg"));
        EditorGUILayout.PrefixLabel("基础设置");
        
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("窗口展示时暂停游戏");
        EditorGUILayout.Separator();
        setting.PauseGameOnConsoleOpen = EditorGUILayout.Toggle(setting.PauseGameOnConsoleOpen);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("使用分类模式");
        EditorGUILayout.Separator();
        setting.UseCategoryMode = EditorGUILayout.Toggle(setting.UseCategoryMode);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("只在选定的位置显示帮助");
        EditorGUILayout.Separator();
        setting.ShowHelpOnSelectedOnly = EditorGUILayout.Toggle(setting.ShowHelpOnSelectedOnly);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("排序选择");
        EditorGUILayout.Separator();
        setting.UseSortedSelection = EditorGUILayout.Toggle(setting.UseSortedSelection);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (!setting.UseSortedSelection)
        {
            EditorGUILayout.LabelField("显示排序警告");
            EditorGUILayout.Separator();
            setting.ShowSortedSelectionWarning = EditorGUILayout.Toggle(setting.ShowSortedSelectionWarning);
        }
        else
        {
            EditorGUILayout.PrefixLabel("排序最大值");
            setting.MaxSortedSelectionSize = EditorGUILayout.IntSlider(setting.MaxSortedSelectionSize, 50, 1000);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("快捷键 只支持单热键");
        EditorGUILayout.Separator();
        setting.MonkeyConsoleOverrideHotKey = (KeyCode)EditorGUILayout.EnumPopup(setting.MonkeyConsoleOverrideHotKey, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
        
        EditorGUILayout.Space();

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Monkey Command Settings", new GUIStyle("PreLabel"));
        EditorGUILayout.Space();

        BasicSettingOnInspectorGUI();

        EditorGUILayout.Space();

        ScanOnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("应用设置")) SavePrefs();
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private void ApplySetting()
    {
        SavePrefs();
        CompilationPipeline.RequestScriptCompilation();
    }

    public static void SavePrefs()
    {
        var internalSettings = MonKeyInternalSettings.Instance;
        if (!internalSettings) return;
        internalSettings.MaxSortedSelectionSize = Instance.MaxSortedSelectionSize;

        internalSettings.MonkeyConsoleOverrideHotKey = Instance.MonkeyConsoleOverrideHotKey.ToString();
        internalSettings.UseCategoryMode = Instance.UseCategoryMode;
        internalSettings.UseSortedSelection = Instance.UseSortedSelection;
        internalSettings.ShowSortedSelectionWarning = Instance.ShowSortedSelectionWarning;
        internalSettings.PauseGameOnConsoleOpen = Instance.PauseGameOnConsoleOpen;
        internalSettings.PutInvalidCommandAtEndOfSearch = Instance.PutInvalidCommandAtEndOfSearch;
        internalSettings.IncludeMenuItems = Instance.IncludeMenuItems;
        internalSettings.IncludeOnlyMenuItemsWithHotKeys = Instance.IncludeOnlyMenuItemsWithHotKeys;

        // 只搜索特定内容
        internalSettings.OnlyScanSpecified = Instance.IncludeModeOnly;

        if (!internalSettings.OnlyScanSpecified)
        {
            internalSettings.ExcludedNameSpaces = Instance.ExcludedNameSpaces;
            internalSettings.ExcludedAssemblies = string.Join(";", Instance.ExcludedAssemblies);
        }

        internalSettings.ForceFocusOnDocked = Instance.ForceFocusOnDocked;
        internalSettings.ShowHelpOnlyOnActiveCommand = Instance.ShowHelpOnSelectedOnly;

        internalSettings.PostSave();
    }
}

public class MonKeySettings : ScriptableObject
{
    /// <summary>
    /// 使用排序模式
    /// </summary>
    public bool UseSortedSelection = true;

    /// <summary>
    /// 使用分类模式
    /// </summary>
    public bool UseCategoryMode = true;

    /// <summary>
    /// 排序最大数量
    /// </summary>
    public int MaxSortedSelectionSize = 1000;

    /// <summary>
    /// 显示排序警告
    /// </summary>
    public bool ShowSortedSelectionWarning = true;

    /// <summary>
    /// 控制台覆盖热键
    /// </summary>
    public KeyCode MonkeyConsoleOverrideHotKey = KeyCode.BackQuote;

    /// <summary>
    /// 窗口展示时暂停游戏
    /// </summary>
    public bool PauseGameOnConsoleOpen = true;

    /// <summary>
    /// 在搜索结束时忽略无效命令
    /// </summary>
    public bool PutInvalidCommandAtEndOfSearch = false;

    /// <summary>
    /// 包含 MenuItem
    /// </summary>
    public bool IncludeMenuItems = true;

    /// <summary>
    /// 只包含带有热键的菜单项
    /// </summary>
    public bool IncludeOnlyMenuItemsWithHotKeys = false;

    /// <summary>
    /// 仅扫描包含目标程序集
    /// </summary>
    public bool IncludeModeOnly = true;

    /// <summary>
    /// 排除的命名空间
    /// </summary>
    public string ExcludedNameSpaces = "";

    /// <summary>
    /// 排除的程序集
    /// </summary>
    public List<string> ExcludedAssemblies = new List<string>();

    /// <summary>
    /// 强制对焦停靠
    /// </summary>
    public bool ForceFocusOnDocked = false;

    /// <summary>
    /// 只在选定的位置显示帮助
    /// </summary>
    public bool ShowHelpOnSelectedOnly = false;

    /// <summary>
    /// 避免关注弹出窗口
    /// </summary>
    public bool PreventFocusOnPopup = false;

    /// <summary>
    /// 使用高级模糊搜索
    /// </summary>
    public bool UseAdvancedFuzzySearch = false;
}