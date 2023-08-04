/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-03                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 13:09:24                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GERect
    {
        #region 序列化属性 Field Property

        /// <summary>
        /// SerializedProperty创建一个字段
        /// </summary>
        public static bool SP(Rect rect, SerializedProperty property, string label, bool includeChildren = false)
        {
            return EditorGUI.PropertyField(rect, property, new GUIContent(label), includeChildren);
        }

        /// <summary>
        /// SerializedProperty创建一个字段
        /// </summary>
        public static bool SP(Rect rect, SerializedProperty property, GUIContent label, bool includeChildren = false)
        {
            return EditorGUI.PropertyField(rect, property, label, includeChildren);
        }

        /// <summary>
        /// SerializedProperty创建一个字段
        /// </summary>
        public static bool SP(Rect rect, SerializedProperty property, bool includeChildren = false)
        {
            return EditorGUI.PropertyField(rect, property, includeChildren);
        }

        /// <summary>
        /// SerializedProperty创建一个字段
        /// </summary>
        public static bool SP(Vector4 pos, Vector4 size, SerializedProperty property, bool includeChildren = false)
        {
            return EditorGUI.PropertyField(new Rect(pos, size), property, includeChildren);
        }

        #endregion
    }
}
