#region

using System;
using System.IO;

#endregion

namespace AIO
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class RingBufferExtend
    {
        /// <summary>
        /// 读取数据流
        /// </summary>
        public static void Read(this BufferRing<byte> buffer, Stream stream, in int count)
        {
            if (count > buffer.Count) CS.WriteLine($"bufferList length < count, {buffer.Count} {count}");
            var alreadyCopyCount = 0;
            while (alreadyCopyCount < count)
            {
                var n = count - alreadyCopyCount;
                if (buffer.Capacity - buffer.ReadOffset > n) //实现方法同上类似
                {
                    stream.Write(buffer.First, buffer.ReadOffset, n);
                    buffer.ReadOffset += n;
                    alreadyCopyCount  += n;
                }
                else
                {
                    stream.Write(buffer.First, buffer.ReadOffset, buffer.Capacity - buffer.ReadOffset);
                    alreadyCopyCount += buffer.Capacity - buffer.ReadOffset;
                    buffer.RemoveFirst();
                }
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        public static void Write(this BufferRing<byte> buffer, Stream stream)
        {
            var count = (int)(stream.Length - stream.Position);
            var alreadyCopyCount = 0;
            while (alreadyCopyCount < count)
            {
                if (buffer.WriteOffset == buffer.Capacity) buffer.AddLast();

                var n = count - alreadyCopyCount;
                if (buffer.Capacity - buffer.WriteOffset > n)
                {
                    _                  =  stream.Read(buffer, buffer.WriteOffset, n);
                    buffer.WriteOffset += count - alreadyCopyCount;
                    alreadyCopyCount   += n;
                }
                else
                {
                    _                  =  stream.Read(buffer, buffer.WriteOffset, buffer.Capacity - buffer.WriteOffset);
                    alreadyCopyCount   += buffer.Capacity - buffer.WriteOffset;
                    buffer.WriteOffset =  buffer.Capacity;
                }
            }
        }
    }
}