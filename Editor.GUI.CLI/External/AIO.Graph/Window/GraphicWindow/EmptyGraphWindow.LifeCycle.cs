/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-26

|||✩ - - - - - |*/

using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// EmptyGraphWindow
    /// </summary>
    public partial class EmptyGraphWindow
    {
        /// <summary>
        /// 脚本启用时调用
        /// </summary>
        protected virtual void Awake()
        {
        }

        /// <summary>
        /// CreateGUl在编辑器窗口的rootVisualElement准备填充时被调用。
        /// </summary>
        protected virtual void CreateGUI()
        {
        }

        /// <summary>
        /// 该函数在加载对象时被调用。
        /// </summary>
        protected abstract void OnEnable();

        /// <summary>
        /// 在这里实现您自己的编辑器GUI。
        /// </summary>
        protected virtual void OnGUI()
        {
        }

        /// <summary>
        /// 当编辑器窗口作为选项卡添加时调用。放入选项卡 在选项卡被添加 都会触发
        /// </summary>
        protected virtual void OnAddedAsTab()
        {
        }

        /// <summary>
        /// 在拖放过程中调用。当一个编辑器窗口标签被分离时，此函数没有文档。
        /// </summary>
        protected virtual void OnTabDetached()
        {
        }

        /// <summary>
        /// 当打开编辑器窗口时调用。此函数未记录。
        /// </summary>
        protected virtual void OnBecameVisible()
        {
        }

        /// <summary>
        /// 当编辑器窗口关闭时调用。此函数没有记录。
        /// </summary>
        protected virtual void OnBecameInvisible()
        {
        }

        /// <summary>
        /// 当主窗口移动时调用。
        /// </summary>
        protected virtual void OnMainWindowMove()
        {
        }

        /// <summary>
        /// 当场景被打开时调用。此函数没有记录。
        /// </summary>
        protected virtual void OnDidOpenScene()
        {
        }

        /// <summary>
        /// 用于在层次结构中的对象或对象组发生更改时发送的消息的处理程序。
        /// </summary>
        protected virtual void OnHierarchyChange()
        {
        }

        /// <summary>
        /// 当窗口获得键盘焦点时调用。
        /// </summary>
        protected virtual void OnFocus()
        {
        }

        /// <summary>
        /// 对窗口的内容执行保存操作。
        /// </summary>
#if UNITY_2020_1_OR_NEWER
        public override void SaveChanges()
        {
            base.SaveChanges();
        }
#else
        public virtual void SaveChanges()
        {
        }
#endif

        /// <summary>
        /// 允许编辑器窗格在通用菜单旁边显示一个小按钮(例如检查器锁图标)。
        /// </summary>
        protected virtual void OnShowButton(in Rect rect)
        {
        }

        /// <summary>
        /// 当修改键被更改时调用。自动注册和取消编辑器应用程序。modifierKeysChanged event该函数无文档。
        /// </summary>
        protected virtual void OnModifierKeysChanged()
        {
        }

        /// <summary>
        /// 在更改此EditorWindow的UI缩放时调用。
        /// </summary>
#if UNITY_2020_1_OR_NEWER
        protected override void OnBackingScaleFactorChanged()
        {
            base.OnBackingScaleFactorChanged();
        }
#else
        protected virtual void OnBackingScaleFactorChanged()
        {
        }
#endif

        /// <summary>
        /// 以每秒10帧的速度被调用，给检查器一个更新的机会。
        /// </summary>
        protected virtual void OnInspectorUpdate()
        {
        }

        /// <summary>
        /// 只有编辑器的函数，Unity在脚本加载或检查器中的值改变时调用。
        /// </summary>
        protected virtual void OnValidate()
        {
        }

        /// <summary>
        /// 当可脚本化对象超出作用域时调用此函数。
        /// </summary>
        protected virtual void OnDisable()
        {
        }

        /// <summary>
        /// 调用OnDestroy关闭EditorWindow窗口。
        /// </summary>
        protected virtual void OnDestroy()
        {
        }

        /// <summary>
        /// 每当选择发生变化时调用。
        /// </summary>
        protected virtual void OnSelectionChange()
        {
        }

        /// <summary>
        /// 每当项目状态更改时发送的消息的处理程序。
        /// </summary>
        protected virtual void OnProjectChange()
        {
        }

        /// <summary>
        /// 当窗口失去键盘焦点时调用。
        /// </summary>
        protected virtual void OnLostFocus()
        {
        }

        /// <summary>
        /// 重置为默认值。
        /// </summary>
        protected virtual void Reset()
        {
        }

        /// <summary>
        /// 将自定义菜单项添加到编辑器窗口。
        /// </summary>
        protected virtual void OnAddItemsToMenu( UnityEditor.GenericMenu menu)
        {
        }

        /// <summary>
        /// 在所有可见窗口上每秒调用多次。
        /// </summary>
        protected virtual void Update()
        {
            // Position isn't reliable in GUI calls due to layouting, so cache it here
            ReliablePosition = position;
        }
    }
}
