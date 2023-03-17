using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AIO
{
    public partial class CPrint
    {
        #region Array

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Array(Array objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var r = string.Format("Count: {0}\r\n", objs.Length);
            if (objs.Length > 0)
            {
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs) str.Append(item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }

            Console.WriteLine(r);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Array(string format, Array objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var r = string.Format("Count: {0}\r\n", objs.Length);
            if (objs.Length > 0)
            {
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs) str.AppendFormat(format, item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }

            Console.WriteLine(r);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Array(ArrayList objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs) str.Append(item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }

            Console.WriteLine(r);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Array(string format, ArrayList objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs) str.AppendFormat(format, item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }

            Console.WriteLine(r);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Array<T>(ICollection<T> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs)
                    str.Append(index++).Append(':').Append(item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }

            Console.WriteLine(r);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Array<T>(string format, ICollection<T> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs)
                    str.AppendFormat(format, item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }

            Console.WriteLine(r);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Array<T>(string format, IList<T> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs)
                    str.AppendFormat(format, item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }

            Console.WriteLine(r);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Array<T>(IList<T> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs)
                    str.Append(index++).Append(':').Append(item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }

            Console.WriteLine(r);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Array<TK, TV>(IDictionary<TK, TV> objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            if (objs is null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var dic in objs)
                {
                    if (dic.Value != null)
                    {
                        str.Append($"[{index++}]{dic.Key?.ToString()}:{dic.Value}\r\n");
                    }
                    else str.Append($"[{index++}]{dic.Key?.ToString()}:NULL\r\n");
                }

                r = str.Remove(r.Length - 2, 2).ToString();
            }

            Console.WriteLine(r);
        }

        #endregion
    }
}