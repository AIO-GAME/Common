#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 对象池系统
    /// </summary>
    partial class PoolSystem<T>
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        public Func<T, int> OnGetEID;

        /// <summary>
        /// 创建对象
        /// </summary>
        public Func<object[], T> OnCreate;

        /// <summary>
        /// 使用中的对象
        /// </summary>
        private Dictionary<int, T> BusyPool { get; set; }

        /// <summary>
        /// 空闲的对象
        /// </summary>
        private Queue<T> FreePool { get; set; }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PoolSystem()
        {
            FreePool = Pool.Queue<T>();
            BusyPool = Pool.Dictionary<int, T>();
        }

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            BusyPool.Free();
            FreePool.Free();
        }

        #endregion

        /// <summary>
        ///  获取实体唯一ID
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual int GetEID(T entity) { return OnGetEID?.Invoke(entity) ?? entity.GetHashCode(); }

        /// <summary>
        ///  清空缓存数据
        /// </summary>
        private void Clear()
        {
            BusyPool.Clear();
            FreePool.Clear();
        }

        /// <summary>
        /// 分配对象
        /// </summary>
        protected virtual T CreateEntity(params object[] args)
        {
            if (OnCreate != null) return OnCreate.Invoke(args);
            return Activator.CreateInstance(typeof(T), args) is T entity ? entity : throw new Exception($"CreateInstance {typeof(T).Name} failed");
        }

        /// <summary>
        ///  添加存活对象
        /// </summary>
        /// <param name="entity">实体</param>
        private void AddSurviving(T entity)
        {
            var eid                                       = GetEID(entity);
            if (!BusyPool.ContainsKey(eid)) BusyPool[eid] = entity;
        }

        /// <summary>
        /// 移除存活对象
        /// </summary>
        /// <param name="entity">实体</param>
        private void RemoveSurviving(T entity)
        {
            FreePool.Enqueue(entity);
            var eid = Inst.GetEID(entity);
            if (BusyPool.ContainsKey(eid)) BusyPool.Remove(eid);
        }
    }
}