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
            typeof(AssetManagerGraphWindow),
            typeof(BuiltInGUIStyleGraphWindow),
            typeof(BuiltInTextureGraphWindow),
            typeof(CScriptManagerGraphWindow),
            typeof(DependAnalysisGraphWindow),
            typeof(IconsListGraphWindow),
            typeof(PackageManagerWindow),
            typeof(ScriptIDWindow),
            typeof(ScriptingDefineSymbols),
            typeof(DllSwitcherWindow),
#endif
        };

        [MenuItem("AIO/Window/Editor Icons")]
        public static void OpenIconsListGraphWindow()
        {
            EHelper.Window.Open<IconsListGraphWindow>(DockedWindowTypes);
        }

        [MenuItem("AIO/Window/Script ID Viewer")]
        public static void OpenScriptIDWindow()
        {
            EHelper.Window.Open<ScriptIDWindow>(DockedWindowTypes);
        }

        [MenuItem("AIO/Window/Package Manager")]
        public static void OpenPackageManagerWindow()
        {
            EHelper.Window.Open<PackageManagerWindow>(DockedWindowTypes);
        }


        [MenuItem("AIO/Window/Dll Switcher")]
        public static void ShowWindow()
        {
            EHelper.Window.Open<DllSwitcherWindow>(DockedWindowTypes);
        }

        [MenuItem("AIO/Window/Query Reference", false, 19)]
        [MenuItem("Assets/Query Reference", false, 19)]
        private static void FindReferences()
        {
            EHelper.Window.Open<DependAnalysisGraphWindow>(DockedWindowTypes);
        }

        private const string LABLE_AssestManagerWindow = "Assest Manager";
        private const string LABLE_BuiltInGUIStyleWindow = "Built In GUIStyle Window";
        private const string LABLE_BuiltInTextureWindow = "Built In Texture Window";
        private const string LABLE_ScriptingDefineSymbolsEditor = "Scripting Define Symbols Editor";

        [MenuItem("AIO/Window/" + LABLE_AssestManagerWindow, false, 101)]
        public static void OpenAssestManagerGraphWindow()
        {
            EHelper.Window.Open<AssetManagerGraphWindow>(DockedWindowTypes);
        }

        [MenuItem("AIO/Window/" + LABLE_BuiltInGUIStyleWindow, false, 102)]
        public static void OpenBuiltInGUIStyleGraphWindow()
        {
            EHelper.Window.Open<BuiltInGUIStyleGraphWindow>(DockedWindowTypes);
        }

        [MenuItem("AIO/Window/" + LABLE_BuiltInTextureWindow, false, 103)]
        public static void OpenBuiltInTextureGraphWindow()
        {
            EHelper.Window.Open<BuiltInTextureGraphWindow>(DockedWindowTypes);
        }

        [MenuItem("AIO/Window/" + LABLE_ScriptingDefineSymbolsEditor, false, 104)]
        public static void OpenScriptingDefineSymbolsr()
        {
            EHelper.Window.Open<ScriptingDefineSymbols>(DockedWindowTypes);
        }
    }
}