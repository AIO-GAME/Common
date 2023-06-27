using System;
using DG.DemiLib.Core;
using UnityEngine;

namespace DG.DemiLib
{
	/// <summary>
	/// Contains both free and pro skins color variations,
	/// and automatically returns the correct one when converted to Color
	/// </summary>
	[Serializable]
	public struct DeSkinColor
	{
		public Color free;

		public Color pro;

		public DeSkinColor(Color free, Color pro)
		{
			this.free = free;
			this.pro = pro;
		}

		public DeSkinColor(float freeGradation, float proGradation)
		{
			free = new Color(freeGradation, freeGradation, freeGradation, 1f);
			pro = new Color(proGradation, proGradation, proGradation, 1f);
		}

		public DeSkinColor(Color color)
		{
			this = default(DeSkinColor);
			free = color;
			pro = color;
		}

		public DeSkinColor(float gradation)
		{
			this = default(DeSkinColor);
			free = new Color(gradation, gradation, gradation, 1f);
			pro = free;
		}

		public static implicit operator Color(DeSkinColor v)
		{
			if (!GUIUtils.isProSkin)
			{
				return v.free;
			}
			return v.pro;
		}

		public static implicit operator DeSkinColor(Color v)
		{
			return new DeSkinColor(v);
		}

		public override string ToString()
		{
			return $"{free}, {pro}";
		}
	}
}
