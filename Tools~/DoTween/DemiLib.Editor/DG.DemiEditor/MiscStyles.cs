using UnityEngine;

namespace DG.DemiEditor
{
	public class MiscStyles
	{
		public GUIStyle line;

		internal void Init()
		{
			line = new GUIStyle(GUI.skin.box).Padding(0, 0, 0, 0).Margin(0, 0, 0, 0).Background(DeStylePalette.whiteSquare);
		}
	}
}
