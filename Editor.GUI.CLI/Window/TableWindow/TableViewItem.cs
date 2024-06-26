﻿#region

using UnityEditor.IMGUI.Controls;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 表格视图元素
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public sealed class TableViewItem<T> : TreeViewItem
    where T : class, new()
    {
        public TableViewItem(int id, int depth, T data) : base(id, depth, data == null ? "Root" : data.ToString())
        {
            Data = data;
        }

        /// <summary>
        /// 元素的数据
        /// </summary>
        public T Data { get; private set; }
    }
}