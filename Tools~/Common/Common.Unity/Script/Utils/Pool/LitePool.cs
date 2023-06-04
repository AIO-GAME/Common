using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIO.Unity
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LitePool<T> where T : CacheableLite, IDisposable, new()
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name => typeof(T).Name;

        /// <summary>
        /// 缓存数量
        /// </summary>
        public int CacheCount => cache.Count;

        private readonly List<T> cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="count"></param>
        public LitePool(in int count = 5)
        {
            cache = new List<T>(count);
        }

        /// <summary>
        /// 创建元素
        /// </summary>
        protected virtual T CreateItem()
        {
            return new T();
        }

        /// <summary>
        /// 检出
        /// </summary>
        /// <returns></returns>
        public T CheckOut()
        {
            if (cache.Count > 0)
            {
                var idx = cache.Count - 1;
                var r = cache[idx];
                cache.RemoveAt(idx);

                r.SetState(ELiteCacheableState.Running);
                return r;
            }
            else
            {
                var r = CreateItem();
                r.SetCacher(CheckIn);

                r.SetState(ELiteCacheableState.Running);
                return r;
            }
        }

        internal void CheckIn(CacheableLite value)
        {
            if (value == null) return;
            if (value.CacheIsIdle) return;

            try
            {
                cache.Add((T)value);
                value.SetState(ELiteCacheableState.Idle);
                value.Reset();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// 减少到指定数量
        /// </summary>
        /// <param name="count"></param>
        public void Reduce(int count)
        {
            while (cache.Count > count)
            {
                var idx = cache.Count - 1;
                var c = cache[idx];
                cache.RemoveAt(idx);
                c.Dispose();
            }
        }
    }
}