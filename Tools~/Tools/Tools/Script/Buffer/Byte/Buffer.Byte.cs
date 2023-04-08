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
    /// 字节缓存类
    /// </summary>
    /// 提供 write, read, set, get 方法
    /// write  :  将写入字节缓存,改变写入进度
    /// read   :  从字节缓存中读取,改变读取进度
    /// set    :  需要传入索引,在字节缓存的指定索引处写入一个,不影响缓存读写进度
    /// get    :  需要传入索引,在字节缓存的指定索引处读取一个,不影响缓存读写进度
    /// method_:  为倒序写入 倒序读取
    public sealed partial class BufferByte : Buffer<byte>, IWriteData, IReadData
    {
        /// <inheritdoc/>
        public BufferByte()
        {
        }

        /// <inheritdoc/>
        public BufferByte(in byte[] bytes) : base(bytes)
        {
        }

        /// <inheritdoc/>
        public BufferByte(in int capacity) : base(capacity)
        {
        }

        private void AutomaticExpansion(in int place)
        {
            if (Arrays.Length <= WriteIndex + place) Capacity = WriteIndex + place;
        }

        /// <inheritdoc />
        public override void Write(in Buffer<byte> data)
        {
            WriteByteArray(data);
        }

        /// <inheritdoc />
        public override void Write(in IList<byte> bytes, in int pos, in int len)
        {
            WriteByteArray(bytes);
        }

        /// <inheritdoc />
        public override void Write(in ICollection<byte> bytes, in int pos, in int len)
        {
            WriteByteArray(bytes);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Concat("Byte Buffer:{ offset = ", ReadIndex, ", top=", WriteIndex, ", data = ", Utils.Hex.ToHex(ToArray()), '}');
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Clear();
            Arrays = null;
        }
    }
}