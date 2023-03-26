using AIO;

using System.Collections.Generic;

using UnityEngine;

public static partial class UtilsEngine
{
    public static class Pool
    {
        /// <summary>
        /// 缓存列表
        /// </summary>
        private static readonly Dictionary<Cacheable, Stack<Cacheable>> Cache = new Dictionary<Cacheable, Stack<Cacheable>>();

        public static T CheckOut<T>(in Object prefab, in Transform parent = null) where T : Cacheable
        {
            if (prefab is GameObject @object) return CheckOut<T>(@object, parent);
            else if (prefab is Component component) return CheckOut<T>(component, parent);
            else Debug.LogError($"Can't instantiate Object:{prefab}");
            return null;
        }

        public static T CheckOut<T>(in GameObject prefab, in Transform parent = null) where T : Cacheable
        {
            return CheckOut(prefab.GetComponent<T>(), parent);
        }

        public static T CheckOut<T>(in Component prefab, in Transform parent = null) where T : Cacheable
        {
            return CheckOut(prefab.GetComponent<T>(), parent);
        }

        public static T CheckOut<T>(in T prefab, in Transform parent = null) where T : Cacheable
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

        internal static void CheckIn(in Cacheable inst, in bool toRoot, in bool toDeactive)
        {
            if (inst.Prefab == null) return;

            if (Cache.TryGetValue(inst.Prefab, out var cache))
            {
                inst.SetPrefab(null);
                cache.Push(inst);

                if (toRoot) inst.transform.SetParent(null, false);
                if (toDeactive) inst.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning($"Destroy cacheable: {inst}");
                Object.Destroy(inst.gameObject);
            }
        }

        public static void Reduce(in Cacheable prefab, in int count)
        {
            if (Cache.TryGetValue(prefab, out var cache))
            {
                while (cache.Count > count)
                {
                    var c = cache.Pop();
                    if (c) Object.Destroy(c.gameObject);
                }
            }
        }

        public static void ReduceAll(in int count)
        {
            foreach (var cache in Cache.Values)
            {
                while (cache.Count > count)
                {
                    var c = cache.Pop();
                    if (c) Object.Destroy(c.gameObject);
                }
            }
        }

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
}