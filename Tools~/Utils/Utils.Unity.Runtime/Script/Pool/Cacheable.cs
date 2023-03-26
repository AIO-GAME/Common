namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
using UnityEngine;

    public class Cacheable : MonoBehaviour
    {
        public Cacheable Prefab { get; private set; }

        public void SetPrefab(Cacheable cacheable)
        {
            Prefab = cacheable;
        }

        private bool checkIn;

        private HashSet<ICacheableHandler> handlers;

        protected virtual void OnCheckIn() { }

        protected virtual void OnCheckOut() { }

        public void AddCacheHandler(in ICacheableHandler value)
        {
            if (handlers == null) handlers = new HashSet<ICacheableHandler>();
            handlers.Add(value);
        }

        public void RemoveCacheHandler(in ICacheableHandler value)
        {
            if (handlers == null) return;
            handlers.Remove(value);
        }

        public void Recycle(in bool toRoot = true, in bool toDeactive = true)
{
            if (this == null || gameObject == null) return;

            if (checkIn) return;
            checkIn = true;

            if (!Prefab)
            {
                Debug.LogWarning($"Destroy cacheable: {this}");
                Destroy(gameObject);
                return;
            }

            if (handlers != null)
                foreach (var h in handlers)
                {
                    try { h.OnCheckOut(); }
                    catch (Exception e) { Debug.LogException(e); }
                }

            CancelInvoke();
            StopAllCoroutines();
            OnCheckIn();
            UtilsEngine.Pool.CheckIn(this, toRoot, toDeactive);
        }

        internal void CheckOut()
        {
            checkIn = false;
            OnCheckOut();

            if (handlers == null) return;
            foreach (var h in handlers)
            {
                try { h.OnCheckOut(); }
                catch (Exception e) { Debug.LogException(e); }
            }
        }
    }
}
