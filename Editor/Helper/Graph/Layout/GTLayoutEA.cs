/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-03                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 13:09:47                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/
namespace UnityEditor
{
    using System;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// 编辑器 GUI Layout 方法库
    /// </summary>
    public static partial class GTLayout
    {
        #region 隔行

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space()
        {
            EditorGUILayout.Space();
        }

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space(int num = 1)
        {
            for (int i = 0; i < num; i++) EditorGUILayout.Space();
        }

#if UNITY_2019_3_OR_NEWER

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space(float width, int num = 1)
        {
            for (int i = 0; i < num; i++) EditorGUILayout.Space(width, true);
        }

        /// <summary>
        /// 隔行
        /// </summary>
        public static void Space(float width, bool expand, int num = 1)
        {
            for (int i = 0; i < num; i++) EditorGUILayout.Space(width, expand);
        }
#endif

        #endregion

        /// <summary>
        /// 按钮
        /// </summary>
        public static float Knob(Vector2 knobSize, float value, float minValue, float maxValue, string unit, Color backgroundColor, Color activeColor, bool showValue, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Knob(knobSize, value, minValue, maxValue, unit, backgroundColor, activeColor, showValue, options);
        }

        /// <summary>
        /// 分隔符
        /// </summary>
        public static void Separator(int num = 1)
        {
            for (int i = 0; i < num; i++) EditorGUILayout.Separator();
        }

        /// <summary>
        /// 复制文本信息
        /// </summary>
        public static Action<string> CopyTextAction = (contents) =>
        {
            TextEditor textEditor = new TextEditor();
            textEditor.text = contents;
            textEditor.OnFocus();
            textEditor.Copy();
        };
    }
}
