/*=================================================================================================|*|
↓  Copyright(C) 2022 by DefaultCompany            |*| ╭╩╮╮╮╔════╗╔═══════╗╔════╗╔═══════╗╔═══════╗  ↩
↓  All Rights Reserved By Author lihongliu.       |*|╭╯L ╭╠╣ No ╠╣ Pains ╠╣ No ╠╣ Gains ╠╣ XNSKY ╟  ↩
↓  Author:      |*| XiNan                         |*|╰◎═◎╯╯╚◎══◎╝╚◎═════◎╝╚◎══◎╝╚◎═════◎╝╚◎═════◎╝  ↩
↓  Email:       |*| 1398581458@qq.com                                                               ↩
↓  Version:     |*| 1.0                           |*| ╭╩╮╮╮╔════╗╔═══╗╔═══╗╔═══════╗╔════╗╔══════╗  ↩
↓  UnityVersion:|*| 2021.2.13f1c1                 |*|╭╯H ╭╠╣Only╠╣You╠╣Can╠╣Cantrol╠╣Your╠╣Future╟  ↩
↓  Date:        |*| 2022-03-03                    |*|╰◎═◎╯╯╚◎══◎╝╚◎═◎╝╚◎═◎╝╚◎═════◎╝╚◎══◎╝╚◎════◎╝  ↩
↓  URL:         |*| www.XiNansky.com                                                                ↩
↓  Nowtime:     |*| 13:25:02                      |*| ╭╩╮╮╮╔═════╗╔════╗╔══════╗╔═══╗╔══════╗╔═══╗  ↩
↓  Description: |*| |U_U|                         |*|╭╯L ╭╠╣There╠╣ Is ╠╣Always╠╣ A ╠╣Better╠╣Way╟  ↩
↓  History:     |*| |>"<|                         |*|╰◎═◎╯╯╚◎═══◎╝╚◎══◎╝╚◎════◎╝╚◎═◎╝╚◎════◎╝╚◎═◎╝  ↩
↓===================================================================================================*/

using UnityEditor;
using UnityEngine;

namespace UnityEditor
{
    public static partial class GTLayout
    {
        /// <summary>
        /// 区域 / 范围
        /// </summary>
        public class Scope
        {
            /// <summary> 用于管理 BeginHorizontal / EndHorizontal 的一次性助手类 </summary>
            public class Horizontal : EditorGUILayout.HorizontalScope
            {
                public Horizontal(params GUILayoutOption[] options) : base(options)
                {
                }

                public Horizontal(GUIStyle style, params GUILayoutOption[] options) : base(style, options)
                {
                }

                protected override void CloseScope()
                {
                    base.CloseScope();
                }
            }

            /// <summary> 用于管理 ToggleGroupScope 的一次性助手类 </summary>
            public class ToggleGroup : EditorGUILayout.ToggleGroupScope
            {
                public ToggleGroup(string label, bool toggle) : base(label, toggle)
                {
                }

                public ToggleGroup(GUIContent label, bool toggle) : base(label, toggle)
                {
                }

                protected override void CloseScope()
                {
                    base.CloseScope();
                }
            }

            /// <summary> 用于管理 BeginVertical / EndVertical 的一次性助手类 </summary>
            public class Vertical : EditorGUILayout.VerticalScope
            {
                public Vertical(params GUILayoutOption[] options) : base(options)
                {
                }

                public Vertical(GUIStyle style, params GUILayoutOption[] options) : base(style, options)
                {
                }

                protected override void CloseScope()
                {
                    base.CloseScope();
                }
            }

            /// <summary> 用于管理 BeginScrollView / EndScrollView 的一次性助手类 </summary>
            public class ScrollView : EditorGUILayout.ScrollViewScope
            {
                public ScrollView(Vector2 scrollPosition, params GUILayoutOption[] options) : base(scrollPosition, options)
                {
                }

                public ScrollView(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options) : base(scrollPosition, style, options)
                {
                }

                public ScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options) : base(scrollPosition, alwaysShowHorizontal,
                    alwaysShowVertical, options)
                {
                }

                public ScrollView(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options) : base(scrollPosition, horizontalScrollbar,
                    verticalScrollbar, options)
                {
                }

                public ScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background,
                    params GUILayoutOption[] options) : base(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options)
                {
                }

                protected override void CloseScope()
                {
                    base.CloseScope();
                }
            }

            /// <summary> 开始一个可以隐藏/显示的组，转换将被动画化。 </summary>
            public class FadeGroup : EditorGUILayout.FadeGroupScope
            {
                /// <param name="value">0:hide,1:show</param>
                public FadeGroup(float value) : base(value)
                {
                }

                protected override void CloseScope()
                {
                    base.CloseScope();
                }
            }
        }
    }
}