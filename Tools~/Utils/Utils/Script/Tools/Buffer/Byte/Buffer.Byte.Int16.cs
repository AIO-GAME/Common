namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public short ReadInt16(in bool reverse = false)
        {
            return Arrays.GetInt16(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public ushort ReadUInt16(in bool reverse = false)
        {
            return Arrays.GetUInt16(ref ReadIndex, reverse);
        }

        /// <inheritdoc/> 
        public void WriteInt16(in short value, in bool reverse = false)
        {
            AutomaticExpansion(2);
            Arrays.SetInt16(ref WriteIndex, value, reverse);
        }

        /// <inheritdoc/> 
        public void WriteUInt16(in ushort value, in bool reverse = false)
        {
            AutomaticExpansion(2);
            Arrays.SetUInt16(ref WriteIndex, value, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteInt16(in short value, in bool reverse = false);
        void WriteUInt16(in ushort value, in bool reverse = false);
    }

    public partial interface IRead
    {
        ushort ReadUInt16(in bool reverse = false);
        short ReadInt16(in bool reverse = false);
    }
}