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

namespace AIO.Unity.Editor
{
    public static partial class GT
    {
        public static class AC
        {
            #region 序列化属性 FieldProperty

            /// <summary>
            /// SerializedProperty创建一个字段
            /// </summary>
            public static bool FieldProperty(Rect rect, SerializedProperty property, string label, bool includeChildren = false)
            {
                return EditorGUI.PropertyField(rect, property, new GUIContent(label), includeChildren);
            }

            /// <summary>
            /// SerializedProperty创建一个字段
            /// </summary>
            public static bool FieldProperty(Rect rect, SerializedProperty property, GUIContent label, bool includeChildren = false)
            {
                return EditorGUI.PropertyField(rect, property, label, includeChildren);
            }

            /// <summary>
            /// SerializedProperty创建一个字段
            /// </summary>
            public static bool FieldProperty(Rect rect, SerializedProperty property, bool includeChildren = false)
            {
                return EditorGUI.PropertyField(rect, property, includeChildren);
            }

            #endregion

            #region 序列化属性 FieldText

            /// <summary>
            /// SerializedProperty创建一个字段
            /// </summary>
            public static string FieldText(Rect rect, string label, string content, GUIStyle style = null)
            {
                if (style == null) style = EditorStyles.textField;
                return EditorGUI.TextField(rect, label, content, style);
            }

            /// <summary>
            /// SerializedProperty创建一个字段
            /// </summary>
            public static string FieldText(Rect rect, GUIContent label, string content, GUIStyle style = null)
            {
                if (style == null) style = EditorStyles.textField;
                return EditorGUI.TextField(rect, label, content, style);
            }

            /// <summary>
            /// SerializedProperty创建一个字段
            /// </summary>
            public static string FieldText(Rect rect, string content, GUIStyle style = null)
            {
                if (style == null) style = EditorStyles.textField;
                return EditorGUI.TextField(rect, content, style);
            }

            /// <summary>
            /// SerializedProperty创建一个字段
            /// </summary>
            public static string FieldText(Vector2 Pos, Vector2 Size, string content, GUIStyle style = null)
            {
                if (style == null) style = EditorStyles.textField;
                return EditorGUI.TextField(new Rect(Pos, Size), content, style);
            }

            #endregion

            #region 序列化属性 TextArea

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static string TextArea(Rect rect, string content, GUIStyle style)
            {
                return EditorGUI.TextArea(rect, content, style);
            }

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static string TextArea(Rect rect, string content)
            {
                return EditorGUI.TextArea(rect, content, EditorStyles.textArea);
            }

            #endregion

            #region 序列化属性 FieldInt

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static int FieldInt(Rect rect, int value, GUIStyle style)
            {
                return EditorGUI.IntField(rect, value, style);
            }

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static int FieldInt(Rect rect, int value)
            {
                return EditorGUI.IntField(rect, value, EditorStyles.textArea);
            }

            #endregion
        }
    }
}