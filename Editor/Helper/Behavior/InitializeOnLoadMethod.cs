using UnityEditor;

namespace AIO.UEditor
{
    public static partial class Behavior
    {
        /// <summary>
        /// 编辑器行为时间监听
        /// </summary>
        public static class InitializeOnLoadMethod
        {
            [InitializeOnLoadMethod]
            private static void Initialize()
            {
                EditorApplication.playModeStateChanged -= EidtorQuit;
                EditorApplication.playModeStateChanged += EidtorQuit;
            }

            private static void EidtorQuit(PlayModeStateChange value)
            {
                if (value == PlayModeStateChange.ExitingPlayMode)
                {
                    // TO DO
                }
            }
        }
    }
}