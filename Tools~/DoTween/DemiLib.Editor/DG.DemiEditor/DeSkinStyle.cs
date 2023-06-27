using DG.DemiLib.Core;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Contains both free and pro skins GUIStyle variations,
	/// and automatically returns the correct one when converted to GUIStyle
	/// </summary>
	public struct DeSkinStyle
	{
		public GUIStyle free;

		public GUIStyle pro;

		public DeSkinStyle(GUIStyle free, GUIStyle pro)
		{
			this.free = free;
			this.pro = pro;
		}

		public DeSkinStyle(GUIStyle style)
		{
			this = default(DeSkinStyle);
			free = style;
			pro = new GUIStyle(style);
		}

		public static implicit operator GUIStyle(DeSkinStyle v)
		{
			if (!GUIUtils.isProSkin)
			{
				return v.free;
			}
			return v.pro;
		}

		public static implicit operator DeSkinStyle(GUIStyle v)
		{
			return new DeSkinStyle(v);
		}
	}
}
