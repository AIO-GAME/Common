using UnityEngine;

namespace AIO
{
    public static class GUHelper
    {
        #region 工具基类 Toolbar

#if UNITY_2018_1_OR_NEWER

        /// <summary> 工具 bar </summary>
        public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.Toolbar(selected, contents, style, GUI.ToolbarButtonSize.Fixed, options);
        }

        /// <summary> 工具 bar </summary>
        public static void Toolbar(int selected, string[] contents)
        {
            GUILayout.Toolbar(selected, contents);
        }
#endif

        #endregion
    }
}