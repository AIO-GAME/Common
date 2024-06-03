#region

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 分页绘制器
    /// </summary>
    public sealed class PagePainter
    {
        /// <summary>
        /// 当前分页
        /// </summary>
        private string _currentPage;

        /// <summary>
        /// 所有分页
        /// </summary>
        private Dictionary<string, Page> Pages = new Dictionary<string, Page>();

        /// <summary>
        /// 所有分页的顺序
        /// </summary>
        private List<string> PagesOrder = new List<string>();

        public PagePainter(Editor host)
        {
            CurrentHost = host ? host : throw new Exception("初始化分页绘制器失败：宿主不能为空！");
            CurrentPage = EditorPrefs.GetString(CurrentHost.GetType().FullName, null);
        }

        /// <summary>
        /// 当前的宿主
        /// </summary>
        public Editor CurrentHost { get; private set; }

        /// <summary>
        /// 当前分页
        /// </summary>
        public string CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                EditorPrefs.SetString(CurrentHost.GetType().FullName, _currentPage);
            }
        }

        /// <summary>
        /// 分页选中时背景风格
        /// </summary>
        public string CheckStyle { get; set; } = "SelectionRect";

        /// <summary>
        /// 分页未选中时背景风格
        /// </summary>
        public string UncheckStyle { get; set; } = "Box";

        /// <summary>
        /// 绘制
        /// </summary>
        public void Painting()
        {
            foreach (var PageName in PagesOrder)
            {
                var page = Pages[PageName];

                GUILayout.BeginVertical(PageName == CurrentPage ? CheckStyle : UncheckStyle);

                GUILayout.BeginHorizontal();
                GUILayout.Space(12);
                var oldValue = CurrentPage == PageName;
                page.Expanded.target = EditorGUILayout.Foldout(oldValue, page.Content, true);
                if (page.Expanded.target != oldValue) CurrentPage = page.Expanded.target ? PageName : null;

                GUILayout.EndHorizontal();

                if (EditorGUILayout.BeginFadeGroup(page.Expanded.faded)) page.OnPaint();

                EditorGUILayout.EndFadeGroup();

                GUILayout.EndVertical();
            }
        }

        /// <summary>
        /// 添加一个分页
        /// </summary>
        /// <param name="pageName">分页名称</param>
        /// <param name="pageIcon">分页图标</param>
        /// <param name="onPaint">绘制方法</param>
        public void AddPage(string pageName, Texture pageIcon, Action onPaint)
        {
            if (Pages.ContainsKey(pageName))
                return;

            Pages.Add(pageName, new Page(CurrentHost, pageName, pageIcon, onPaint, pageName == CurrentPage));
            PagesOrder.Add(pageName);
        }

        /// <summary>
        /// 移除一个分页
        /// </summary>
        /// <param name="pageName">分页名称</param>
        public void RemovePage(string pageName)
        {
            if (!Pages.ContainsKey(pageName))
                return;

            Pages.Remove(pageName);
            PagesOrder.Remove(pageName);
        }

        /// <summary>
        /// 清空所有分页
        /// </summary>
        public void ClearPage()
        {
            Pages.Clear();
            PagesOrder.Clear();
        }

        #region Nested type: Page

        /// <summary>
        /// 分页
        /// </summary>
        private class Page
        {
            public Page(Editor host, string name, Texture icon, Action onPaint, bool expanded)
            {
                Host = host;
                Name = name;
                Content = new GUIContent
                {
                    image = icon,
                    text  = name
                };
                Expanded = new AnimBool(expanded, Host.Repaint);
                OnPaint  = onPaint;
            }

            /// <summary>
            /// 宿主
            /// </summary>
            public Editor Host { get; private set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// GUI绘制内容
            /// </summary>
            public GUIContent Content { get; private set; }

            /// <summary>
            /// 是否展开分页
            /// </summary>
            public AnimBool Expanded { get; private set; }

            /// <summary>
            /// 绘制方法
            /// </summary>
            public Action OnPaint { get; private set; }
        }

        #endregion
    }
}