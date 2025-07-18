#region

using System;
using System.Collections.Generic;
using System.IO;

#endregion

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
    public partial class BufferByte
        : Buffer<byte>,
          IWriteBasics, IReadBasics
    {
        /// <inheritdoc/>
        public BufferByte() { }

        /// <inheritdoc/>
        public BufferByte(byte[] bytes, int index = 0, int count = 0) : base(bytes, index, count) { }

        /// <inheritdoc/>
        public BufferByte(int capacity) : base(capacity) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BufferByte(FileSystemInfo info) : base(File.ReadAllBytes(info.FullName)) { }

        private void AutomaticExpansion(int place)
        {
            if (Arrays.Length <= WriteIndex + place) Capacity = WriteIndex + place;
        }

        /// <inheritdoc />
        public override void Write(Buffer<byte> data) { WriteByteArray(data); }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="start">开始下标</param>
        /// <param name="count">长度</param>
        /// <returns>返回数组</returns>
        public byte[] Read(int start, int count)
        {
            if (count <= 0) return Array.Empty<byte>();
            if (start + count > WriteIndex)
                throw new Exception(
                                    string.Format("数组越界: start + count > source length -> {0} + {1} = {2}",
                                                  start, count, WriteIndex));

            var buffer = new byte[count];
            Array.ConstrainedCopy(Arrays, start, buffer, 0, count);
            Array.Reverse(buffer);
            return buffer;
        }

        /// <inheritdoc />
        public override void Write(IList<byte> bytes, int pos, int len) { WriteByteArray(bytes); }

        /// <inheritdoc />
        public override void Write(ICollection<byte> bytes, int pos, int len) { WriteByteArray(bytes); }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Concat("Byte Buffer:{ offset = ", ReadIndex, ", top=", WriteIndex, ", data = ",
                                 AHelper.Hex.ToHex(ToArray()), '}');
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Clear();
            Arrays = null;
        }
    }
}