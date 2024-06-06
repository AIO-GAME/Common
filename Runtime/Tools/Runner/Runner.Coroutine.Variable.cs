#region

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace AIO
{
    partial class Runner
    {
        /// <summary>
        /// 等待一帧
        /// </summary>
        /// <remarks>公共Coroutine变量</remarks>
        public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();

        /// <summary>
        /// 等待下一帧
        /// </summary>
        /// <remarks>公共Coroutine变量</remarks>
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

        private static readonly Dictionary<float, WaitForSeconds>         waitForSeconds         = new Dictionary<float, WaitForSeconds>(32);
        private static readonly Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealtime = new Dictionary<float, WaitForSecondsRealtime>(32);
        private static readonly Dictionary<Func<bool>, WaitUntil>         waitUntil              = new Dictionary<Func<bool>, WaitUntil>(32);
        private static readonly Dictionary<Func<bool>, WaitWhile>         waitWhile              = new Dictionary<Func<bool>, WaitWhile>(32);

        private class YieldReturnNull : IEnumerator
        {
            public object Current    => null;
            public bool   MoveNext() => false;
            public void   Reset()    { }
        }

        /// <summary>
        /// 空的协程变量
        /// </summary>
        public static readonly IEnumerator YieldReturn = new YieldReturnNull();

        /// <summary>
        /// 获取协程变量
        /// </summary>
        public static WaitForSeconds WaitForSeconds(float k)
        {
            if (!waitForSeconds.TryGetValue(k, out var tmp))
                waitForSeconds.Add(k, tmp = new WaitForSeconds(k));
            return tmp;
        }

        /// <summary>
        /// 等待实时时间
        /// </summary>
        /// <param name="k"> 等待时间 </param>
        /// <returns> 返回协程变量 </returns>
        public static WaitForSecondsRealtime WaitForSecondsRealtime(float k)
        {
            if (!waitForSecondsRealtime.TryGetValue(k, out var tmp))
                waitForSecondsRealtime[k] = tmp = new WaitForSecondsRealtime(k);
            return tmp;
        }

        /// <summary>
        /// 等待直到条件为真
        /// </summary>
        public static WaitUntil WaitUntil(Func<bool> k)
        {
            if (!waitUntil.TryGetValue(k, out var tmp))
                waitUntil[k] = tmp = new WaitUntil(k);
            return tmp;
        }

        /// <summary>
        /// 等待直到条件为假
        /// </summary>
        public static WaitWhile WaitWhile(Func<bool> k)
        {
            if (!waitWhile.TryGetValue(k, out var tmp))
                waitWhile[k] = tmp = new WaitWhile(k);
            return tmp;
        }

        /// <summary>
        /// 删除协程变量
        /// </summary>
        public static void WaitForSecondsDelete(float k) { waitForSeconds.Remove(k); }

        /// <summary>
        /// 删除协程变量
        /// </summary>
        public static void WaitForSecondsRealtimeDelete(float k) { waitForSecondsRealtime.Remove(k); }

        /// <summary>
        /// 删除协程变量
        /// </summary>
        public static void WaitUntilDelete(Func<bool> k) { waitUntil.Remove(k); }

        /// <summary>
        /// 删除协程变量
        /// </summary>
        public static void WaitWhileDelete(Func<bool> k) { waitWhile.Remove(k); }

        /// <summary>
        /// 清空协程变量
        /// </summary>
        public static void WaitForSecondsClear() { waitForSeconds.Clear(); }

        /// <summary>
        /// 清空协程变量
        /// </summary>
        public static void WaitForSecondsRealtimeClear() { waitForSecondsRealtime.Clear(); }

        /// <summary>
        /// 清空协程变量
        /// </summary>
        public static void WaitUntilClear() { waitUntil.Clear(); }

        /// <summary>
        /// 清空协程变量
        /// </summary>
        public static void WaitWhileClear() { waitWhile.Clear(); }
    }
}