#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 抽屉检视器
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class DrawerAttribute : InspectorAttribute
    {
        /// <summary>
        /// 抽屉检视器
        /// </summary>
        /// <param name="name">显示名称</param>
        /// <param name="defaultOpened">默认是否打开</param>
        /// <param name="toggleOnLabelClick">抽屉的标签是否也可点击</param>
        public DrawerAttribute(string name, bool defaultOpened = false, bool toggleOnLabelClick = true)
        {
            Name               = name;
            Condition          = null;
            Style              = null;
            DefaultOpened      = defaultOpened;
            ToggleOnLabelClick = toggleOnLabelClick;
        }

        /// <summary>
        /// 抽屉检视器
        /// </summary>
        /// <param name="name">显示名称</param>
        /// <param name="condition">显示条件判断方法的名称，返回值必须为bool</param>
        /// <param name="defaultOpened">默认是否打开</param>
        /// <param name="toggleOnLabelClick">抽屉的标签是否也可点击</param>
        public DrawerAttribute(string name,
                               string condition,
                               bool   defaultOpened      = false,
                               bool   toggleOnLabelClick = true)
        {
            Name               = name;
            Condition          = condition;
            Style              = null;
            DefaultOpened      = defaultOpened;
            ToggleOnLabelClick = toggleOnLabelClick;
        }

        /// <summary>
        /// 抽屉检视器
        /// </summary>
        /// <param name="name">显示名称</param>
        /// <param name="condition">显示条件判断方法的名称，返回值必须为bool</param>
        /// <param name="style">GUI样式</param>
        /// <param name="defaultOpened">默认是否打开</param>
        /// <param name="toggleOnLabelClick">抽屉的标签是否也可点击</param>
        public DrawerAttribute(string name,
                               string condition,
                               string style,
                               bool   defaultOpened      = false,
                               bool   toggleOnLabelClick = true)
        {
            Name               = name;
            Condition          = condition;
            Style              = style;
            DefaultOpened      = defaultOpened;
            ToggleOnLabelClick = toggleOnLabelClick;
        }

        public string Name               { get; private set; }
        public string Condition          { get; private set; }
        public string Style              { get; private set; }
        public bool   DefaultOpened      { get; private set; }
        public bool   ToggleOnLabelClick { get; private set; }
    }
}