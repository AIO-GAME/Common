using System;
using System.Diagnostics;

using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AIO
{
    /// <summary>
    /// Unity 输出
    /// </summary>
    public partial class UPrint : Print
    {
        /// <summary>
        /// 控制台
        /// </summary>
        public class Console
        {

            #region :: ENABLED

            /// <summary>
            /// 开启 全部输出
            /// EDITOR  : [编辑器模式 只需启动一次 可实现在编辑模式和运行时自动开启]
            /// RUNTIME : [发布运行模式 程序每次启动 都需调用一次]
            /// </summary>
            public static void EnabledALL()
            {
                EnabledLog();
                EnabledError();
            }

            /// <summary>
            /// 关闭 LOG 输出
            /// </summary>
            public static void EnabledLog()
            {
                if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
                UnityConsole.EnabledLog();
            }

            /// <summary>
            /// 关闭 ERROR 输出
            /// </summary>
            public static void EnabledError()
            {
                if (Print.IsNotOut || Print.NoStatus(Print.ERROR)) return;
                UnityConsole.EnabledError();
            }

            /// <summary>
            /// 关闭 全部输出
            /// </summary>
            public static void DisableALL()
            {
                UnityConsole.DisableLog();
                UnityConsole.DisableError();
            }

            /// <summary>
            /// 关闭 LOG 输出
            /// </summary>
            public static void DisableLog()
            {
                UnityConsole.DisableLog();
            }

            /// <summary>
            /// 关闭 ERROR 输出
            /// </summary>
            public static void DisableError()
            {
                UnityConsole.DisableError();
            }

            #endregion
        }

        /// <summary>
        /// 输出异常
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Exception<E, T>(in E exception, in T obj) where E : Exception where T : Object
        {
            if (Print.IsNotOut || Print.NoStatus(Print.EXCEPTION)) return;
            Debug.unityLogger.Log(LogType.Exception, exception, obj);
        }

        /// <summary>
        /// 输出异常
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Exception<E>(in E exception) where E : Exception
        {
            if (Print.IsNotOut || Print.NoStatus(Print.EXCEPTION)) return;
            Debug.unityLogger.Log(LogType.Exception, exception, null);
        }
    }
}