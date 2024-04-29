#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    partial class PoolSystem<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PoolSystem()
        {
            FreePool = Pool.Queue<T>();
            BusyPool = Pool.Dictionary<int, T>();
        }

        /// <summary>
        /// 使用中的对象
        /// </summary>
        protected Dictionary<int, T> BusyPool { get; private set; }

        /// <summary>
        /// 空闲的对象
        /// </summary>
        protected Queue<T> FreePool { get; private set; }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; set; }

        #region IDisposable Members

        /// <inheritdoc />
        public virtual void Dispose()
        {
            BusyPool.Free();
            FreePool.Free();
        }

        #endregion

        /// <summary>
        ///  获取实体唯一ID
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual int GetEID(T entity)
        {
            return entity.GetHashCode();
        }

        /// <summary>
        ///  清空缓存数据
        /// </summary>
        protected virtual void Clear()
        {
            BusyPool.Clear();
            FreePool.Clear();
        }

        /// <summary>
        /// 分配对象
        /// </summary>
        protected abstract T CreateEntity();
    }
}