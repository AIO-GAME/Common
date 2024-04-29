#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    partial class ExtendIDictionary
    {
        /// <summary>
        /// 添加相同元素
        /// </summary>
        public static void AddUnion<TK, TV>(this IDictionary<TK, TV> dict, in IDictionary<TK, TV> others)
        {
            if (dict == null || others == null) return;
            foreach (var kv in others)
                if (!dict.ContainsKey(kv.Key))
                    dict.Add(kv.Key, kv.Value);
        }
    }
}