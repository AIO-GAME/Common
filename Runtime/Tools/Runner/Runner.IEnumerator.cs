#region namespace

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
#if SUPPORT_UNITASK
using Cysharp.Threading.Tasks;
#endif

#endregion

namespace AIO
{
    partial class Runner
    {
        [ContextStatic]
        private static readonly ConcurrentDictionary<IEnumerator, bool> QueuesCoroutine = new ConcurrentDictionary<IEnumerator, bool>();

        [ContextStatic]
        private static bool QueuesCoroutineState;

        #region 协程

        private static void UpdateCoroutine()
        {
            QueuesCoroutineState = true;
            foreach (var coroutine in QueuesCoroutine.Where(coroutine => !coroutine.Value))
            {
#if SUPPORT_UNITASK
                coroutine.Key.ToUniTask().Preserve().SuppressCancellationThrow();
#else
                instance.StartCoroutine(coroutine.Key);
#endif
                QueuesCoroutine[coroutine.Key] = true;
            }

            QueuesCoroutine.Where(coroutine => coroutine.Value)
                           .Select(coroutine => coroutine.Key)
                           .ToList()
                           .ForEach(coroutine => QueuesCoroutine.TryRemove(coroutine, out _));
            QueuesCoroutineState = false;
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void PushUpdate(IEnumerator coroutine) { QueuesCoroutine.TryAdd(coroutine, false); }

        #endregion
    }
}