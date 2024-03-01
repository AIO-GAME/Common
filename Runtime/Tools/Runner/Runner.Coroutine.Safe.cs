﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
                instance.StartCoroutine(coroutine);
#if UNITY_EDITOR
            else EditorCoroutineLooper.StartCoroutine(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(IList<IEnumerator> coroutines)
        {
#if UNITY_EDITOR
            if (IsRuntime)
#endif
                for (var index = 0; index < coroutines.Count; index++)
                {
                    instance.StartCoroutine(coroutines[index]);
                }
#if UNITY_EDITOR
            else
                for (var index = 0; index < coroutines.Count; index++)
                {
                    EditorCoroutineLooper.StartCoroutine(coroutines[index]);
                }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(IList<Func<IEnumerator>> coroutines)
        {
#if UNITY_EDITOR
            if (IsRuntime)
#endif
                for (var index = 0; index < coroutines.Count; index++)
                {
                    instance.StartCoroutine(coroutines[index]?.Invoke());
                }
#if UNITY_EDITOR
            else
                for (var index = 0; index < coroutines.Count; index++)
                {
                    EditorCoroutineLooper.StartCoroutine(coroutines[index]?.Invoke());
                }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(IEnumerator coroutine)
        {
#if UNITY_EDITOR
            if (IsRuntime)
#endif
                instance.StopCoroutine(coroutine);
#if UNITY_EDITOR
            else EditorCoroutineLooper.StopCoroutine(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(IList<IEnumerator> coroutines)
        {
#if UNITY_EDITOR
            if (IsRuntime)
#endif
                for (var index = 0; index < coroutines.Count; index++)
                {
                    instance.StopCoroutine(coroutines[index]);
                }
#if UNITY_EDITOR
            else
                for (var index = 0; index < coroutines.Count; index++)
                {
                    EditorCoroutineLooper.StopCoroutine(coroutines[index]);
                }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(IList<Func<IEnumerator>> coroutines)
        {
#if UNITY_EDITOR
            if (IsRuntime)
#endif
                for (var index = 0; index < coroutines.Count; index++)
                {
                    instance.StopCoroutine(coroutines[index]?.Invoke());
                }
#if UNITY_EDITOR
            else
                for (var index = 0; index < coroutines.Count; index++)
                {
                    EditorCoroutineLooper.StopCoroutine(coroutines[index]?.Invoke());
                }
#endif
        }
    }
}