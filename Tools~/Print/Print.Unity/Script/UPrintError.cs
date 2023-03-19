using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using UnityEngine;

using Debug = UnityEngine.Debug;

namespace AIO
{
    public partial class UPrint
    {
        /// <summary>
        /// 错误日志 Json 结构
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Error<T>(in T obj, in EFormat format)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.ERROR)) return;
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
            }
            Debug.unityLogger.Log(LogType.Error, obj);
        }

        /// <summary>
        /// 错误日志 Json 结构
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Error(in object obj)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.ERROR)) return;
            Debug.unityLogger.Log(LogType.Error, obj);
        }

        /// <summary>
        /// 错误 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Error<T>(in T objs) where T : IEnumerable
        {
            if (Print.IsNotOut || Print.NoStatus(Print.ERROR)) return;
            if (objs == null)
            {
                Debug.unityLogger.Log(LogType.Error, string.Format("{0} is null", nameof(objs)));
                return;
            }

            if (objs is IDictionary dictionary)
            {
                Debug.unityLogger.Log(LogType.Error, IDictionary(dictionary));
                return;
            }

            if (objs is IList list)
            {
                Debug.unityLogger.Log(LogType.Error, IList(list));
                return;
            }

            if (objs is ICollection collection)
            {
                Debug.unityLogger.Log(LogType.Error, ICollection(collection));
                return;
            }

            Debug.unityLogger.Log(LogType.Error, IEnumerable(objs));
        }
     
        /// <summary>
        /// 错误 数组
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Error<T>(params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.ERROR)) return;
            Debug.unityLogger.Log(LogType.Error, IList(objs));
        }

        /// <summary>
        /// 错误 数组
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Error(params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.ERROR)) return;
            Debug.unityLogger.Log(LogType.Error, IList(objs));
        }

        /// <summary>
        /// 错误
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void ErrorFormat<T>(in string format, params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.ERROR)) return;
            Debug.unityLogger.LogFormat(LogType.Error, string.Format(format, objs));
        }

        /// <summary>
        /// 错误
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void ErrorFormat(in string format, params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.ERROR)) return;
            Debug.unityLogger.LogFormat(LogType.Error, string.Format(format, objs));
        }
    }
}