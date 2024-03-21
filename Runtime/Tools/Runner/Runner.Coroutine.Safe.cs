using System;
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
            if (instance == null)
                Initialize();
            if (IsRuntime)
#endif
                instance.StartCoroutine(coroutine);
#if UNITY_EDITOR
            else EditorCoroutineLooper.Start(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(IList<IEnumerator> coroutines)
        {
#if UNITY_EDITOR
            if (instance == null)
                Initialize();
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
                    EditorCoroutineLooper.Start(coroutines[index]);
                }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStartCoroutine(IList<Func<IEnumerator>> coroutines)
        {
#if UNITY_EDITOR
            if (instance == null)
                Initialize();
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
                    EditorCoroutineLooper.Start(coroutines[index]?.Invoke());
                }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(IEnumerator coroutine)
        {
#if UNITY_EDITOR
            if (instance == null)
                Initialize();
            if (IsRuntime)
#endif
                instance.StopCoroutine(coroutine);
#if UNITY_EDITOR
            else EditorCoroutineLooper.Stop(coroutine);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(IList<IEnumerator> coroutines)
        {
#if UNITY_EDITOR
            if (instance == null)
                Initialize();
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
                    EditorCoroutineLooper.Stop(coroutines[index]);
                }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SafeStopCoroutine(IList<Func<IEnumerator>> coroutines)
        {
#if UNITY_EDITOR
            if (instance == null)
                Initialize();
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
                    EditorCoroutineLooper.Stop(coroutines[index]?.Invoke());
                }
#endif
        }
    }
}