using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	public class ToolbarStyles
	{
		public GUIStyle def;

		public GUIStyle defNoPadding;

		public GUIStyle large;

		public GUIStyle small;

		public GUIStyle stickyTop;

		public GUIStyle box;

		public GUIStyle flat;

		internal void Init()
		{
			def = new GUIStyle(EditorStyles.toolbar).Height(18).StretchWidth();
			defNoPadding = def.Clone().Padding(0);
			large = new GUIStyle(def).Height(23);
			small = new GUIStyle(def).Height(13);
			stickyTop = new GUIStyle(def).MarginTop(0);
			box = new GUIStyle(GUI.skin.box).Height(20).StretchWidth().Padding(5, 6, 1, 0)
				.Margin(0, 0, 0, 0);
			flat = new GUIStyle(GUI.skin.box).Height(18).StretchWidth().Padding(5, 6, 0, 0)
				.Margin(0, 0, 0, 0)
				.Background(DeStylePalette.whiteSquare);
		}
	}
}
