#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace AIO
{
    partial class Runner
    {
        private static class EditorCoroutineLooper
        {
            private static readonly List<IEnumerator> Looper = new List<IEnumerator>();
            private static readonly List<IEnumerator> DropItems = new List<IEnumerator>();
            private static bool M_Started;

            /// <summary>
            /// 开启Loop
            /// </summary>
            /// <param name="iterator">方法</param>
            public static void StartLoop(IEnumerator iterator)
            {
                if (iterator == null) return;
                if (!Looper.Contains(iterator)) Looper.Add(iterator);
                if (Looper.Count == 0) return;
                if (M_Started) return;
                M_Started = true;
                EditorApplication.update += Update;
            }

            private static void Update()
            {
                if (Looper.Count > 0)
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

                            //隐藏时别执行Loop
                            if (!instance.gameObject.activeInHierarchy) continue;

                            //执行Loop，执行完毕也丢弃Looper
                            if (item != null && !item.MoveNext()) DropItems.Add(item);
                        }

                        //集中处理丢弃的Looper
                        for (var i = 0; i < DropItems.Count; i++)
                        {
                            if (DropItems[i] == null) continue;
                            Looper.Remove(DropItems[i]);
                        }

                        DropItems.Clear();
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