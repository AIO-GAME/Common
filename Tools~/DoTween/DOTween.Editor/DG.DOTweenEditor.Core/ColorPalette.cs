using System;
using DG.DemiLib;
using UnityEngine;

namespace DG.DOTweenEditor.Core
{
	[Serializable]
	public class ColorPalette : DeColorPalette
	{
		/// <summary>
		/// Custom colors
		/// </summary>
		[Serializable]
		public class Custom
		{
			public DeSkinColor stickyDivider = new DeSkinColor(Color.black, new Color(0.5f, 0.5f, 0.5f));
		}

		public Custom custom = new Custom();
	}
}
