#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

#endregion

namespace AIO
{
    partial class BufferByte : IWriteFloat, IReadFloat
    {
        /// <inheritdoc/> 
        public float ReadFloat(bool reverse = false) { return Arrays.GetFloat(ref ReadIndex, true, reverse); }

        /// <inheritdoc/> 
        public void WriteFloat(float value, bool reverse = false)
        {
            var bytes = Encoding.UTF8.GetBytes(value.ToString(NumberFormatInfo.CurrentInfo));
            WriteByteArray(bytes, reverse);
        }

        /// <inheritdoc/>
        public float[] ReadFloatArray(bool reverse = false)
        {
            var len    = ReadLen();
            var floats = new float[len];
            for (var i = 0; i < len; i++)
            {
                var bytes = Arrays.GetByteArray(ref ReadIndex, reverse);
                floats[i] = float.Parse(Encoding.UTF8.GetString(bytes), NumberFormatInfo.CurrentInfo);
            }

            return floats;
        }

        /// <inheritdoc/>
        public void WriteFloatArray(ICollection<float> value, bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value)
            {
                var bytes = Encoding.UTF8.GetBytes(item.ToString(NumberFormatInfo.CurrentInfo));
                WriteByteArray(bytes, reverse);
            }
        }
    }
}