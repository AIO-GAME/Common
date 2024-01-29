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

        /// <summary>
        /// 初始化快捷工具箱
        /// </summary>
        [InitializeOnLoadMethod]
        private static void OnInitLnkTools()
        {
            Content_Thumbnail = EditorGUIUtility.IconContent("ArrowNavigationLeft");
            Content_Thumbnail.tooltip = "Thumbnail";
            Content_Restore = EditorGUIUtility.IconContent("ArrowNavigationRight");
            Content_Restore.tooltip = "Restore";

            Thumbnail = EditorPrefs.GetBool("Thumbnail", false);
            IsEnableLnkTools = EditorPrefs.GetBool(EditorPrefsTable.LnkTools_Enable, true);
            if (!IsEnableLnkTools) return;
            Height = Data.Count * 22 - 2;
            SceneView.duringSceneGui += OnLnkToolsGUI;
        }

        /// <summary>
        /// 快捷工具箱界面
        /// </summary>
        private static void OnLnkToolsGUI(SceneView sceneView)
        {
            var rect = new Rect(
                sceneView.position.width - 10,
                sceneView.in2DMode ? 2 : sceneView.position.height / 2 - 10,
                10,
                20);
            Handles.BeginGUI();
            if (GUI.Button(rect, Thumbnail ? Content_Thumbnail : Content_Restore, "InvisibleButton"))
            {
                Thumbnail = !Thumbnail;
                EditorPrefs.SetBool("Thumbnail", Thumbnail);
            }

            if (!Thumbnail)
            {
                rect.Set(
                    sceneView.position.width - 45,
                    sceneView.in2DMode ? rect.y : rect.y + 10 - Height / 2,
                    30,
                    20);
                foreach (var lnk in Data)
                {
                    if (lnk.ShowMode != ELnkShowMode.SceneView) continue;
                    switch (lnk.Mode)
                    {
                        case ELnkToolsMode.OnlyRuntime:
                            GUI.enabled = EditorApplication.isPlaying;
                            break;
                        case ELnkToolsMode.OnlyEditor:
                            GUI.enabled = !EditorApplication.isPlaying;
                            break;
                    }


                    GUI.backgroundColor = lnk.BackgroundColor;
                    GELayout.Button(rect, lnk.Content, lnk.Invoke);
                    rect.y += 22;
                    if (!GUI.enabled) GUI.enabled = true;
                }

                GUI.backgroundColor = Color.white;
            }

            Handles.EndGUI();
        }
    }
}
#endif