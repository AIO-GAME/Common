using DG.DemiEditor;
using UnityEngine;

namespace DG.DOTweenEditor.Core
{
	public class StylePalette : DeStylePalette
	{
		public class Custom : DeStyleSubPalette
		{
			public GUIStyle stickyToolbar;

			public GUIStyle stickyTitle;

			public GUIStyle warningLabel;

			public GUIStyle inlineToggle;

			/// <summary>
			/// Needs to be overridden in order to initialize new styles added from inherited classes
			/// </summary>
			public override void Init()
			{
				stickyToolbar = new GUIStyle(DeGUI.styles.toolbar.flat);
				stickyTitle = new GUIStyle(GUI.skin.label).Clone(FontStyle.Bold, 11).MarginBottom(0).ContentOffsetX(-2f);
				warningLabel = new GUIStyle(GUI.skin.label).Add(Color.black, Format.RichText).Background(DeStylePalette.orangeSquare);
				inlineToggle = DeGUI.styles.button.bBlankBorder.Clone().MarginTop(4).MarginBottom(0)
					.PaddingBottom(3);
			}
		}

		public readonly Custom custom = new Custom();
	}
}
