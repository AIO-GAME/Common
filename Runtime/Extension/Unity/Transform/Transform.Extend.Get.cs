using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AIO.UEngine
{
    partial class TransformExtend
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="trans">源目标</param>
        /// <param name="recursion">是否递归</param>
        public static void SortChild(this Transform trans, bool recursion)
        {
            var transforms = new List<Transform>(GetChildes(trans));
            transforms.Sort((lh, rh) => string.Compare(lh.name, rh.name, StringComparison.CurrentCulture));
            foreach (var child in transforms)
            {
                child.SetAsLastSibling();
                if (recursion) SortChild(child, true);
            }
        }


        /// <summary>
        /// 获取子物体
        /// </summary>
        /// <param name="trans">源目标</param>
        public static Transform[] GetChildes(this Transform trans)
        {
            if (trans.childCount <= 0) return Array.Empty<Transform>();
            var objs = new Transform[trans.childCount];
            for (var i = 0; i < trans.childCount; i++) objs[i] = trans.GetChild(i);
            return objs;
        }

        /// <summary>
        /// 获取全部子物体
        /// </summary>
        /// <param name="trans">源目标</param>
        public static List<Transform> GetChildesAll(this Transform trans)
        {
            var list = new List<Transform>();
            if (trans.childCount <= 0) return list;
            FillChild(trans, list, true);
            return list;
        }

        /// <summary>
        /// 返回找到指定父物体为止的索引下标
        /// </summary>
        /// <param name="trans">源目标</param>
        /// <param name="name">父物体名称</param>
        public static int[] GetTransformSiblingIndex(this Transform trans, in string name)
        {
            if (trans != null && trans != default)
            {
                var str = new StringBuilder();
                var transform = trans;
                while (transform.name != name)
                {
                    str.Append(transform.GetSiblingIndex());
                    transform = transform.parent;
                    if (transform == null || transform.name == name) break;
                    str.Append('.');
                }

                var split = str.ToString().Split('.');
                var index = new int[split.Length];
                for (var i = split.Length - 1; i >= 0; i--)
                {
                    index[split.Length - i - 1] = int.Parse(split[i]);
                }

                return index;
            }

            return Array.Empty<int>();
        }

        /// <summary>
        /// 获取全部子物体相同组件
        /// </summary>
        public static List<T> GetChildesComponents<T>(this Transform trans) where T : Component
        {
            if (trans.childCount <= 0) return new List<T>();
            var list = new List<T>();
            for (var i = 0; i < trans.childCount; i++)
            {
                list.AddRange(trans.GetChild(i).GetComponents<T>());
            }

            return list;
        }
    }
}