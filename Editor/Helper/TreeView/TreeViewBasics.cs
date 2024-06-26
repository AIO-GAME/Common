﻿#region namespace

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    public abstract class TreeViewBasics : TreeView
    {
        public static readonly Color ColorLine         = new Color(0.5f, 0.5f, 0.5f, 0.3f);
        public static readonly Color ColorAlternatingA = new Color(63 / 255f, 63 / 255f, 63 / 255f, 1); //#3F3F3F
        public static readonly Color ColorAlternatingB = new Color(56 / 255f, 56 / 255f, 56 / 255f, 1); //#383838

        /// <summary>
        ///   子节点数量
        /// </summary>
        protected int Count => rootItem?.children?.Count ?? 0;

        /// <summary>
        ///    控件ID
        /// </summary>
        protected int ContentID;

        /// <summary>
        ///   主列索引
        /// </summary>
        protected int MainColumnIndex = 0;

        protected TreeViewBasics(TreeViewState state, MultiColumnHeader header) : base(state, header)
        {
            multiColumnHeader.SetSorting(0, false);
            multiColumnHeader.sortingChanged += SortingChanged;
        }

        #region 工具函数

        public void SetLabel(string     label)          => multiColumnHeader.GetColumn(0).headerContent = EditorGUIUtility.TrTextContent(label);
        public void SetLabel(string     label, int col) => multiColumnHeader.GetColumn(col).headerContent = EditorGUIUtility.TrTextContent(label);
        public void SetLabel(GUIContent label)          => multiColumnHeader.GetColumn(0).headerContent = label;
        public void SetLabel(GUIContent label, int col) => multiColumnHeader.GetColumn(col).headerContent = label;

        private void SortingChanged(MultiColumnHeader header)
        {
            if (OnSorting(header.sortedColumnIndex, header.IsSortedAscending(header.sortedColumnIndex)))
            {
                Reload();
            }
        }

        /// <summary>
        /// 重载并选中
        /// </summary>
        public void ReloadAndSelect()
        {
            Reload();
            SetFocus();
        }

        /// <summary>
        /// 选中
        /// </summary>
        public void Select(int index)
        {
            if (Count == 0 || Count <= index || index < 0)
            {
                SetFocus();
                return;
            }

            SelectionChanged(new[] { index });
            SetFocus();
        }

        /// <summary>
        /// 选中
        /// </summary>
        public void Select(IList<int> hashCodes)
        {
            if (hashCodes.Count > 0) SelectionChanged(hashCodes);
            SetFocus();
        }

        /// <summary>
        /// 重载并选中
        /// </summary>
        public void ReloadAndSelect(int hc)
        {
            if (Count == 0 || Count <= hc || hc < 0)
            {
                SetFocus();
                return;
            }

            Reload();
            SelectionChanged(new[] { hc });
            SetFocus();
        }

        /// <summary>
        /// 重载并选中
        /// </summary>
        public void ReloadAndSelect(IList<int> hashCodes)
        {
            Reload();
            if (hashCodes.Count > 0) SelectionChanged(hashCodes);
            SetFocus();
        }

        protected static UEditor.RowGUIArgs Cast(RowGUIArgs args)
        {
            var RowGUIArgs = new UEditor.RowGUIArgs
            {
                item       = args.item, label = args.label, rowRect = args.rowRect, row = args.row, selected = args.selected, focused = args.focused,
                isRenaming = args.isRenaming
            };
            return RowGUIArgs;
        }

        #endregion

        #region sealed override

        /// <summary>
        ///     绘制行
        /// </summary>
        protected sealed override void RowGUI(RowGUIArgs args)
        {
            switch (args.item)
            {
                case ITVItemDraw item:
                {
                    var count = args.GetNumVisibleColumns() - 1;
                    for (var i = 0; i <= count; i++)
                    {
                        EditorGUI.BeginChangeCheck();
                        var cellRect = args.GetCellRect(i);
                        CenterRectUsingSingleLineHeight(ref cellRect);
                        var cast = Cast(args);
                        try
                        {
                            item.OnDraw(cellRect, i, ref cast);
                        }
                        catch (Exception)
                        {
                            GUIUtility.ExitGUI();
                            return;
                        }

                        if (i == count)
                        {
                            cellRect.Set(cellRect.width + cellRect.x + count - 1, args.rowRect.y, 1, args.rowRect.height - 1);
                            EditorGUI.DrawRect(cellRect, ColorLine);
                        }

                        if (EditorGUI.EndChangeCheck()) Reload();
                    }

                    break;
                }
            }
        }

        public sealed override void OnGUI(Rect rect)
        {
            base.OnGUI(rect);
            ContentID = GUIUtility.GetControlID(FocusType.Passive, rect);
            multiColumnHeader.state.AutoWidth(rect.width, MainColumnIndex);

            OnDraw(rect);
            if (Event.current.type == EventType.MouseDown
             && Event.current.button == 0
             && rect.Contains(Event.current.mousePosition)
               ) SetSelection(state.selectedIDs, TreeViewSelectionOptions.FireSelectionChanged);

            rect.height = 26;
            EditorGUI.DrawRect(rect, ColorLine);
        }

        /// <summary>
        ///     能否重新命名
        /// </summary>
        /// <param name="item">选中组件</param>
        /// <returns>Ture:能 False:不能</returns>
        protected sealed override bool CanRename(TreeViewItem item)
        {
            if (item is ITVItemDraw draw) return draw.AllowRename && state.lastClickedID == item.id;
            return base.CanRename(item);
        }

        /// <summary>
        ///     获取重命名矩形
        /// </summary>
        /// <param name="rowRect">行的矩形</param>
        /// <param name="row">行数</param>
        /// <param name="item">选中组件</param>
        /// <returns></returns>
        protected sealed override Rect GetRenameRect(Rect rowRect, int row, TreeViewItem item)
        {
            if (item is ITVItemDraw draw) return draw.GetRenameRect(rowRect, row);
            return base.GetRenameRect(rowRect, row, item);
        }

        /// <summary>
        ///     构建根节点
        /// </summary>
        /// <returns>根节点</returns>
        protected sealed override TreeViewItem BuildRoot() => new TreeViewItem
        {
            id = 0, depth = -1, displayName = "root", children = new List<TreeViewItem>()
        };

        /// <summary>
        ///     获取行高
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="item">选中组件</param>
        /// <returns>行高</returns>
        protected sealed override float GetCustomRowHeight(int row, TreeViewItem item)
        {
            if (item is ITVItemDraw draw) return draw.GetHeight();
            return base.GetCustomRowHeight(row, item);
        }

        /// <inheritdoc />
        protected sealed override IList<TreeViewItem> BuildRows(TreeViewItem root)
        {
            if (root.children is null) root.children = new List<TreeViewItem>();
            else root.children.Clear();
            OnBuildRows(root);
            SetupDepthsFromParentsAndChildren(root);
            return base.BuildRows(root);
        }

        /// <summary>
        ///     右键点击 空白区域
        /// </summary>
        protected sealed override void ContextClicked()
        {
            ReloadAndSelect();
            var menu = new GenericMenu();
            OnContextClicked(menu);
            if (menu.GetItemCount() == 0) return;
            menu.ShowAsContext();
            Event.current?.Use();
        }

        /// <summary>
        ///     组件是否匹配搜索
        /// </summary>
        /// <param name="item">组件</param>
        /// <param name="search">搜索内容</param>
        /// <returns>Ture:匹配 False:不匹配</returns>
        protected sealed override bool DoesItemMatchSearch(TreeViewItem item, string search)
        {
            if (string.IsNullOrEmpty(search) || item is null) return true;
            if (item is ITVItemDraw draw) return draw.MatchSearch(search);
            return base.DoesItemMatchSearch(item, search);
        }

        /// <summary>
        ///    搜索内容改变
        /// </summary>
        /// <param name="newSearch">新搜索内容</param>
        protected override void SearchChanged(string newSearch) { }

        #endregion

        #region abstract

        /// <summary>
        ///     构建行
        /// </summary>
        /// <param name="root">根节点</param>
        protected abstract void OnBuildRows(TreeViewItem root);

        #endregion

        #region virtual

        /// <summary>
        ///     右键点击空白区域
        /// </summary>
        /// <param name="menu">菜单</param>
        protected virtual void OnContextClicked(GenericMenu menu) { }

        /// <summary>
        ///     绘制
        /// </summary>
        protected virtual void OnDraw(Rect rect) { }

        /// <summary>
        ///     排序
        /// </summary>
        /// <param name="col"> 列 </param>
        /// <param name="ascending"> 是否升序 </param>
        /// <returns> 是否重新加载 </returns>
        protected virtual bool OnSorting(int col, bool ascending) => false;

        #endregion

        #region MultiColumn Heade rColumn

        protected static MultiColumnHeaderState.Column GetMultiColumnHeaderColumn(
            string name,
            float  width,
            float  min,
            float  max,
            bool   sort
        ) => new MultiColumnHeaderState.Column
        {
            headerContent         = new GUIContent(name),
            width                 = width,
            minWidth              = min,
            maxWidth              = max,
            sortedAscending       = true,
            headerTextAlignment   = TextAlignment.Center,
            sortingArrowAlignment = TextAlignment.Center,
            canSort               = sort,
            autoResize            = true,
            allowToggleVisibility = false
        };

        protected static MultiColumnHeaderState.Column GetMultiColumnHeaderColumn(
            string name
        ) => GetMultiColumnHeaderColumn(name, 100, 80, 200, true);

        protected static MultiColumnHeaderState.Column GetMultiColumnHeaderColumn(
            string name,
            float  width,
            float  min,
            float  max
        ) => GetMultiColumnHeaderColumn(name, width, min, max, true);

        protected static MultiColumnHeaderState.Column GetMultiColumnHeaderColumn(
            string name,
            float  width,
            bool   sort
        ) => GetMultiColumnHeaderColumn(name, width, width, width, sort);

        protected static MultiColumnHeaderState.Column GetMultiColumnHeaderColumn(
            string name,
            float  width,
            float  minWidth,
            bool   sort
        ) => GetMultiColumnHeaderColumn(name, width, minWidth, width, sort);

        protected static MultiColumnHeaderState.Column GetMultiColumnHeaderColumn(
            string name,
            float  width,
            float  minWidth
        ) => GetMultiColumnHeaderColumn(name, width, minWidth, width, true);

        protected static MultiColumnHeaderState.Column GetMultiColumnHeaderColumn(
            string name,
            float  width
        ) => GetMultiColumnHeaderColumn(name, width, width, width, true);

        #endregion
    }
}