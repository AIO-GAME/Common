namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public long ReadInt64(in bool reverse = false)
        {
            return Arrays.GetInt64(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public ulong ReadUInt64(in bool reverse = false)
        {
            return Arrays.GetUInt64(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt64(in long value, in bool reverse = false)
        {
            AutomaticExpansion(8);
            Arrays.SetInt64(ref WriteIndex, value, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt64(in ulong value, in bool reverse = false)
        {
            AutomaticExpansion(8);
            Arrays.SetUInt64(ref WriteIndex, value, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteInt64(in long value, in bool reverse = false);
        void WriteUInt64(in ulong value, in bool reverse = false);
    }

    public partial interface IRead
    {
        long ReadInt64(in bool reverse = false);
        ulong ReadUInt64(in bool reverse = false);
    }
}