using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class FlexibleDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        /// <summary>
        /// 获取V值
        /// </summary>
        public new TValue this[TKey key]
        {
            get { return base[key]; }
            set
            {
                if (ContainsKey(key))
                {
                    base[key] = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }
    }
}