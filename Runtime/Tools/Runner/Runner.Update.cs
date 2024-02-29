#define ENABLE_UPDATE_FUNCTION_CALLBACK


#if SUPPORT_UNITASK
using Cysharp.Threading.Tasks;
#endif

#if (ENABLE_UPDATE_FUNCTION_CALLBACK)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIO
{
    partial class Runner
    {
        /// <summary>
        /// Update等待队列
        /// </summary>
        [ContextStatic] private static readonly Queue<Delegate> actionQueuesUpdateFunc = new Queue<Delegate>();

        /// <summary>
        /// 执行状态
        /// </summary>
        [ContextStatic] private static volatile bool noActionQueueToExecuteUpdateFunc = true;

        #region 协程

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void Update(IEnumerator coroutine)
        {
#if SUPPORT_UNITASK
            coroutine.ToUniTask().Preserve().SuppressCancellationThrow();
#else
            instance.StartCoroutine(coroutine);
#endif
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void Update(Func<IEnumerator> coroutine)
        {
#if SUPPORT_UNITASK
            coroutine.Invoke().ToUniTask().Preserve().SuppressCancellationThrow();
#else
            instance.StartCoroutine(coroutine?.Invoke());
#endif
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void Update(params Func<IEnumerator>[] coroutines)
        {
            if (coroutines.Length == 0) return;
            foreach (var coroutine in coroutines)
            {
#if SUPPORT_UNITASK
                coroutine.Invoke().ToUniTask().Preserve().SuppressCancellationThrow();
#else
                instance.StartCoroutine(coroutine?.Invoke());
#endif
            }
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void Update(IList<Func<IEnumerator>> coroutines)
        {
            if (coroutines is null || coroutines.Count == 0) return;
            for (var index = 0; index < coroutines.Count; index++)
            {
#if SUPPORT_UNITASK
                coroutines[index]?.Invoke().ToUniTask().Preserve().SuppressCancellationThrow();
#else
                instance.StartCoroutine(coroutines[index]?.Invoke());
#endif
            }
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        public static void Update(params IEnumerator[] coroutines)
        {
            if (coroutines.Length == 0) return;
            foreach (var coroutine in coroutines)
            {
#if SUPPORT_UNITASK
                coroutine.ToUniTask().Preserve().SuppressCancellationThrow();
#else
                    instance.StartCoroutine(coroutine);
#endif
            }
        }

        #endregion

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update<T1>(Action<T1> action, T1 param1)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;
            void Action() => action.Invoke(param1);
        }

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;
            void Action() => action.Invoke(param1, param2);
        }

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update<T1, T2, T3>(Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;
            void Action() => action.Invoke(param1, param2, param3);
        }

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action,
            T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;
            void Action() => action.Invoke(param1, param2, param3, param4);
        }

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update(Action action)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(action);
                noActionQueueToExecuteUpdateFunc = false;
            }
        }

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update(params Delegate[] delegates)
        {
            if (delegates.Length == 0) return;
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(delegates.Length == 1 ? delegates[0] : Delegate.Combine(delegates));
                noActionQueueToExecuteUpdateFunc = false;
            }
        }

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update(Delegate action)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(action);
                noActionQueueToExecuteUpdateFunc = false;
            }
        }

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update(Action action1, Action action2)
        {
            if (action1 is null) throw new ArgumentNullException(nameof(action1));
            if (action2 is null) throw new ArgumentNullException(nameof(action2));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(Delegate.Combine(action1, action2));
                noActionQueueToExecuteUpdateFunc = false;
            }
        }

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update(params Action[] actions)
        {
            if (actions.Length == 0) return;
            lock (actionQueuesUpdateFunc)
            {
                foreach (var action in actions) actionQueuesUpdateFunc.Enqueue(action);
                noActionQueueToExecuteUpdateFunc = false;
            }
        }

        /// <summary>
        /// 在Update中执行
        /// </summary>
        public static void Update(IList<Action> actions)
        {
            if (actions is null) throw new ArgumentNullException(nameof(actions));
            if (actions.Count == 0) return;
            lock (actionQueuesUpdateFunc)
            {
                for (var index = 0; index < actions.Count; index++) actionQueuesUpdateFunc.Enqueue(actions[index]);
                noActionQueueToExecuteUpdateFunc = false;
            }
        }

        partial class ThreadMono
        {
            public void Update()
            {
                //判断当前是否有操作正在执行
                if (noActionQueueToExecuteUpdateFunc) return;

                //清空队列中 残留的操作函数
                lock (actionQueuesUpdateFunc)
                {
                    if (actionQueuesUpdateFunc.Count == 0) return;
                    noActionQueueToExecuteUpdateFunc = true; //并开启执行状态
                    lock (mActionCopiedQueueUpdateFunc)
                    {
                        mActionCopiedQueueUpdateFunc.AddRange(actionQueuesUpdateFunc); //将等待队列中的操作 复制到执行队列中
                        actionQueuesUpdateFunc.Clear();
                        var tempArray = Delegate.Combine(mActionCopiedQueueUpdateFunc.ToArray());
                        mActionCopiedQueueUpdateFunc.Clear();
#if SUPPORT_UNITASK
                        Action().ToUniTask().Preserve().SuppressCancellationThrow();
#else
                        StartCoroutine(Action());
#endif
                        return;

                        IEnumerator Action()
                        {
                            tempArray?.DynamicInvoke();
                            tempArray = null;
                            yield break;
                        }
                    }
                }
            }
        }
    }

#endif
}