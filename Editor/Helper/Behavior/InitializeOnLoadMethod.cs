#region

using UnityEditor;

#endregion

namespace AIO.UEditor
{
    public static partial class Behavior
    {
        #region Nested type: InitializeOnLoadMethod

        /// <summary>
        /// 编辑器行为时间监听
        /// </summary>
        public static class InitializeOnLoadMethod
        {
            [InitializeOnLoadMethod]
            private static void Initialize()
            {
                EditorApplication.playModeStateChanged -= EditorQuit;
                EditorApplication.playModeStateChanged += EditorQuit;
            }

            private static void EditorQuit(PlayModeStateChange value)
            {
                if (value == PlayModeStateChange.ExitingPlayMode)
                {
                    // TO DO
                }
            }
        }

        #endregion
    }
}