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
        [Conditional(MACRO_DEFINITION)]
        public static void Log<T>(in T obj, in EFormat format)
        {
            if (IsNotOut || NoStatus(LOG)) return;
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
                case EFormat.None:
                default: break;
            }

            Debug.unityLogger.Log(LogType.Log, obj);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log(in string obj)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Log, obj);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log(in object objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Log, objs);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log<T>(in T objs) where T : IEnumerable
        {
            if (IsNotOut || NoStatus(LOG)) return;
            if (objs == null)
            {
                Debug.unityLogger.Log(LogType.Log, string.Format("{0} is null", nameof(objs)));
                return;
            }

            switch (objs)
            {
                case IDictionary dictionary:
                    Debug.unityLogger.Log(LogType.Log, IDictionary(dictionary));
                    return;
                case IList list:
                    Debug.unityLogger.Log(LogType.Log, IList(list));
                    return;
                case ICollection collection:
                    Debug.unityLogger.Log(LogType.Log, ICollection(collection));
                    return;
                default:
                    Debug.unityLogger.Log(LogType.Log, IEnumerable(objs));
                    break;
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void LogFormat<T>(in string format, params T[] objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.LogFormat(LogType.Log, string.Format(format, objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void LogFormat(in string format, params object[] objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.LogFormat(LogType.Log, string.Format(format, objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log<T>(params T[] objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Log, IList(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log(params object[] objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Log, IList(objs));
        }
    }
}