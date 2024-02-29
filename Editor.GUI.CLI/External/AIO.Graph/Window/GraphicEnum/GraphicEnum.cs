using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace AIO.UEditor
{
    /// <summary>
    /// 图形枚举类
    /// </summary>
    [DisplayName("枚举图形类")]
    public partial class GraphicEnum : GraphicRect
    {
        private static Dictionary<int, EnumInfo> Data;

        static GraphicEnum()
        {
            Data = new Dictionary<int, EnumInfo>();
        }

        /// <summary>
        /// 获取描述信息
        /// </summary>
        public static Dictionary<T, string> GetDescription<T>() where T : struct, Enum
        {
            var type = typeof(T);
            var DescriptionDic = new Dictionary<T, string>();
            var index = 0;
            var values = Enum.GetValues(type);
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute(typeof(DescriptionAttribute), false);
                if (attribute is DescriptionAttribute description)
                    DescriptionDic.Add((T)values.GetValue(index++), description.Description);
                else DescriptionDic.Add((T)values.GetValue(index++), field.Name);
            }

            return DescriptionDic;
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        public static Dictionary<object, string> GetDescription(in Type type)
        {
            var DescriptionDic = new Dictionary<object, string>();
            var index = 0;
            var values = Enum.GetValues(type);
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute(typeof(DescriptionAttribute), false);
                if (attribute is DescriptionAttribute description)
                    DescriptionDic.Add(values.GetValue(index++), description.Description);
                else DescriptionDic.Add(values.GetValue(index++), field.Name);
            }

            return DescriptionDic;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public T GetValue<T>()
        {
            return (T)EnumValue;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public object GetValue(in Type type)
        {
            return Enum.Parse(type, EnumValue.ToString());
        }

        private EnumInfo Info;

        /// <summary>
        /// 显示名称
        /// </summary>
        public bool ShowName;

        /// <summary>
        /// 单项选择
        /// </summary>
        public bool MultipleChoice;

        private GenericMenu menu;

        private object EnumValue;

        /// <summary>
        /// 更新
        /// </summary>
        public GraphicEnum Update<T>(in T type) where T : struct, Enum
        {
            return Update(type, false, false);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public GraphicEnum Update<T>(in T type, in bool showName, in bool multipleChoice) where T : struct, Enum
        {
            ShowName = showName;
            MultipleChoice = multipleChoice;
            EnumValue = type;
            Info = EnumInfo.Create<T>();
            menu = new GenericMenu();
            menu.allowDuplicateNames = false;
            foreach (var description in Info.DescriptionDic)
            {
                menu.AddItem(
                    new GUIContent(description.Value),
                    EnumValue.GetHashCode() == description.Key.GetHashCode(),
                    () => EnumValue = description.Key);
            }

            return this;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public GraphicEnum Update<T>(in T type, in bool showName) where T : struct, Enum
        {
            return Update(type, showName, false);
        }

        /// <summary>
        /// 绘制
        /// </summary>
        protected override void OnDraw()
        {
            RectData = EditorGUILayout.GetControlRect();
            EditorGUI.DropShadowLabel(RectData, Info.DescriptionDic[EnumValue]);
            OnOpenEvent();
        }

        /// <summary>
        /// 打开事件
        /// </summary>
        public override void EventContextClick(in Event eventData)
        {
            if (RectData.Contains(Event.current.mousePosition))
            {
                menu.ShowAsContext();
                eventData.Use();
            }
        }
    }
}