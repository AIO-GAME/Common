/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using AIO.Unity.Editor;
using UnityEditor;

namespace AIO.Unity.Editor
{
    /// <summary>
    /// 编辑器标题
    /// </summary>
    public static partial class EditorMenu
    {
        /// <summary>
        /// 停靠窗口类型集合
        /// </summary>
        public static readonly Type[] DockedWindowTypes =
        {
#if UNITY_2019_4_OR_NEWER
            typeof(AssestManagerGraphWindow),
            typeof(BuiltInGUIStyleGraphWindow),
            typeof(BuiltInTextureGraphWindow),
            typeof(ScriptingDefineSymbols),
#endif
        };

        private const string LABLE_AssestManagerWindow = "Assest Manager";
        private const string LABLE_BuiltInGUIStyleWindow = "Built In GUIStyle Window";
        private const string LABLE_BuiltInTextureWindow = "Built In Texture Window";
        private const string LABLE_ScriptingDefineSymbolsEditor = "Scripting Define Symbols Editor";

#if MONKEYCOMMANDER
        [MonKey.Command(LABLE_AssestManagerWindow,
            Help = "资源管理器",
            Category = "Windows",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem("Tools/Window/" + LABLE_AssestManagerWindow, false, 101)]
        public static void OpenAssestManagerWindow()
        {
            UtilsEditor.Window.Open<AssestManagerGraphWindow>(LABLE_AssestManagerWindow, true, DockedWindowTypes);
        }

#if MONKEYCOMMANDER
        [MonKey.Command(LABLE_BuiltInGUIStyleWindow,
            Help = "GUIStyle 风格管理器",
            Category = "Windows",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem("Tools/Window/" + LABLE_BuiltInGUIStyleWindow, false, 102)]
        public static void OpenBuiltInGUIStyleWindow()
        {
            UtilsEditor.Window.Open<BuiltInGUIStyleGraphWindow>(LABLE_BuiltInGUIStyleWindow, true, DockedWindowTypes);
        }

#if MONKEYCOMMANDER
        [MonKey.Command(LABLE_BuiltInTextureWindow,
            Help = "Texture 管理器",
            Category = "Windows",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem("Tools/Window/" + LABLE_BuiltInTextureWindow, false, 103)]
        public static void OpenBuiltInTextureWindow()
        {
            UtilsEditor.Window.Open<BuiltInTextureGraphWindow>(LABLE_BuiltInTextureWindow, true, DockedWindowTypes);
        }

#if MONKEYCOMMANDER
        [MonKey.Command(LABLE_ScriptingDefineSymbolsEditor,
            Help = "宏定义 管理器",
            Category = "Windows",
            DefaultValidation = MonKey.DefaultValidation.IN_EDIT_MODE,
            AlwaysShow = true,
            IgnoreHotKeyConflict = false
        )]
#endif
        [MenuItem("Tools/Window/" + LABLE_ScriptingDefineSymbolsEditor, false, 104)]
        public static void OpenScriptingDefineSymbolsEditor()
        {
            UtilsEditor.Window.Open<ScriptingDefineSymbols>(LABLE_BuiltInTextureWindow, true, DockedWindowTypes);
        }
    }
}