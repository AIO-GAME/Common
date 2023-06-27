using UnityEngine;

namespace DG.DemiEditor
{
	public class BoxStyles
	{
		public GUIStyle def;

		public GUIStyle flat;

		public GUIStyle flatAlpha10;

		public GUIStyle flatAlpha25;

		public GUIStyle sticky;

		public GUIStyle stickyTop;

		public GUIStyle outline01;

		public GUIStyle outline02;

		public GUIStyle outline03;

		public GUIStyle roundOutline01;

		public GUIStyle roundOutline02;

		internal void Init()
		{
			def = new GUIStyle(GUI.skin.box).Padding(6, 6, 6, 6);
			flat = new GUIStyle(def).Background(DeStylePalette.whiteSquare);
			flatAlpha10 = new GUIStyle(def).Background(DeStylePalette.whiteSquareAlpha10);
			flatAlpha25 = new GUIStyle(def).Background(DeStylePalette.whiteSquareAlpha25);
			sticky = new DeSkinStyle(new GUIStyle(flatAlpha25).MarginTop(-2).MarginBottom(0), new GUIStyle(flatAlpha10).MarginTop(-2).MarginBottom(0));
			stickyTop = new DeSkinStyle(new GUIStyle(flatAlpha25).MarginTop(-2).MarginBottom(7), new GUIStyle(flatAlpha10).MarginTop(-2).MarginBottom(7));
			outline01 = DeGUI.styles.box.flat.Clone().Background(DeStylePalette.squareBorderEmpty01);
			outline02 = outline01.Clone().Border(new RectOffset(5, 5, 5, 5)).Background(DeStylePalette.squareBorderEmpty02);
			outline03 = outline01.Clone().Border(new RectOffset(7, 7, 7, 7)).Background(DeStylePalette.squareBorderEmpty03);
			roundOutline01 = outline02.Clone().Background(DeStylePalette.squareBorderCurvedEmpty);
			roundOutline02 = outline02.Clone().Background(DeStylePalette.squareBorderCurvedEmptyThick);
		}
	}
}
