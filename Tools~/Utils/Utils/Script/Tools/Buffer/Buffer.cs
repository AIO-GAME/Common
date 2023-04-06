/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2020-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public partial class Buffer<T>
    {
        /// <summary>
        /// 默认容量:32B = 256bit
        /// </summary>
        public const int CAPACITY = 32;

        /// <summary>
        /// 最大缓存:4M
        /// </summary>
        public const int MAX_CAPACITY = 1024 * 1024 * 4;

        /// <summary>
        /// 数据缓存
        /// </summary>
        protected T[] Arrays { get; set; }

        /// <summary>
        /// 当前写入位置
        /// </summary>
        protected int WriteIndex;

        /// <summary>
        /// 当前读取位置
        /// </summary>
        protected int ReadIndex;
    }

    /// <summary>
    /// 缓冲流
    /// </summary>
    public abstract partial class Buffer<T> where T : struct
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Buffer()
        {
            WriteIndex = ReadIndex = 0;
        }

        /// <summary>
        /// 构建一个指定容量的Buffer
        /// </summary>
        public Buffer(int Capacity = CAPACITY) : this()
        {
            if (Capacity < 8) Capacity = 8;
            Arrays = new T[Capacity];
        }

        /// <summary>
        /// 构建一个指定数据的ByteBuffer
        /// </summary>
        public Buffer(T[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                Arrays = new T[CAPACITY];
                WriteIndex = 0;
            }
            else
            {
                Arrays = bytes;
                WriteIndex = bytes.Length;
            }
            ReadIndex = 0;
        }

        /// <summary>
        /// 缓存写入进度: 当前首位
        /// </summary>
        public virtual int WriteOffset
        {
            get => WriteIndex;
            set
            {
                if (value < ReadIndex) Console.WriteLine($"SetTop invalid top : {value}");
                if (value > Arrays.Length) Capacity = value;
                WriteIndex = value;
            }
        }

        /// <summary>
        /// 缓存写入进度: 当前读取游标下标
        /// </summary>
        public virtual int ReadOffset
        {
            get => ReadIndex;
            set
            {
                if (value < 0 || value > WriteIndex) Console.WriteLine($"SetOffset invalid offset : {value}");
                ReadIndex = value;
            }
        }

        /// <summary> 
        /// 返回数据可读取长度
        /// </summary>
        public virtual int Count
        {
            get { return WriteIndex - ReadIndex; }
        }

        /// <summary> 
        /// 数据缓存:容量
        /// </summary>
        public virtual int Capacity
        {
            get => Arrays.Length;
            protected set
            {
                var c = Arrays.Length;
                if (value > c)
                {   // 每次扩大一倍 +1是防止i=0导致死循环
                    while (c < value) c = (c << 1) + 1;
                    T[] newArray = new T[c];
                    Array.Copy(Arrays, 0, newArray, 0, WriteIndex);
                    Arrays = newArray;
                }
            }
        }

        /// <summary> 
        /// 清空
        /// </summary>
        public virtual void Clear()
        {
            WriteIndex = ReadIndex = 0;
        }

        /// <summary> 
        /// 获取有效字节数组
        /// </summary>
        public virtual T[] ToArray()
        {
            var Len = WriteIndex - ReadIndex;
            if (Len == 0) return new T[0];
            var bytes = new T[Len];
            Array.Copy(Arrays, ReadIndex, bytes, 0, Len);
            return bytes;
        }

        /// <summary> 
        /// 将指定字节缓冲区数据写入当前缓存区
        /// </summary>
        public virtual void Write(in Buffer<T> data)
        {
            Write(data.Arrays, data.ReadIndex, data.Count);
        }

        /// <summary> 
        /// 将指定字节缓冲区数据写入当前缓存区
        /// </summary>
        public virtual void Write(in ICollection<T> data)
        {
            Write(data, 0, data.Count);
        }

        /// <summary> 
        /// 写入byte数组
        /// </summary>
        public virtual void Write(in IList<T> bytes)
        {
            Write(bytes, 0, bytes.Count);
        }

        /// <summary>
        /// 写入byte数组(从position开始写入len个)
        /// </summary>
        public virtual void Write(in IList<T> bytes, in int pos, in int len)
        {
            if (len <= 0) return;
            if (pos >= len) throw new IndexOutOfRangeException("pos should have less than len");

            var EndIndex = WriteIndex + len;
            if (Arrays.Length < EndIndex) Capacity = EndIndex;
            for (int i = pos; i < len; i++) Arrays[WriteIndex + i] = bytes[i];
            WriteIndex = EndIndex;
        }


        /// <summary>
        /// 写入byte数组(从position开始写入len个)
        /// </summary>
        public virtual void Write(in ICollection<T> bytes, in int pos, in int len)
        {
            if (len <= 0) return;
            if (pos >= len) throw new IndexOutOfRangeException("pos should have less than len");

            var EndIndex = WriteIndex + len;
            if (Arrays.Length < EndIndex) Capacity = EndIndex;
            var i = pos;
            foreach (var item in bytes)
            {
                if (i++ >= len) break;
                Arrays[WriteIndex + i] = item;
            }
            WriteIndex = EndIndex;
        }
    }
}
