#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 一维对象池系统
    /// </summary>
    /// <typeparam name="E">对象池泛型</typeparam>
    /// <typeparam name="T">实体泛型</typeparam>
    public abstract partial class PoolSystem<T, E> : IDisposable
    where T : PoolSystem<T, E>, new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PoolSystem()
        {
            FreePool = Pool.Queue<E>();
            BusyPool = Pool.Dictionary<int, E>();
        }

        #region IDisposable Members

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose() { }

        #endregion

        /// <summary>
        ///  获取实体唯一ID
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual int GetEID(E entity)
        {
            return entity.GetHashCode();
        }

        /// <summary>
        ///  清空缓存数据
        /// </summary>
        protected virtual void ClearCache() { }
    }
}