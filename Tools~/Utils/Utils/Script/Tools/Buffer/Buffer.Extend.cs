namespace AIO
{
    using System;
    using System.Collections.Generic;

    public partial class Buffer<T>
    {
        /// <summary>
        /// 增加
        /// </summary>
        public static Buffer<T> operator +(Buffer<T> Buffer, in Buffer<T> Target)
        {
            Buffer.Write(Target.ToArray());
            return Buffer;
        }

        /// <summary>
        /// 增加
        /// </summary>
        public static Buffer<T> operator +(Buffer<T> Buffer, in T[] Target)
        {
            Buffer.Write(Target);
            return Buffer;
        }

        /// <summary>
        /// 增加
        /// </summary>
        public static Buffer<T> operator +(Buffer<T> Buffer, in ICollection<T> Target)
        {
            Buffer.Write(Target);
            return Buffer;
        }

        /// <summary>
        /// 隐式转化为数组
        /// </summary>
        public static implicit operator T[](in Buffer<T> Buffer)
        {
            return Buffer.ToArray();
        }

        /// <summary>
        /// 隐式转化为数组
        /// </summary>
        public static implicit operator HashSet<T>(in Buffer<T> Buffer)
        {
            return new HashSet<T>(Buffer.ToArray());
        }

        /// <summary>
        /// 隐式转化为List
        /// </summary>
        public static implicit operator List<T>(in Buffer<T> Buffer)
        {
            return new List<T>(Buffer.ToArray());
        }
    }
}
