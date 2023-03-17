using System;
using System.Diagnostics;

using UnityEngine;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class UPrint
    {
        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogJson<T>(T obj)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.Log(LogType.Log, Json.Serialize(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void WarnJson<T>(T obj)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Warn)) return;
            Debug.unityLogger.Log(LogType.Warning, Json.Serialize(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void ErrorJson<T>(T obj)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Error)) return;
            Debug.unityLogger.Log(LogType.Error, Json.Serialize(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Exception<E, T>(E exception, T obj) where E : Exception where T : Object
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Exception)) return;
            Debug.unityLogger.Log(LogType.Exception, exception, obj);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Exception<E>(E exception) where E : Exception
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Exception)) return;
            Debug.unityLogger.Log(LogType.Exception, exception, null);
        }
    }
}