using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	public class ButtonStyles
	{
		public GUIStyle def;

		public GUIStyle tool;

		public GUIStyle toolNoFixedH;

		public GUIStyle toolL;

		public GUIStyle toolS;

		public GUIStyle toolIco;

		public GUIStyle toolFoldoutClosed;

		public GUIStyle toolFoldoutClosedWLabel;

		public GUIStyle toolFoldoutClosedWStretchedLabel;

		public GUIStyle toolFoldoutOpen;

		public GUIStyle toolFoldoutOpenWLabel;

		public GUIStyle toolFoldoutOpenWStretchedLabel;

		public GUIStyle toolLFoldoutClosed;

		public GUIStyle toolLFoldoutClosedWLabel;

		public GUIStyle toolLFoldoutClosedWStretchedLabel;

		public GUIStyle toolLFoldoutOpen;

		public GUIStyle toolLFoldoutOpenWLabel;

		public GUIStyle toolLFoldoutOpenWStretchedLabel;

		public GUIStyle foldoutClosedWLabel;

		public GUIStyle foldoutOpenWLabel;

		public GUIStyle bBlankBorder;

		public GUIStyle bBlankBorderCompact;

		public GUIStyle flatWhite;

		public GUIStyle transparent;

		internal void Init()
		{
			def = new GUIStyle(GUI.skin.button);
			tool = new GUIStyle(EditorStyles.toolbarButton).ContentOffsetY(-1f);
			if (DeGUI.usesInterFont)
			{
				tool.Height((int)(tool.fixedHeight - 3f));
			}
			toolNoFixedH = new GUIStyle(EditorStyles.toolbarButton).ContentOffsetY(-1f).Height(0);
			toolL = new GUIStyle(EditorStyles.toolbarButton).Height(23).ContentOffsetY(0f);
			toolS = new GUIStyle(EditorStyles.toolbarButton).Height(13).ContentOffsetY(0f).Padding(0);
			toolIco = new GUIStyle(tool).StretchWidth(doStretch: false).Width(22f).ContentOffsetX(-1f);
			toolFoldoutClosed = new GUIStyle(GUI.skin.button)
			{
				alignment = TextAnchor.MiddleLeft,
				active = 
				{
					background = DeStylePalette.transparent,
					scaledBackgrounds = new Texture2D[0]
				},
				fixedWidth = 14f,
				normal = 
				{
					background = DeStylePalette.ico_foldout_closed,
					scaledBackgrounds = new Texture2D[0]
				},
				border = EditorStyles.foldout.border,
				padding = new RectOffset(14, 0, 0, 0),
				margin = new RectOffset(0, 3, 0, 0),
				overflow = new RectOffset(-2, 0, -2, 0),
				stretchHeight = true,
				contentOffset = new Vector2(2f, -1f),
				richText = true
			};
			toolFoldoutClosedWLabel = toolFoldoutClosed.Clone(DeGUI.usesInterFont ? 11 : 9).Width(0f).StretchWidth(doStretch: false);
			toolFoldoutClosedWStretchedLabel = toolFoldoutClosedWLabel.Clone().StretchWidth();
			toolFoldoutOpen = new GUIStyle(toolFoldoutClosed)
			{
				normal = 
				{
					background = DeStylePalette.ico_foldout_open,
					scaledBackgrounds = new Texture2D[0]
				}
			};
			toolFoldoutOpenWLabel = new GUIStyle(toolFoldoutClosedWLabel)
			{
				normal = 
				{
					background = DeStylePalette.ico_foldout_open,
					scaledBackgrounds = new Texture2D[0]
				}
			};
			toolFoldoutOpenWStretchedLabel = toolFoldoutOpenWLabel.Clone().StretchWidth();
			toolLFoldoutClosed = toolFoldoutClosed.Clone().OverflowTop(-4);
			toolLFoldoutClosedWLabel = toolFoldoutClosedWLabel.Clone().OverflowTop(-4);
			toolLFoldoutClosedWStretchedLabel = toolFoldoutClosedWStretchedLabel.Clone().OverflowTop(-4);
			toolLFoldoutOpen = toolFoldoutOpen.Clone().OverflowTop(-4);
			toolLFoldoutOpenWLabel = toolFoldoutOpenWLabel.Clone().OverflowTop(-4);
			toolLFoldoutOpenWStretchedLabel = toolFoldoutOpenWStretchedLabel.Clone().OverflowTop(-4);
			foldoutOpenWLabel = toolFoldoutOpenWStretchedLabel.Clone(12);
			foldoutClosedWLabel = toolFoldoutClosedWStretchedLabel.Clone(12);
			bBlankBorder = new GUIStyle(GUI.skin.button).Add(TextAnchor.MiddleCenter, Color.white).Background(DeStylePalette.squareBorderCurved).Padding(5, 4, 1, 2)
				.Border(new RectOffset(4, 4, 4, 4))
				.Overflow(-1, -1, 0, 0)
				.ContentOffsetX(-1f);
			bBlankBorderCompact = bBlankBorder.Clone().Padding(5, 4, 0, 0).ContentOffsetY(-1f);
			flatWhite = DeGUI.styles.button.tool.Clone(TextAnchor.MiddleCenter).Background(DeStylePalette.whiteSquare).Margin(0)
				.Padding(0)
				.Border(0, 0, 0, 0)
				.Overflow(0)
				.Height(0)
				.ContentOffset(0f, 0f);
			transparent = flatWhite.Clone().Background(null);
		}
	}
}
