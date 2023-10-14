/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-10-09               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="contents">An array of text, image and tooltips for the button.</param>
        /// <param name="cb">回调</param>
        /// <param name="width">单体宽度</param>
        /// <returns><see cref="T"/>选中目标</returns>
        public static T Selection<T>(T selected, IDictionary<string, T> contents, Action<T> cb, float width = 100)
        {
            using (new GUILayout.HorizontalScope(GEStyle.PRInsertion, GUILayout.Width(contents.Count * width)))
            {
                foreach (var content in contents)
                {
                    if (GUILayout.Button(content.Key, selected.Equals(content.Value) ? GEStyle.PreLabel : GEStyle.INEditColliderButton, GUILayout.Width(width)))
                    {
                        selected = content.Value;
                        cb(selected);
                        return selected;
                    }
                }
            }

            return selected;
        }
        /// <summary>
        /// Make a Selection Grid 
        /// </summary>
        /// <param name="selected">The index of the selected button. <see cref="int"/></param>
        /// <param name="contents">An array of text, image and tooltips for the button.</param>
        /// <param name="cb">回调</param>
        /// <param name="width">单体宽度</param>
        /// <returns><see cref="T"/>选中目标</returns>
        public static T Selection<T>(T selected, IDictionary<GUIContent, T> contents, Action<T> cb, float width = 100)
        {
            using (new GUILayout.HorizontalScope(GEStyle.PRInsertion, GUILayout.Width(contents.Count * width)))
            {
                foreach (var content in contents)
                {
                    if (GUILayout.Button(content.Key, selected.Equals(content.Value) ? GEStyle.PreLabel : GEStyle.INEditColliderButton, GUILayout.Width(width)))
                    {
                        selected = content.Value;
                        cb(selected);
                        return selected;
                    }
                }
            }

            return selected;
        }
    }
}