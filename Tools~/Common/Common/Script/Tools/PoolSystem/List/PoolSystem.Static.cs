// #region
//
// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
//
// #endregion
//
// namespace AIO
// {
//     partial class PoolSystem<T, E>
//     {
//         /// <summary>
//         /// 使用中的对象
//         /// </summary>
//         protected static Dictionary<int, E> BusyPool { get; private set; }
//
//         /// <summary>
//         /// 空闲的对象
//         /// </summary>
//         protected static Queue<E> FreePool { get; private set; }
//
//         /// <summary>
//         /// 卸载系统
//         /// </summary>
//         public static void UnInstall()
//         {
//             BusyPool.Free();
//             FreePool.Free();
//             Inst.Dispose();
//             Inst = null;
//         }
//
//         /// <summary>
//         /// 获取使用中的对象
//         /// </summary>
//         /// <returns></returns>
//         public static ICollection<E> GetBusyPool()
//         {
//             var list = Pool.List<E>();
//             foreach (var item in BusyPool) list.Add(item.Value);
//
//             return list;
//         }
//
//         /// <summary>
//         /// 获取单位类型
//         /// </summary>
//         /// <param name="id">实例ID</param>
//         /// <returns>实例单位对象</returns>
//         public static E GetSurviving(in int id)
//         {
//             if (BusyPool.ContainsKey(id)) return BusyPool[id];
//             throw new KeyNotFoundException(string.Format("对象不存在 ID -> {0}", id));
//         }
//
//         /// <summary>
//         /// 获取单位类型
//         /// </summary>
//         /// <param name="ids">实例ID列表</param>
//         /// <returns>实例单位对象</returns>
//         public static ICollection<E> GetSurviving(IEnumerable<int> ids)
//         {
//             var list = Pool.List<E>();
//             foreach (var id in ids)
//             {
//                 if (BusyPool.ContainsKey(id)) list.Add(BusyPool[id]);
//                 throw new KeyNotFoundException(string.Format("对象不存在 ID -> {0}", id));
//             }
//
//             return list;
//         }
//
//         /// <summary>
//         /// 清理缓存数据
//         /// </summary>
//         public static void ClearCacheData()
//         {
//             BusyPool.Clear();
//             FreePool.Clear();
//             Inst.ClearCache();
//         }
//
//         /// <summary>
//         ///  添加存活对象
//         /// </summary>
//         /// <param name="entity">实体</param>
//         protected static void AddSurviving(E entity)
//         {
//             var eid = Inst.GetEID(entity);
//             if (!BusyPool.ContainsKey(eid)) BusyPool.Add(eid, entity);
//         }
//
//         /// <summary>
//         /// 移除存活对象
//         /// </summary>
//         /// <param name="entity">实体</param>
//         protected static void RemoveSurviving(E entity)
//         {
//             var eid = Inst.GetEID(entity);
//             if (BusyPool.ContainsKey(eid)) BusyPool.Remove(eid);
//         }
//
//         /// <summary>
//         /// 回收对象
//         /// </summary>
//         /// <param name="entity">实体</param>
//         public static void Recycle(E entity)
//         {
//             FreePool.Enqueue(entity);
//             RemoveSurviving(entity);
//         }
//
//         /// <summary>
//         /// 回收对象
//         /// </summary>
//         /// <param name="eid">实体ID</param>
//         public static void Recycle(int eid)
//         {
//             if (BusyPool.TryGetValue(eid, out var entity))
//             {
//                 FreePool.Enqueue(entity);
//                 RemoveSurviving(entity);
//             }
//         }
//
//         /// <summary>
//         /// 回收所有正在使用的对象
//         /// </summary>
//         public static void RecycleBusy()
//         {
//             foreach (var entity in BusyPool) FreePool.Enqueue(entity.Value);
//
//             BusyPool.Clear();
//         }
//     }
// }