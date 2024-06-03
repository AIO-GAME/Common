#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 游戏数据
    /// </summary>
    public abstract class GBean
        : IBinDeserialize,
          IBinSerialize,
          IReset,
          IDisposable
    {
        #region IBinDeserialize Members

        /// <inheritdoc/>
        public abstract void Deserialize(IReadData buffer);

        #endregion

        #region IBinSerialize Members

        /// <inheritdoc/>
        public abstract void Serialize(IWriteData buffer);

        #endregion

        #region IDisposable Members

        /// <inheritdoc/>
        public virtual void Dispose() { }

        #endregion

        #region IReset Members

        /// <summary>
        /// 重置
        /// </summary>
        public virtual void Reset() { }

        #endregion

        /// <inheritdoc/>
        public virtual void Initialize() { }

        /// <summary>
        /// 首次数据存放
        /// </summary>
        public virtual void First() { }

        /// <summary>
        /// 清空数据
        /// </summary>
        public virtual void Clean() { }
    }
}