using System.Collections;
using System.Diagnostics;

using UnityEngine;

using Debug = UnityEngine.Debug;

namespace AIO
{
    public  partial class UPrint
    {

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Warn<T>(in T obj, in EFormat format)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.WARNING)) return;
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
            }
            Debug.unityLogger.Log(LogType.Warning, obj);
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Warn(in object objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.WARNING)) return;
            Debug.unityLogger.Log(LogType.Warning, objs);
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Warn<T>(in T objs) where T : IEnumerable
        {
            if (Print.IsNotOut || Print.NoStatus(Print.WARNING)) return;
            if (objs == null)
            {
                Debug.unityLogger.Log(LogType.Warning, string.Format("{0} is null", nameof(objs)));
                return;
            }

            if (objs is IDictionary dictionary)
            {
                Debug.unityLogger.Log(LogType.Warning, IDictionary(dictionary));
                return;
            }

            if (objs is IList list)
            {
                Debug.unityLogger.Log(LogType.Warning, IList(list));
                return;
            }

            if (objs is ICollection collection)
            {
                Debug.unityLogger.Log(LogType.Warning, ICollection(collection));
                return;
            }

            Debug.unityLogger.Log(LogType.Warning, IEnumerable(objs));
        }

       
        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void WarnFormat<T>(in string format, params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.WARNING)) return;
            Debug.unityLogger.LogFormat(LogType.Warning, string.Format(format, objs));
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void WarnFormat(in string format, params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.WARNING)) return;
            Debug.unityLogger.LogFormat(LogType.Warning, string.Format(format, objs));
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Warn<T>(params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.WARNING)) return;
            Debug.unityLogger.Log(LogType.Warning, IList(objs));
        }

        /// <summary>
        /// 警告
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Warn(params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.WARNING)) return;
            Debug.unityLogger.Log(LogType.Warning, IList(objs));
        }
    }
}