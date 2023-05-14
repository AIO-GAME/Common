using System;
using System.Collections.Generic;
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
        public double[] ReadDoubleArray(in bool all, in bool reverse = false)
        {
            return Arrays.GetDoubleArray(ref ReadIndex, all, reverse);
        }

        /// <inheritdoc/> 
        public void WriteDouble(in double value, bool all = false, in bool reverse = false)
        {
            var bytes = all ? Encoding.UTF8.GetBytes(value.ToString(NumberFormatInfo.CurrentInfo)) : BitConverter.GetBytes(value);
            WriteByteArray(bytes, reverse);
        }

        /// <inheritdoc/> 
        public void WriteDoubleArray(in ICollection<double> value, in bool all, in bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value) WriteDouble(item, all, reverse);
        }
    }
}