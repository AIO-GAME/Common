using System.Collections.Generic;

namespace AIO
{
    public static partial class ExtendIDictionary
    {
        /// <summary>
        /// 移除存在的元素
        /// </summary>
        public static void Remove<TK, TV>(this IDictionary<TK, TV> dict, in IDictionary<TK, TV> others)
        {
            if (dict == null || others == null) return;
            foreach (var kv in others)
            {
                if (dict.ContainsKey(kv.Key))
                {
                    dict.Remove(kv.Key);
                }
            }
        }

        /// <summary>
        /// 移除存在的元素
        /// </summary>
        public static void Remove<TK, TV>(this IDictionary<TK, TV> dict, in IEnumerable<TK> others)
        {
            if (dict == null || others == null) return;
            foreach (var kv in others)
            {
                if (dict.ContainsKey(kv)) dict.Remove(kv);
            }
        }

        /// <summary>
        /// 移除存在的元素
        /// </summary>
        public static void Remove<TK, TV>(this IDictionary<TK, TV> dict, params TK[] others)
        {
            if (dict == null || others == null) return;
            foreach (var kv in others)
            {
                if (dict.ContainsKey(kv)) dict.Remove(kv);
            }
        }
    }
}