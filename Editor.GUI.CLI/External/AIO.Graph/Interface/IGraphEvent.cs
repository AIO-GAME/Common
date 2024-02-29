using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 图像事件触发
    /// </summary>
    public interface IGraphEvent
    {
        /// <summary>
        /// 开启事件监听
        /// </summary>
        void OnOpenEvent();

        /// <summary>
        ///  验证一个特殊的命令(例如复制和粘贴)。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventValidateCommand(in Event eventData);

        /// <summary>
        /// 直接操作装置(手指、笔)离开屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventTouchUp(in Event eventData);

        /// <summary>
        /// 直接操作装置(手指、笔)静止事件(长触)
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventTouchStationary(in Event eventData);

        /// <summary>
        /// 直接操纵装置(手指、笔)在屏幕上移动(拖动)。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventTouchMove(in Event eventData);

        /// <summary>
        /// 直接操作装置(手指、笔)进入屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventTouchEnter(in Event eventData);

        /// <summary>
        /// 直接操作装置(手指、笔)离开屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventTouchLeave(in Event eventData);

        /// <summary>
        /// 直接操作装置(手指、笔)触摸屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventTouchDown(in Event eventData);

        /// <summary>
        /// 滚轮被移动了。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventScrollWheel(in Event eventData);

        /// <summary>
        /// 执行一个特殊的命令。复制粘贴
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventExecuteCommand(in Event eventData);

        /// <summary>
        /// 松开一个键盘键。
        /// </summary>
        /// <param name="eventData">数据</param>
        /// <param name="keyCode">按键类型</param>
        void EventKeyUp(in Event eventData, in KeyCode keyCode);

        /// <summary>
        /// 按了一个键盘键。
        /// </summary>
        /// <param name="eventData">数据</param>
        /// <param name="keyCode">按键类型</param>
        void EventKeyDown(in Event eventData, in KeyCode keyCode);

        /// <summary>
        /// 编辑器:拖放操作更新。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventDragUpdated(in Event eventData);

        /// <summary>
        /// 仅限编辑器:执行拖放操作。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventDragPerform(in Event eventData);

        /// <summary>
        /// 仅限编辑器:拖放操作退出。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventDragExited(in Event eventData);

        /// <summary>
        /// 重绘事件。每帧发送一个。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventRepaint(in Event eventData);

        /// <summary>
        /// 一个布局事件。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventLayout(in Event eventData);

        /// <summary>
        /// 事件应该被忽略。
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventIgnore(in Event eventData);

        /// <summary>
        /// 已经处理的事件
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventUsed(in Event eventData);

        /// <summary>
        /// 鼠标右键单击
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventContextClick(in Event eventData);

        /// <summary>
        /// 鼠标离开窗口
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventMouseLeaveWindow(in Event eventData);

        /// <summary>
        /// 鼠标进入窗口
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventMouseEnterWindow(in Event eventData);

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventMouseMove(in Event eventData);

        /// <summary>
        /// 鼠标拖拽
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventMouseDrag(in Event eventData);

        /// <summary>
        /// 鼠标送开
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventMouseUp(in Event eventData);

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="eventData">数据</param>
        void EventMouseDown(in Event eventData);
    }
}