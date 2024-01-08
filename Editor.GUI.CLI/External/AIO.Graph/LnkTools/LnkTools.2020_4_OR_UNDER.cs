/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-08
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#if !UNITY_2021_1_OR_NEWER
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    [InitializeOnLoad]
    internal static partial class LnkToolsHelper
    {
        private static bool IsEnableLnkTools;
        private static float Height;
        private static GUIContent Content_Thumbnail;
        private static GUIContent Content_Restore;

        /// <summary>
        /// 缩略图
        /// </summary>
        private static bool Thumbnail;

        static LnkToolsHelper()
        {
            OnInitLnkTools();
        }

        /// <summary>
        /// 初始化快捷工具箱
        /// </summary>
        private static void OnInitLnkTools()
        {
            Content_Thumbnail = EditorGUIUtility.IconContent("ArrowNavigationLeft");
            Content_Thumbnail.tooltip = "Thumbnail";
            Content_Restore = EditorGUIUtility.IconContent("ArrowNavigationRight");
            Content_Restore.tooltip = "Restore";

            Thumbnail = EditorPrefs.GetBool("Thumbnail", false);
            IsEnableLnkTools = EditorPrefs.GetBool(EditorPrefsTable.LnkTools_Enable, true);
            if (!IsEnableLnkTools) return;
            GetLnkTools();

            Height = (LnkToolList.Count + 1) * 22 + 3;
            SceneView.duringSceneGui += OnLnkToolsGUI;
        }

        /// <summary>
        /// 快捷工具箱界面
        /// </summary>
        private static void OnLnkToolsGUI(SceneView sceneView)
        {
            Handles.BeginGUI();
            var rect = Rect.zero;
            var x = sceneView.position.width - 45;
            var y = sceneView.in2DMode ? 25 : sceneView.position.height / 2 - Height / 2;

            rect.Set(sceneView.position.width - 10, y, 10, 20);
            if (GUI.Button(rect, Thumbnail ? Content_Thumbnail : Content_Restore, "InvisibleButton"))
            {
                Thumbnail = !Thumbnail;
                EditorPrefs.SetBool("Thumbnail", Thumbnail);
            }


            if (!Thumbnail)
            {
                var height = 22 + 3 - Height / 2;
                foreach (var lnk in LnkToolList)
                {
                    switch (lnk.Mode)
                    {
                        case LnkToolsMode.OnlyRuntime:
                            GUI.enabled = EditorApplication.isPlaying;
                            break;
                        case LnkToolsMode.OnlyEditor:
                            GUI.enabled = !EditorApplication.isPlaying;
                            break;
                        default:
                            GUI.enabled = true;
                            break;
                    }

                    rect.Set(x, y + height, 30, 20);
                    height += 22;

                    GUI.backgroundColor = lnk.BGColor;
                    if (GUI.Button(rect, lnk.Content))
                    {
                        lnk.Method.Invoke(null, null);
                    }

                    GUI.backgroundColor = Color.white;
                }
            }

            GUI.enabled = true;

            Handles.EndGUI();
        }
    }
}
#endif