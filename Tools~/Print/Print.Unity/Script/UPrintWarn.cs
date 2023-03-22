using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AIO
{
    public partial class UPrint
    {
        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Warn<T>(in T obj, in EFormat format)
        {
            if (IsNotOut || NoStatus(WARNING)) return;
            switch (format)
            {
                case EFormat.Json:
                    Debug.unityLogger.Log(LogType.Warning, Json.Serialize(obj));
                    return;
                case EFormat.Array:
                    if (obj is IEnumerable enumerable)
                    {
                        Warn(enumerable);
                        return;
                    }

                    break;
                case EFormat.None:
                default: break;
            }

            Debug.unityLogger.Log(LogType.Warning, obj);
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Warn(in string obj)
        {
            if (IsNotOut || NoStatus(WARNING)) return;
            Debug.unityLogger.Log(LogType.Warning, obj);
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Warn(in object objs)
        {
            if (IsNotOut || NoStatus(WARNING)) return;
            Debug.unityLogger.Log(LogType.Warning, objs);
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Warn<T>(in T objs) where T : IEnumerable
        {
            if (IsNotOut || NoStatus(WARNING)) return;
            if (objs == null)
            {
                Debug.unityLogger.Log(LogType.Warning, string.Format("{0} is null", nameof(objs)));
                return;
            }

            switch (objs)
            {
                case IDictionary dictionary:
                    Debug.unityLogger.Log(LogType.Warning, IDictionary(dictionary));
                    return;
                case IList list:
                    Debug.unityLogger.Log(LogType.Warning, IList(list));
                    return;
                case ICollection collection:
                    Debug.unityLogger.Log(LogType.Warning, ICollection(collection));
                    return;
                default:
                    Debug.unityLogger.Log(LogType.Warning, IEnumerable(objs));
                    break;
            }
        }


        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void WarnFormat<T>(in string format, params T[] objs)
        {
            if (IsNotOut || NoStatus(WARNING)) return;
            Debug.unityLogger.LogFormat(LogType.Warning, string.Format(format, objs));
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void WarnFormat(in string format, params object[] objs)
        {
            if (IsNotOut || NoStatus(WARNING)) return;
            Debug.unityLogger.LogFormat(LogType.Warning, string.Format(format, objs));
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Warn<T>(params T[] objs)
        {
            if (IsNotOut || NoStatus(WARNING)) return;
            Debug.unityLogger.Log(LogType.Warning, IList(objs));
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Warn(params object[] objs)
        {
            if (IsNotOut || NoStatus(WARNING)) return;
            Debug.unityLogger.Log(LogType.Warning, IList(objs));
        }
    }
}