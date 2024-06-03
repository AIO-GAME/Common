#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 脚本管理
    /// </summary>
    [GWindow("脚本管理", Group = "Tools", MinSizeWidth = 600, MinSizeHeight = 600)]
    public class CScriptManagerGraphWindow : GraphicWindow
    {
        private List<Type> List;
        private Vector2    Vector;

        public CScriptManagerGraphWindow()
        {
            List   = new List<Type>();
            Vector = new Vector2();
            foreach (var item in
                     from Assembly in AHelper.Assembly.GetReferenceAssemblies(AppDomain.CurrentDomain)
                     from item in Assembly.GetTypes()
                     where item.FullName != null && item.FullName.Contains("Framework")
                     select item)
                List.Add(item);
        }

        /// <summary>
        /// 当可脚本化对象超出作用域时调用此函数。
        /// </summary>
        protected override void OnDisable()
        {
            List.Clear();
            List = null;
        }

        protected override void OnDraw()
        {
            using (GELayout.VHorizontal(EditorStyles.helpBox, GTOptions.Width(true)))
            {
                GELayout.Space(5);
                GELayout.Label("类名");
                GELayout.Label("命名空间", GTOptions.Width(200));
                GELayout.Label("静态类", new GUIStyle("CenteredLabel"), GTOptions.Width(100));
                GELayout.Label("属性", GTOptions.Width(500));
                GELayout.Space(10);
            }

            using (var scope = GELayout.VScrollView(Vector))
            {
                Vector = scope.scrollPosition;
                foreach (var type in List)
                    using (GELayout.VHorizontal(EditorStyles.helpBox, GTOptions.Width(true), GTOptions.Height(60)))
                    {
                        GELayout.Space(10);
                        GELayout.Label(type.Name);
                        GELayout.Label(type.Namespace, GTOptions.Width(200));
                        GELayout.Label(type.IsAbstract, new GUIStyle("CenteredLabel"), GTOptions.Width(100));
                        GELayout.Label(type.Attributes.ToString(), GTOptions.Width(500));
                        GELayout.Space(10);
                    }
            }
        }
    }
}