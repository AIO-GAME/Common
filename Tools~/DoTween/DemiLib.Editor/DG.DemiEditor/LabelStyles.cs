using UnityEngine;

namespace DG.DemiEditor
{
	public class LabelStyles
	{
		public GUIStyle bold;

		public GUIStyle rightAligned;

		public GUIStyle wordwrap;

		public GUIStyle wordwrapRichtText;

		public GUIStyle toolbar;

		public GUIStyle toolbarRightAligned;

		public GUIStyle toolbarL;

		public GUIStyle toolbarS;

		public GUIStyle toolbarBox;

		internal void Init()
		{
			bold = new GUIStyle(GUI.skin.label).Add(FontStyle.Bold);
			rightAligned = new GUIStyle(GUI.skin.label).Add(TextAnchor.MiddleRight);
			wordwrap = new GUIStyle(GUI.skin.label).Add(Format.WordWrap);
			wordwrapRichtText = wordwrap.Clone(Format.RichText);
			toolbar = new GUIStyle(GUI.skin.label).Add(DeGUI.usesInterFont ? 11 : 9).ContentOffset(new Vector2(-2f, (!DeGUI.usesInterFont) ? 1 : (-1)));
			toolbarRightAligned = toolbar.Clone(TextAnchor.MiddleRight);
			toolbarL = new GUIStyle(toolbar).ContentOffsetY(3f);
			toolbarS = new GUIStyle(toolbar).Add(DeGUI.usesInterFont ? 10 : 8, FontStyle.Bold).ContentOffsetY(-2f);
			toolbarBox = new GUIStyle(toolbar).ContentOffsetY(0f);
		}
	}
}
