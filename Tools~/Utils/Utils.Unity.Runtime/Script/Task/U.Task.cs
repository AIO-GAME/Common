using System;
using System.Threading.Tasks;

using UnityEngine;

public static partial class UtilsEngine
{
    public class TaskX
    {
        public static async Task RunOnThread(Action action)
        {
            if (action == null) return;
            if (Application.platform == RuntimePlatform.WebGLPlayer) action.Invoke();
            else await Task.Run(action);
        }
    }
}