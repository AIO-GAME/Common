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
        public static void Assert(bool condition)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, (object)"Assertion failed");
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert<T>(bool condition, T context) where T : Object
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, (object)"Assertion failed", context);
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert(bool condition, object message)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, message);
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert(bool condition, string message)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, (object)message);
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert<T>(bool condition, object message, T context) where T : Object
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, message, context);
        }

        /// <summary>
        ///   <para>Assert a condition and logs an error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert<T>(bool condition, string message, T context) where T : Object
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log) || condition) return;
            Debug.unityLogger.Log(LogType.Assert, (object)message, context);
        }

        /// <summary>
        ///   <para>Assert a condition and logs a formatted error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void AssertFormat(bool condition, string format, params object[] args)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log) || condition) return;
            Debug.unityLogger.LogFormat(LogType.Assert, format, args);
        }

        /// <summary>
        ///   <para>Assert a condition and logs a formatted error message to the Unity console on failure.</para>
        /// </summary>
        /// <param name="condition">Condition you expect to be true.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        /// <param name="context">Object to which the message applies.</param>
        [Conditional("UNITY_ASSERTIONS")]
        public static void AssertFormat<T>(bool condition, T context, string format, params object[] args) where T : Object
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log) || condition) return;
            Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
        }


        /// <summary>
        /// 
        /// </summary>
        [Conditional("UNITY_ASSERTIONS")]
        public static void LogAssert<T, UObject>(T args, UObject uobject) where UObject : Object
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.Log(LogType.Assert, args, uobject);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional("UNITY_ASSERTIONS")]
        public static void LogAssert<T>(T args)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.Log(LogType.Assert, args);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional("UNITY_ASSERTIONS")]
        public static void LogAssertFormat<T>(string format, params T[] args)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.Log(LogType.Assert, format, args);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional("UNITY_ASSERTIONS")]
        public static void LogAssertFormat(string format, params object[] args)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Debug.unityLogger.Log(LogType.Assert, format, args);
        }
    }
}