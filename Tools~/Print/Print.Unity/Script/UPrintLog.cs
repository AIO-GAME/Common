using System.Collections;
using System.Diagnostics;
using AIO.Internal;

namespace UnityEngine
{
    public partial class Print
    {
        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
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
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Log(in string obj)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Log, obj);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Log(in object objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Log, objs);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Log<T>(in T objs) where T : IEnumerable
        {
            if (IsNotOut || NoStatus(LOG)) return;
            if (objs == null)
            {
                Debug.unityLogger.Log(LogType.Log, "objs is null");
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
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void LogFormat(in string format, params object[] objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.LogFormat(LogType.Log, string.Format(format, objs));
        }
    }
}