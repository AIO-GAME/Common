#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    ///     图形窗口
    /// </summary>
    public abstract partial class GraphicWindow : EmptyGraphWindow, IGraphRect
    {
        /// <summary>
        ///     组名 编写额外的窗口
        /// </summary>
        public string Group { get; }

        /// <summary>
        ///     控件ID
        /// </summary>
        protected int ControlId { get; private set; }

        /// <summary>
        ///     组列表
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="type">类型</param>
        private static void AddGroup(string key, Type type)
        {
            if (!GroupTable.ContainsKey(key)) GroupTable.Add(key, new List<Type>());
            if (GroupTable[key].Contains(type)) return;
            GroupTable[key].Add(type);
        }

        /// <summary>
        ///     组列表
        /// </summary>
        /// <param name="key">键值</param>
        /// <typeparam name="T">类型</typeparam>
        private static void AddGroup<T>(string key)
        where T : EditorWindow
        {
            if (!GroupTable.ContainsKey(key)) GroupTable[key] = new List<Type>();
            var type                                          = typeof(T);
            if (GroupTable[key].Contains(type)) return;
            GroupTable[key].Add(type);
        }

        /// <summary>
        ///     组列表
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="type">类型</param>
        private static IEnumerable<Type> GetGroup(string key, Type type)
        {
            if (type is null) return Array.Empty<Type>();
            return GroupTable.TryGetValue(key, out var value)
                ? value.Where(item => item != type)
                : Array.Empty<Type>();
        }

        /// <summary>
        ///     组列表
        /// </summary>
        /// <param name="key">键值</param>
        /// <typeparam name="T">类型</typeparam>
        private static IEnumerable<Type> GetGroup<T>(string key)
        where T : EditorWindow => GetGroup(key, typeof(T));

        /// <summary>
        ///     视图元素
        /// </summary>
        protected List<GraphicRect> GraphicItems { get; private set; }

        /// <inheritdoc />
        protected GraphicWindow()
        {
            var attribute = GetType().GetCustomAttribute<GWindowAttribute>(false);
            if (attribute != null)
            {
                minSize = new Vector2
                {
                    x = attribute.MinSizeWidth == 0 ? minSize.x : attribute.MinSizeWidth, y = attribute.MinSizeHeight == 0 ? minSize.y : attribute.MinSizeHeight
                };

                maxSize = new Vector2
                {
                    x = attribute.MaxSizeWidth == 0 ? maxSize.x : attribute.MaxSizeWidth, y = attribute.MaxSizeHeight == 0 ? maxSize.y : attribute.MaxSizeHeight
                };
                if (string.IsNullOrEmpty(attribute.Group)) return;
                Group = attribute.Group;
            }

            if (string.IsNullOrEmpty(Group)) Group = "Default";
            RefreshGroup();
        }

        /// <summary>
        ///     有效程序集
        /// </summary>
        protected static ICollection<Assembly> Assemblies { get; private set; }

        private void RefreshGroup()
        {
            var subtype  = typeof(EditorWindow);
            var typeSelf = GetType();
            if (Assemblies is null)
            {
                Assemblies = new List<Assembly>();
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.FullName.Contains("Editor")) continue;
                    Assemblies.Add(assembly);
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type == typeSelf || !type.IsSubclassOf(subtype)) continue;
                        var attribute = type.GetCustomAttribute<GWindowAttribute>(false);
                        if (attribute is null) continue;
                        if (string.IsNullOrEmpty(attribute.Group)) continue;
                        if (!Group.Equals(attribute.Group)) continue;
                        AddGroup(Group, type);
                    }
                }
            }
            else
            {
                foreach (var type
                         in from assembly in Assemblies
                            from type in assembly.GetTypes()
                            where type != typeSelf && type.IsSubclassOf(subtype)
                            let attribute = type.GetCustomAttribute<GWindowAttribute>(false)
                            where !string.IsNullOrEmpty(attribute?.Group)
                            where Group.Equals(attribute.Group)
                            select type
                        ) AddGroup(Group, type);
            }
        }

        #region sealed

        /// <inheritdoc />
        protected sealed override void OnEnable()
        {
            GraphicItems = new List<GraphicRect>();
#if UNITY_2020_1_OR_NEWER
            if (!docked)
#endif
            {
                try
                {
                    position = new Rect(new Vector2(
                                                    (Screen.currentResolution.width - minSize.x) / 2,
                                                    (Screen.currentResolution.height - minSize.y) / 2),
                                        minSize);
                }
                catch
                {
                    // ignored
                }
            }

            OnGUIStyle();
            OnActivation();
        }

        /// <inheritdoc />
        protected sealed override void OnGUI() { Draw(); }

        /// <inheritdoc />
        protected sealed override void Awake() { OnAwake(); }

        /// <inheritdoc />
        protected sealed override void Update()
        {
            base.Update();
            OnUpdate();
        }

        /// <inheritdoc />
        protected sealed override void OnDestroy()
        {
            OnDisable();
            GraphicItems?.Clear();
            EHelper.Window.Free(this);
#if UNITY_2021_1_OR_NEWER || !UNITY_2020_1_OR_NEWER
            AssetDatabase.SaveAssets();
#endif
        }

        /// <inheritdoc />
#if UNITY_2019_1_OR_NEWER
        public sealed override IEnumerable<Type> GetExtraPaneTypes()
        {
            return string.IsNullOrEmpty(Group) ? base.GetExtraPaneTypes() : GetGroup(Group, GetType());
        }
#else
        public override IEnumerable<Type> GetExtraPaneTypes()
        {
            if (string.IsNullOrEmpty(Group)) return Array.Empty<Type>();
            return GroupList;
        }
#endif

        #endregion

        public Rect RectData => position;

        public Rect RectCenter => new Rect(position.size / 2, position.size);

        public void Draw()
        {
            try { OnDraw(); }
            catch (Exception e)
            {
                Debug.LogException(e);
                Close();
                EHelper.Window.Free(this);
                GUIUtility.ExitGUI();
                return;
            }

            ControlId = GUIUtility.GetControlID(FocusType.Passive, position);
        }

        public void Clear() { OnClear(); }

        void IGraphRect.OnAwake() => OnAwake();

        void IDisposable.Dispose() { }

        #region virtual

        /// <summary>
        ///     激活
        /// </summary>
        protected virtual void OnActivation() { }

        /// <summary>
        ///     初始化皮肤格式化
        /// </summary>
        protected virtual void OnGUIStyle() { }

        /// <summary>
        ///    绘制
        /// </summary>
        protected virtual void OnDraw() { }

        /// <summary>
        ///     清除
        /// </summary>
        protected virtual void OnClear() { }

        /// <summary>
        ///     脚本启用时调用
        /// </summary>
        protected virtual void OnAwake() { }

        /// <summary>
        ///     在所有可见窗口上每秒调用多次。
        /// </summary>
        protected virtual void OnUpdate() { }

        #endregion

        #region event

        /// <summary>
        ///     开启输入事件监听
        /// </summary>
        public void OnOpenEvent()
        {
            if (Event.current == null) return;
            switch (Event.current.GetTypeForControl(ControlId))
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

        /// <summary>
        ///     验证一个特殊的命令(例如复制和粘贴)。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventValidateCommand(in Event eventData) { }

        /// <summary>
        ///     直接操作装置(手指、笔)离开屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchUp(in Event eventData) { }

        /// <summary>
        ///     直接操作装置(手指、笔)静止事件(长触)
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchStationary(in Event eventData) { }

        /// <summary>
        ///     直接操纵装置(手指、笔)在屏幕上移动(拖动)。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchMove(in Event eventData) { }

        /// <summary>
        ///     直接操作装置(手指、笔)进入屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchEnter(in Event eventData) { }

        /// <summary>
        ///     直接操作装置(手指、笔)离开屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchLeave(in Event eventData) { }

        /// <summary>
        ///     直接操作装置(手指、笔)触摸屏幕。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventTouchDown(in Event eventData) { }

        /// <summary>
        ///     滚轮被移动了。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventScrollWheel(in Event eventData) { }

        /// <summary>
        ///     执行一个特殊的命令。复制粘贴
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventExecuteCommand(in Event eventData) { }

        /// <summary>
        ///     松开一个键盘键。
        /// </summary>
        /// <param name="eventData">数据</param>
        /// <param name="keyCode">按键类型</param>
        public virtual void EventKeyUp(in Event eventData, in KeyCode keyCode) { }

        /// <summary>
        ///     按了一个键盘键。
        /// </summary>
        /// <param name="eventData">数据</param>
        /// <param name="keyCode">按键类型</param>
        public virtual void EventKeyDown(in Event eventData, in KeyCode keyCode) { }

        /// <summary>
        ///     编辑器:拖放操作更新。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventDragUpdated(in Event eventData) { }

        /// <summary>
        ///     仅限编辑器:执行拖放操作。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventDragPerform(in Event eventData) { }

        /// <summary>
        ///     仅限编辑器:拖放操作退出。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventDragExited(in Event eventData) { }

        /// <summary>
        ///     重绘事件。每帧发送一个。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventRepaint(in Event eventData) { }

        /// <summary>
        ///     一个布局事件。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventLayout(in Event eventData) { }

        /// <summary>
        ///     事件应该被忽略。
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventIgnore(in Event eventData) { }

        /// <summary>
        ///     已经处理的事件
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventUsed(in Event eventData) { }

        /// <summary>
        ///     鼠标右键单击
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventContextClick(in Event eventData) { }

        /// <summary>
        ///     鼠标离开窗口
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseLeaveWindow(in Event eventData) { }

        /// <summary>
        ///     鼠标进入窗口
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseEnterWindow(in Event eventData) { }

        /// <summary>
        ///     鼠标移动
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseMove(in Event eventData) { }

        /// <summary>
        ///     鼠标拖拽
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseDrag(in Event eventData) { }

        /// <summary>
        ///     鼠标送开
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseUp(in Event eventData) { }

        /// <summary>
        ///     鼠标按下
        /// </summary>
        /// <param name="eventData">数据</param>
        public virtual void EventMouseDown(in Event eventData) { }

        #endregion

        /// <summary>
        ///     创建设置提供者
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="scope">面板类型</param>
        /// <returns>
        ///     <see cref="SettingsProvider" />
        /// </returns>
        protected static GraphicSettingsProvider CreateSettingsProvider(string path, SettingsScope scope = SettingsScope.User)
        {
            return new GraphicSettingsProvider($"AIO/{path}", scope);
        }
    }
}