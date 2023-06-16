using System;

namespace AIO
{
    /// <summary>
    /// 数据缓存留
    /// </summary>
    public partial class Buffer
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
        /// 当前写入位置
        /// </summary>
        protected int WriteIndex;

        /// <summary>
        /// 当前读取位置
        /// </summary>
        protected int ReadIndex;

        /// <summary>
        /// 初始化
        /// </summary>
        protected Buffer()
        {
            WriteIndex = ReadIndex = 0;
        }

        /// <summary> 
        /// 清空
        /// </summary>
        public virtual void Clear()
        {
            WriteIndex = ReadIndex = 0;
        }

        /// <summary>
        /// 缓存写入进度: 当前首位
        /// </summary>
        public virtual int WriteOffset
        {
            get => WriteIndex;
            set
            {
                if (value < ReadIndex) Console.WriteLine($"Set Top invalid top : {value}");
                if (value > Capacity) Capacity = value;
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
                if (value < 0 || value > WriteIndex) Console.WriteLine($"Set Offset invalid offset : {value}");
                ReadIndex = value;
            }
        }

        /// <summary> 
        /// 返回数据可读取长度
        /// </summary>
        public virtual int Count => WriteIndex - ReadIndex;

        /// <summary> 
        /// 数据缓存:容量
        /// </summary>
        public virtual int Capacity { get; protected set; }

        /// <summary>
        /// 跳过
        /// </summary>
        public void Skip(int count)
        {
            ReadIndex += count + 1;
        }

        /// <summary>
        /// 检查剩余数量 
        /// Flase:不满足
        /// Ture:满足
        /// </summary>
        public bool CheckSize(int size)
        {
            return WriteIndex - ReadIndex >= size;
        }
    }
}