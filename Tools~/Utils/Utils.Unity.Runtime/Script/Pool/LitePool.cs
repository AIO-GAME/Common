namespace AIO
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    public class LitePool<T> where T : CacheableLite, IDisposable, new()
    {
        public string Name => typeof(T).Name;

        public int CacheCount => cache.Count;

        private List<T> cache;

        public LitePool(in int count = 5)
        {
            cache = new List<T>(count);
        }

        protected virtual T CreateItem()
        {
            return new T();
        }

        public T CheckOut()
        {
            if (cache.Count > 0)
            {
                var idx = cache.Count - 1;
                var r = cache[idx];
                cache.RemoveAt(idx);

                r.SetState(LiteCacheableState.Running);
                return r;
            }
            else
            {
                var r = CreateItem();
                r.SetCacher(CheckIn);

                r.SetState(LiteCacheableState.Running);
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
                value.SetState(LiteCacheableState.Idle);
                value.Reset();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

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
