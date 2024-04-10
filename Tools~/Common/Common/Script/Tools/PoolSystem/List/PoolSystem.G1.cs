#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    public abstract partial class PoolSystem<T> : IDisposable
    where T : new()
    {
        /// <summary>
        /// 实例
        /// </summary>
        protected static PoolSystem<T> Instance { get; private set; }

        /// <summary>
        /// 是否存在实例
        /// </summary>
        public static bool IsInstance
        {
            [DebuggerStepThrough] get => Instance != null;
        }

        /// <summary>
        /// 初始化系统
        /// </summary>
        protected static void CreateInstance<TY>()
        where TY : PoolSystem<T>, new()
        {
            if (Instance is null) Instance = Activator.CreateInstance<TY>();
        }

        /// <summary>
        ///  添加存活对象
        /// </summary>
        /// <param name="entity">实体</param>
        protected void AddSurviving(T entity)
        {
            var eid = GetEID(entity);
            if (!BusyPool.ContainsKey(eid)) BusyPool[eid] = entity;
        }

        /// <summary>
        /// 移除存活对象
        /// </summary>
        /// <param name="entity">实体</param>
        protected void RemoveSurviving(T entity)
        {
            FreePool.Enqueue(entity);
            var eid = Instance.GetEID(entity);
            if (Instance.BusyPool.ContainsKey(eid)) Instance.BusyPool.Remove(eid);
        }

        /// <summary>
        /// 卸载系统
        /// </summary>
        public static void UnInstall()
        {
            Instance.Dispose();
            Instance = null;
        }

        /// <summary>
        /// 清理缓存数据
        /// </summary>
        public static void ClearCache()
        {
            Instance.Clear();
        }

        #region Allocate

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns>实体</returns>
        public static T Alloc()
        {
            var entity = Instance.FreePool.Count == 0 ? Instance.CreateEntity() : Instance.FreePool.Dequeue();
            Instance.AddSurviving(entity);
            return entity;
        }

        #endregion

        #region Find

        /// <summary>
        /// 查询正在使用的对象
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> FindAll()
        {
            return Instance.BusyPool.Select(item => item.Value);
        }

        /// <summary>
        /// 查询正在使用的对象
        /// </summary>
        /// <param name="id">实例ID</param>
        /// <returns>实例单位对象</returns>
        public static T Find(in int id)
        {
            Instance.BusyPool.TryGetValue(id, out var entity);
            return entity;
        }

        /// <summary>
        /// 查询正在使用的对象
        /// </summary>
        /// <param name="ids">实例ID列表</param>
        /// <returns>实例单位对象</returns>
        public static IEnumerable<T> Find(IEnumerable<int> ids)
        {
            return from id in ids where Instance.BusyPool.ContainsKey(id) select Instance.BusyPool[id];
        }

        #endregion

        #region Recycle

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="entity">实体</param>
        public static void Recycle(T entity)
        {
            Instance.RemoveSurviving(entity);
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="eid">实体ID</param>
        public static void Recycle(int eid)
        {
            if (Instance.BusyPool.TryGetValue(eid, out var entity)) Recycle(entity);
        }

        /// <summary>
        /// 回收所有正在使用的对象
        /// </summary>
        public static void RecycleAll()
        {
            foreach (var entity in Instance.BusyPool) Instance.FreePool.Enqueue(entity.Value);

            Instance.BusyPool.Clear();
        }

        #endregion
    }
}