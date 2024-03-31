#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace AIO
{
    partial class Runner
    {
        private static class EditorCoroutineLooper
        {
            private static readonly List<IEnumerator> Looper;
            private static readonly List<IEnumerator> DropItems;
            private static bool M_Started;

            static EditorCoroutineLooper()
            {
                Looper = new List<IEnumerator>();
                DropItems = new List<IEnumerator>();
                EditorApplication.quitting += () =>
                {
                    Looper.Clear();
                    DropItems.Clear();
                    M_Started = false;
                };
            }

            public static void Start(IEnumerator iterator)
            {
                if (iterator is null) return;
                if (!Looper.Contains(iterator)) Looper.Add(iterator);
                if (Looper.Count == 0) return;
                if (M_Started) return;
                M_Started = true;
                EditorApplication.update += Update;
            }

            public static void Stop(IEnumerator iterator)
            {
                if (iterator is null) return;
                if (!Looper.Contains(iterator)) return;
                Looper.Remove(iterator);
                instance.StopCoroutine(iterator);
            }

            private static void Update()
            {
                if (Looper.Count > 0)
                {
                    lock (Looper)
                    {
                        using (var items = Looper.GetEnumerator())
                        {
                            while (items.MoveNext())
                            {
                                var item = items.Current;
                                if (instance == null) //卸载时丢弃Looper
                                {
                                    DropItems.Add(item);
                                    continue;
                                }

                                if (!instance.gameObject.activeInHierarchy) continue; //隐藏时别执行Loop
                                if (item != null && !item.MoveNext()) DropItems.Add(item); //执行Loop，执行完毕也丢弃Looper
                            }

                            foreach (var t in DropItems.Where(t => t != null)) Looper.Remove(t); //集中处理丢弃的Looper
                            DropItems.Clear();
                        }
                    }

                    if (Looper.Count > 0) return;
                }

                EditorApplication.update -= Update;
                M_Started = false;
            }
        }
    }
}
#endif