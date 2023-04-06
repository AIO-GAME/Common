namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public sbyte ReadSByte()
        {
            return (sbyte)Arrays[ReadIndex++];
        }

        /// <inheritdoc/> 
        public void WriteSByte(in sbyte value)
        {
            AutomaticExpansion(1);
            Arrays[WriteIndex++] = (byte)value;
        }
    }

    public partial interface IWrite
    {
        /// <summary>
        /// 写入Sbyte数组
        /// </summary>
        /// <param name="value">输入源</param>
        void WriteSByte(in sbyte value);
    }

    public partial interface IRead
    {
        /// <summary>
        /// 读取Sbyte数组
        /// </summary>
        /// <returns>返回数组</returns>
        sbyte ReadSByte();
    }
}