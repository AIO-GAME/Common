﻿#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 通用菜单检视器（支持 string 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class GenericMenuAttribute : InspectorAttribute
    {
        /// <summary>
        /// 通用菜单检视器（支持 string 类型）
        /// </summary>
        /// <param name="generateMenu">生成菜单的所有选项的方法名称，返回值必须为string[]</param>
        /// <param name="chooseMenu">选择菜单选项后调用的方法名称，必须带有一个string参数</param>
        public GenericMenuAttribute(string generateMenu, string chooseMenu = null)
        {
            GenerateMenu = generateMenu;
            ChooseMenu   = chooseMenu;
        }

        public string GenerateMenu { get; private set; }
        public string ChooseMenu   { get; private set; }
    }
}