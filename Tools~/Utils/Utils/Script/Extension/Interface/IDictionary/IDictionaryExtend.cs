using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 字典扩展
    /// </summary>
    public static partial class IDictionaryExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="others"></param>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        public static void Union<TK, TV>(this IDictionary<TK, TV> dict, IDictionary<TK, TV> others)
        {
            if (dict == null || others == null) return;
            foreach (var kv in others)
            {
                if (!dict.ContainsKey(kv.Key))
                {
                    dict.Add(kv.Key, kv.Value);
                }
            }
        }
    }
}