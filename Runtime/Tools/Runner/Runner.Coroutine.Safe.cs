﻿#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    partial class Runner
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(in IEnumerator coroutine)
        {
#if UNITY_EDITOR
            while (!instance) Initialize();
            if (IsRuntime)
                try
                {
#endif
                    instance.StartCoroutine(coroutine);
#if UNITY_EDITOR
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(e);
                }
            else EditorCoroutineLooper.Start(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(in IEnumerable<IEnumerator> coroutines)
        {
#if UNITY_EDITOR
            while (!instance) Initialize();
            if (IsRuntime)
            {
#endif
                foreach (var enumerator in coroutines) instance.StartCoroutine(enumerator);
#if UNITY_EDITOR
            }
            else
            {
                foreach (var enumerator in coroutines) EditorCoroutineLooper.Start(enumerator);
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(in IEnumerable<Func<IEnumerator>> coroutines)
        {
#if UNITY_EDITOR
            while (!instance) Initialize();
            if (IsRuntime)
            {
#endif
                foreach (var func in coroutines) instance.StartCoroutine(func?.Invoke());
#if UNITY_EDITOR
            }
            else
            {
                foreach (var func in coroutines) EditorCoroutineLooper.Start(func?.Invoke());
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(in IEnumerator coroutine)
        {
#if UNITY_EDITOR
            while (!instance) Initialize();
            if (IsRuntime)
#endif
                instance.StopCoroutine(coroutine);
#if UNITY_EDITOR
            else EditorCoroutineLooper.Stop(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(in IEnumerable<IEnumerator> coroutines)
        {
#if UNITY_EDITOR
            while (!instance) Initialize();
            if (IsRuntime)
            {
#endif
                foreach (var func in coroutines) instance.StopCoroutine(func);
#if UNITY_EDITOR
            }
            else
            {
                foreach (var func in coroutines) EditorCoroutineLooper.Stop(func);
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(in IEnumerable<Func<IEnumerator>> coroutines)
        {
#if UNITY_EDITOR
            while (!instance) Initialize();
            if (IsRuntime)
            {
#endif
                foreach (var func in coroutines) instance.StopCoroutine(func?.Invoke());
#if UNITY_EDITOR
            }
            else
            {
                foreach (var func in coroutines) EditorCoroutineLooper.Stop(func?.Invoke());
            }
#endif
        }
    }
}