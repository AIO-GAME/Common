/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2021-11-04                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// 闭环数据流
    /// </summary>
    /// 参考链接
    /// <see>
    ///     <cref>https://ifeve.com/dissecting-disruptor-whats-so-special/</cref>
    /// </see>
    public sealed class BufferRing<T> : Buffer<T> where T : struct
    {
        /// <summary>
        /// 存储队列 该队列专门存储数据作为对象池
        /// </summary>
        private static readonly Dictionary<int, ConcurrentQueue<T[]>> FreedDic = new Dictionary<int, ConcurrentQueue<T[]>>();

        /// <summary>
        /// 读写队列 该队列专门处理数据读写
        /// </summary>
        private readonly Queue<T[]> UsedDic;

        /// <summary>
        /// 容量
        /// </summary>
        public override int Capacity { get; protected set; }

        /// <summary>
        /// 第一个读取的数组长度 队列最上面的数据数组
        /// </summary>
        public T[] First
        {
            get
            {
                if (UsedDic.Count == 0)
                    AddLast();
                return UsedDic.Peek();
            }
        }

        /// <summary>
        /// 最后读取的数组长度 队列最下面的数据数组
        /// </summary>
        public T[] Last
        {
            get
            {
                if (UsedDic.Count == 0)
                    AddLast();
                return Arrays;
            }
        }

        /// <summary>
        /// 当前可读取数据长度
        /// </summary>
        public override int Count
        {
            get
            {
                if (UsedDic.Count == 0) return 0;
                return ((UsedDic.Count - 1) * Capacity) + WriteIndex - ReadIndex;
            }
        }

        /// <summary>
        /// 缓存写入进度: 当前首位
        /// </summary>
        public override int WriteOffset
        {
            get => WriteIndex;
            set
            {
                if (value < ReadIndex)
                    Console.WriteLine($"Set Top invalid top : {value}");
                WriteIndex = value;
            }
        }

        /// <summary> 返回:有效字节数组 </summary>
        public override T[] ToArray()
        {
            var len = WriteIndex - ReadIndex; //当前数组剩余长度
            if (len < 0) throw new ArgumentOutOfRangeException($"Ring Buffer Error : WriteIndex - ReadIndex < 0 !!!");
            var bytes = new T[UsedDic.Count * Capacity + len]; //全部数据
            var index = 0;
            foreach (var item in UsedDic.ToArray())
            {
                if (index == UsedDic.Count - 1) break;
                Array.Copy(item, 0, bytes, index++ * (Capacity), Capacity);
            }

            Array.Copy(Arrays, ReadIndex, bytes, (UsedDic.Count - 1) * Capacity, len);
            return bytes;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BufferRing(int capacity = 4096)
        {
            UsedDic = new Queue<T[]>();
            if (!FreedDic.ContainsKey(capacity))
                FreedDic.Add(capacity, new ConcurrentQueue<T[]>());
            Capacity = capacity;
            AddLast();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BufferRing(IList<T> buffer, int capacity = 4096) : this(capacity)
        {
            Write(buffer);
        }

        /// <summary>
        /// 添加 读写数据数组 进入 读写队列
        /// </summary>
        internal void AddLast()
        {
            T[] buffer;
            // 对象池中有废弃数组 可以入列到 读写数据队列
            if (FreedDic[Capacity].Count > 0)
                FreedDic[Capacity].TryDequeue(out buffer);
            // 否则 则重新创建一个byte[SIZE] 数组
            else buffer = new T[Capacity];

            UsedDic.Enqueue(buffer); //
            Arrays = buffer;

            ReadIndex = WriteIndex = 0;
        }

        /// <summary>
        /// 移除最上层的数据 添加进入到对象池
        /// </summary>
        internal void RemoveFirst()
        {
            if (UsedDic.Count > 0)
                FreedDic[Capacity].Enqueue(UsedDic.Dequeue());
            ReadIndex = 0;
        }

        /// <summary>
        /// 读取
        /// </summary>
        public int Read(T[] buffer)
        {
            return Read(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 读取
        /// </summary>
        public T[] Read(int count)
        {
            var Buffer = new T[count];
            Read(Buffer, 0, count);
            return Buffer;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="Buffer">字节数组</param>
        /// <param name="Offset">开始值 偏移量</param>
        /// <param name="count">读取长度</param>
        /// <returns>实际读取长度</returns>
        public int Read(T[] Buffer, int Offset, int count)
        {
            if (Buffer.Length < Offset + count)
                Console.WriteLine($"Buffer List length < count, buffer length: {Buffer.Length} {Offset} {count}");

            var length = this.Count;
            // 传入读取长度 比 存储容量小的时候 则默认读取 存储容量的最大值 并将实际读取长度返回
            if (length < count) count = length;

            var alreadyCopyCount = 0;
            // 拷贝的长度 小余 实际读取长度 则继续拷贝
            while (alreadyCopyCount < count)
            {
                // 剩余拷贝长度
                var n = count - alreadyCopyCount;
                if (Capacity - ReadIndex > n)
                {
                    // 队列数组剩余容量 大于 实际需要拷贝长度 则正常拷贝
                    Array.Copy(First, ReadIndex, Buffer, alreadyCopyCount + Offset, n);
                    ReadIndex += n;
                    alreadyCopyCount += n;
                }
                else
                {
                    // 队列数组剩余容量不足 则先把剩余数组数据拷贝 再重置读写开始位置 最后让处理数据队列 mUsed 出列一个
                    Array.Copy(First, ReadIndex, Buffer, alreadyCopyCount + Offset, Capacity - ReadIndex);
                    alreadyCopyCount += Capacity - ReadIndex;
                    RemoveFirst();
                }
            }

            return count;
        }


        /// <inheritdoc />
        public override void Write(in IList<T> buffer)
        {
            if (buffer == null) return;
            Write(buffer, 0, buffer.Count);
        }

        /// <inheritdoc />
        public override void Write(in IList<T> buffer, in int offset, in int count)
        {
            if (buffer == null) return;
            var alreadyCopyCount = 0;

            while (alreadyCopyCount < count)
            {
                if (WriteIndex == Capacity) //如果写入的开始位置 等于 容量 则在队列新增一个数组
                {
                    AddLast();
                }

                var n = count - alreadyCopyCount;
                if (Capacity - WriteIndex > n) //写入数组长度 大余 实际写入长度 正常写入
                {
                    //Array.Copy(buffer, alreadyCopyCount + offset, Arrays, WriteIndex, n);
                    for (var i = alreadyCopyCount + offset; i < n; i++)
                        Arrays[WriteIndex + i] = buffer[i];

                    WriteIndex += count - alreadyCopyCount;
                    alreadyCopyCount += n;
                }
                else //否则 则只会写入当前写入数组容量最大值为止 再次进入 写入位置 容量大小判断 进入循环
                {
                    //Array.Copy(buffer, alreadyCopyCount + offset, Arrays, WriteIndex, Capacity - WriteIndex);
                    for (var i = alreadyCopyCount + offset; i < Capacity - WriteIndex; i++)
                        Arrays[WriteIndex + i] = buffer[i];

                    alreadyCopyCount += Capacity - WriteIndex;
                    WriteIndex = Capacity;
                }
            }
        }

        /// <inheritdoc />
        public override void Clear()
        {
            while (UsedDic.Count > 0)
                RemoveFirst();
            AddLast();
        }

        /// <summary>
        /// 判断Last数组是否满
        /// </summary>
        public void IsLastFull()
        {
            if (WriteIndex == Capacity)
                AddLast();
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            while (UsedDic.Count > 0)
                RemoveFirst();
        }
    }

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
            if (count > buffer.Count) Console.WriteLine($"bufferList length < count, {buffer.Count} {count}");
            var alreadyCopyCount = 0;
            while (alreadyCopyCount < count)
            {
                var n = count - alreadyCopyCount;
                if (buffer.Capacity - buffer.ReadOffset > n) //实现方法同上类似
                {
                    stream.Write(buffer.First, buffer.ReadOffset, n);
                    buffer.ReadOffset += n;
                    alreadyCopyCount += n;
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
                if (buffer.WriteOffset == buffer.Capacity)
                {
                    buffer.AddLast();
                }

                var n = count - alreadyCopyCount;
                if (buffer.Capacity - buffer.WriteOffset > n)
                {
                    _ = stream.Read(buffer, buffer.WriteOffset, n);
                    buffer.WriteOffset += count - alreadyCopyCount;
                    alreadyCopyCount += n;
                }
                else
                {
                    _ = stream.Read(buffer, buffer.WriteOffset, buffer.Capacity - buffer.WriteOffset);
                    alreadyCopyCount += buffer.Capacity - buffer.WriteOffset;
                    buffer.WriteOffset = buffer.Capacity;
                }
            }
        }
    }
}