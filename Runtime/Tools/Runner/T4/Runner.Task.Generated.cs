using System;
using System.Collections;
#if SUPPORT_UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace AIO
{
    partial class Runner
    {
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
        /// </summary>
        /// <param name="action">The coroutine to start.</param>
        public static async void StartTask(Action action)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(action);
#else
                await Task.Factory.StartNew(action);
#endif
            }
            else
            {
                PushUpdate(action);
            }
        }
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static async void StartTask<T1>(Action<T1> action, T1 t1)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(Action);
#else
                await Task.Factory.StartNew(Action);
#endif
            }
            else
            {
                PushUpdate(action, t1);
            }
            return;
            void Action() { action?.Invoke(t1); }
        }
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static async void StartTask<T1, T2>(Action<T1, T2> action, T1 t1, T2 t2)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(Action);
#else
                await Task.Factory.StartNew(Action);
#endif
            }
            else
            {
                PushUpdate(action, t1, t2);
            }
            return;
            void Action() { action?.Invoke(t1, t2); }
        }
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static async void StartTask<T1, T2, T3>(Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(Action);
#else
                await Task.Factory.StartNew(Action);
#endif
            }
            else
            {
                PushUpdate(action, t1, t2, t3);
            }
            return;
            void Action() { action?.Invoke(t1, t2, t3); }
        }
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static async void StartTask<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(Action);
#else
                await Task.Factory.StartNew(Action);
#endif
            }
            else
            {
                PushUpdate(action, t1, t2, t3, t4);
            }
            return;
            void Action() { action?.Invoke(t1, t2, t3, t4); }
        }
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static async void StartTask<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(Action);
#else
                await Task.Factory.StartNew(Action);
#endif
            }
            else
            {
                PushUpdate(action, t1, t2, t3, t4, t5);
            }
            return;
            void Action() { action?.Invoke(t1, t2, t3, t4, t5); }
        }
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static async void StartTask<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(Action);
#else
                await Task.Factory.StartNew(Action);
#endif
            }
            else
            {
                PushUpdate(action, t1, t2, t3, t4, t5, t6);
            }
            return;
            void Action() { action?.Invoke(t1, t2, t3, t4, t5, t6); }
        }
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <param name="action">The coroutine to start.</param>
        public static async void StartTask<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(Action);
#else
                await Task.Factory.StartNew(Action);
#endif
            }
            else
            {
                PushUpdate(action, t1, t2, t3, t4, t5, t6, t7);
            }
            return;
            void Action() { action?.Invoke(t1, t2, t3, t4, t5, t6, t7); }
        }
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
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
        public static async void StartTask<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(Action);
#else
                await Task.Factory.StartNew(Action);
#endif
            }
            else
            {
                PushUpdate(action, t1, t2, t3, t4, t5, t6, t7, t8);
            }
            return;
            void Action() { action?.Invoke(t1, t2, t3, t4, t5, t6, t7, t8); }
        }
     
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
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
        public static async void StartTask<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
        {
            if (IsAllowThread)
            {
#if SUPPORT_UNITASK
                await UniTask.RunOnThreadPool(Action);
#else
                await Task.Factory.StartNew(Action);
#endif
            }
            else
            {
                PushUpdate(action, t1, t2, t3, t4, t5, t6, t7, t8, t9);
            }
            return;
            void Action() { action?.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9); }
        }

#if SUPPORT_UNITASK
        /// <summary>
        /// Starts a task on a thread pool or the main thread.
        /// </summary>
        public static async void StartTask(IEnumerator action)
        {
            if (IsAllowThread)
            {
                await action;
            }
            else
            {
                PushUpdate(action);
            }
        }
#endif
    }
}