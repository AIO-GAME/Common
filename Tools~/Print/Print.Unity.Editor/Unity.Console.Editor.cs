/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2023-01-08                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO.Unity.Editor
{
    using UnityEditor;
    using UnityEngine;

    internal static class UnityConsoleEditor
    {
        private static bool IS_EDITOR_SWITCH_LOG => PlayerPrefs.GetInt(MENU_EDITOR_SWITCH_LOG, -1) == 1;
        private static bool IS_EDITOR_SWITCH_ERROR => PlayerPrefs.GetInt(MENU_EDITOR_SWITCH_ERROR, -1) == 1;
        private static bool IS_DEVELOPERMODE => PlayerPrefs.GetInt(MENU_DEVELOPERMODE, -1) == 1;

        /// <summary>
        /// 输出日志
        /// </summary>
        private const string MENU_DEVELOPERMODE = "Tools/Debug/Developer Mode";

        /// <summary>
        /// 错误日志
        /// </summary>
        private const string MENU_EDITOR_SWITCH_LOG = "Tools/Debug/Console Log";

        /// <summary>
        /// 开发者模式
        /// </summary>
        private const string MENU_EDITOR_SWITCH_ERROR = "Tools/Debug/Console Error";

        [MenuItem(MENU_EDITOR_SWITCH_LOG)]
        private static void EditorSwitchLOG()
        {
            PlayerPrefs.SetInt(MENU_EDITOR_SWITCH_LOG, !IS_EDITOR_SWITCH_LOG ? 1 : -1);
            PlayerPrefs.Save();
            EditorProxy();
            AssetDatabase.RefreshSettings();
        }

        [MenuItem(MENU_EDITOR_SWITCH_ERROR)]
        private static void EditorSwitchERROR()
        {
            PlayerPrefs.SetInt(MENU_EDITOR_SWITCH_ERROR, !IS_EDITOR_SWITCH_ERROR ? 1 : -1);
            PlayerPrefs.Save();
            EditorProxy();
            AssetDatabase.RefreshSettings();
        }

        private static void MenuRefresh()
        {
            Menu.SetChecked(MENU_EDITOR_SWITCH_ERROR, IS_EDITOR_SWITCH_ERROR);
            Menu.SetChecked(MENU_EDITOR_SWITCH_LOG, IS_EDITOR_SWITCH_LOG);
            Menu.SetChecked(MENU_DEVELOPERMODE, IS_DEVELOPERMODE);
        }

        [InitializeOnLoadMethod]
        [RuntimeInitializeOnLoadMethod]
        private static void EditorProxy()
        {
            MenuRefresh();

            if (IS_EDITOR_SWITCH_ERROR) UnityConsole.EnabledError();
            else UnityConsole.DisableError();
            if (IS_EDITOR_SWITCH_LOG) UnityConsole.EnabledLog();
            else UnityConsole.DisableLog();
        }

        /// <summary>
        /// 打开开发者模式
        /// </summary>
        [MenuItem(MENU_DEVELOPERMODE, false, 0)]
        private static void OpenDeveloperMode()
        {
            EditorPrefs.SetBool("DeveloperMode", !IS_DEVELOPERMODE);
            PlayerPrefs.SetInt(MENU_DEVELOPERMODE, !IS_DEVELOPERMODE ? 1 : -1);
            MenuRefresh();
        }
    }
}