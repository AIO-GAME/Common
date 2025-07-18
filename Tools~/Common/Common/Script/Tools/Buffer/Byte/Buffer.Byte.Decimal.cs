using System.Collections.Generic;

namespace AIO
{
    partial class BufferByte : IWriteDecimal, IReadDecimal
    {
        /// <inheritdoc/> 
        public decimal ReadDecimal(bool reverse = false) { return Arrays.GetDecimal(ref ReadIndex, reverse); }

        /// <inheritdoc/> 
        public void WriteDecimal(decimal value, bool reverse = false)
        {
            var array = decimal.GetBits(value);
            WriteLen(array.Length);
            AutomaticExpansion(array.Length * 4);
            foreach (var item in array) WriteInt32(item, reverse);
        }

        /// <inheritdoc/>
        public decimal[] ReadDecimalArray(bool reverse = false)
        {
            var len   = ReadLen();
            var array = new decimal[len];
            for (var i = 0; i < len; i++)
            {
                array[i] = ReadDecimal(reverse);
            }

            return array;
        }

        /// <inheritdoc/>
        public void WriteDecimalArray(ICollection<decimal> value, bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value)
            {
                WriteDecimal(item, reverse);
            }
        }
    }
}