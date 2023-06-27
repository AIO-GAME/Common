using System;
using UnityEngine;

namespace DG.DemiLib
{
	/// <summary>
	/// Background colors
	/// </summary>
	[Serializable]
	public class DeColorBG
	{
		/// <summary>Editor background color</summary>
		public DeSkinColor editor = new DeSkinColor(new Color32(194, 194, 194, byte.MaxValue), new Color32(56, 56, 56, byte.MaxValue));

		public DeSkinColor def = Color.white;

		public DeSkinColor critical = new DeSkinColor(new Color(0.9411765f, 0.2388736f, 0.006920422f, 1f), new Color(1f, 0.2482758f, 0f, 1f));

		public DeSkinColor divider = new DeSkinColor(0.6f, 0.3f);

		public DeSkinColor toggleOn = new DeSkinColor(new Color(0.3158468f, 0.875f, 0.1351103f, 1f), new Color(0.2183823f, 0.7279412f, 0.09099264f, 1f));

		public DeSkinColor toggleOff = new DeSkinColor(0.75f, 0.4f);

		public DeSkinColor toggleCritical = new DeSkinColor(new Color(0.9411765f, 0.2388736f, 0.006920422f, 1f), new Color(1f, 0.2482758f, 0f, 1f));
	}
}
