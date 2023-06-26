﻿/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Reflection;
using AIO;
using UnityEngine;

namespace UnityEditor
{
    /// <summary>
    /// 图形窗口
    /// </summary>
    public abstract class GraphicWindow : EmptyGraphWindow, IGraphEvent
    {
        /// <summary>
        /// 组名 编写额外的窗口
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// 标题名
        /// </summary>
        private GUIContent Title { get; }

        /// <summary>
        /// 最小大小
        /// </summary>
        private Vector2 MinSize { get; }

        /// <summary>
        /// 组列表
        /// </summary>
        private List<Type> GroupList;

        /// <summary>
        /// 视图元素
        /// </summary>
        protected List<GraphicRect> GraphicItems { get; private set; }

        /// <inheritdoc />
        public GraphicWindow()
        {
            GraphicItems = Pool.List<GraphicRect>.New();
            GroupList = new List<Type>();
            var attribute = GetType().GetCustomAttribute<WindowExtraAttribute>(false);
            if (attribute != null && !string.IsNullOrEmpty(attribute.Group))
            {
                Group = attribute.Group;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.Attributes != TypeAttributes.Class) continue;
                        foreach (var customAttribute in type.GetCustomAttributes<WindowExtraAttribute>(false))
                        {
                            if (Group.Equals(customAttribute.Group))
                            {
                                if (type == GetType()) break;
                                GroupList.Add(type);
                                break;
                            }
                        }
                    }
                }
            }

            var titleAttribute = GetType().GetCustomAttribute<WindowTitleAttribute>(false);
            if (titleAttribute != null) Title = titleAttribute.Title;
            else Title = new GUIContent(GetType().Name.Prettify());

            var minSizeAttribute = GetType().GetCustomAttribute<WindowMinSizeAttribute>(false);
            if (minSizeAttribute != null) MinSize = new Vector2(minSizeAttribute.Width, minSizeAttribute.Height);
            else MinSize = minSize;
        }

        /// <summary>
        /// 初始化皮肤格式化
        /// </summary>
        protected virtual void OnGUIStyle()
        {
        }

        /// <inheritdoc />
        protected override void OnEnable()
        {
            if (!docked)
            {
                position = new Rect(
                    new Vector2(
                        Screen.currentResolution.width * 0.5f - MinSize.x / 2,
                        Screen.currentResolution.height * 0.5f - MinSize.y / 2),
                    MinSize);
            }

            OnGUIStyle();
        }

        /// <inheritdoc />
        protected sealed override void Awake()
        {
            titleContent = Title;
            minSize = MinSize;
            OnAwake();
        }

        /// <summary>
        /// 脚本启用时调用
        /// </summary>
        protected virtual void OnAwake()
        {
        }

        /// <summary>
        /// 获取与窗口关联的额外窗格。
        /// </summary>
        public sealed override IEnumerable<Type> GetExtraPaneTypes()
        {
            if (string.IsNullOrEmpty(Group)) return base.GetExtraPaneTypes();
            GroupList.Add(base.GetExtraPaneTypes());
            return GroupList;
        }

        /// <summary>
        /// 在所有可见窗口上每秒调用多次。
        /// </summary>
        protected sealed override void Update()
        {
            base.Update();
            OnUpdate();
        }

        /// <summary>
        /// 在所有可见窗口上每秒调用多次。
        /// </summary>
        protected virtual void OnUpdate()
        {
        }

        #region Event

        /// <summary>
        /// 开启输入事件监听
        /// </summary>
        public void OnOpenEvent()
        {
            if (Event.current != null)
            {
                switch (Event.current.type)
                {
                    case EventType.ContextClick:
                        EventContextClick(Event.current);
                        break;

                    case EventType.MouseUp:
                        EventMouseUp(Event.current);
                        break;
                    case EventType.MouseDown:
                        EventMouseDown(Event.current);
                        break;
                    case EventType.MouseDrag:
                        EventMouseDrag(Event.current);
                        break;
                    case EventType.MouseMove:
                        EventMouseMove(Event.current);
                        break;
                    case EventType.MouseEnterWindow:
                        EventMouseEnterWindow(Event.current);
                        break;
                    case EventType.MouseLeaveWindow:
                        EventMouseLeaveWindow(Event.current);
                        break;

                    case EventType.Used:
                        EventUsed(Event.current);
                        break;
                    case EventType.Ignore:
                        EventIgnore(Event.current);
                        break;
                    case EventType.Layout:
                        EventLayout(Event.current);
                        break;
                    case EventType.Repaint:
                        EventRepaint(Event.current);
                        break;

                    case EventType.DragExited:
                        EventDragExited(Event.current);
                        break;
                    case EventType.DragPerform:
                        EventDragPerform(Event.current);
                        break;
                    case EventType.DragUpdated:
                        EventDragUpdated(Event.current);
                        break;

                    case EventType.KeyDown:

                        EventKeyDown(Event.current, Event.current.keyCode);
                        break;
                    case EventType.KeyUp:
                        EventKeyUp(Event.current, Event.current.keyCode);
                        break;
                    case EventType.ScrollWheel:
                        EventScrollWheel(Event.current);
                        break;

                    case EventType.ExecuteCommand:
                        EventExecuteCommand(Event.current);
                        break;
                    case EventType.ValidateCommand:
                        EventValidateCommand(Event.current);
                        break;

                    case EventType.TouchDown:
                        EventTouchDown(Event.current);
                        break;
                    case EventType.TouchLeave:
                        EventTouchLeave(Event.current);
                        break;
                    case EventType.TouchEnter:
                        EventTouchEnter(Event.current);
                        break;
                    case EventType.TouchMove:
                        EventTouchMove(Event.current);
                        break;
                    case EventType.TouchStationary:
                        EventTouchStationary(Event.current);
                        break;
                    case EventType.TouchUp:
                        EventTouchUp(Event.current);
                        break;
                }
            }
        }

        /// <summary>
        ///  验证一个特殊的命令(例如复制和粘贴)。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventValidateCommand(in Event eventData)
        {
        }

        /// <summary>
        /// 直接操作装置(手指、笔)离开屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchUp(in Event eventData)
        {
        }

        /// <summary>
        /// 直接操作装置(手指、笔)静止事件(长触)
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchStationary(in Event eventData)
        {
        }

        /// <summary>
        /// 直接操纵装置(手指、笔)在屏幕上移动(拖动)。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchMove(in Event eventData)
        {
        }

        /// <summary>
        /// 直接操作装置(手指、笔)进入屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchEnter(in Event eventData)
        {
        }

        /// <summary>
        /// 直接操作装置(手指、笔)离开屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchLeave(in Event eventData)
        {
        }

        /// <summary>
        /// 直接操作装置(手指、笔)触摸屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchDown(in Event eventData)
        {
        }

        /// <summary>
        /// 滚轮被移动了。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventScrollWheel(in Event eventData)
        {
        }

        /// <summary>
        /// 执行一个特殊的命令。复制粘贴
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventExecuteCommand(in Event eventData)
        {
        }

        /// <summary>
        /// 松开一个键盘键。
        /// </summary>
        /// <param name="eventData">数据</param>
        /// <param name="keyCode">按键类型</param>
        public virtual void EventKeyUp(in Event eventData, in KeyCode keyCode)
        {
        }

        /// <summary>
        /// 按了一个键盘键。
        /// </summary>
        /// <param name="eventData">数据</param>
        /// <param name="keyCode">按键类型</param>
        public virtual void EventKeyDown(in Event eventData, in KeyCode keyCode)
        {
        }

        /// <summary>
        /// 编辑器:拖放操作更新。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventDragUpdated(in Event eventData)
        {
        }

        /// <summary>
        /// 仅限编辑器:执行拖放操作。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventDragPerform(in Event eventData)
        {
        }

        /// <summary>
        /// 仅限编辑器:拖放操作退出。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventDragExited(in Event eventData)
        {
        }

        /// <summary>
        /// 重绘事件。每帧发送一个。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventRepaint(in Event eventData)
        {
        }

        /// <summary>
        /// 一个布局事件。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventLayout(in Event eventData)
        {
        }

        /// <summary>
        /// 事件应该被忽略。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventIgnore(in Event eventData)
        {
        }

        /// <summary>
        /// 已经处理的事件
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventUsed(in Event eventData)
        {
        }

        /// <summary>
        /// 鼠标右键单击
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventContextClick(in Event eventData)
        {
        }

        /// <summary>
        /// 鼠标离开窗口
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseLeaveWindow(in Event eventData)
        {
        }

        /// <summary>
        /// 鼠标进入窗口
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseEnterWindow(in Event eventData)
        {
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseMove(in Event eventData)
        {
        }

        /// <summary>
        /// 鼠标拖拽
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseDrag(in Event eventData)
        {
        }

        /// <summary>
        /// 鼠标送开
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseUp(in Event eventData)
        {
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseDown(in Event eventData)
        {
        }

        #endregion
    }
}