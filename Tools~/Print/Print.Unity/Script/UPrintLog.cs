using System.Collections;
using System.Diagnostics;

using UnityEngine;

using Debug = UnityEngine.Debug;

namespace AIO
{
    public partial class UPrint
    {
        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log<T>(in T obj,in EFormat format)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            switch (format)
            {
                case EFormat.Json:
                    Debug.unityLogger.Log(LogType.Log, Json.Serialize(obj));
                    return;
                case EFormat.Array:
                    if (obj is IEnumerable enumerable)
                    {
                        Log(enumerable);
                        return;
                    }
                    break;
            }
            Debug.unityLogger.Log(LogType.Log, obj);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log(in object objs) 
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Debug.unityLogger.Log(LogType.Log, objs);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log<T>(in T objs) where T : IEnumerable
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            if (objs == null)
            {
                Debug.unityLogger.Log(LogType.Log, string.Format("{0} is null", nameof(objs)));
                return;
            }

            if (objs is IDictionary dictionary)
            {
                Debug.unityLogger.Log(LogType.Log, IDictionary(dictionary));
                return;
            }

            if (objs is IList list)
            {
                Debug.unityLogger.Log(LogType.Log, IList(list));
                return;
            }

            if (objs is ICollection collection)
            {
                Debug.unityLogger.Log(LogType.Log, ICollection(collection));
                return;
            }

            Debug.unityLogger.Log(LogType.Log, IEnumerable(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat<T>(in string format, params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Debug.unityLogger.LogFormat(LogType.Log, string.Format(format, objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat(in string format, params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Debug.unityLogger.LogFormat(LogType.Log, string.Format(format, objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log<T>(params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Debug.unityLogger.Log(LogType.Log, IList(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log(params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Debug.unityLogger.Log(LogType.Log, IList(objs));
        }
    }
}