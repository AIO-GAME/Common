#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace AIO
{
    /// <summary>
    /// 对象池系统
    /// </summary>
    public static class PoolSystem
    {
        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="entity">实体</param>
        public static void Recycle<T>(T entity)
        where T : new()
        {
            PoolSystem<T>.Recycle(entity);
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="id">实体ID</param>
        public static void Recycle<T>(int id)
        where T : new()
        {
            PoolSystem<T>.Recycle(id);
        }

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns>实体</returns>
        public static T Alloc<T>()
        where T : new()
        {
            return PoolSystem<T>.Alloc();
        }

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns>实体</returns>
        public static T Alloc<T>(params object[] args)
        where T : new()
        {
            return PoolSystem<T>.Alloc(args);
        }

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns>实体</returns>
        public static void Alloc<T>(out T entity)
        where T : new()
        {
            entity = PoolSystem<T>.Alloc();
        }
    }

    /// <summary>
    /// 对象池系统
    /// </summary>
    internal partial class PoolSystem<T> : Singleton<PoolSystem<T>>, IDisposable
    where T : new()
    {
        /// <summary>
        /// 卸载系统
        /// </summary>
        public static void UnInstall() { Inst.Dispose(); }

        /// <summary>
        /// 清理缓存数据
        /// </summary>
        public static void ClearCache() { Inst.Clear(); }

        #region Allocate

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns>实体</returns>
        public static T Alloc(params object[] args)
        {
            var entity = Inst.FreePool.Count == 0 ? Inst.CreateEntity(args) : Inst.FreePool.Dequeue();
            Inst.AddSurviving(entity);
            return entity;
        }

        /// <summary>
        /// 分配对象
        /// </summary>
        /// <returns>实体</returns>
        public static T Alloc()
        {
            var entity = Inst.FreePool.Count == 0 ? Inst.CreateEntity() : Inst.FreePool.Dequeue();
            Inst.AddSurviving(entity);
            return entity;
        }

        #endregion

        #region Find

        /// <summary>
        /// 查询正在使用的对象
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> FindAll() { return Inst.BusyPool.Select(item => item.Value); }

        /// <summary>
        /// 查询正在使用的对象
        /// </summary>
        /// <param name="id">实例ID</param>
        /// <returns>实例单位对象</returns>
        public static T Find(in int id)
        {
            Inst.BusyPool.TryGetValue(id, out var entity);
            return entity;
        }

        /// <summary>
        /// 查询正在使用的对象
        /// </summary>
        /// <param name="ids">实例ID列表</param>
        /// <returns>实例单位对象</returns>
        public static IEnumerable<T> Find(IEnumerable<int> ids) { return from id in ids where Inst.BusyPool.ContainsKey(id) select Inst.BusyPool[id]; }

        #endregion

        #region Recycle

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="entity">实体</param>
        public static void Recycle(T entity) { Inst.RemoveSurviving(entity); }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="eid">实体ID</param>
        public static void Recycle(int eid)
        {
            if (Inst.BusyPool.TryGetValue(eid, out var entity)) Recycle(entity);
        }

        /// <summary>
        /// 回收所有正在使用的对象
        /// </summary>
        public static void RecycleAll()
        {
            foreach (var kvp in Inst.BusyPool) Inst.FreePool.Enqueue(kvp.Value);

            Inst.BusyPool.Clear();
        }

        #endregion
    }
}