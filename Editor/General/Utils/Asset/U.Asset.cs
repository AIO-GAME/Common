#region

using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    public partial class EHelper
    {
        #region Nested type: Asset

        /// <summary>
        /// 资源
        /// </summary>
        public static partial class Asset
        {
            /// <summary>
            /// 标记目标
            /// </summary>
            public static void SetDirty(Object obj)
            {
                EditorUtility.SetDirty(obj);
            }
        }

        #endregion
    }
}