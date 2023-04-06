using System.Collections.Generic;

namespace AIO
{
    public partial interface IWrite
    {
        /// <summary>
        /// 写入Bool数组
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="reverse">是否反转</param>
        void WriteBoolArray(in ICollection<bool> value, in bool reverse = false);
    }

    public partial interface IRead
    {
        /// <summary>
        /// 读取Bool数组
        /// </summary>
        /// <param name="reverse">是否反转</param>
        /// <returns>Bool数组</returns>
        bool[] ReadBoolArray(in bool reverse = false);
    }

    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public bool[] ReadBoolArray(in bool reverse = false)
        {
            return Arrays.GetBoolArray(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteBoolArray(in ICollection<bool> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            AutomaticExpansion(value.Count);
            Arrays.SetBoolArray(ref WriteIndex, value, reverse);
        }
    }
}