using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor.UI
{
	public static class EditorGUIUtils
	{
		private static bool _stylesSet;

		private static bool _additionalStylesSet;

		public static GUIStyle boldLabelStyle;

		public static GUIStyle setupLabelStyle;

		public static GUIStyle redLabelStyle;

		public static GUIStyle btBigStyle;

		public static GUIStyle btSetup;

		public static GUIStyle btImgStyle;

		public static GUIStyle wrapCenterLabelStyle;

		public static GUIStyle handlelabelStyle;

		public static GUIStyle handleSelectedLabelStyle;

		public static GUIStyle wordWrapLabelStyle;

		public static GUIStyle wordWrapRichTextLabelStyle;

		public static GUIStyle wordWrapItalicLabelStyle;

		public static GUIStyle titleStyle;

		public static GUIStyle logoIconStyle;

		public static GUIStyle sideBtStyle;

		public static GUIStyle sideLogoIconBoldLabelStyle;

		public static GUIStyle wordWrapTextArea;

		public static GUIStyle popupButton;

		public static GUIStyle btIconStyle;

		public static GUIStyle infoboxStyle;

		private static Texture2D _logo;

		public static readonly string[] FilteredEaseTypes = new string[36]
		{
			"Linear", "InSine", "OutSine", "InOutSine", "InQuad", "OutQuad", "InOutQuad", "InCubic", "OutCubic", "InOutCubic",
			"InQuart", "OutQuart", "InOutQuart", "InQuint", "OutQuint", "InOutQuint", "InExpo", "OutExpo", "InOutExpo", "InCirc",
			"OutCirc", "InOutCirc", "InElastic", "OutElastic", "InOutElastic", "InBack", "OutBack", "InOutBack", "InBounce", "OutBounce",
			"InOutBounce", "Flash", "InFlash", "OutFlash", "InOutFlash", ":: AnimationCurve"
		};

		public static Texture2D logo
		{
			get
			{
				if (_logo == null)
				{
					_logo = AssetDatabase.LoadAssetAtPath(string.Concat("Package", EditorUtils.editorADBDir, "Imgs/DOTweenIcon.png"), typeof(Texture2D)) as Texture2D;
					EditorUtils.SetEditorTexture(_logo, FilterMode.Bilinear, 128);
				}
				return _logo;
			}
		}

		public static Ease FilteredEasePopup(string label, Ease currEase, GUIStyle style = null)
		{
			if (style == null)
			{
				style = EditorStyles.popup;
			}
			return FilteredEasePopup(EditorGUILayout.GetControlRect(label != null, 18f, style), label, currEase, style);
		}

		public static Ease FilteredEasePopup(Rect rect, string label, Ease currEase, GUIStyle style = null)
		{
			int num = ((currEase == Ease.INTERNAL_Custom) ? (FilteredEaseTypes.Length - 1) : Array.IndexOf(FilteredEaseTypes, currEase.ToString()));
			if (num == -1)
			{
				num = 0;
			}
			num = ((label == null) ? EditorGUI.Popup(rect, num, FilteredEaseTypes, (style == null) ? EditorStyles.popup : style) : EditorGUI.Popup(rect, label, num, FilteredEaseTypes, (style == null) ? EditorStyles.popup : style));
			if (num != FilteredEaseTypes.Length - 1)
			{
				return (Ease)Enum.Parse(typeof(Ease), FilteredEaseTypes[num]);
			}
			return Ease.INTERNAL_Custom;
		}

		public static void InspectorLogo()
		{
			GUILayout.Box(logo, logoIconStyle);
		}

		public static bool ToggleButton(bool toggled, GUIContent content, bool alert = false, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = ((!toggled) ? Color.white : (alert ? Color.red : Color.green));
			if ((guiStyle == null) ? GUILayout.Button(content, options) : GUILayout.Button(content, guiStyle, options))
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			GUI.backgroundColor = backgroundColor;
			return toggled;
		}

		public static void SetGUIStyles(Vector2? footerSize = null)
		{
			if (!_additionalStylesSet && footerSize.HasValue)
			{
				_additionalStylesSet = true;
				Vector2 value = footerSize.Value;
				btImgStyle = new GUIStyle(GUI.skin.button);
				btImgStyle.normal.background = null;
				btImgStyle.imagePosition = ImagePosition.ImageOnly;
				btImgStyle.padding = new RectOffset(0, 0, 0, 0);
				btImgStyle.fixedHeight = value.y;
			}
			if (!_stylesSet)
			{
				_stylesSet = true;
				boldLabelStyle = new GUIStyle(GUI.skin.label);
				boldLabelStyle.fontStyle = FontStyle.Bold;
				redLabelStyle = new GUIStyle(GUI.skin.label);
				redLabelStyle.normal.textColor = Color.red;
				setupLabelStyle = new GUIStyle(boldLabelStyle);
				setupLabelStyle.alignment = TextAnchor.MiddleCenter;
				wrapCenterLabelStyle = new GUIStyle(GUI.skin.label);
				wrapCenterLabelStyle.wordWrap = true;
				wrapCenterLabelStyle.alignment = TextAnchor.MiddleCenter;
				btBigStyle = new GUIStyle(GUI.skin.button);
				btBigStyle.padding = new RectOffset(0, 0, 10, 10);
				btSetup = new GUIStyle(btBigStyle);
				btSetup.padding = new RectOffset(10, 10, 6, 6);
				btSetup.wordWrap = true;
				btSetup.richText = true;
				titleStyle = new GUIStyle(GUI.skin.label)
				{
					fontSize = 12,
					fontStyle = FontStyle.Bold
				};
				handlelabelStyle = new GUIStyle(GUI.skin.label)
				{
					normal = 
					{
						textColor = Color.white
					},
					alignment = TextAnchor.MiddleLeft
				};
				handleSelectedLabelStyle = new GUIStyle(handlelabelStyle)
				{
					normal = 
					{
						textColor = Color.yellow
					},
					fontStyle = FontStyle.Bold
				};
				wordWrapLabelStyle = new GUIStyle(GUI.skin.label);
				wordWrapLabelStyle.wordWrap = true;
				wordWrapRichTextLabelStyle = new GUIStyle(GUI.skin.label);
				wordWrapRichTextLabelStyle.wordWrap = true;
				wordWrapRichTextLabelStyle.richText = true;
				wordWrapItalicLabelStyle = new GUIStyle(wordWrapLabelStyle);
				wordWrapItalicLabelStyle.fontStyle = FontStyle.Italic;
				logoIconStyle = new GUIStyle(GUI.skin.box);
				Texture2D texture2D3 = (logoIconStyle.active.background = (logoIconStyle.normal.background = null));
				logoIconStyle.margin = new RectOffset(0, 0, 0, 0);
				logoIconStyle.padding = new RectOffset(0, 0, 0, 0);
				sideBtStyle = new GUIStyle(GUI.skin.button);
				sideBtStyle.margin.top = 1;
				sideBtStyle.padding = new RectOffset(0, 0, 2, 2);
				sideLogoIconBoldLabelStyle = new GUIStyle(boldLabelStyle);
				sideLogoIconBoldLabelStyle.alignment = TextAnchor.MiddleLeft;
				sideLogoIconBoldLabelStyle.padding.top = 2;
				wordWrapTextArea = new GUIStyle(GUI.skin.textArea);
				wordWrapTextArea.wordWrap = true;
				popupButton = new GUIStyle(EditorStyles.popup);
				popupButton.fixedHeight = 18f;
				popupButton.margin.top++;
				btIconStyle = new GUIStyle(GUI.skin.button);
				btIconStyle.padding.left -= 2;
				btIconStyle.fixedWidth = 24f;
				btIconStyle.stretchWidth = false;
				infoboxStyle = new GUIStyle(GUI.skin.box)
				{
					alignment = TextAnchor.UpperLeft,
					richText = true,
					wordWrap = true,
					padding = new RectOffset(5, 5, 5, 6),
					normal = 
					{
						textColor = Color.white,
						background = Texture2D.whiteTexture
					}
				};
			}
		}
	}
}
