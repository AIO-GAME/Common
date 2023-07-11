﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-11
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using AIO;

namespace AIO
{
    /// <summary>
    /// 对象池系统
    /// </summary>
    /// <typeparam name="E">对象池泛型</typeparam>
    /// <typeparam name="T">实体泛型</typeparam>
    public abstract partial class PoolSystem<T, E> : IDisposable where T : PoolSystem<T, E>, new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PoolSystem()
        {
            BusyPool = Pool.Dictionary<int, List<E>>();
            FreePool = Pool.Dictionary<int, Queue<E>>();
            Surviving = Pool.Dictionary<int, E>();
        }

        /// <summary>
        ///  获取实体唯一ID
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual int GetEID(E entity)
        {
            return entity.GetHashCode();
        }

        /// <summary>
        ///  获取配置类型唯一ID
        /// </summary>
        /// <param name="entity">实体</param>
        protected abstract int GetTID(E entity);

        /// <summary>
        ///  清空缓存数据
        /// </summary>
        protected virtual void ClearCache()
        {
        }

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}