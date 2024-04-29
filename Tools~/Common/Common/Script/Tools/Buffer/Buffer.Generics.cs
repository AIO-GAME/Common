#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 缓冲流
    /// </summary>
    public partial class Buffer<T> : Buffer
    where T : struct
    {
        /// <summary>
        /// 构建一个指定容量的Buffer
        /// </summary>
        protected Buffer(int capacity = CAPACITY)
        {
            Arrays = new T[capacity < 8 ? 8 : capacity];
        }
        //
        // /// <summary>
        // /// 构建一个指定数据的ByteBuffer
        // /// </summary>
        // protected Buffer(T[] bytes)
        // {
        //     if (bytes == null || bytes.Length == 0)
        //     {
        //         Arrays = new T[CAPACITY];
        //         WriteIndex = 0;
        //     }
        //     else
        //     {
        //         WriteIndex = bytes.Length;
        //         Arrays = new T[WriteIndex];
        //         Array.ConstrainedCopy(bytes, 0, Arrays, 0, WriteIndex);
        //     }
        //
        //     ReadIndex = 0;
        // }

        /// <summary>
        /// 构建一个指定数据的ByteBuffer
        /// </summary>
        public Buffer(T[] bytes, int index = 0, int count = 0)
        {
            if (bytes == null || bytes.Length == 0)
            {
                Arrays     = new T[CAPACITY];
                WriteIndex = 0;
            }
            else
            {
                if (count == 0) count = bytes.Length;
                WriteIndex = count - index;
                Arrays     = new T[WriteIndex];
                Array.ConstrainedCopy(bytes, index, Arrays, 0, WriteIndex);
            }

            ReadIndex = 0;
        }

        /// <summary>
        /// 数据缓存
        /// </summary>
        internal T[] Arrays { get; set; }

        /// <summary> 
        /// 数据缓存:容量
        /// </summary>
        public override int Capacity
        {
            get => Arrays.Length;
            protected set
            {
                var c = Arrays.Length;
                if (value > c)
                {
                    // 每次扩大一倍 +1是防止i=0导致死循环
                    while (c < value) c = (c << 1) + 1;
                    var newArray = new T[c];
                    Array.ConstrainedCopy(Arrays, 0, newArray, 0, WriteIndex);
                    Arrays = newArray;
                }
            }
        }

        /// <summary>
        /// Is the buffer empty?
        /// </summary>
        public bool IsEmpty => Arrays is null || (WriteIndex == ReadIndex && ReadIndex == 0);

        /// <summary>
        /// 自动扩容一杯
        /// </summary>
        /// <remarks>Reserve the buffer of the given capacity</remarks>
        public void Reserve()
        {
            Capacity = Arrays.Length << (1 + 1);
        }

        /// <summary>
        /// 自动扩容
        /// </summary>
        /// <remarks>Reserve the buffer of the given capacity</remarks>
        public void Reserve(int capacity)
        {
            Capacity = capacity;
        }

        /// <summary> 
        /// 获取有效字节数组
        /// </summary>
        public virtual T[] ToArray()
        {
            var Len = WriteIndex - ReadIndex;
            if (Len <= 0) return Array.Empty<T>();
            var bytes = new T[Len];
            Array.ConstrainedCopy(Arrays, ReadIndex, bytes, 0, Len);
            return bytes;
        }

        /// <summary> 
        /// 将指定字节缓冲区数据写入当前缓存区
        /// </summary>
        public virtual void Write(Buffer<T> data)
        {
            Write(data.Arrays, data.ReadIndex, data.Count);
        }

        /// <summary> 
        /// 将指定字节缓冲区数据写入当前缓存区
        /// </summary>
        public virtual void Write(ICollection<T> bytes)
        {
            Write(bytes, 0, bytes.Count);
        }

        /// <summary>
        /// 写入byte数组(从position开始写入len个)
        /// </summary>
        public virtual void Write(IList<T> bytes, int pos, int len)
        {
            if (len <= 0) return;
            if (pos >= len) throw new IndexOutOfRangeException("pos should have less than len");

            var EndIndex = WriteIndex + len;
            if (Arrays.Length < EndIndex) Capacity               = EndIndex;
            for (var i = pos; i < len; i++) Arrays[WriteIndex++] = bytes[i];
            WriteIndex = EndIndex;
        }

        /// <summary>
        /// 写入byte数组(从position开始写入len个)
        /// </summary>
        public virtual void Write(ICollection<T> bytes, int pos, int len)
        {
            if (len <= 0) return;
            if (pos >= len) throw new IndexOutOfRangeException("pos should have less than len");

            var EndIndex = WriteIndex + len;
            if (Arrays.Length < EndIndex) Capacity = EndIndex;
            var i = pos;
            foreach (var item in bytes)
            {
                if (i++ >= len) break;
                Arrays[WriteIndex++] = item;
            }

            WriteIndex = EndIndex;
        }
    }
}