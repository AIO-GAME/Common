using UnityEditor;

namespace AIO.Unity.Editor
{
    /// <summary>
    /// 编辑器行为时间监听
    /// </summary>
    public static class EditorBehavior
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