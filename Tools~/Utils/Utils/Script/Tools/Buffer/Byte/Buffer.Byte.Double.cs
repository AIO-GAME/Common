using System;
using System.Globalization;
using System.Text;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public double ReadDouble(in bool all = false, in bool reverse = false)
        {
            return Arrays.GetDouble(ref ReadIndex, all, reverse);
        }

        /// <inheritdoc/> 
        public void WriteDouble(in double value, bool all = false, in bool reverse = false)
        {
            var bytes = all ? Encoding.UTF8.GetBytes(value.ToString(NumberFormatInfo.CurrentInfo)) : BitConverter.GetBytes(value);
            WriteByteArray(bytes, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteDouble(in double value, bool all = false, in bool reverse = false);
    }

    public partial interface IRead
    {
        double ReadDouble(in bool all = false, in bool reverse = false);
    }
}