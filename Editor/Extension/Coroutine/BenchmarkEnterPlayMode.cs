using System;
using UnityEditor;

public class BenchmarkEnterPlayMode
{
    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        return;

        void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state is PlayModeStateChange.ExitingEditMode or PlayModeStateChange.ExitingPlayMode)
                SessionState.SetFloat("state_exit_time", (float)EditorApplication.timeSinceStartup);
            else
                Console.WriteLine(state + " in: " + ((float)EditorApplication.timeSinceStartup - SessionState.GetFloat("state_exit_time", 0f)) + " seconds");
        }
    }
}