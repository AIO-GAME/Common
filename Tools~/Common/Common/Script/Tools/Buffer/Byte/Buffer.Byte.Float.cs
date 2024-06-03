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
        public float ReadFloat(bool all = false, bool reverse = false)
        {
            return Arrays.GetFloat(ref ReadIndex, all, reverse);
        }

        /// <inheritdoc/> 
        public float[] ReadFloatArray(bool all, bool reverse = false)
        {
            return Arrays.GetFloatArray(ref ReadIndex, all, reverse);
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteFloat(float value, bool all = false, bool reverse = false)
        {
            var bytes = all ? Encoding.UTF8.GetBytes(value.ToString(NumberFormatInfo.CurrentInfo)) : BitConverter.GetBytes(value);
            WriteByteArray(bytes, reverse);
        }

        /// <inheritdoc/> 
        public void WriteFloatArray(ICollection<float> value, bool all, bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value) WriteFloat(item, all, reverse);
        }

        #endregion
    }
}