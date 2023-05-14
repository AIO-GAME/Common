using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 该结构体实现了IEqualityComparer接口，用于比较ConversionQuery类型的对象
    /// </summary>
    internal struct ConversionQueryComparer : IEqualityComparer<ConversionQuery>
    {
        /// <summary>
        /// 重写Equals方法，比较两个ConversionQuery对象是否相等
        /// </summary>
        /// <param name="x">第一个ConversionQuery对象</param>
        /// <param name="y">第二个ConversionQuery对象</param>
        /// <returns>如果两个对象相等，则返回true，否则返回false</returns>
       
        public bool Equals(ConversionQuery x, ConversionQuery y)
        {
            return x.Equals(y);
        }

        /// <summary>
        /// 重写GetHashCode方法，获取ConversionQuery对象的哈希码
        /// </summary>
        /// <param name="obj">ConversionQuery对象</param>
        /// <returns>ConversionQuery对象的哈希码</returns>
        public int GetHashCode(ConversionQuery obj)
        {
            return obj.GetHashCode();
        }
    }
}