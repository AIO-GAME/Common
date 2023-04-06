using System;
using System.Globalization;
using System.Text;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public float ReadFloat(in bool all = false, in bool reverse = false)
        {
            return Arrays.GetFloat(ref ReadIndex, all, reverse);
        }

        /// <inheritdoc/> 
        public void WriteFloat(in float value, bool all = false, in bool reverse = false)
        {
            var bytes = all ? Encoding.UTF8.GetBytes(value.ToString(NumberFormatInfo.CurrentInfo)) : BitConverter.GetBytes(value);
            WriteByteArray(bytes, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteFloat(in float value, bool all = false, in bool reverse = false);
    }

    public partial interface IRead
    {
        float ReadFloat(in bool all = false, in bool reverse = false);
    }
}