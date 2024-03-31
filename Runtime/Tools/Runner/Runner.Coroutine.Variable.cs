/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-03-18
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIO
{
    partial class Runner
    {
        /// <summary>
        /// 等待一帧
        /// </summary>
        /// <remarks>公共Coroutine变量</remarks>
        public static WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();

        /// <summary>
        /// 等待下一帧
        /// </summary>
        /// <remarks>公共Coroutine变量</remarks>
        public static WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

        //需要用函数获得动态存储的Coroutine变量，采用键值对以免重复添加与快速查找
        private static readonly Dictionary<float, WaitForSeconds> waitForSeconds =
            new Dictionary<float, WaitForSeconds>();

        private static readonly Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealtime =
            new Dictionary<float, WaitForSecondsRealtime>();

        private static readonly Dictionary<Func<bool>, WaitUntil> waitUntil =
            new Dictionary<Func<bool>, WaitUntil>();

        private static readonly Dictionary<Func<bool>, WaitWhile> waitWhile =
            new Dictionary<Func<bool>, WaitWhile>();

        /// <summary>
        /// 获取协程变量
        /// </summary>
        public static WaitForSeconds WaitForSeconds(float k)
        {
            if (!waitForSeconds.TryGetValue(k, out var tmp))
                waitForSeconds.Add(k, tmp = new WaitForSeconds(k));
            return tmp;
        }

        public static WaitForSecondsRealtime WaitForSecondsRealtime(float k)
        {
            if (!waitForSecondsRealtime.TryGetValue(k, out var tmp))
                waitForSecondsRealtime.Add(k, (tmp = new WaitForSecondsRealtime(k)));
            return tmp;
        }

        public static WaitUntil WaitUntil(Func<bool> k)
        {
            if (!waitUntil.TryGetValue(k, out var tmp))
                waitUntil.Add(k, (tmp = new WaitUntil(k)));
            return tmp;
        }

        public static WaitWhile WaitWhile(Func<bool> k)
        {
            if (!waitWhile.TryGetValue(k, out var tmp))
                waitWhile.Add(k, tmp = new WaitWhile(k));
            return tmp;
        }

        //删除协程变量，若一个协程变量会被长期使用则尽量不要删除
        public static void DeleteWaitForSeconds(float k)
        {
            waitForSeconds.Remove(k);
        }

        public static void DeleteWaitForSecondsRealtime(float k)
        {
            waitForSecondsRealtime.Remove(k);
        }

        public static void DeleteWaitUntil(Func<bool> k)
        {
            waitUntil.Remove(k);
        }

        public static void DeleteWaitWhile(Func<bool> k)
        {
            waitWhile.Remove(k);
        }

        /// <summary>
        /// 清空协程变量
        /// </summary>
        public static void ClearWaitForSeconds()
        {
            waitForSeconds.Clear();
        }

        public static void ClearWaitForSecondsRealtime()
        {
            waitForSecondsRealtime.Clear();
        }

        public static void ClearWaitUntil()
        {
            waitUntil.Clear();
        }

        public static void ClearWaitWhile()
        {
            waitWhile.Clear();
        }
    }
}