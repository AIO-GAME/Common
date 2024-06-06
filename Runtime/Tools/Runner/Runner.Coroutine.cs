#region

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace AIO
{
    partial class Runner
    {
        #region StartCoroutine

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(IEnumerator coroutine) { SafeStartCoroutine(coroutine); }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(MonoBehaviour behaviour, IEnumerator coroutine) { SafeStartCoroutine(behaviour, coroutine); }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(MonoBehaviour behaviour, Func<IEnumerator> coroutine) { SafeStartCoroutine(behaviour, coroutine.Invoke()); }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(params IEnumerator[] coroutines) { SafeStartCoroutine(coroutines); }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(params Func<IEnumerator>[] coroutines)
        {
            foreach (var func in coroutines) SafeStartCoroutine(func.Invoke());
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(MonoBehaviour behaviour, params Func<IEnumerator>[] coroutines)
        {
            foreach (var func in coroutines) SafeStartCoroutine(behaviour, func.Invoke());
        }

        #endregion

        #region StopCoroutine

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(MonoBehaviour behaviour) { SafeStopCoroutine(behaviour); }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(MonoBehaviour behaviour, IEnumerator coroutine) { SafeStopCoroutine(behaviour, coroutine); }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(IEnumerator coroutine) { SafeStopCoroutine(coroutine); }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(IEnumerator coroutine, params IEnumerator[] coroutines)
        {
            SafeStopCoroutine(coroutine);
            SafeStopCoroutine(coroutines);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(IEnumerable<IEnumerator> coroutines) { SafeStopCoroutine(coroutines); }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(Func<IEnumerator> coroutine) { SafeStopCoroutine(coroutine.Invoke()); }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(in Func<IEnumerator> coroutine, params Func<IEnumerator>[] coroutines)
        {
            SafeStopCoroutine(coroutine?.Invoke());
            foreach (var func in coroutines) SafeStopCoroutine(func?.Invoke());
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(in ICollection<Func<IEnumerator>> coroutines)
        {
            if (coroutines.Count == 0) return;
            foreach (var func in coroutines) SafeStopCoroutine(func?.Invoke());
        }

        /// <summary>
        /// 结束全部协程
        /// </summary>
        public static void StopAllCoroutines()
        {
            instance.StopAllCoroutines();
            QueuesCoroutine.Clear();
        }

        #endregion
    }
}