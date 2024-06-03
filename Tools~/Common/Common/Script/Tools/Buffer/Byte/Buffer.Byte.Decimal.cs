#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class BufferByte
    {
        #region IReadData Members

        /// <inheritdoc/> 
        public decimal ReadDecimal(bool reverse = false)
        {
            return Arrays.GetDecimal(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public decimal[] ReadDecimalArray(bool reverse = false)
        {
            return Arrays.GetDecimalArray(ref ReadIndex, reverse);
        }

        #endregion

        #region IWriteData Members

        /// <inheritdoc/> 
        public void WriteDecimal(decimal value, bool reverse = false)
        {
            var array = decimal.GetBits(value);
            WriteLen(array.Length);
            AutomaticExpansion(array.Length * 4);
            foreach (var item in array) Arrays.SetInt32(ref WriteIndex, item, reverse);
        }

        /// <inheritdoc/> 
        public void WriteDecimalArray(ICollection<decimal> value, bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value) WriteDecimal(item, reverse);
        }

        #endregion
    }
}