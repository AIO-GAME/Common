#region namespace

#if SUPPORT_UNITASK
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
#endif
using System;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    partial class Runner
    {
#if SUPPORT_UNITASK
        /// <summary>
        /// 开一个新的作业执行函数
        /// </summary>
        public static async void StartTask<T>(T action)
        where T : System.Collections.IEnumerator
        {
            if (IsAllowThread)
            {
                await action;
            }
            else
            {
                Update(action);
            }
        }
#endif

        /// <summary>
        /// 开一个新的作业执行函数
        /// </summary>
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
                Update(action);
            }
        }

        /// <summary>
        /// 开一个新的作业执行函数
        /// </summary>
        public static async void StartTask<T>(Action<T> action, T state)
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
                Update(Action);
            }

            return;

            void Action() { action.Invoke(state); }
        }

        /// <summary>
        /// 开一个新的作业执行函数
        /// </summary>
        public static async void StartTask(Func<Action> action)
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
                Update(action);
            }
        }
    }
}