#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AIO;

#endregion

namespace UnityEngine
{
    public partial class Print
    {
        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Run<T>(
            in string title,
            in T      actions,
            in string format = "g"
        )
        where T : IEnumerable<Action>
        {
            if (IsNotOut || NoStatus(LOG)) return;
            var all = new UPrintElapse(title).Start();
            var index = 1;
            foreach (var action in actions)
                using (var p = new UPrintElapse(index++.ToString()).Start())
                {
                    action.Invoke();
                    p.Finish(format);
                }

            all.Finish();
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Run(
            in string title,
            in Action action,
            in string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new UPrintElapse(title).Start())
            {
                action.Invoke();
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Run(
            in string title,
            in Action action0,
            in Action action1,
            in string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new UPrintElapse(title).Start())
            {
                action0.Invoke();
                action1.Invoke();
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Run(
            in string title,
            in Action action0,
            in Action action1,
            in Action action2,
            in string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new UPrintElapse(title).Start())
            {
                action0.Invoke();
                action1.Invoke();
                action2.Invoke();
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Run(
            in string title,
            in Action action0,
            in Action action1,
            in Action action2,
            in Action action3,
            in string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new UPrintElapse(title).Start())
            {
                action0.Invoke();
                action1.Invoke();
                action2.Invoke();
                action3.Invoke();
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Run<T>(
            in string    title,
            in Action<T> action,
            in T         tValue,
            in string    format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new UPrintElapse(title).Start())
            {
                action.Invoke(tValue);
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Run<T, V>(
            in string       title,
            in Action<T, V> action,
            in T            tValue,
            in V            vValue,
            in string       format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new UPrintElapse(title).Start())
            {
                action.Invoke(tValue, vValue);
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Run<T, V, W>(
            in string          title,
            in Action<T, V, W> action,
            in T               tValue,
            in V               vValue,
            in W               wValue,
            in string          format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new UPrintElapse(title).Start())
            {
                action.Invoke(tValue, vValue, wValue);
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION), DebuggerHidden, DebuggerNonUserCode]
        public static void Run<T, V, W, Z>(
            in string             title,
            in Action<T, V, W, Z> action,
            in T                  tValue,
            in V                  vValue,
            in W                  wValue,
            in Z                  zValue,
            in string             format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new UPrintElapse(title).Start())
            {
                action.Invoke(tValue, vValue, wValue, zValue);
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        public static R Run<T, R>(
            in string     title,
            in Func<T, R> action,
            in T          tValue,
            in string     format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return action.Invoke(tValue);
            R r;
            using (var p = new UPrintElapse(title).Start())
            {
                r = action.Invoke(tValue);
                p.Finish(format);
            }

            return r;
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        public static R Run<T, V, R>(
            in string        title,
            in Func<T, V, R> action,
            in T             tValue,
            in V             vValue,
            in string        format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return action.Invoke(tValue, vValue);
            R r;
            using (var p = new UPrintElapse(title).Start())
            {
                r = action.Invoke(tValue, vValue);
                p.Finish(format);
            }

            return r;
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        public static R Run<T, V, W, R>(
            in string           title,
            in Func<T, V, W, R> action,
            in T                tValue,
            in V                vValue,
            in W                wValue,
            in string           format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return action.Invoke(tValue, vValue, wValue);
            R r;
            using (var p = new UPrintElapse(title).Start())
            {
                r = action.Invoke(tValue, vValue, wValue);
                p.Finish(format);
            }

            return r;
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        public static R Run<T, V, W, Z, R>(
            in string              title,
            in Func<T, V, W, Z, R> action,
            in T                   tValue,
            in V                   vValue,
            in W                   wValue,
            in Z                   zValue,
            in string              format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return action.Invoke(tValue, vValue, wValue, zValue);
            R r;
            using (var p = new UPrintElapse(title).Start())
            {
                r = action.Invoke(tValue, vValue, wValue, zValue);
                p.Finish(format);
            }

            return r;
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        public static async Task Run<T>(
            string title,
            T      actions,
            string format = "g"
        )
        where T : IEnumerable<Task>
        {
            if (IsNotOut || NoStatus(LOG)) return;
            var all = new UPrintElapse(title).Start();
            var index = 1;
            foreach (var action in actions)
                using (var p = new UPrintElapse(index++.ToString()).Start())
                {
                    await action;
                    p.Finish(format);
                }

            all.Finish();
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        public static async Task Run(
            string title,
            Task   action,
            string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new UPrintElapse(title).Start())
            {
                await action;
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        public static async Task<R> Run<R>(
            string  title,
            Task<R> action,
            string  format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return await action;
            R r;
            using (var p = new UPrintElapse(title).Start())
            {
                r = await action;
                p.Finish(format);
            }

            return r;
        }

        #region Nested type: UPrintElapse

        /// <summary>
        /// 执行时间
        /// </summary>
        internal class UPrintElapse : PrintElapse
        {
            public UPrintElapse(in string title) : base(title) { }

            public override void Finish(string format = "g")
            {
                stopWatch.Stop();
                Log(string.Format("{0} {1} {2}",
                                  $"[<color=#E47833><b>{stopWatch.Elapsed.ToString(format)}</b></color>]".PadRight(30), "->", Title));
            }
        }

        #endregion
    }
}