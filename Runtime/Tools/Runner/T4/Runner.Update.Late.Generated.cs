using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIO
{
    partial class Runner
    {
        #region Delegation

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate(Delegate[] agrs)
        {
            switch (agrs.Length)
            {
                case 0: return;
                case 1:
                    PushUpdateLate(agrs[0]);
                    return;
                default:
                    QueuesDelegateUpdateLate.Enqueue(Delegate.Combine(agrs));
                    QueuesDelegateUpdateLateState = false;
                    break;
            }
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate(Delegate agr)
        {
            if (agr is null) throw new ArgumentNullException(nameof(agr));
            QueuesDelegateUpdateLate.Enqueue(agr);
            QueuesDelegateUpdateLateState = false;
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate(Delegate agr1, Delegate agr2)
        {
            Debug.Assert(agr1 != null, nameof(agr1));
            Debug.Assert(agr2 != null, nameof(agr2));
            QueuesDelegateUpdateLate.Enqueue(Delegate.Combine(agr1, agr2));
            QueuesDelegateUpdateLateState = false;
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate(Delegate agr1, Delegate agr2, Delegate agr3)
        {
            Debug.Assert(agr1 != null, nameof(agr1));
            Debug.Assert(agr2 != null, nameof(agr2));
            Debug.Assert(agr3 != null, nameof(agr3));
            QueuesDelegateUpdateLate.Enqueue(Delegate.Combine(agr1, agr2, agr3));
            QueuesDelegateUpdateLateState = false;
        }

        #endregion

        #region Action

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate(Action arg)
        {
            if (arg is null) throw new ArgumentNullException(nameof(arg));
            QueuesDelegateUpdateLate.Enqueue(arg);
            QueuesDelegateUpdateLateState = false;
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate(ICollection<Action> args)
        {
            if (args is null) throw new ArgumentNullException(nameof(args));
            if (args.Count == 0) return;
            foreach (var arg in args) QueuesDelegateUpdateLate.Enqueue(arg);
            QueuesDelegateUpdateLateState = false;
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate(Action agr1, Action agr2)
        {
            Debug.Assert(agr1 != null, nameof(agr1));
            Debug.Assert(agr2 != null, nameof(agr2));
            QueuesDelegateUpdateLate.Enqueue(Delegate.Combine(agr1, agr2));
            QueuesDelegateUpdateLateState = false;
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate(Action agr1, Action agr2, Action agr3)
        {
            Debug.Assert(agr1 != null, nameof(agr1));
            Debug.Assert(agr2 != null, nameof(agr2));
            Debug.Assert(agr3 != null, nameof(agr3));
            QueuesDelegateUpdateLate.Enqueue(Delegate.Combine(agr1, agr2, agr3));
            QueuesDelegateUpdateLateState = false;
        }

        #endregion
 
        #region Generic

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate<T1>(Action<T1> action, T1 param1)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            QueuesDelegateUpdateLate.Enqueue(new Action(Delegate));
            QueuesDelegateUpdateLateState = false;
            return;
            void Delegate() => action.Invoke(param1);
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            QueuesDelegateUpdateLate.Enqueue(new Action(Delegate));
            QueuesDelegateUpdateLateState = false;
            return;
            void Delegate() => action.Invoke(param1, param2);
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate<T1, T2, T3>(Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            QueuesDelegateUpdateLate.Enqueue(new Action(Delegate));
            QueuesDelegateUpdateLateState = false;
            return;
            void Delegate() => action.Invoke(param1, param2, param3);
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            QueuesDelegateUpdateLate.Enqueue(new Action(Delegate));
            QueuesDelegateUpdateLateState = false;
            return;
            void Delegate() => action.Invoke(param1, param2, param3, param4);
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            QueuesDelegateUpdateLate.Enqueue(new Action(Delegate));
            QueuesDelegateUpdateLateState = false;
            return;
            void Delegate() => action.Invoke(param1, param2, param3, param4, param5);
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            QueuesDelegateUpdateLate.Enqueue(new Action(Delegate));
            QueuesDelegateUpdateLateState = false;
            return;
            void Delegate() => action.Invoke(param1, param2, param3, param4, param5, param6);
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            QueuesDelegateUpdateLate.Enqueue(new Action(Delegate));
            QueuesDelegateUpdateLateState = false;
            return;
            void Delegate() => action.Invoke(param1, param2, param3, param4, param5, param6, param7);
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            QueuesDelegateUpdateLate.Enqueue(new Action(Delegate));
            QueuesDelegateUpdateLateState = false;
            return;
            void Delegate() => action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8);
        }

        /// <summary>
        /// Pushes the specified update action to the update late queue.
        /// </summary>
        public static void PushUpdateLate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            QueuesDelegateUpdateLate.Enqueue(new Action(Delegate));
            QueuesDelegateUpdateLateState = false;
            return;
            void Delegate() => action.Invoke(param1, param2, param3, param4, param5, param6, param7, param8, param9);
        }
        #endregion
    }
}