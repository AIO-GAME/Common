
namespace AIO
{
    using System;

    /// <summary>
    /// 游戏数据
    /// </summary>
    public abstract class GBean :
        IBinDeserialize,
        IBinSerialize,
        IReset,
        IDisposable
    {

        /// <inheritdoc/>
        public abstract void Deserialize(IReadData buffer);

        /// <inheritdoc/>
        public abstract void Serialize(IWriteData buffer);

        /// <inheritdoc/>
        public virtual void Initialize()
        {
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// 首次数据存放
        /// </summary>
        public virtual void First()
        {
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public virtual void Clean()
        {
        }

        /// <summary>
        /// 重置
        /// </summary>
        public virtual void Reset()
        {
        }
    }
}
