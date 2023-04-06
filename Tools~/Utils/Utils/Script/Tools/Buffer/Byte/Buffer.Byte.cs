/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// Buffer 读取接口
    /// </summary>
    public partial interface IRead
    {
    }

    /// <summary>
    /// Buffer 写入接口
    /// </summary>
    public partial interface IWrite
    {
        /// <summary> 
        /// 将指定字节缓冲区数据写入当前缓存区
        /// </summary>
        void Write(in Buffer<byte> data);


        /// <summary> 
        /// 将指定字节缓冲区数据写入当前缓存区
        /// </summary>/>
        void Write(in IList<byte> bytes, in int pos, in int len);

        /// <summary> 
        /// 将指定字节缓冲区数据写入当前缓存区
        /// </summary>
        void Write(in ICollection<byte> bytes, in int pos, in int len);
    }

    /// <summary>
    /// 字节缓存类
    /// </summary>
    /// 提供 write, read, set, get 方法
    /// write  :  将写入字节缓存,改变写入进度
    /// read   :  从字节缓存中读取,改变读取进度
    /// set    :  需要传入索引,在字节缓存的指定索引处写入一个,不影响缓存读写进度
    /// get    :  需要传入索引,在字节缓存的指定索引处读取一个,不影响缓存读写进度
    /// method_:  为倒序写入 倒序读取
    public partial class BufferByte : Buffer<byte>, IWrite, IRead
    {
        /// <inheritdoc/>
        public BufferByte()
        {
        }

        /// <inheritdoc/>
        public BufferByte(byte[] bytes) : base(bytes)
        {
        }

        /// <inheritdoc/>
        public BufferByte(int capacity) : base(capacity)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AutomaticExpansion(in int place)
        {
            if (Arrays.Length <= WriteIndex + place) Capacity = WriteIndex + place;
        }

        /// <inheritdoc cref="IWrite"/>
        public override void Write(in Buffer<byte> data)
        {
            WriteByteArray(data);
        }

        /// <inheritdoc cref="IWrite"/>
        public override void Write(in IList<byte> bytes, in int pos, in int len)
        {
            WriteByteArray(bytes);
        }

        /// <inheritdoc cref="IWrite"/>
        public override void Write(in ICollection<byte> bytes, in int pos, in int len)
        {
            WriteByteArray(bytes);
        }

        /// <inheritdoc/>
        public sealed override string ToString()
        {
            return string.Concat("ByteBuffer:{ offset = ", ReadIndex, ", top=", WriteIndex, ", data = ", Utils.Hex.ToHex(ToArray()), '}');
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Clear();
            Arrays = null;
        }
    }
}