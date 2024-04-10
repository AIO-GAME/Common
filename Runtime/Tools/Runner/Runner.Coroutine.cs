#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class Runner
    {
        #region StartCoroutine

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(Action coroutine)
        {
            SafeStartCoroutine(StartCoroutineActionEx(coroutine));
        }

        private static IEnumerator StartCoroutineActionEx(Action coroutine)
        {
            coroutine?.Invoke();
            yield break;
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(IEnumerator coroutine)
        {
            SafeStartCoroutine(coroutine);
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(IEnumerator coroutine, params IEnumerator[] coroutines)
        {
            if (coroutines.Length == 0) return;
            SafeStartCoroutine(coroutine);
            SafeStartCoroutine(coroutines);
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(Func<IEnumerator> coroutine)
        {
            SafeStartCoroutine(coroutine?.Invoke());
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(ICollection<IEnumerator> coroutines)
        {
            if (coroutines is null || coroutines.Count == 0) return;
            SafeStartCoroutine(coroutines);
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(Func<IEnumerator> coroutine, params Func<IEnumerator>[] coroutines)
        {
            SafeStartCoroutine(coroutine?.Invoke());
            SafeStartCoroutine(coroutines);
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(ICollection<Func<IEnumerator>> coroutines)
        {
            if (coroutines.Count == 0) return;
            SafeStartCoroutine(coroutines);
        }

        #endregion

        #region StopCoroutine

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(in IEnumerator coroutine)
        {
            SafeStopCoroutine(coroutine);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(in IEnumerator coroutine, params IEnumerator[] coroutines)
        {
            SafeStopCoroutine(coroutine);
            SafeStopCoroutine(coroutines);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(in ICollection<IEnumerator> coroutines)
        {
            if (coroutines.Count == 0) return;
            SafeStopCoroutine(coroutines);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(in Func<IEnumerator> coroutine)
        {
            SafeStopCoroutine(coroutine?.Invoke());
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(in Func<IEnumerator> coroutine, params Func<IEnumerator>[] coroutines)
        {
            SafeStopCoroutine(coroutine?.Invoke());
            SafeStopCoroutine(coroutines);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(in ICollection<Func<IEnumerator>> coroutines)
        {
            if (coroutines.Count == 0) return;
            SafeStopCoroutine(coroutines);
        }

        /// <summary>
        /// 结束全部协程
        /// </summary>
        public static void StopAllCoroutines()
        {
            instance.StopAllCoroutines();
        }

        #endregion
    }
}