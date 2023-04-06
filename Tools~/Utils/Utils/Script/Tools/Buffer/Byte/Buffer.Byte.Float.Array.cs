using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public float[] ReadFloatArray(in bool all, in bool reverse = false)
        {
            return Arrays.GetFloatArray(ref ReadIndex, all, reverse);
        }

        /// <inheritdoc/> 
        public void WriteFloatArray(in ICollection<float> value, in bool all, in bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value) WriteFloat(item, all, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteFloatArray(in ICollection<float> value, in bool all, in bool reverse = false);
    }

    public partial interface IRead
    {
        float[] ReadFloatArray(in bool all, in bool reverse = false);
    }
}