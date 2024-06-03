#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class CPrint
    {
        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static async void RunAsync(string title, Task action, string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new CPrintElapse(title).Start())
            {
                await action;
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static async void RunAsync<T>(string title, T actions, string format = "g")
        where T : IEnumerable<Task>
        {
            if (IsNotOut || NoStatus(LOG)) return;
            var all   = new CPrintElapse(title).Start();
            var index = 1;
            foreach (var action in actions)
                using (var p = new CPrintElapse(index++.ToString()).Start())
                {
                    await action;
                    p.Finish(format);
                }

            all.Finish();
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Run<T>(in string title, in T actions, in string format = "g")
        where T : IEnumerable<Action>
        {
            if (IsNotOut || NoStatus(LOG)) return;
            var all   = new CPrintElapse(title).Start();
            var index = 1;
            foreach (var action in actions)
                using (var p = new CPrintElapse(index++.ToString()).Start())
                {
                    action.Invoke();
                    p.Finish(format);
                }

            all.Finish();
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Run(in string title, in Action action, in string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new CPrintElapse(title).Start())
            {
                action.Invoke();
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Run(in string title, in Action action0, in Action action1, in string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new CPrintElapse(title).Start())
            {
                action0.Invoke();
                action1.Invoke();
                p.Finish(format);
            }
        }

        /// <summary>
        ///  输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Run(in string title,
                               in Action action0,
                               in Action action1,
                               in Action action2,
                               in string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new CPrintElapse(title).Start())
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
        [Conditional(MACRO_DEFINITION)]
        public static void Run(in string title,
                               in Action action0,
                               in Action action1,
                               in Action action2,
                               in Action action3,
                               in string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new CPrintElapse(title).Start())
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
        [Conditional(MACRO_DEFINITION)]
        public static void Run<T>(in string title, in Action<T> action, in T tValue, in string format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new CPrintElapse(title).Start())
            {
                action.Invoke(tValue);
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Run<T, V>(in string       title,
                                     in Action<T, V> action,
                                     in T            tValue,
                                     in V            vValue,
                                     in string       format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new CPrintElapse(title).Start())
            {
                action.Invoke(tValue, vValue);
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Run<T, V, W>(in string          title,
                                        in Action<T, V, W> action,
                                        in T               tValue,
                                        in V               vValue,
                                        in W               wValue,
                                        in string          format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new CPrintElapse(title).Start())
            {
                action.Invoke(tValue, vValue, wValue);
                p.Finish(format);
            }
        }

        /// <summary>
        /// 输出详细执行时间
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Run<T, V, W, Z>(in string             title,
                                           in Action<T, V, W, Z> action,
                                           in T                  tValue,
                                           in V                  vValue,
                                           in W                  wValue,
                                           in Z                  zValue,
                                           in string             format = "g")
        {
            if (IsNotOut || NoStatus(LOG)) return;
            using (var p = new CPrintElapse(title).Start())
            {
                action.Invoke(tValue, vValue, wValue, zValue);
                p.Finish(format);
            }
        }

        #region Nested type: CPrintElapse

        /// <summary>
        /// 执行时间
        /// </summary>
        internal class CPrintElapse : PrintElapse
        {
            public CPrintElapse(string title) : base(title) { }

            protected internal override void Finish(string format = "g")
            {
                stopWatch.Stop();
                Log($"{$"{stopWatch.Elapsed.ToString(format, CultureInfo.CurrentCulture)}",-10} -> {Title}");
            }
        }

        #endregion
    }
}