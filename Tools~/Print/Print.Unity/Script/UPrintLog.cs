using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using UnityEngine;

using Debug = UnityEngine.Debug;

namespace AIO
{
    public partial class UPrint
    {
        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogArray<T>(ICollection<T> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var message = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var str = new StringBuilder(message).Append("\r\n");
                foreach (var item in objs) str.Append(string.Format(item.ToString())).Append("\r\n");
                message = str.Remove(message.Length - 2, 2).ToString();
            }

            Debug.unityLogger.Log(LogType.Log, message);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogArray<T>(IList<T> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var message = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var str = new StringBuilder(message).Append("\r\n");
                foreach (var item in objs) str.Append(string.Format(item.ToString())).Append("\r\n");
                message = str.Remove(message.Length - 2, 2).ToString();
            }

            Debug.unityLogger.Log(LogType.Log, message);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogArray<TK, TV>(IDictionary<TK, TV> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var message = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(message).Append("\r\n");
                foreach (var kv in objs)
                {
                    if (kv.Value != null)
                    {
                        str.AppendFormat("[{0}]{1}:{2}\r\n", index++, kv.Key.ToString(), kv.Value.ToString());
                    }
                    else str.AppendFormat("[{0}]{1}:NULL\r\n", index++, kv.Key.ToString());
                }

                message = str.Remove(message.Length - 2, 2).ToString();
            }

            Debug.unityLogger.Log(LogType.Log, message);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat<T>(string format, params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.LogFormat(LogType.Log, format, objs);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat(string format, params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.LogFormat(LogType.Log, format, objs);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log<T>(params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.Log(LogType.Log, string.Join(" ", objs.Select(arg => arg)));
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log(params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.Log(LogType.Log, string.Join(" ", objs.Select(arg => arg)));
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log<T>(T objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.Log(LogType.Log, objs);
        }
    }
}