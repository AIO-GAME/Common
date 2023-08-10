/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using UnityEditor;

namespace AIO.UEditor
{
    /// <summary>
    /// 编辑器标题
    /// </summary>
    public static partial class MenuItem_Tools
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
            typeof(CScriptManagerGraphWindow),
            typeof(DependAnalysisGraphWindow),
            typeof(IconsListGraphWindow),
            typeof(PluginsManagerWindow),
            typeof(PackageManagerWindow),
            typeof(ScriptIDWindow),
            typeof(ScriptingDefineSymbols),
#endif
        };

        [MenuItem("Tools/Window/Editor Icons")]
        public static void OpenIconsListGraphWindow()
        {
            UtilsEditor.Window.Open<IconsListGraphWindow>(DockedWindowTypes);
        }

        [MenuItem("Tools/Window/Script ID Viewer")]
        public static void OpenScriptIDWindow()
        {
            UtilsEditor.Window.Open<ScriptIDWindow>(DockedWindowTypes);
        }

        [MenuItem("Tools/Window/Package Manager")]
        public static void OpenPackageManagerWindow()
        {
            UtilsEditor.Window.Open<PackageManagerWindow>(DockedWindowTypes);
        }

        [MenuItem("Tools/Window/Plugins Manager")]
        public static void OpenPluginsManagerWindow()
        {
            UtilsEditor.Window.Open<PluginsManagerWindow>(DockedWindowTypes);
        }

        private const string LABLE_AssestManagerWindow = "Assest Manager";
        private const string LABLE_BuiltInGUIStyleWindow = "Built In GUIStyle Window";
        private const string LABLE_BuiltInTextureWindow = "Built In Texture Window";
        private const string LABLE_ScriptingDefineSymbolsEditor = "Scripting Define Symbols Editor";

        [MenuItem("Tools/Window/" + LABLE_AssestManagerWindow, false, 101)]
        public static void OpenAssestManagerGraphWindow()
        {
            UtilsEditor.Window.Open<AssestManagerGraphWindow>(DockedWindowTypes);
        }

        [MenuItem("Tools/Window/" + LABLE_BuiltInGUIStyleWindow, false, 102)]
        public static void OpenBuiltInGUIStyleGraphWindow()
        {
            UtilsEditor.Window.Open<BuiltInGUIStyleGraphWindow>(DockedWindowTypes);
        }

        [MenuItem("Tools/Window/" + LABLE_BuiltInTextureWindow, false, 103)]
        public static void OpenBuiltInTextureGraphWindow()
        {
            UtilsEditor.Window.Open<BuiltInTextureGraphWindow>(DockedWindowTypes);
        }

        [MenuItem("Tools/Window/" + LABLE_ScriptingDefineSymbolsEditor, false, 104)]
        public static void OpenScriptingDefineSymbolsr()
        {
            UtilsEditor.Window.Open<ScriptingDefineSymbols>(DockedWindowTypes);
        }
    }
}