/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace AIO
{
    public partial struct GTContent
    {
        #region Temp

        public static GUIContent[] Temp(IEnumerable<GUIContent> collection)
        {
            return collection.ToArray();
        }

        public static GUIContent[] Temp(ICollection<Texture> collection)
        {
            var guiContentArray = new GUIContent[collection.Count];
            var index = 0;
            foreach (var item in collection) guiContentArray[index++] = new GUIContent(item);
            return guiContentArray;
        }

        public static GUIContent[] Temp(ICollection<int> collection)
        {
            var guiContentArray = new GUIContent[collection.Count];
            var index = 0;
            foreach (var item in collection) guiContentArray[index++] = new GUIContent(item.ToString());
            return guiContentArray;
        }

        public static GUIContent[] Temp(ICollection<long> collection)
        {
            var guiContentArray = new GUIContent[collection.Count];
            var index = 0;
            foreach (var item in collection) guiContentArray[index++] = new GUIContent(item.ToString());
            return guiContentArray;
        }

        public static GUIContent[] Temp(ICollection<double> collection)
        {
            var guiContentArray = new GUIContent[collection.Count];
            var index = 0;
            foreach (var item in collection) guiContentArray[index++] = new GUIContent(item.ToString(CultureInfo.InvariantCulture));
            return guiContentArray;
        }

        public static GUIContent[] Temp(ICollection<float> collection)
        {
            var guiContentArray = new GUIContent[collection.Count];
            var index = 0;
            foreach (var item in collection) guiContentArray[index++] = new GUIContent(item.ToString(CultureInfo.InvariantCulture));
            return guiContentArray;
        }

        public static GUIContent[] Temp(ICollection<string> collection)
        {
            var guiContentArray = new GUIContent[collection.Count];
            var index = 0;
            foreach (var item in collection) guiContentArray[index++] = new GUIContent(item);
            return guiContentArray;
        }

        #endregion

        /// <summary>
        /// 内容
        /// </summary>
        public GUIContent Content { get; private set; }

        /// <summary>
        /// 选项
        /// </summary>
        private List<GUILayoutOption> Options { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        public GUILayoutOption[] Option
        {
            get
            {
                if (Options == null) Options = new List<GUILayoutOption>();
                return Options.ToArray();
            }
        }

        /// <summary>
        /// 选项
        /// </summary>
        public GUIStyle Style;

        #region content options

        public GTContent(string content, float width)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { GUILayout.Width(width) };
            Style = null;
        }

        public GTContent(Texture content, float width)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { GUILayout.Width(width) };
            Style = null;
        }

        public GTContent(GUIContent content, float width)
        {
            Content = content;
            Options = new List<GUILayoutOption> { GUILayout.Width(width) };
            Style = null;
        }

        #endregion

        #region content options

        public GTContent(string content, float width, float height)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { GUILayout.Width(width), GUILayout.Height(height) };
            Style = null;
        }

        public GTContent(Texture content, float width, float height)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { GUILayout.Width(width), GUILayout.Height(height) };
            Style = null;
        }

        public GTContent(GUIContent content, float width, float height)
        {
            Content = content;
            Options = new List<GUILayoutOption> { GUILayout.Width(width), GUILayout.Height(height) };
            Style = null;
        }

        #endregion

        #region content options

        public GTContent(string content, params GUILayoutOption[] options)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { options };
            Style = null;
        }

        public GTContent(GUIContent content, params GUILayoutOption[] options)
        {
            Content = content;
            Options = new List<GUILayoutOption> { options };
            Style = null;
        }

        public GTContent(Texture content, params GUILayoutOption[] options)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { options };
            Style = null;
        }

        #endregion

        #region content

        public GTContent(GUIContent content)
        {
            Content = content;
            Options = null;
            Style = null;
        }

        public GTContent(string content)
        {
            Content = new GUIContent(content);
            Options = null;
            Style = null;
        }

        public GTContent(Texture content)
        {
            Content = new GUIContent(content);
            Options = null;
            Style = null;
        }

        #endregion

        private GTContent(IEnumerable<GUILayoutOption> options)
        {
            Content = GUIContent.none;
            Options = new List<GUILayoutOption> { options };
            Style = null;
        }

        private GTContent(GUILayoutOption options)
        {
            Content = GUIContent.none;
            Options = new List<GUILayoutOption> { options };
            Style = null;
        }

        public override string ToString()
        {
            return Content.ToString();
        }

        public static implicit operator GTContent(Enum content) =>
            new GTContent(content.ToString());

        public static implicit operator GTContent(string content) =>
            new GTContent(content);

        public static implicit operator GTContent(int content) =>
            new GTContent(content.ToString(CultureInfo.CurrentCulture));

        public static implicit operator GTContent(float content) =>
            new GTContent(content.ToString(CultureInfo.CurrentCulture));

        public static implicit operator GTContent(long content) =>
            new GTContent(content.ToString(CultureInfo.CurrentCulture));

        public static implicit operator GTContent(double content) =>
            new GTContent(content.ToString(CultureInfo.CurrentCulture));

        public static implicit operator GTContent(GUIContent content) =>
            new GTContent(content);

        public static implicit operator GTContent(Texture content) =>
            new GTContent(content);

        public static implicit operator GTContent(GUILayoutOption content) =>
            new GTContent(content);

        public static implicit operator GTContent(GUILayoutOption[] content) =>
            new GTContent(content);

        public static implicit operator GUILayoutOption[](GTContent content) =>
            content.Options.ToArray();

        public static implicit operator List<GUILayoutOption>(GTContent content) =>
            content.Options;

        public static implicit operator GUIContent(GTContent content) =>
            content.Content;

        public static implicit operator GUIStyle(GTContent content) =>
            content.Style;
    }
}