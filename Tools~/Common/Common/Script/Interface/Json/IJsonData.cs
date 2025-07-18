#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 数据序列化
    /// </summary>
    public interface IJsonData
        : IDisposable,
          IJsonDeserialize,
          IJsonSerialize,
          IReset,
          ICloneable { }

    /// <summary>
    /// 二进制数据基类
    /// </summary>
    [Serializable]
    public abstract class JsonData : IJsonData
    {
        /// <summary>
        /// 重置数据
        /// </summary>
        public virtual void Dispose() { Reset(); }

        /// <inheritdoc />
        public abstract void Deserialize(IReadJsonData buffer);

        /// <inheritdoc />
        public abstract void Serialize(IWriteJsonData buffer);

        /// <inheritdoc />
        public object Clone() { return MemberwiseClone(); }

        /// <inheritdoc />
        public virtual void Reset() { }
    }
}