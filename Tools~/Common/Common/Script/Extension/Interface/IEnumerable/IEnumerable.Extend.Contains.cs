using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class IEnumerableExtend
    {
        /// <summary>
        /// 判断集合中 是否有重复
        /// </summary>
        /// <param name="chars">集合</param>
        /// <param name="targetChars">匹配集合</param>
        /// <returns>Ture:存在 False:不存在</returns>
        public static bool Contains<T>(this IEnumerable<T> chars, in IEnumerable<T> targetChars)
        {
            if (chars == null || targetChars == null) return false;
            return chars.Intersect(targetChars).Any();
        }
        
    }
}