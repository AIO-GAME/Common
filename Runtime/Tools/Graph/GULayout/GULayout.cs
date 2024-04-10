#region

using System;
using UnityEngine;

#endregion

namespace AIO
{
    public abstract partial class GULayout
    {
        /// <summary>
        /// 复制文本信息
        /// </summary>
        public static readonly Action<string> CopyTextAction = contents =>
        {
            var text = new TextEditor { text = contents };
            text.OnFocus();
            text.Copy();
        };

        #region 隔行

        /// <summary>
        /// 插入一个灵活的空间元素
        /// </summary>
        public static void FlexibleSpace()
        {
            GUILayout.FlexibleSpace();
        }

        /// <summary>
        /// 分隔符
        /// </summary>
        public static void FlexibleSpace(int num)
        {
            for (var i = 0; i < num; i++) GUILayout.FlexibleSpace();
        }

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space(float pixel)
        {
            GUILayout.Space(pixel);
        }

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space(int num, float pixel)
        {
            for (var i = 0; i < num; i++) GUILayout.Space(pixel);
        }

        #endregion
    }
}