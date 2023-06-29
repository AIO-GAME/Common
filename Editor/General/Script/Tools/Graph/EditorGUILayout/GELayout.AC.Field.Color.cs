/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Internal;
using Object = UnityEngine.Object;

namespace UnityEditor
{
    public partial class GELayout
    {
        #region 颜色选择框 Field Color

        /// <summary> 颜色选择框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">颜色</param>
        /// <param name="showEyedropper">颜色选择器显示滴管控制</param>
        /// <param name="showAlpha">用户设置颜色的alpha值</param>
        /// <param name="hdr">true:颜色视为HDR值,false:视为标准的LDR</param>
        /// <param name="options">排版格式</param>
        public static Color Field(GUIContent label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(label, value, showEyedropper, showAlpha, hdr, options);
        }

        /// <summary> 颜色选择框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">颜色</param>
        /// <param name="options">排版格式</param>
        public static Color Field(string label, Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(label, value, options);
        }

        /// <summary> 颜色选择框 </summary>
        /// <param name="value">颜色</param>
        /// <param name="options">排版格式</param>
        public static Color Field(Color value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.ColorField(value, options);
        }

        #endregion
    }
}