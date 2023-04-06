namespace AIO
{
    public partial interface IWrite
    {
        /// <summary>
        /// 写入Bool
        /// </summary>
        void WriteBool(in bool b);
    }

    public partial interface IRead
    {
        /// <summary>
        /// 读取Bool
        /// </summary>
        bool ReadBool();
    }

    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public bool ReadBool()
        {
            return Arrays[ReadIndex++] != 0;
        }

        /// <inheritdoc/> 
        public void WriteBool(in bool b)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = (byte)(b ? 1 : 0);
        }
    }
}