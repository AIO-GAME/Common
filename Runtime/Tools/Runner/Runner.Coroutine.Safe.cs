#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace AIO
{
    partial class Runner
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(IEnumerator coroutine)
        {
#if UNITY_EDITOR
            if (IsRuntime)
#endif
                QueuesCoroutine.TryAdd(coroutine, CoroutineData.Create());
#if UNITY_EDITOR
            else EditorCoroutineLooper.Start(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(IEnumerable<IEnumerator> coroutines)
        {
#if UNITY_EDITOR
            if (IsRuntime)
            {
#endif
                foreach (var enumerator in coroutines) QueuesCoroutine.TryAdd(enumerator, CoroutineData.Create());
#if UNITY_EDITOR
            }
            else
            {
                foreach (var enumerator in coroutines) EditorCoroutineLooper.Start(enumerator);
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(IEnumerator coroutine)
        {
#if UNITY_EDITOR
            if (IsRuntime)
            {
#endif
                if (QueuesCoroutine.TryGetValue(coroutine, out var data)) data.Cancel();
#if UNITY_EDITOR
            }
            else EditorCoroutineLooper.Stop(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(IEnumerable<IEnumerator> coroutines)
        {
#if UNITY_EDITOR
            if (IsRuntime)
            {
#endif
                foreach (var enumerator in coroutines) instance.StopCoroutine(enumerator);
#if UNITY_EDITOR
            }
            else
            {
                foreach (var enumerator in coroutines) EditorCoroutineLooper.Stop(enumerator);
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(MonoBehaviour behaviour, IEnumerator coroutine)
        {
#if UNITY_EDITOR
            if (IsRuntime)
#endif
                QueuesCoroutine.TryAdd(coroutine, CoroutineData.Create(behaviour));
#if UNITY_EDITOR
            else EditorCoroutineLooper.Start(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(MonoBehaviour behaviour, IEnumerable<IEnumerator> coroutines)
        {
#if UNITY_EDITOR
            if (IsRuntime)
            {
#endif
                foreach (var enumerator in coroutines) QueuesCoroutine.TryAdd(enumerator, CoroutineData.Create(behaviour));
#if UNITY_EDITOR
            }
            else
            {
                foreach (var enumerator in coroutines) EditorCoroutineLooper.Start(enumerator);
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(Object behaviour, IEnumerator coroutine)
        {
#if UNITY_EDITOR
            if (IsRuntime)
            {
#endif
                foreach (var data in QueuesCoroutine
                                     .Where(data => behaviour == data.Value.Behaviour)
                                     .Where(data => Equals(data.Key, coroutine))
                        ) data.Value.Cancel();
#if UNITY_EDITOR
            }
            else EditorCoroutineLooper.Stop(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(Object behaviour)
        {
            foreach (var data in QueuesCoroutine.Values.Where(data => behaviour == data.Behaviour))
            {
                data.Cancel();
            }
        }
    }
}