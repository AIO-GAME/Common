﻿/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2020-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 缓冲流
    /// </summary>
    public abstract partial class Buffer<T> : Buffer where T : struct
    {
        /// <summary>
        /// 构建一个指定容量的Buffer
        /// </summary>
        protected Buffer(in int capacity = CAPACITY)
        {
            Arrays = new T[capacity < 8 ? 8 : capacity];
        }

        /// <summary>
        /// 数据缓存
        /// </summary>
        protected T[] Arrays { get; set; }

        /// <summary>
        /// 构建一个指定数据的ByteBuffer
        /// </summary>
        protected Buffer(in T[] bytes)
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
                    Array.Copy(Arrays, 0, newArray, 0, WriteIndex);
                    Arrays = newArray;
                }
            }
        }

        /// <summary> 
        /// 获取有效字节数组
        /// </summary>
        public virtual T[] ToArray()
        {
            var Len = WriteIndex - ReadIndex;
            if (Len <= 0) return Array.Empty<T>();
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
            for (var i = pos; i < len; i++) Arrays[WriteIndex + i] = bytes[i];
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