﻿#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace AIO
{
    partial class RHelper
    {
        #region Nested type: Pool

        /// <summary>
        /// 对象池
        /// </summary>
        public static class Pool
        {
            /// <summary>
            /// 缓存列表
            /// </summary>
            private static readonly Dictionary<Cacheable, Stack<Cacheable>> Cache =
                new Dictionary<Cacheable, Stack<Cacheable>>();

            /// <summary>
            /// 检出
            /// </summary>
            /// <param name="prefab">预制件</param>
            /// <param name="parent">父物体</param>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T CheckOut<T>(in Object prefab, in Transform parent = null)
            where T : Cacheable
            {
                var go = prefab as GameObject;
                if (go) return CheckOut<T>(go, parent);

                var component = prefab as Component;
                if (component) return CheckOut<T>(component, parent);

                Debug.LogError($"Can't instantiate Object:{prefab}");
                return null;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="prefab"></param>
            /// <param name="parent"></param>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T CheckOut<T>(in GameObject prefab, in Transform parent = null)
            where T : Cacheable
            {
                return CheckOut(prefab.GetComponent<T>(), parent);
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="prefab"></param>
            /// <param name="parent"></param>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T CheckOut<T>(in Component prefab, in Transform parent = null)
            where T : Cacheable
            {
                return CheckOut(prefab.GetComponent<T>(), parent);
            }

            /// <summary>
            /// 检出
            /// </summary>
            /// <param name="prefab">缓存表</param>
            /// <param name="parent"></param>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T CheckOut<T>(in T prefab, in Transform parent = null)
            where T : Cacheable
            {
                if (!Cache.TryGetValue(prefab, out var cache))
                {
                    cache = new Stack<Cacheable>();
                    Cache.Add(prefab, cache);
                }

                Cacheable r = null;
                while (cache.Count > 0)
                {
                    r = cache.Pop();
                    if (r) break;
                }

                if (r == null) r = Object.Instantiate(prefab);
                r.SetPrefab(prefab);
                r.transform.SetParent(parent, false);
                r.gameObject.SetActive(true);
                r.CheckOut();
                return (T)r;
            }

            internal static void CheckIn(in Cacheable inst, in bool toRoot, in bool toActivate)
            {
                if (inst.Prefab == null) return;

                if (Cache.TryGetValue(inst.Prefab, out var cache))
                {
                    inst.SetPrefab(null);
                    cache.Push(inst);

                    if (toRoot) inst.transform.SetParent(null, false);
                    if (toActivate) inst.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning($"Destroy cacheable: {inst}");
                    Object.Destroy(inst.gameObject);
                }
            }

            /// <summary>
            /// 减少指定类型的缓存到指定数量
            /// </summary>
            /// <param name="prefab">缓存表</param>
            /// <param name="count">数量</param>
            public static void Reduce(in Cacheable prefab, in int count)
            {
                if (Cache.TryGetValue(prefab, out var cache))
                    while (cache.Count > count)
                    {
                        var c = cache.Pop();
                        if (c) Object.Destroy(c.gameObject);
                    }
            }

            /// <summary>
            /// 减少缓存到指定数量
            /// </summary>
            /// <param name="count">保留数量</param>
            public static void ReduceAll(in int count)
            {
                foreach (var cache in Cache.Values)
                    while (cache.Count > count)
                    {
                        var c = cache.Pop();
                        if (c) Object.Destroy(c.gameObject);
                    }
            }

            /// <summary>
            /// 移除
            /// </summary>
            /// <param name="prefab">缓存物体</param>
            public static void Remove(Cacheable prefab)
            {
                if (Cache.TryGetValue(prefab, out var cache))
                {
                    Cache.Remove(prefab);
                    while (cache.Count > 0)
                    {
                        var c = cache.Pop();
                        if (c) Object.Destroy(c.gameObject);
                    }
                }
            }
        }

        #endregion
    }
}