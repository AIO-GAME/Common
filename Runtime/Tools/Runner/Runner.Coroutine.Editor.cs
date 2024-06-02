#if UNITY_EDITOR

#region namespace

using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

#endregion

namespace AIO
{
    partial class Runner
    {
        private static class EditorCoroutineLooper
        {
            private static readonly ConcurrentDictionary<IEnumerator, bool> Looper;
            private static          bool                                    M_Started;

            static EditorCoroutineLooper()
            {
                Looper                     =  new ConcurrentDictionary<IEnumerator, bool>();
                EditorApplication.quitting += DisposeEditorLooper;
            }

            private static void DisposeEditorLooper()
            {
                M_Started = false;
                Looper.Clear();
                EditorApplication.quitting -= DisposeEditorLooper;
            }

            public static void Start(IEnumerator iterator)
            {
                if (iterator is null) return;
                if (!Looper.ContainsKey(iterator)) Looper.TryAdd(iterator, false);
                if (Looper.IsEmpty || M_Started) return;
                M_Started                =  true;
                EditorApplication.update += Update;
            }

            public static void Stop(IEnumerator iterator)
            {
                if (Looper.IsEmpty || iterator is null || !Looper.ContainsKey(iterator)) return;
                Looper.TryUpdate(iterator, true, false);
            }

            private static void Update()
            {
                if (Looper.Count > 0)
                {
                    foreach (var (current, value) in Looper)
                    {
                        if (value) continue; // 已经执行过的不再执行
                        if (!instance)       // 卸载时丢弃Looper
                        {
                            Looper.Clear();
                            EditorApplication.update -= Update;
                            M_Started                =  false;
                            return;
                        }

                        if (!instance.gameObject.activeInHierarchy) continue;            // 隐藏时别执行Loop
                        if (!current.MoveNext()) Looper.TryUpdate(current, true, false); // 执行完毕则标记为true
                    }

                    //集中处理丢弃的Looper value为true的
                    Looper.Where(pair => pair.Value).Select(pair => pair.Key).ToList().ForEach(key => Looper.TryRemove(key, out _));
                    if (Looper.Count > 0) return;
                }

                EditorApplication.update -= Update;
                M_Started                =  false;
            }
        }
    }
}
#endif