using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIO
{
    partial class Runner
    {
        #region StartCoroutine
        
        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(IEnumerator coroutine)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
                instance.StartCoroutine(coroutine);
#if UNITY_EDITOR
            else EditorCoroutineLooper.StartLoop( coroutine);
#endif
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(params IEnumerator[] coroutines)
        {
            if (coroutines.Length == 0) return;
            foreach (var enumerator in coroutines)
            {
                instance.StartCoroutine(enumerator);
            }
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(IList<IEnumerator> coroutines)
        {
            if (coroutines is null || coroutines.Count == 0) return;
            for (var index = 0; index < coroutines.Count; index++)
            {
                instance.StartCoroutine(coroutines[index]);
            }
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(Func<IEnumerator> coroutine)
        {
            instance.StartCoroutine(coroutine?.Invoke());
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(params Func<IEnumerator>[] coroutines)
        {
            if (coroutines.Length == 0) return;
            foreach (var enumerator in coroutines)
            {
                instance.StartCoroutine(enumerator?.Invoke());
            }
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void StartCoroutine(IList<Func<IEnumerator>> coroutines)
        {
            if (coroutines.Count == 0) return;
            for (var index = 0; index < coroutines.Count; index++)
            {
                instance.StartCoroutine(coroutines[index]?.Invoke());
            }
        }

        #endregion

        #region StopCoroutine

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(IEnumerator coroutine)
        {
            instance.StopCoroutine(coroutine);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(Coroutine coroutine)
        {
            instance.StopCoroutine(coroutine);
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(Func<IEnumerator> coroutine)
        {
            instance.StopCoroutine(coroutine?.Invoke());
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(Func<Coroutine> coroutine)
        {
            instance.StopCoroutine(coroutine?.Invoke());
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(params Func<IEnumerator>[] coroutines)
        {
            if (coroutines.Length == 0) return;
            foreach (var coroutine in coroutines)
            {
                instance.StopCoroutine(coroutine?.Invoke());
            }
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(params Func<Coroutine>[] coroutines)
        {
            if (coroutines.Length == 0) return;
            foreach (var coroutine in coroutines)
            {
                instance.StopCoroutine(coroutine?.Invoke());
            }
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(IList<Func<IEnumerator>> coroutines)
        {
            if (coroutines.Count == 0) return;
            for (var index = 0; index < coroutines.Count; index++)
            {
                instance.StopCoroutine(coroutines[index]?.Invoke());
            }
        }

        /// <summary>
        /// 结束协程
        /// </summary>
        public static void StopCoroutine(IList<Func<Coroutine>> coroutines)
        {
            if (coroutines.Count == 0) return;
            for (var index = 0; index < coroutines.Count; index++)
            {
                instance.StopCoroutine(coroutines[index]?.Invoke());
            }
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