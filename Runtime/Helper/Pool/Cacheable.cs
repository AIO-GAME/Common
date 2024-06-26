﻿#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace AIO
{
    /// <summary>
    ///
    /// </summary>
    public class Cacheable : MonoBehaviour
    {
        private bool checkIn;

        private HashSet<ICacheableHandler> handlers;

        /// <summary>
        ///
        /// </summary>
        public Cacheable Prefab { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cacheable"></param>
        public void SetPrefab(Cacheable cacheable)
        {
            Prefab = cacheable;
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual void OnCheckIn() { }

        /// <summary>
        ///
        /// </summary>
        protected virtual void OnCheckOut() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public void AddCacheHandler(in ICacheableHandler value)
        {
            if (handlers == null) handlers = new HashSet<ICacheableHandler>();
            handlers.Add(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public void RemoveCacheHandler(in ICacheableHandler value)
        {
            handlers?.Remove(value);
        }

        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="toRoot">放置到根节点</param>
        /// <param name="toActivate"></param>
        public void Recycle(in bool toRoot = true, in bool toActivate = true)
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
                    try
                    {
                        h.OnCheckOut();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }

            CancelInvoke();
            StopAllCoroutines();
            OnCheckIn();
            RHelper.Pool.CheckIn(this, toRoot, toActivate);
        }

        internal void CheckOut()
        {
            checkIn = false;
            OnCheckOut();

            if (handlers == null) return;
            foreach (var h in handlers)
                try
                {
                    h.OnCheckOut();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
        }
    }
}