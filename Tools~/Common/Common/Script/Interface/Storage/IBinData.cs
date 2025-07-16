#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 数据序列化
    /// </summary>
    public interface IBinData
        : IDisposable,
          IBinDeserialize,
          IBinSerialize,
          IReset,
          ICloneable { }

    /// <summary>
    /// 二进制数据基类
    /// </summary>
    public abstract class BinData : IBinData
    {
        public BinData() { }

        /// <summary>
        /// 重置数据
        /// </summary>
        public virtual void Dispose() { Reset(); }

        /// <inheritdoc />
        public abstract void Deserialize(IReadData buffer);

        /// <inheritdoc />
        public abstract void Serialize(IWriteData buffer);

        /// <inheritdoc />
        public object Clone()
        {
            var bin = new BufferByte();
            Serialize(bin);
            var instance = Activator.CreateInstance(GetType());
            if (bin.Count <= 0) return instance;

            if (!(instance is IBinDeserialize clone)) return instance;
            clone.Deserialize(bin);
            return clone;
        }

        /// <inheritdoc />
        public virtual void Reset() { }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <typeparam name="T"> 数据类型 </typeparam>
        /// <returns> 解析后的数据 </returns>
        public static T Parser<T>(IReadData buffer)
        where T : IBinDeserialize, new()
        {
            var parser = Activator.CreateInstance<T>();
            parser.Deserialize(buffer);
            return parser;
        }
    }
}