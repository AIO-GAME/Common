/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-06-04
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class EHelper
    {
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
    }
}
