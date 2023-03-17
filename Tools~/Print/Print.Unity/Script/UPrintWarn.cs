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
    public static partial class UPrint
    {
        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void WarnArray<T>(ICollection<T> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Warn)) return;
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

            Debug.unityLogger.Log(LogType.Warning, message);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void WarnArray<T>(IList<T> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Warn)) return;
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

            Debug.unityLogger.Log(LogType.Warning, message);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void WarnArray<TK, TV>(IDictionary<TK, TV> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Warn)) return;
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

            Debug.unityLogger.Log(LogType.Warning, message);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void WarnFormat<T>(string format, params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Warn)) return;
            Debug.unityLogger.LogFormat(LogType.Warning, format, objs);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void WarnFormat(string format, params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Warn)) return;
            Debug.unityLogger.LogFormat(LogType.Warning, format, objs);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Warn<T>(params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Warn)) return;
            Debug.unityLogger.Log(LogType.Warning, string.Join(" ", objs.Select(arg => arg)));
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Warn(params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Warn)) return;
            Debug.unityLogger.Log(LogType.Warning, string.Join(" ", objs.Select(arg => arg)));
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Warn<T>(T objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Warn)) return;
            Debug.unityLogger.Log(LogType.Warning, objs);
        }
    }
}