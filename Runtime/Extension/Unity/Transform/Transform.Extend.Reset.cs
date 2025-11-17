#region

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Scripting;

#endregion

namespace AIO
{
    partial class TransformExtend
    {
        /// <summary>
        /// 重置父节点并可选择是否重置Transform属性
        /// </summary>
        /// <param name="trans">自身Transform</param>
        /// <param name="parent"> 新的父节点</param>
        /// <param name="isReset"> 是否重置Transform属性</param>
        public static void ResetParent(this Transform trans, Transform parent, bool isReset = true)
        {
            var pos      = isReset ? Vector3.zero : trans.localPosition;
            var rotation = isReset ? Quaternion.identity : trans.localRotation;
            var scale    = isReset ? Vector3.one : trans.localScale;
            trans.SetParent(parent, false);
            trans.localPosition = pos;
            trans.localRotation = rotation;
            trans.localScale    = scale;
        }

        /// <summary>
        /// 重置Transform属性
        /// </summary>
        /// <param name="trans">自身Transform</param>
        public static void Reset(this Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale    = Vector3.one;
        }
    }
}