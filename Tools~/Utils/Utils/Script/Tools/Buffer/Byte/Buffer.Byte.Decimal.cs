namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public decimal ReadDecimal(in bool reverse = false)
        {
            return Arrays.GetDecimal(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteDecimal(in decimal value, in bool reverse = false)
        {
            var array = decimal.GetBits(value);
            WriteLen(array.Length);
            AutomaticExpansion(array.Length * 4);
            foreach (var item in array) Arrays.SetInt32(ref WriteIndex, item, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteDecimal(in decimal value, in bool reverse = false);
    }

    public partial interface IRead
    {
        decimal ReadDecimal(in bool reverse = false);
    }
}