using System;
using UnityEngine;

namespace UnityEngine
{
    public static partial class UtilsEngine
    {
        /// <summary>
        /// 作业
        /// </summary>
        public static class Task
        {
            /// <summary>
            /// 作业运行
            /// </summary>
            /// <param name="action">函数</param>
            public static async System.Threading.Tasks.Task RunOnThread(Action action)
            {
                if (action == null) return;
                if (Application.platform == RuntimePlatform.WebGLPlayer) action.Invoke();
                else await System.Threading.Tasks.Task.Run(action);
            }
        }
    }
}