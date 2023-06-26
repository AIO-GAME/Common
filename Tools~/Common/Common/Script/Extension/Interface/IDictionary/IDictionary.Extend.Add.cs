using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class IDictionaryExtend
    {
        /// <summary>
        /// 添加相同元素
        /// </summary>
        public static void AddUnion<TK, TV>(this IDictionary<TK, TV> dict, in IDictionary<TK, TV> others)
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