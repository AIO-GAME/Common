﻿#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

#endregion

namespace AIO
{
    public struct GTContent
    {
        public bool Equals(GTContent other)
        {
            return Equals(Style, other.Style) && Equals(Content, other.Content) && Equals(Options, other.Options);
        }

        public override bool Equals(object obj)
        {
            return obj is GTContent other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Style, Content, Options);
        }

        public static readonly GTContent Empty = new GTContent
        {
            Content = GUIContent.none,
            Options = null,
            Style   = null
        };

        public static bool IsNullOrEmpty(GTContent content)
        {
            return content == Empty;
        }

        public static bool operator ==(GTContent a, GTContent b)
        {
            if (a.Content != b.Content) return false;
            if (a.Options != b.Options) return false;
            if (a.Style != b.Style) return false;
            return true;
        }

        public static bool operator !=(GTContent a, GTContent b)
        {
            return !(a == b);
        }

        #region Temp

        public static GUIContent Temp(string content)
        {
            return new GUIContent(content);
        }

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
            foreach (var item in collection)
                guiContentArray[index++] = new GUIContent(item.ToString(CultureInfo.InvariantCulture));
            return guiContentArray;
        }

        public static GUIContent[] Temp(ICollection<float> collection)
        {
            var guiContentArray = new GUIContent[collection.Count];
            var index = 0;
            foreach (var item in collection)
                guiContentArray[index++] = new GUIContent(item.ToString(CultureInfo.InvariantCulture));
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
        public GUIContent Content { get; set; }

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
        public GUIStyle Style { get; set; }

        #region content options

        public GTContent(string content, float width)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { GUILayout.Width(width) };
            Style   = null;
        }

        public GTContent(Texture content, float width)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { GUILayout.Width(width) };
            Style   = null;
        }

        public GTContent(GUIContent content, float width)
        {
            Content = content;
            Options = new List<GUILayoutOption> { GUILayout.Width(width) };
            Style   = null;
        }

        #endregion

        #region content options

        public GTContent(string content, float width, float height)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { GUILayout.Width(width), GUILayout.Height(height) };
            Style   = null;
        }

        public GTContent(Texture content, float width, float height)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption> { GUILayout.Width(width), GUILayout.Height(height) };
            Style   = null;
        }

        public GTContent(GUIContent content, float width, float height)
        {
            Content = content;
            Options = new List<GUILayoutOption> { GUILayout.Width(width), GUILayout.Height(height) };
            Style   = null;
        }

        #endregion

        #region content options

        public GTContent(string content, params GUILayoutOption[] options)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption>(options);
            Style   = null;
        }

        public GTContent(GUIContent content, params GUILayoutOption[] options)
        {
            Content = content;
            Options = new List<GUILayoutOption>(options);
            Style   = null;
        }

        public GTContent(Texture content, params GUILayoutOption[] options)
        {
            Content = new GUIContent(content);
            Options = new List<GUILayoutOption>(options);
            Style   = null;
        }

        #endregion

        #region content

        public GTContent(GUIContent content)
        {
            Content = content;
            Options = null;
            Style   = null;
        }

        public GTContent(string content)
        {
            Content = new GUIContent(content);
            Options = null;
            Style   = null;
        }

        public GTContent(Texture content)
        {
            Content = new GUIContent(content);
            Options = null;
            Style   = null;
        }

        #endregion

        private GTContent(IEnumerable<GUILayoutOption> options)
        {
            Content = GUIContent.none;
            Options = new List<GUILayoutOption>(options);
            Style   = null;
        }

        private GTContent(GUILayoutOption options)
        {
            Content = GUIContent.none;
            Options = new List<GUILayoutOption> { options };
            Style   = null;
        }

        public override string ToString()
        {
            return Content.ToString();
        }

        public static implicit operator GTContent(Enum content)
        {
            return new GTContent(content.ToString());
        }

        public static implicit operator GTContent(string content)
        {
            return new GTContent(content);
        }

        public static implicit operator GTContent(int content)
        {
            return new GTContent(content.ToString(CultureInfo.CurrentCulture));
        }

        public static implicit operator GTContent(float content)
        {
            return new GTContent(content.ToString(CultureInfo.CurrentCulture));
        }

        public static implicit operator GTContent(long content)
        {
            return new GTContent(content.ToString(CultureInfo.CurrentCulture));
        }

        public static implicit operator GTContent(double content)
        {
            return new GTContent(content.ToString(CultureInfo.CurrentCulture));
        }

        public static implicit operator GTContent(GUIContent content)
        {
            return new GTContent(content);
        }

        public static implicit operator GTContent(Texture content)
        {
            return new GTContent(content);
        }

        public static implicit operator GTContent(GUILayoutOption content)
        {
            return new GTContent(content);
        }

        public static implicit operator GTContent(GUILayoutOption[] content)
        {
            return new GTContent(content);
        }

        public static implicit operator GUILayoutOption[](GTContent content)
        {
            return content.Options.ToArray();
        }

        public static implicit operator List<GUILayoutOption>(GTContent content)
        {
            return content.Options;
        }

        public static implicit operator GUIContent(GTContent content)
        {
            return content.Content;
        }

        public static implicit operator GUIStyle(GTContent content)
        {
            return content.Style;
        }
    }
}