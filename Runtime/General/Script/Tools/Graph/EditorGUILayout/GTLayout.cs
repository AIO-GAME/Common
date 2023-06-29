/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

namespace UnityEngine
{
    public static partial class GULayout
    {
        #region 隔行

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