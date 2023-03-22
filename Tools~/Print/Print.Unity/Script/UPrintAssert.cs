using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AIO
{
    public partial class UPrint
    {
        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert(in bool condition)
        {
            if (IsNotOut || NoStatus(LOG) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, "Assertion failed");
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert<T>(in T args)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Assert, args);
        }

        /// <summary>
        /// 断言
        /// </summary>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert<T, O>(in T args, in O uobject) where O : Object
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Assert, args, uobject);
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert<T>(in bool condition, in T context) where T : Object
        {
            if (IsNotOut || NoStatus(LOG) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, (object)"Assertion failed", context);
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert(in bool condition, in object message)
        {
            if (IsNotOut || NoStatus(LOG) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, message);
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert(in bool condition, in string message)
        {
            if (IsNotOut || NoStatus(LOG) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, message);
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert<T>(in bool condition, in object message, in T context) where T : Object
        {
            if (IsNotOut || NoStatus(LOG) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, message, context);
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert<T>(in bool condition, in string message, in T context) where T : Object
        {
            if (IsNotOut || NoStatus(LOG) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, (object)message, context);
        }

        /// <summary>
        ///   <para>Assert a condition and logs a formatted error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void AssertFormat(in bool condition, in string format, params object[] args)
        {
            if (IsNotOut || NoStatus(LOG) || condition) return;
            Debug.unityLogger.LogFormat(LogType.Assert, string.Format(format, args));
        }

        /// <summary>
        ///   <para>Assert a condition and logs a formatted error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void AssertFormat<T>(in bool condition, in T context, in string format, params object[] args) where T : Object
        {
            if (IsNotOut || NoStatus(LOG) || condition) return;
            Debug.unityLogger.LogFormat(LogType.Assert, context, string.Format(format, args));
        }

        /// <summary>
        /// 断言
        /// </summary>
        [Conditional("UNITY_ASSERTIONS")]
        public static void AssertFormat<T>(in string format, params T[] args)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Assert, string.Format(format, args));
        }

        /// <summary>
        /// 断言
        /// </summary>
        [Conditional("UNITY_ASSERTIONS")]
        public static void AssertFormat(in string format, params object[] args)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Debug.unityLogger.Log(LogType.Assert, string.Format(format, args));
        }
    }
}