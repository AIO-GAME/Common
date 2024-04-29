using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace AIO.UEditor
{
    public struct RowGUIArgs
    {
        /// <summary>
        ///   <para>Item for the current row being handled in TreeView.RowGUI.</para>
        /// </summary>
        public TreeViewItem item;

        /// <summary>
        ///   <para>Label used for text rendering of the item displayName. Note this is an empty string when isRenaming == true.</para>
        /// </summary>
        public string label;

        /// <summary>
        ///   <para>Row rect for the current row being handled.</para>
        /// </summary>
        public Rect rowRect;

        /// <summary>
        ///   <para>Row index into the list of current rows.</para>
        /// </summary>
        public int row;

        /// <summary>
        ///   <para>This value is true when the current row's item is part of the current selection.</para>
        /// </summary>
        public bool selected;

        /// <summary>
        ///   <para>This value is true only when the TreeView has keyboard focus and the TreeView's window has focus.</para>
        /// </summary>
        public bool focused;

        /// <summary>
        ///   <para>This value is true when the ::item is currently being renamed.</para>
        /// </summary>
        public bool isRenaming;
    }

    public interface ITVItemDraw
    {
        /// <summary>
        ///     绘制
        /// </summary>
        /// <param name="cell"> 单元格矩形 </param>
        /// <param name="col"> 列 </param>
        /// <param name="args"> 行参数 </param>
        void OnDraw(Rect cell, int col, ref RowGUIArgs args);

        /// <summary>
        ///     是否允许改变展开状态
        /// </summary>
        bool AllowChangeExpandedState { get; }

        /// <summary>
        ///     是否允许重命名
        /// </summary>
        bool AllowRename { get; }

        /// <summary>
        ///     高度
        /// </summary>
        float GetHeight();

        /// <summary>
        ///     获取重命名矩形
        /// </summary>
        Rect GetRenameRect(Rect rowRect, int row);

        /// <summary>
        ///    匹配搜索
        /// </summary>
        /// <param name="search"> 搜索 </param>
        /// <returns> 是否匹配 </returns>
        bool MatchSearch(string search);
    }
}