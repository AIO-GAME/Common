using UnityEngine;

namespace AIO.UEditor
{
    partial class GraphicRect
    {
        /// <summary>
        /// 开启输入事件监听
        /// </summary>
        public void OnOpenEvent()
        {
            if (IsEvent == false) return;
            if (Event.current != null)
            {
                switch (Event.current.type)
                {
                    case EventType.ExecuteCommand:
                        EventExecuteCommand(Event.current);
                        break;
                    case EventType.ValidateCommand:
                        EventValidateCommand(Event.current);
                        break;

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
#if UNITY_2020_1_OR_NEWER
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
#endif
                }
            }
        }

        /// <summary>
        /// 事件类型 验证命令
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventValidateCommand(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 触摸抬起
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventTouchUp(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 触摸静止
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventTouchStationary(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 触摸移动
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventTouchMove(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 触摸进入
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventTouchEnter(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 触摸离开
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventTouchLeave(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 触摸按下
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventTouchDown(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 滚轮滚动
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventScrollWheel(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 执行命令
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventExecuteCommand(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 鼠标抬起
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="keyCode"></param>
        public virtual void EventKeyUp(in Event eventData, in KeyCode keyCode)
        {
        }

        /// <summary>
        /// 事件类型 鼠标按下
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="keyCode"></param>
        public virtual void EventKeyDown(in Event eventData, in KeyCode keyCode)
        {
        }

        /// <summary>
        /// 事件类型 拖拽更新
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventDragUpdated(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 拖拽执行
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventDragPerform(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 拖拽退出
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventDragExited(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 重绘
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventRepaint(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 布局
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventLayout(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 忽略
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventIgnore(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 失去焦点
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventUsed(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 右键点击
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventContextClick(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 鼠标离开窗口
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventMouseLeaveWindow(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 鼠标进入窗口
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventMouseEnterWindow(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 鼠标离开
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventMouseMove(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 鼠标拖拽
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventMouseDrag(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 鼠标抬起
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventMouseUp(in Event eventData)
        {
        }

        /// <summary>
        /// 事件类型 鼠标按下
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void EventMouseDown(in Event eventData)
        {
        }
    }
}