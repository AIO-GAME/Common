#region

using System;

#endregion

namespace AIO
{
    partial class Runner
    {
        #region Turple Update

        /// <summary>
        /// 在 Update 中执行
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

            void Action()
            {
                action.Invoke(param1);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateFix<T1>(Action<T1> action, T1 param1)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateLate<T1>(Action<T1> action, T1 param1)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1);
            }
        }

        /// <summary>
        /// 在 Update 中执行
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

            void Action()
            {
                action.Invoke(param1, param2);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateFix<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateLate<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2);
            }
        }

        /// <summary>
        /// 在 Update 中执行
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

            void Action()
            {
                action.Invoke(param1, param2, param3);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateFix<T1, T2, T3>(Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateLate<T1, T2, T3>(Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3);
            }
        }

        /// <summary>
        /// 在 Update 中执行
        /// </summary>
        public static void Update<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateFix<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateLate<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4);
            }
        }

        /// <summary>
        /// 在 Update 中执行
        /// </summary>
        public static void Update<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateFix<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateLate<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5);
            }
        }

        /// <summary>
        /// 在 Update 中执行
        /// </summary>
        public static void Update<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateFix<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateLate<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6);
            }
        }

        /// <summary>
        /// 在 Update 中执行
        /// </summary>
        public static void Update<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateFix<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateLate<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7);
            }
        }

        /// <summary>
        /// 在 Update 中执行
        /// </summary>
        public static void Update<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateFix<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateLate<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
            }
        }

        /// <summary>
        /// 在 Update 中执行
        /// </summary>
        public static void Update<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Enqueue(new Action(Action));
                noActionQueueToExecuteUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateFix<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9);
            }
        }

        /// <summary>
        /// 在 Update Fix 中执行
        /// </summary>
        public static void UpdateLate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            lock (actionQueuesUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Enqueue(Action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }

            return;

            void Action()
            {
                action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9);
            }
        }

        #endregion

        #region Turple Update Fix

        #endregion
    }
}