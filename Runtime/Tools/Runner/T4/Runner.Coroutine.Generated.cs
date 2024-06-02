using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIO
{
    partial class Runner
    {
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine(Action action) { SafeStartCoroutine(ActionAsync.Create(action)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine<T1>(Action<T1> action, T1 t1) { SafeStartCoroutine(ActionAsync.Create(action, t1)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine<T1, T2>(Action<T1, T2> action, T1 t1, T2 t2) { SafeStartCoroutine(ActionAsync.Create(action, t1, t2)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine<T1, T2, T3>(Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3) { SafeStartCoroutine(ActionAsync.Create(action, t1, t2, t3)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4) { SafeStartCoroutine(ActionAsync.Create(action, t1, t2, t3, t4)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) { SafeStartCoroutine(ActionAsync.Create(action, t1, t2, t3, t4, t5)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) { SafeStartCoroutine(ActionAsync.Create(action, t1, t2, t3, t4, t5, t6)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) { SafeStartCoroutine(ActionAsync.Create(action, t1, t2, t3, t4, t5, t6, t7)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <typeparam name="T8">The type of the 8th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8) { SafeStartCoroutine(ActionAsync.Create(action, t1, t2, t3, t4, t5, t6, t7, t8)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <typeparam name="T8">The type of the 8th parameter.</typeparam>
        /// <typeparam name="T9">The type of the 9th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) { SafeStartCoroutine(ActionAsync.Create(action, t1, t2, t3, t4, t5, t6, t7, t8, t9)); }

        #region IEnumerator
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine(Func<IEnumerator> func) { SafeStartCoroutine(func.Invoke()); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine<T1>(Func<T1, IEnumerator> func, T1 t1) { SafeStartCoroutine(func.Invoke(t1)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine<T1, T2>(Func<T1, T2, IEnumerator> func, T1 t1, T2 t2) { SafeStartCoroutine(func.Invoke(t1, t2)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine<T1, T2, T3>(Func<T1, T2, T3, IEnumerator> func, T1 t1, T2 t2, T3 t3) { SafeStartCoroutine(func.Invoke(t1, t2, t3)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine<T1, T2, T3, T4>(Func<T1, T2, T3, T4, IEnumerator> func, T1 t1, T2 t2, T3 t3, T4 t4) { SafeStartCoroutine(func.Invoke(t1, t2, t3, t4)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, IEnumerator> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) { SafeStartCoroutine(func.Invoke(t1, t2, t3, t4, t5)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, IEnumerator> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) { SafeStartCoroutine(func.Invoke(t1, t2, t3, t4, t5, t6)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, IEnumerator> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) { SafeStartCoroutine(func.Invoke(t1, t2, t3, t4, t5, t6, t7)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <typeparam name="T8">The type of the 8th parameter.</typeparam>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, IEnumerator> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8) { SafeStartCoroutine(func.Invoke(t1, t2, t3, t4, t5, t6, t7, t8)); }
     
        /// <summary>
        /// Starts a coroutine on the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <typeparam name="T8">The type of the 8th parameter.</typeparam>
        /// <typeparam name="T9">The type of the 9th parameter.</typeparam>
        /// <param name="func">The function to be invoked.</param>
        public static void StartCoroutine<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IEnumerator> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) { SafeStartCoroutine(func.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9)); }

        #endregion
    }
}