using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace AIO
{
    public partial class CPrint
    {
        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void RunDetail(string title, IEnumerable<Action> actions, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var all = new CPrintElapse(title).Start();
            var index = 1;
            foreach (var action in actions)
            {
                var p = new CPrintElapse(index++.ToString()).Start();
                action.Invoke();
                p.Finish(format);
            }

            all.Finish();
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Run(string title, Action action, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var p = new CPrintElapse(title).Start();
            action.Invoke();
            p.Finish(format);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Run(string title, Action action0, Action action1, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var p = new CPrintElapse(title).Start();
            action0.Invoke();
            action1.Invoke();
            p.Finish(format);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Run(string title, Action action0, Action action1, Action action2, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var p = new CPrintElapse(title).Start();
            action0.Invoke();
            action1.Invoke();
            action2.Invoke();
            p.Finish(format);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Run(string title, Action action0, Action action1, Action action2, Action action3, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var p = new CPrintElapse(title).Start();
            action0.Invoke();
            action1.Invoke();
            action2.Invoke();
            action3.Invoke();
            p.Finish(format);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Run(string title, ICollection<Action> actions, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var p = new CPrintElapse(title).Start();
            foreach (var action in actions) action.Invoke();
            p.Finish(format);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Run<T>(string title, Action<T> action, T tValue, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var p = new CPrintElapse(title).Start();
            action.Invoke(tValue);
            p.Finish(format);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Run<T, V>(string title, Action<T, V> action, T tValue, V vValue, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var p = new CPrintElapse(title).Start();
            action.Invoke(tValue, vValue);
            p.Finish(format);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Run<T, V, W>(string title, Action<T, V, W> action, T tValue, V vValue, W wValue, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var p = new CPrintElapse(title).Start();
            action.Invoke(tValue, vValue, wValue);
            p.Finish(format);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Run<T, V, W, Z>(string title, Action<T, V, W, Z> action, T tValue, V vValue, W wValue, Z zValue, string format = "g")
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            var p = new CPrintElapse(title).Start();
            action.Invoke(tValue, vValue, wValue, zValue);
            p.Finish(format);
        }

        /// <summary>
        /// 执行时间
        /// </summary>
        internal class CPrintElapse : PrintElapse
        {
            public CPrintElapse(string title) : base(title)
            {
            }

            public override void Finish(string format = "g")
            {
                stopWatch.Stop();
                Line(string.Format(CultureInfo.CurrentCulture, "{0}=>[{1}]", Title, stopWatch.Elapsed.ToString(format)));
            }
        }
    }
}