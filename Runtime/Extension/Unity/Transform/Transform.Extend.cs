#region

using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    public static partial class TransformExtend
    {
        private static char[] gSeps = { '/', '.' };

        /// <summary>
        /// 销毁全部子物体
        /// </summary>
        public static void DestroyChildes(this Transform trans)
        {
            if (trans == null) return;
            while (trans.childCount > 0) //依次从第一个开始销毁 如果第一个子物体销毁完成 则销毁第二个
                if (trans.GetChild(0).childCount == 0)
                    trans.GetChild(0).gameObject.Destroy();
                else trans.GetChild(0).DestroyChildes();
        }

        /// <summary>
        /// 根据索引 获取当前物体下指定物体的子物体
        /// </summary>
        public static Transform TransformSiblingIndexToObj(this Transform trans, in IList<int> indexes)
        {
            return indexes.Count == 0 ? null : indexes.Aggregate(trans, (current, index) => current.GetChild(index));
        }

        /// <summary>
        /// 全路径
        /// </summary>
        /// <param name="tran">自身</param>
        /// <returns>在场景中的全路径</returns>
        public static string FullName(this Transform tran)
        {
            var tfs = Pool.List<Transform>();
            var tf = tran;
            tfs.Add(tf);
            while (tf.parent)
            {
                tf = tf.parent;
                tfs.Add(tf);
            }

            var builder = new StringBuilder();
            builder.Append(tfs[tfs.Count - 1].name);
            for (var i = tfs.Count - 2; i >= 0; i--) builder.Append("/" + tfs[i].name);

            tfs.Free();
            return builder.ToString();
        }

        #region Find

        public static T FindComponentInChild<T>(this Transform transform, in string name)
        where T : Component
        {
            var r = transform.Find(name);
            return r.Equals(null) ? null : r.GetComponent<T>();
        }

        public static void FillChild(this Transform parent, in ICollection<Transform> r, in bool recursion)
        {
            for (int i = 0, max = parent.childCount; i < max; ++i)
            {
                var child = parent.GetChild(i);
                r.Add(child);
                if (recursion) FillChild(child, r, true);
            }
        }

        public static Transform FindOrCreateChild(this Transform self, string target, char[] sep = null)
        {
            var r = self.Find(target);
            if (r == null)
            {
                var ps = target.Split(sep ?? gSeps);
                var parent = self;
                foreach (var p in ps)
                {
                    r = parent.Find(p);
                    if (r == null) r = GoUtils.CreateGameObject(p, parent).transform;
                    parent = r;
                }
            }

            return r;
        }

        public static Transform FindChildR(this Transform self, string target)
        {
            if (string.Equals(self.name, target)) return self;

            for (int i = 0, max = self.childCount; i < max; ++i)
            {
                var tf = FindChildR(self.GetChild(i), target);
                if (tf != null) return tf;
            }

            return null;
        }

        #endregion

        #region Remove

        public static void RemoveInactiveChild(this Transform tf)
        {
            for (var i = tf.childCount - 1; i >= 0; --i)
            {
                var child = tf.GetChild(i);
                if (child.gameObject.activeSelf) RemoveInactiveChild(child);
                else Object.Destroy(child.gameObject);
            }
        }

        public static void RemoveInactiveChildImm(this Transform tf)
        {
            for (var i = tf.childCount - 1; i >= 0; --i)
            {
                var child = tf.GetChild(i);
                if (child.gameObject.activeSelf) RemoveInactiveChild(child);
                else Object.DestroyImmediate(child.gameObject);
            }
        }

        #endregion
    }
}