using System;
using System.Collections;
using System.Collections.Generic;

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
        public static void StartCoroutine(params IEnumerator[] coroutines)
        {
            if (coroutines.Length == 0) return;
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
        public static void StartCoroutine(IList<IEnumerator> coroutines)
        {
            if (coroutines is null || coroutines.Count == 0) return;
            SafeStartCoroutine(coroutines);
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(params Func<IEnumerator>[] coroutines)
        {
            if (coroutines.Length == 0) return;
            SafeStartCoroutine(coroutines);
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(IList<Func<IEnumerator>> coroutines)
        {
            if (coroutines.Count == 0) return;
            SafeStartCoroutine(coroutines);
        }

        #endregion

        #region StopCoroutine

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(IEnumerator coroutine)
        {
            SafeStopCoroutine(coroutine);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(params IEnumerator[] coroutines)
        {
            if (coroutines.Length == 0) return;
            SafeStopCoroutine(coroutines);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(IList<IEnumerator> coroutines)
        {
            if (coroutines.Count == 0) return;
            SafeStopCoroutine(coroutines);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(Func<IEnumerator> coroutine)
        {
            SafeStopCoroutine(coroutine?.Invoke());
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(params Func<IEnumerator>[] coroutines)
        {
            if (coroutines.Length == 0) return;
            SafeStopCoroutine(coroutines);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(IList<Func<IEnumerator>> coroutines)
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