using System;
using DG.DemiLib;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// GUI extension methods
	/// </summary>
	public static class GUIStyleExtensions
	{
		/// <summary>
		/// Clones the style and adds the given formats to it. You can pass any of these types of values:
		/// <list type="bullet">
		/// <item><term>Format:</term><description>Rich-text, wordwrap</description></item>
		/// <item><term>FontStyle:</term><description>Font style</description></item>
		/// <item><term>TextAnchor:</term><description>Content anchor</description></item>
		/// <item><term>int:</term><description>Font size</description></item>
		/// <item><term>Color/DeSkinColor:</term><description>Font color</description></item>
		/// </list>
		/// </summary>
		public static GUIStyle Clone(this GUIStyle style, params object[] formats)
		{
			return new GUIStyle(style).Add(formats);
		}

		/// <summary>
		/// Adds the given formats to the style. You can pass any of these types of values:
		/// <list type="bullet">
		/// <item><term>Format:</term><description>RichText, WordWrap</description></item>
		/// <item><term>FontStyle:</term><description>Font style</description></item>
		/// <item><term>TextAnchor:</term><description>Content anchor</description></item>
		/// <item><term>int:</term><description>Font size</description></item>
		/// <item><term>Color/DeSkinColor:</term><description>Font color</description></item>
		/// </list>
		/// </summary>
		public static GUIStyle Add(this GUIStyle style, params object[] formats)
		{
			foreach (object obj in formats)
			{
				Type type = obj.GetType();
				if (type == typeof(Format))
				{
					switch ((Format)obj)
					{
					case Format.RichText:
						style.richText = true;
						break;
					case Format.NoRichText:
						style.richText = false;
						break;
					case Format.WordWrap:
						style.wordWrap = true;
						break;
					case Format.NoWordWrap:
						style.wordWrap = false;
						break;
					}
				}
				else if (type == typeof(FontStyle))
				{
					style.fontStyle = (FontStyle)obj;
				}
				else if (type == typeof(TextAnchor))
				{
					style.alignment = (TextAnchor)obj;
				}
				else if (type == typeof(int))
				{
					style.fontSize = (int)obj;
				}
				else if (type == typeof(Color) || type == typeof(DeSkinColor))
				{
					GUIStyleState normal = style.normal;
					GUIStyleState onNormal = style.onNormal;
					GUIStyleState active = style.active;
					GUIStyleState onActive = style.onActive;
					GUIStyleState focused = style.focused;
					GUIStyleState onFocused = style.onFocused;
					GUIStyleState hover = style.hover;
					Color color2 = (style.onHover.textColor = ((type == typeof(Color)) ? ((Color)obj) : ((Color)(DeSkinColor)obj)));
					Color color4 = (hover.textColor = color2);
					Color color6 = (onFocused.textColor = color4);
					Color color8 = (focused.textColor = color6);
					Color color10 = (onActive.textColor = color8);
					Color color12 = (active.textColor = color10);
					Color color15 = (normal.textColor = (onNormal.textColor = color12));
				}
			}
			return style;
		}

		/// <summary>
		/// Sets the border of the style
		/// </summary>
		public static GUIStyle Border(this GUIStyle style, RectOffset border)
		{
			style.border = border;
			return style;
		}

		/// <summary>
		/// Sets the border of the style
		/// </summary>
		public static GUIStyle Border(this GUIStyle style, int left, int right, int top, int bottom)
		{
			style.border = new RectOffset(left, right, top, bottom);
			return style;
		}

		/// <summary>
		/// Sets the background of the style
		/// </summary>
		public static GUIStyle Background(this GUIStyle style, Texture2D background, Texture2D pressBackground = null)
		{
			return style.Background(background, pressBackground, null);
		}

		/// <summary>
		/// Sets the background of the style
		/// </summary>
		public static GUIStyle Background(this GUIStyle style, Texture2D background, Texture2D pressBackground, Texture2D overBackground)
		{
			if (background == null)
			{
				background = DeStylePalette.transparent;
			}
			if (pressBackground == null)
			{
				pressBackground = background;
			}
			if (overBackground == null)
			{
				overBackground = background;
			}
			Texture2D texture2D3 = (style.normal.background = (style.onNormal.background = background));
			Texture2D[] array3 = (style.normal.scaledBackgrounds = (style.onNormal.scaledBackgrounds = new Texture2D[0]));
			GUIStyleState focused = style.focused;
			GUIStyleState onFocused = style.onFocused;
			GUIStyleState hover = style.hover;
			Texture2D texture2D5 = (style.onHover.background = overBackground);
			Texture2D texture2D7 = (hover.background = texture2D5);
			texture2D3 = (focused.background = (onFocused.background = texture2D7));
			GUIStyleState focused2 = style.focused;
			GUIStyleState onFocused2 = style.onFocused;
			GUIStyleState hover2 = style.hover;
			Texture2D[] array5 = (style.onHover.scaledBackgrounds = new Texture2D[0]);
			Texture2D[] array7 = (hover2.scaledBackgrounds = array5);
			array3 = (focused2.scaledBackgrounds = (onFocused2.scaledBackgrounds = array7));
			texture2D3 = (style.active.background = (style.onActive.background = pressBackground));
			array3 = (style.active.scaledBackgrounds = (style.onActive.scaledBackgrounds = new Texture2D[0]));
			return style;
		}

		/// <summary>
		/// Sets the contentOffset of the style
		/// </summary>
		public static GUIStyle ContentOffset(this GUIStyle style, Vector2 offset)
		{
			style.contentOffset = offset;
			return style;
		}

		/// <summary>
		/// Sets the contentOffset of the style
		/// </summary>
		public static GUIStyle ContentOffset(this GUIStyle style, float offsetX, float offsetY)
		{
			style.contentOffset = new Vector2(offsetX, offsetY);
			return style;
		}

		/// <summary>
		/// Sets the X contentOffset of the style
		/// </summary>
		public static GUIStyle ContentOffsetX(this GUIStyle style, float offsetX)
		{
			Vector2 contentOffset = style.contentOffset;
			contentOffset.x = offsetX;
			style.contentOffset = contentOffset;
			return style;
		}

		/// <summary>
		/// Sets the Y contentOffset of the style
		/// </summary>
		public static GUIStyle ContentOffsetY(this GUIStyle style, float offsetY)
		{
			Vector2 contentOffset = style.contentOffset;
			contentOffset.y = offsetY;
			style.contentOffset = contentOffset;
			return style;
		}

		/// <summary>
		/// Sets the margin of the style
		/// </summary>
		public static GUIStyle Margin(this GUIStyle style, RectOffset margin)
		{
			style.margin = margin;
			return style;
		}

		/// <summary>
		/// Sets the margin of the style
		/// </summary>
		public static GUIStyle Margin(this GUIStyle style, int left, int right, int top, int bottom)
		{
			style.margin = new RectOffset(left, right, top, bottom);
			return style;
		}

		/// <summary>
		/// Sets the margin of the style
		/// </summary>
		public static GUIStyle Margin(this GUIStyle style, int margin)
		{
			style.margin = new RectOffset(margin, margin, margin, margin);
			return style;
		}

		/// <summary>
		/// Sets the left margin of the style
		/// </summary>
		public static GUIStyle MarginLeft(this GUIStyle style, int left)
		{
			style.margin.left = left;
			return style;
		}

		/// <summary>
		/// Sets the right margin of the style
		/// </summary>
		public static GUIStyle MarginRight(this GUIStyle style, int right)
		{
			style.margin.right = right;
			return style;
		}

		/// <summary>
		/// Sets the top margin of the style
		/// </summary>
		public static GUIStyle MarginTop(this GUIStyle style, int top)
		{
			style.margin.top = top;
			return style;
		}

		/// <summary>
		/// Sets the bottom margin of the style
		/// </summary>
		public static GUIStyle MarginBottom(this GUIStyle style, int bottom)
		{
			style.margin.bottom = bottom;
			return style;
		}

		/// <summary>
		/// Sets the overflow of the style
		/// </summary>
		public static GUIStyle Overflow(this GUIStyle style, RectOffset overflow)
		{
			style.overflow = overflow;
			return style;
		}

		/// <summary>
		/// Sets the overflow of the style
		/// </summary>
		public static GUIStyle Overflow(this GUIStyle style, int left, int right, int top, int bottom)
		{
			style.overflow = new RectOffset(left, right, top, bottom);
			return style;
		}

		/// <summary>
		/// Sets the overflow of the style
		/// </summary>
		public static GUIStyle Overflow(this GUIStyle style, int overflow)
		{
			style.overflow = new RectOffset(overflow, overflow, overflow, overflow);
			return style;
		}

		/// <summary>
		/// Sets the left overflow of the style
		/// </summary>
		public static GUIStyle OverflowLeft(this GUIStyle style, int left)
		{
			style.overflow.left = left;
			return style;
		}

		/// <summary>
		/// Sets the right overflow of the style
		/// </summary>
		public static GUIStyle OverflowRight(this GUIStyle style, int right)
		{
			style.overflow.right = right;
			return style;
		}

		/// <summary>
		/// Sets the top overflow of the style
		/// </summary>
		public static GUIStyle OverflowTop(this GUIStyle style, int top)
		{
			style.overflow.top = top;
			return style;
		}

		/// <summary>
		/// Sets the bottom overflow of the style
		/// </summary>
		public static GUIStyle OverflowBottom(this GUIStyle style, int bottom)
		{
			style.overflow.bottom = bottom;
			return style;
		}

		/// <summary>
		/// Sets the padding of the style
		/// </summary>
		public static GUIStyle Padding(this GUIStyle style, RectOffset padding)
		{
			style.padding = padding;
			return style;
		}

		/// <summary>
		/// Sets the padding of the style
		/// </summary>
		public static GUIStyle Padding(this GUIStyle style, int left, int right, int top, int bottom)
		{
			style.padding = new RectOffset(left, right, top, bottom);
			return style;
		}

		/// <summary>
		/// Sets the padding of the style
		/// </summary>
		public static GUIStyle Padding(this GUIStyle style, int padding)
		{
			style.padding = new RectOffset(padding, padding, padding, padding);
			return style;
		}

		/// <summary>
		/// Sets the left padding of the style
		/// </summary>
		public static GUIStyle PaddingLeft(this GUIStyle style, int left)
		{
			style.padding.left = left;
			return style;
		}

		/// <summary>
		/// Sets the right padding of the style
		/// </summary>
		public static GUIStyle PaddingRight(this GUIStyle style, int right)
		{
			style.padding.right = right;
			return style;
		}

		/// <summary>
		/// Sets the top padding of the style
		/// </summary>
		public static GUIStyle PaddingTop(this GUIStyle style, int top)
		{
			style.padding.top = top;
			return style;
		}

		/// <summary>
		/// Sets the bottom padding of the style
		/// </summary>
		public static GUIStyle PaddingBottom(this GUIStyle style, int bottom)
		{
			style.padding.bottom = bottom;
			return style;
		}

		/// <summary>
		/// Sets the Y fixedWidth of the style
		/// </summary>
		public static GUIStyle Width(this GUIStyle style, float width)
		{
			style.fixedWidth = width;
			return style;
		}

		/// <summary>
		/// Sets the fixedHeight of the style
		/// </summary>
		public static GUIStyle Height(this GUIStyle style, int height)
		{
			style.fixedHeight = height;
			return style;
		}

		/// <summary>
		/// Sets the stretchHeight property of the style
		/// </summary>
		public static GUIStyle StretchHeight(this GUIStyle style, bool doStretch = true)
		{
			style.stretchHeight = doStretch;
			return style;
		}

		/// <summary>
		/// Sets the stretchWidth property of the style
		/// </summary>
		public static GUIStyle StretchWidth(this GUIStyle style, bool doStretch = true)
		{
			style.stretchWidth = doStretch;
			return style;
		}
	}
}
