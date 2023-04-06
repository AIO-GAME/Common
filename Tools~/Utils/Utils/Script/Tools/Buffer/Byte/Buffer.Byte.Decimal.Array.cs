using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public decimal[] ReadDecimalArray(in bool reverse = false)
        {
            return Arrays.GetDecimalArray(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteDecimalArray(in ICollection<decimal> value, in bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value) WriteDecimal(item, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteDecimalArray(in ICollection<decimal> value, in bool reverse = false);
    }

    public partial interface IRead
    {
        decimal[] ReadDecimalArray(in bool reverse = false);
    }
}