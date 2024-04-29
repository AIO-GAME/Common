#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

        /// <inheritdoc/> 
        public double ReadDouble(bool all = false, bool reverse = false)
        {
            return Arrays.GetDouble(ref ReadIndex, all, reverse);
        }

        /// <inheritdoc/> 
        public double[] ReadDoubleArray(bool all, bool reverse = false)
        {
            return Arrays.GetDoubleArray(ref ReadIndex, all, reverse);
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteDouble(double value, bool all = false, bool reverse = false)
        {
            var bytes = all ? Encoding.UTF8.GetBytes(value.ToString(NumberFormatInfo.CurrentInfo)) : BitConverter.GetBytes(value);
            WriteByteArray(bytes, reverse);
        }

        /// <inheritdoc/> 
        public void WriteDoubleArray(ICollection<double> value, bool all, bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value) WriteDouble(item, all, reverse);
        }

        #endregion
    }
}