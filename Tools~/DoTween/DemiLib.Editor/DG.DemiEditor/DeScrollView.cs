using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Returns by <see cref="M:DG.DemiEditor.DeGUI.BeginScrollView(UnityEngine.Rect,DG.DemiEditor.DeScrollView,System.Boolean)" />.
	/// Contains properties and methods to manage non-layout scrollview better.<para />
	/// Remember to use <see cref="M:DG.DemiEditor.DeScrollView.IncreaseContentHeightBy(System.Single)" /> or <see cref="M:DG.DemiEditor.DeScrollView.SetContentHeight(System.Single)" /> to increase or set the full content height
	/// </summary>
	public struct DeScrollView
	{
		private static readonly Stack<DeScrollView> _CurrScrollViews = new Stack<DeScrollView>();

		/// <summary>Area used by ScrollView and its content</summary>
		public Rect area { get; private set; }

		/// <summary>Full content area regardless if visible or not. Its height should be set manually based on the contents' height</summary>
		public Rect fullContentArea { get; private set; }

		/// <summary>Content area currently visible (scroll bars excluded)</summary>
		public Rect visibleContentArea { get; private set; }

		/// <summary>Current scrollPosition</summary>
		public Vector2 scrollPosition { get; private set; }

		/// <summary>
		/// Returns the current <see cref="T:DG.DemiEditor.DeScrollView" /> open, or an empty one if none is open.
		/// </summary>
		public static DeScrollView Current()
		{
			if (_CurrScrollViews.Count != 0)
			{
				return _CurrScrollViews.Peek();
			}
			return default(DeScrollView);
		}

		/// <summary>
		/// Sets the <see cref="P:DG.DemiEditor.DeScrollView.fullContentArea" /> height
		/// </summary>
		/// <param name="height"></param>
		public void SetContentHeight(float height)
		{
			fullContentArea = new Rect(fullContentArea.x, fullContentArea.y, fullContentArea.width, height);
		}

		/// <summary>
		/// Increase the <see cref="P:DG.DemiEditor.DeScrollView.fullContentArea" /> height by the given amount
		/// </summary>
		/// <param name="value"></param>
		public void IncreaseContentHeightBy(float value)
		{
			fullContentArea = new Rect(fullContentArea.x, fullContentArea.y, fullContentArea.width, fullContentArea.height + value);
		}

		/// <summary>
		/// Returns a Rect for a single line at the current scrollView yMax
		/// </summary>
		/// <param name="height">If less than 0 uses default line height, otherwise the value passed</param>
		/// <param name="increaseScrollViewHeight">if TRUE (default) automatically increases the height of the <see cref="P:DG.DemiEditor.DeScrollView.fullContentArea" /> accordingly</param>
		/// <returns></returns>
		public Rect GetSingleLineRect(float height = -1f, bool increaseScrollViewHeight = true)
		{
			Rect result = new Rect(fullContentArea.x, fullContentArea.yMax, fullContentArea.width, (height < 0f) ? EditorGUIUtility.singleLineHeight : height);
			if (increaseScrollViewHeight)
			{
				IncreaseContentHeightBy(result.height);
			}
			return result;
		}

		/// <summary>
		/// Returns TRUE if the given rect is at least partially visible in the displayed scroll area
		/// </summary>
		public bool IsVisible(Rect r)
		{
			return visibleContentArea.Overlaps(r);
		}

		internal void Begin(Rect viewArea, bool resetContentHeightToZero)
		{
			area = viewArea;
			fullContentArea = new Rect(fullContentArea.x, fullContentArea.y, (fullContentArea.height > area.height) ? (area.width - 16f) : area.width, fullContentArea.height);
			scrollPosition = GUI.BeginScrollView(area, scrollPosition, fullContentArea);
			if (resetContentHeightToZero)
			{
				fullContentArea = new Rect(fullContentArea.x, fullContentArea.y, fullContentArea.width, 0f);
			}
			visibleContentArea = new Rect(area.x, scrollPosition.y, fullContentArea.width, area.height);
			_CurrScrollViews.Push(this);
		}

		internal void End()
		{
			_CurrScrollViews.Pop();
			GUI.EndScrollView();
		}
	}
}
