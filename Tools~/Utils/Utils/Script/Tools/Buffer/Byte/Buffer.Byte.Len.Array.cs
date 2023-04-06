using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public int[] ReadLenArray()
        {
            return Arrays.GetLenArray(ref ReadIndex);
        }

        /// <inheritdoc/> 
        public void WriteLenArray(in ICollection<int> value)
        {
            WriteLen(value.Count);
            foreach (var item in value) WriteLen(item);
        }
    }

    public partial interface IWrite
    {
        /// <summary>
        /// 写入Sbyte数组
        /// </summary>
        /// <param name="value">输入源</param>
        void WriteLenArray(in ICollection<int> value);
    }

    public partial interface IRead
    {
        /// <summary>
        /// 读取Sbyte数组
        /// </summary>
        /// <returns>返回数组</returns>
        int[] ReadLenArray();
    }
}