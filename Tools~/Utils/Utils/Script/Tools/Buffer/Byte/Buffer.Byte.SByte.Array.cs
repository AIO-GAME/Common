using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public sbyte[] ReadSByteArray(in bool reverse = false)
        {
            return Arrays.GetSByteArray(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteSByteArray(in ICollection<sbyte> value, in bool reverse = false)
        {
            AutomaticExpansion(value.Count);
            Arrays.SetSByteArray(ref WriteIndex, value, reverse);
        }
    }

    public partial interface IWrite
    {
        /// <summary>
        /// 写入Sbyte数组
        /// </summary>
        /// <param name="value">输入源</param>
        /// <param name="reverse">是否反转</param>
        void WriteSByteArray(in ICollection<sbyte> value, in bool reverse = false);
    }

    public partial interface IRead
    {
        /// <summary>
        /// 读取Sbyte数组
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>返回数组</returns>
        sbyte[] ReadSByteArray(in bool reverse = false);
    }
}