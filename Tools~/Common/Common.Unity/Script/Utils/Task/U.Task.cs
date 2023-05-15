﻿using System;
using System.Threading.Tasks;

using UnityEngine;

public static partial class UtilsEngine
{
    /// <summary>
    /// 作业
    /// </summary>
    public class TaskX
    {
        /// <summary>
        /// 作业运行
        /// </summary>
        /// <param name="action">函数</param>
        public static async Task RunOnThread(Action action)
        {
            if (action == null) return;
            if (Application.platform == RuntimePlatform.WebGLPlayer) action.Invoke();
            else await Task.Run(action);
        }
    }
}