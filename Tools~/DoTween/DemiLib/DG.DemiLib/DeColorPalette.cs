using System;
using System.Globalization;
using UnityEngine;

namespace DG.DemiLib
{
	/// <summary>
	/// Stores a color palette, which can be passed to default DeGUI layouts when calling <code>DeGUI.BeginGUI</code>,
	/// and changed at any time by calling <code>DeGUI.ChangePalette</code>.
	/// You can inherit from this class to create custom color palettes with more hColor options.
	/// </summary>
	[Serializable]
	public class DeColorPalette
	{
		public DeColorGlobal global = new DeColorGlobal();

		public DeColorBG bg = new DeColorBG();

		public DeColorContent content = new DeColorContent();

		/// <summary>
		/// Converts a HEX color to a Unity Color and returns it
		/// </summary>
		/// <param name="hex">The HEX color, either with or without the initial # (accepts both regular and short format)</param>
		public static Color HexToColor(string hex)
		{
			if (hex[0] == '#')
			{
				hex = hex.Substring(1);
			}
			int length = hex.Length;
			if (length < 6)
			{
				float r = ((float)HexToInt(hex[0]) + (float)HexToInt(hex[0]) * 16f) / 255f;
				float g = ((float)HexToInt(hex[1]) + (float)HexToInt(hex[1]) * 16f) / 255f;
				float b = ((float)HexToInt(hex[2]) + (float)HexToInt(hex[2]) * 16f) / 255f;
				float a = ((length == 4) ? (((float)HexToInt(hex[3]) + (float)HexToInt(hex[3]) * 16f) / 255f) : 1f);
				return new Color(r, g, b, a);
			}
			float r2 = ((float)HexToInt(hex[1]) + (float)HexToInt(hex[0]) * 16f) / 255f;
			float g2 = ((float)HexToInt(hex[3]) + (float)HexToInt(hex[2]) * 16f) / 255f;
			float b2 = ((float)HexToInt(hex[5]) + (float)HexToInt(hex[4]) * 16f) / 255f;
			float a2 = ((length == 8) ? (((float)HexToInt(hex[7]) + (float)HexToInt(hex[6]) * 16f) / 255f) : 1f);
			return new Color(r2, g2, b2, a2);
		}

		private static int HexToInt(char hexVal)
		{
			return int.Parse(hexVal.ToString(), NumberStyles.HexNumber);
		}
	}
}
