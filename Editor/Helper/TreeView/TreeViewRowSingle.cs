#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    ///     SingleRowTreeEditor
    /// </summary>
    public abstract class TreeViewRowSingle : TreeViewBasics
    {
        /// <summary>
        ///    单选
        /// </summary>
        public event Action<int> OnSingleSelectionChanged;

        protected void InvokeSelectionChanged(int id)
        {
            if (rootItem.children.Count == 0) return;
            OnSingleSelectionChanged?.Invoke(id);
        }

        private readonly string FullName;
        protected        bool   AllowDrag        { get; set; } = true;
        protected        bool   AllowMultiSelect { get; set; }

        protected TreeViewRowSingle(TreeViewState state, MultiColumnHeader header) : base(state, header)
        {
            // ReSharper disable VirtualMemberCallInConstructor
            OnInitialize();
            FullName                      = GetType().FullName;
            showBorder                    = false;
            showAlternatingRowBackgrounds = true;
            useScrollView                 = true;
            baseIndent                    = 10;
            extraSpaceBeforeIconAndLabel  = 20;
            Reload();
            SetFocus();
        }

        #region 虚函数

        /// <summary>
        ///     初始化
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        ///     重命名完成
        /// </summary>
        /// <param name="args">重命名参数</param>
        protected virtual void OnRename(RenameEndedArgs args) { }

        /// <summary>
        ///     选择
        /// </summary>
        /// <param name="id">ID</param>
        protected virtual void OnSelection(int id) { }

        /// <summary>
        ///     拖拽交换数据
        /// </summary>
        /// <param name="from">源</param>
        /// <param name="to">目标</param>
        protected virtual void OnDragSwapData(int from, int to) { }

        /// <summary>
        ///     右键点击Item区域
        /// </summary>
        /// <param name="menu">菜单</param>
        /// <param name="item">选中组件</param>
        protected virtual void OnContextClicked(GenericMenu menu, TreeViewItem item) { }

        /// <summary>
        ///     右键点击Item区域
        /// </summary>
        /// <param name="menu">菜单</param>
        /// <param name="item">选中组件</param>
        protected virtual void OnContextClicked(GenericMenu menu, IList<TreeViewItem> item) { }

        /// <summary>
        ///     按键按下
        /// </summary>
        /// <param name="evt"> 按键事件 </param>
        /// <param name="item"> 选中组件 </param>
        protected virtual void OnEventKeyDown(Event evt, TreeViewItem item)
        {
            switch (evt.keyCode)
            {
                case KeyCode.DownArrow: // 数字键盘 下键
                {
                    var temp = item.id + 1;
                    var id   = temp >= Count ? 0 : temp;
                    SetSelection(new[] { id }, TreeViewSelectionOptions.RevealAndFrame);
                    InvokeSelectionChanged(id);
                    break;
                }
                case KeyCode.UpArrow: // 数字键盘 上键
                {
                    var temp = item.id - 1;
                    var id   = temp < 0 ? Count - 1 : temp;
                    SetSelection(new[] { id }, TreeViewSelectionOptions.RevealAndFrame);
                    InvokeSelectionChanged(id);
                    break;
                }
            }
        }

        /// <summary>
        ///     按键抬起
        /// </summary>
        /// <param name="evt"> 按键事件 </param>
        /// <param name="item"> 选中组件 </param>
        protected virtual void OnEventKeyUp(Event evt, TreeViewItem item) { }

        #endregion

        #region 接口实现

        /// <summary>
        ///     更改名称完成
        /// </summary>
        protected sealed override void RenameEnded(RenameEndedArgs args)
        {
            if (!args.acceptedRename
             || string.IsNullOrEmpty(args.newName)
             || args.newName == args.originalName
               ) return;
            OnRename(args);
            EndRename();
        }

        /// <summary>
        ///     是否能多选
        /// </summary>
        /// <param name="item">选中组件</param>
        /// <returns>Ture:能 False:不能</returns>
        protected sealed override bool CanMultiSelect(TreeViewItem item) => AllowMultiSelect;

        /// <summary>
        ///     是否能改变展开状态
        /// </summary>
        /// <param name="item">选中组件</param>
        /// <returns>Ture:能 False:不能</returns>
        protected sealed override bool CanChangeExpandedState(TreeViewItem item)
        {
            if (item is ITVItemDraw draw) return draw.AllowChangeExpandedState;
            return base.CanChangeExpandedState(item);
        }

        /// <summary>
        ///    搜索改变
        /// </summary>
        /// <param name="newSearch">新搜索</param>
        protected override void SearchChanged(string newSearch)
        {
            if (string.IsNullOrEmpty(newSearch))
            {
                Reload();
                return;
            }

            var search = newSearch.ToLower();
            rootItem.children = rootItem.children.Where(item => DoesItemMatchSearch(item, search)).ToList();
            SetupDepthsFromParentsAndChildren(rootItem);
            BuildRows(rootItem);
        }

        /// <summary>
        ///     多选
        /// </summary>
        /// <param name="id">ID</param>
        protected sealed override void DoubleClickedItem(int id)
        {
            SelectionClick(rootItem.children[id], true);
            state.selectedIDs.Add(id);
            OnSelection(id);
            OnSingleSelectionChanged?.Invoke(id);
            Event.current?.Use();
        }

        /// <summary>
        ///    按键事件
        /// </summary>
        protected sealed override void KeyEvent()
        {
            if (state.selectedIDs.Count < 1) return;
            var code = Event.current.keyCode;
            if (code == KeyCode.None) return;
            var evt = Event.current;
            switch (Event.current.type)
            {
                case EventType.KeyDown:
                {
                    OnEventKeyDown(evt, rootItem.children[state.selectedIDs[0]]);
                    Event.current.Use();
                    break;
                }
                case EventType.KeyUp:
                {
                    OnEventKeyUp(evt, rootItem.children[state.selectedIDs[0]]);
                    Event.current.Use();
                    break;
                }
            }
        }

        #region 拖拽事件

        /// <summary>
        ///     设置拖拽
        /// </summary>
        /// <param name="args"></param>
        protected sealed override void SetupDragAndDrop(SetupDragAndDropArgs args) { }

        /// <summary>
        ///     处理拖拽
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected sealed override DragAndDropVisualMode HandleDragAndDrop(DragAndDropArgs args)
        {
            if (!GUI.enabled) return DragAndDropVisualMode.None;
            if (DragAndDrop.activeControlID == ContentID)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.None;
                return DragAndDropVisualMode.None;
            }

            switch (args.dragAndDropPosition)
            {
                case DragAndDropPosition.BetweenItems: return DragAndDropVisualMode.Rejected;
                case DragAndDropPosition.OutsideItems: return DragAndDropVisualMode.None;
            }

            if (!(DragAndDrop.GetGenericData(FullName) is CanStartDragArgs dragArgs)
             || dragArgs.draggedItem == null
               ) return DragAndDropVisualMode.None;

            if (args.parentItem == null
             || dragArgs.draggedItem.id == args.parentItem.id
               ) return DragAndDropVisualMode.Rejected;

            if (args.performDrop)
            {
                DragAndDrop.AcceptDrag();
                OnDragSwapData(dragArgs.draggedItem.id, args.parentItem.id);
                ReloadAndSelect(args.parentItem.id);
                DragAndDrop.PrepareStartDrag();
                HandleUtility.Repaint();
            }

            return DragAndDropVisualMode.Move;
        }

        /// <summary>
        ///     是否能开始拖拽
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected sealed override bool CanStartDrag(CanStartDragArgs args)
        {
            if (!AllowDrag || !GUI.enabled) return false;
            if (args.draggedItemIDs.Count != 1 || args.draggedItem == null) return false;

            if (!IsSelected(args.draggedItem.id))
            {
                OnSelection(args.draggedItem.id);
                OnSingleSelectionChanged?.Invoke(args.draggedItem.id);
                return false;
            }

            DragAndDrop.activeControlID = ContentID;
            if (HasSelection() && isDragging)
            {
                DragAndDrop.SetGenericData(FullName, args);
                DragAndDrop.StartDrag(FullName);
                DragAndDrop.visualMode = DragAndDropVisualMode.Move;
                Event.current?.Use();
                return true;
            }

            DragAndDrop.PrepareStartDrag();
            Event.current?.Use();
            return true;
        }

        #endregion

        #region 点击事件

        /// <inheritdoc />
        protected sealed override void SelectionChanged(IList<int> selectedIds)
        {
            if (rootItem.children.Count == 0) return;
            if (selectedIds is null || selectedIds.Count == 0) return;
            var count = rootItem.children.Count;
            var temp  = selectedIds.ToList();
            for (var i = 0; i < temp.Count; i++)
                if (temp[i] >= count)
                    temp.Remove(i);
            if (temp.Count == 0) return;
            SetSelection(temp, TreeViewSelectionOptions.RevealAndFrame);
            if (OnSingleSelectionChanged is null) return;
            foreach (var index in temp) OnSingleSelectionChanged.Invoke(index);
        }

        /// <inheritdoc />
        protected sealed override void SingleClickedItem(int id)
        {
            if (state.lastClickedID == id) return;
            SelectionClick(rootItem.children[id], false);
            state.selectedIDs.Clear();
            state.selectedIDs.Add(id);
            state.lastClickedID = id;
            OnSingleSelectionChanged?.Invoke(id);
            Event.current?.Use();
        }

        #endregion

        #region 右键事件

        /// <summary>
        ///     右键点击 Item区域
        /// </summary>
        protected sealed override void ContextClickedItem(int id)
        {
            var menu = new GenericMenu();
            if (AllowMultiSelect && state.selectedIDs.Count > 1)
            {
                OnContextClicked(menu, state.selectedIDs.Select(i => rootItem.children[i]).ToList());
            }
            else
            {
                OnContextClicked(menu, rootItem.children[id]);
                ReloadAndSelect(id);
            }

            if (menu.GetItemCount() == 0) return;
            menu.ShowAsContext();
            Event.current?.Use();
        }

        #endregion

        #endregion
    }
}