namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public int ReadInt32(in bool reverse = false)
        {
            return Arrays.GetInt32(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public uint ReadUInt32(in bool reverse = false)
        {
            return Arrays.GetUInt32(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt32(in int value, in bool reverse = false)
        {
            AutomaticExpansion(4);
            Arrays.SetInt32(ref WriteIndex, value, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt32(in uint value, in bool reverse = false)
        {
            AutomaticExpansion(4);
            Arrays.SetUInt32(ref WriteIndex, value, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteInt32(in int value, in bool reverse = false);
        void WriteUInt32(in uint value, in bool reverse = false);
    }

    public partial interface IRead
    {
        int ReadInt32(in bool reverse = false);
        uint ReadUInt32(in bool reverse = false);
    }
}