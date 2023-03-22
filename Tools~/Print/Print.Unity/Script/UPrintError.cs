using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AIO
{
    public partial class UPrint
    {
        /// <summary>
        /// 错误日志 Json 结构
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Error<T>(in T obj, in EFormat format)
        {
            if (IsNotOut || NoStatus(ERROR)) return;
            switch (format)
            {
                case EFormat.Json:
                    Debug.unityLogger.Log(LogType.Error, Json.Serialize(obj));
                    return;
                case EFormat.Array:
                    if (obj is IEnumerable enumerable)
                    {
                        Error(enumerable);
                        return;
                    }

                    break;
                case EFormat.None:
                default: break;
            }

            Debug.unityLogger.Log(LogType.Error, obj);
        }

        /// <summary>
        /// 错误
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Error(in object obj)
        {
            if (IsNotOut || NoStatus(ERROR)) return;
            Debug.unityLogger.Log(LogType.Error, obj);
        }

        /// <summary>
        /// 错误
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Error(in string obj)
        {
            if (IsNotOut || NoStatus(ERROR)) return;
            Debug.unityLogger.Log(LogType.Error, obj);
        }

        /// <summary>
        /// 错误 
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Error<T>(in T objs) where T : IEnumerable
        {
            if (IsNotOut || NoStatus(ERROR)) return;
            if (objs == null)
            {
                Debug.unityLogger.Log(LogType.Error, string.Format("{0} is null", nameof(objs)));
                return;
            }

            switch (objs)
            {
                case IDictionary dictionary:
                    Debug.unityLogger.Log(LogType.Error, IDictionary(dictionary));
                    return;
                case IList list:
                    Debug.unityLogger.Log(LogType.Error, IList(list));
                    return;
                case ICollection collection:
                    Debug.unityLogger.Log(LogType.Error, ICollection(collection));
                    return;
                default:
                    Debug.unityLogger.Log(LogType.Error, IEnumerable(objs));
                    break;
            }
        }

        /// <summary>
        /// 错误 数组
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Error<T>(params T[] objs)
        {
            if (IsNotOut || NoStatus(ERROR)) return;
            Debug.unityLogger.Log(LogType.Error, IList(objs));
        }

        /// <summary>
        /// 错误 数组
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Error(params object[] objs)
        {
            if (IsNotOut || NoStatus(ERROR)) return;
            Debug.unityLogger.Log(LogType.Error, IList(objs));
        }

        /// <summary>
        /// 错误
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void ErrorFormat<T>(in string format, params T[] objs)
        {
            if (IsNotOut || NoStatus(ERROR)) return;
            Debug.unityLogger.LogFormat(LogType.Error, string.Format(format, objs));
        }

        /// <summary>
        /// 错误
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void ErrorFormat(in string format, params object[] objs)
        {
            if (IsNotOut || NoStatus(ERROR)) return;
            Debug.unityLogger.LogFormat(LogType.Error, string.Format(format, objs));
        }
    }
}