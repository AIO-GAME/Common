namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public byte ReadByte()
        {
            return Arrays[ReadIndex++];
        }

        /// <inheritdoc/> 
        public void WriteByte(in byte value)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = value;
        }
    }

    public partial interface IWrite
    {
        void WriteByte(in byte value);
    }

    public partial interface IRead
    {
        byte ReadByte();
    }
}