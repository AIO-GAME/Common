using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// GUILayout methods
	/// </summary>
	public static class DeGUILayout
	{
		public class ToolbarScope : DeScope
		{
			public ToolbarScope(params GUILayoutOption[] options)
			{
				BeginToolbar(options);
			}

			public ToolbarScope(GUIStyle style, params GUILayoutOption[] options)
			{
				BeginToolbar(style, options);
			}

			public ToolbarScope(Color backgroundShade, GUIStyle style, params GUILayoutOption[] options)
			{
				BeginToolbar(backgroundShade, style, options);
			}

			protected override void CloseScope()
			{
				EndToolbar();
			}
		}

		public class VBoxScope : DeScope
		{
			public VBoxScope(GUIStyle style)
			{
				BeginVBox(style);
			}

			protected override void CloseScope()
			{
				EndVBox();
			}
		}

		private static int _activePressButtonId = -1;

		private static MethodInfo _miGradientField;

		private static MethodInfo _fooMiGetSliderRect;

		private static MethodInfo _miGetSliderRect
		{
			get
			{
				if (_fooMiGetSliderRect == null)
				{
					_fooMiGetSliderRect = typeof(EditorGUILayout).GetMethod("GetSliderRect", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[2]
					{
						typeof(bool),
						typeof(GUILayoutOption[])
					}, null);
				}
				return _fooMiGetSliderRect;
			}
		}

		/// <summary>
		/// A button that triggers an immediate repaint when hovered/pressed/unhovered
		/// (which otherwise doesn't happen if you set a background to the button's GUIStyle).<para />
		/// Requires <see cref="P:UnityEditor.EditorWindow.wantsMouseMove" /> to be activated.
		/// </summary>
		public static bool ActiveButton(GUIContent content, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			return DeGUI.ActiveButton(GUILayoutUtility.GetRect(content, guiStyle, options), content, guiStyle);
		}

		/// <summary>
		/// A button that triggers an immediate repaint when hovered/pressed/unhovered
		/// (which otherwise doesn't happen if you set a background to the button's GUIStyle)
		/// and also assigns different GUI colors based on the button's state and the given one.<para />
		/// Requires <see cref="P:UnityEditor.EditorWindow.wantsMouseMove" /> to be activated.
		/// </summary>
		/// <param name="content">Content</param>
		/// <param name="onNormal">Default color</param>
		/// <param name="guiStyle">Style</param>
		/// <param name="options">GUILayout options</param>
		public static bool ActiveButton(GUIContent content, Color onNormal, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return DeGUI.ActiveButton(GUILayoutUtility.GetRect(content, guiStyle, options), content, onNormal, null, null, guiStyle);
		}

		/// <summary>
		/// A button that triggers an immediate repaint when hovered/pressed/unhovered
		/// (which otherwise doesn't happen if you set a background to the button's GUIStyle)
		/// and also assigns different GUI colors based on the button's state with options to eventually auto-generate them.<para />
		/// Requires <see cref="P:UnityEditor.EditorWindow.wantsMouseMove" /> to be activated.
		/// </summary>
		/// <param name="content">Content</param>
		/// <param name="onNormal">Default color</param>
		/// <param name="onHover">Hover color (if NULL auto-generates it from the given one by making it brighter</param>
		/// <param name="onPressed">Pressed color (if NULL auto-generates it from the given one by making it even brighter</param>
		/// <param name="guiStyle">Style</param>
		/// <param name="options">GUILayout options</param>
		public static bool ActiveButton(GUIContent content, Color onNormal, Color? onHover = null, Color? onPressed = null, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			return DeGUI.ActiveButton(GUILayoutUtility.GetRect(content, guiStyle, options), content, onNormal, onHover, onPressed, guiStyle);
		}

		/// <summary>Shaded button</summary>
		public static bool ShadedButton(Color shade, string text, params GUILayoutOption[] options)
		{
			return ShadedButton(shade, new GUIContent(text, ""), null, options);
		}

		/// <summary>Shaded button</summary>
		public static bool ShadedButton(Color shade, string text, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return ShadedButton(shade, new GUIContent(text, ""), guiStyle, options);
		}

		/// <summary>Shaded button</summary>
		public static bool ShadedButton(Color shade, GUIContent content, params GUILayoutOption[] options)
		{
			return ShadedButton(shade, content, null, options);
		}

		/// <summary>Shaded button</summary>
		public static bool ShadedButton(Color shade, GUIContent content, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = shade;
			bool result = ((guiStyle == null) ? GUILayout.Button(content, options) : GUILayout.Button(content, guiStyle, options));
			GUI.backgroundColor = backgroundColor;
			return result;
		}

		/// <summary>Colored button</summary>
		public static bool ColoredButton(Color shade, Color contentColor, string text, params GUILayoutOption[] options)
		{
			return ColoredButton(shade, contentColor, new GUIContent(text, ""), null, options);
		}

		/// <summary>Colored button</summary>
		public static bool ColoredButton(Color shade, Color contentColor, string text, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return ColoredButton(shade, contentColor, new GUIContent(text, ""), guiStyle, options);
		}

		/// <summary>Colored button</summary>
		public static bool ColoredButton(Color shade, Color contentColor, GUIContent content, params GUILayoutOption[] options)
		{
			return ColoredButton(shade, contentColor, content, null, options);
		}

		/// <summary>Colored button</summary>
		public static bool ColoredButton(Color shade, Color contentColor, GUIContent content, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = shade;
			if (guiStyle == null)
			{
				guiStyle = DeGUI.styles.button.def;
			}
			bool result = GUILayout.Button(content, guiStyle.Clone(contentColor), options);
			GUI.backgroundColor = backgroundColor;
			return result;
		}

		/// <summary>
		/// Draws a button that returns TRUE the first time it's pressed, instead than when its released.
		/// </summary>
		public static bool PressButton(string text, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return PressButton(new GUIContent(text, ""), guiStyle, options);
		}

		/// <summary>
		/// Draws a button that returns TRUE the first time it's pressed, instead than when its released.
		/// </summary>
		public static bool PressButton(GUIContent content, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return DeGUI.PressButton(GUILayoutUtility.GetRect(content, guiStyle, options), content, guiStyle);
		}

		/// <summary>Toolbar foldout button</summary>
		public static bool ToolbarFoldoutButton(bool toggled, string text = null, bool isLarge = false, bool stretchedLabel = false, Color? forceLabelColor = null)
		{
			GUIStyle style = ((!isLarge) ? ((!string.IsNullOrEmpty(text)) ? ((!toggled) ? (stretchedLabel ? DeGUI.styles.button.toolFoldoutClosedWStretchedLabel : DeGUI.styles.button.toolFoldoutClosedWLabel) : (stretchedLabel ? DeGUI.styles.button.toolFoldoutOpenWStretchedLabel : DeGUI.styles.button.toolFoldoutOpenWLabel)) : (toggled ? DeGUI.styles.button.toolFoldoutOpen : DeGUI.styles.button.toolFoldoutClosed)) : ((!string.IsNullOrEmpty(text)) ? ((!toggled) ? (stretchedLabel ? DeGUI.styles.button.toolLFoldoutClosedWStretchedLabel : DeGUI.styles.button.toolLFoldoutClosedWLabel) : (stretchedLabel ? DeGUI.styles.button.toolLFoldoutOpenWStretchedLabel : DeGUI.styles.button.toolLFoldoutOpenWLabel)) : (toggled ? DeGUI.styles.button.toolLFoldoutOpen : DeGUI.styles.button.toolLFoldoutClosed)));
			if (forceLabelColor.HasValue)
			{
				style = style.Clone(forceLabelColor.Value);
			}
			if (GUILayout.Button(text, style))
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			return toggled;
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(bool toggled, string text, params GUILayoutOption[] options)
		{
			return ToggleButton(toggled, new GUIContent(text, ""), null, null, options);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(bool toggled, string text, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return ToggleButton(toggled, new GUIContent(text, ""), null, guiStyle, options);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(bool toggled, string text, DeColorPalette colorPalette, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			return ToggleButton(toggled, new GUIContent(text, ""), colorPalette, guiStyle, options);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(bool toggled, GUIContent content, params GUILayoutOption[] options)
		{
			return ToggleButton(toggled, content, null, null, options);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(bool toggled, GUIContent content, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return ToggleButton(toggled, content, null, guiStyle, options);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(bool toggled, GUIContent content, DeColorPalette colorPalette, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			DeColorPalette deColorPalette = colorPalette ?? DeGUI.colors;
			return ToggleButton(toggled, content, deColorPalette.bg.toggleOff, deColorPalette.bg.toggleOn, deColorPalette.content.toggleOff, deColorPalette.content.toggleOn, guiStyle, options);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(bool toggled, string text, Color bgOnColor, Color contentOnColor, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			return ToggleButton(toggled, new GUIContent(text, ""), DeGUI.colors.bg.toggleOff, bgOnColor, DeGUI.colors.content.toggleOff, contentOnColor, guiStyle, options);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(bool toggled, GUIContent content, Color bgOnColor, Color contentOnColor, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			return ToggleButton(toggled, content, DeGUI.colors.bg.toggleOff, bgOnColor, DeGUI.colors.content.toggleOff, contentOnColor, guiStyle, options);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(bool toggled, GUIContent content, Color bgOffColor, Color bgOnColor, Color contentOffColor, Color contenOnColor, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			Color backgroundColor = GUI.backgroundColor;
			Color contentColor = GUI.contentColor;
			GUI.backgroundColor = (toggled ? bgOnColor : bgOffColor);
			GUI.contentColor = (toggled ? contenOnColor : contentOffColor);
			if (guiStyle == null)
			{
				guiStyle = DeGUI.styles.button.bBlankBorder;
			}
			if (GUILayout.Button(content, guiStyle, options))
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			DeGUI.SetGUIColors(backgroundColor, contentColor, null);
			return toggled;
		}

		/// <summary>Begins an horizontal toolbar layout</summary>
		public static void BeginToolbar(params GUILayoutOption[] options)
		{
			BeginToolbar(Color.white, null, options);
		}

		/// <summary>Begins an horizontal toolbar layout</summary>
		public static void BeginToolbar(GUIStyle style, params GUILayoutOption[] options)
		{
			BeginToolbar(Color.white, style, options);
		}

		/// <summary>Begins an horizontal toolbar layout</summary>
		public static void BeginToolbar(Color backgroundShade, params GUILayoutOption[] options)
		{
			BeginToolbar(backgroundShade, null, options);
		}

		/// <summary>Begins an horizontal toolbar layout</summary>
		public static void BeginToolbar(Color backgroundShade, GUIStyle style, params GUILayoutOption[] options)
		{
			GUI.backgroundColor = backgroundShade;
			GUILayout.BeginHorizontal(style ?? DeGUI.styles.toolbar.def, options);
			GUI.backgroundColor = Color.white;
		}

		/// <summary>Ends an horizontal toolbar layout</summary>
		public static void EndToolbar()
		{
			GUILayout.EndHorizontal();
		}

		/// <summary>A toolbar with a label</summary>
		public static void Toolbar(string text, params GUILayoutOption[] options)
		{
			Toolbar(text, Color.white, null, null, options);
		}

		/// <summary>A toolbar with a label</summary>
		public static void Toolbar(string text, GUIStyle toolbarStyle, params GUILayoutOption[] options)
		{
			Toolbar(text, Color.white, toolbarStyle, null, options);
		}

		/// <summary>A toolbar with a label</summary>
		public static void Toolbar(string text, GUIStyle toolbarStyle, GUIStyle labelStyle, params GUILayoutOption[] options)
		{
			Toolbar(text, Color.white, toolbarStyle, labelStyle, options);
		}

		/// <summary>A toolbar with a label</summary>
		public static void Toolbar(string text, Color backgroundShade, params GUILayoutOption[] options)
		{
			Toolbar(text, backgroundShade, null, null, options);
		}

		/// <summary>A toolbar with a label</summary>
		public static void Toolbar(string text, Color backgroundShade, GUIStyle toolbarStyle, params GUILayoutOption[] options)
		{
			Toolbar(text, backgroundShade, toolbarStyle, null, options);
		}

		/// <summary>A toolbar with a label</summary>
		public static void Toolbar(string text, Color backgroundShade, GUIStyle toolbarStyle, GUIStyle labelStyle, params GUILayoutOption[] options)
		{
			BeginToolbar(backgroundShade, toolbarStyle, options);
			if (labelStyle == null)
			{
				labelStyle = DeGUI.styles.label.toolbar;
			}
			GUILayout.Label(text, labelStyle);
			EndToolbar();
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiColorField(GUIContent label, string fieldName, IList sources, params GUILayoutOption[] options)
		{
			return DeGUI.MultiColorField(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.colorField, options), label, fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiColorFieldAdvanced(GUIContent label, string fieldName, IList sources, bool alphaOnly, params GUILayoutOption[] options)
		{
			return DeGUI.MultiColorFieldAdvanced(alphaOnly ? ((Rect)_miGetSliderRect.Invoke(null, new object[2]
			{
				label.HasText(),
				options
			})) : EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.colorField, options), label, fieldName, sources, alphaOnly);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiCurveField(GUIContent label, string fieldName, IList sources, params GUILayoutOption[] options)
		{
			return DeGUI.MultiCurveField(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.colorField, options), label, fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiEnumPopup<T>(GUIContent label, string fieldName, IList sources, params GUILayoutOption[] options) where T : Enum
		{
			return DeGUI.MultiEnumPopup<T>(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.popup, options), label, fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiEnumPopup(GUIContent label, Type enumType, string fieldName, IList sources, params GUILayoutOption[] options)
		{
			return DeGUI.MultiEnumPopup(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.popup, options), label, enumType, fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiFloatField(GUIContent label, string fieldName, IList sources, float? min = null, float? max = null, params GUILayoutOption[] options)
		{
			return DeGUI.MultiFloatField(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.numberField, options), label, fieldName, sources, min, max);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiIntField(GUIContent label, string fieldName, IList sources, int? min = null, int? max = null, params GUILayoutOption[] options)
		{
			return DeGUI.MultiIntField(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.numberField, options), label, fieldName, sources, min, max);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiIntSlider(GUIContent label, string fieldName, IList sources, int min, int max, params GUILayoutOption[] options)
		{
			return DeGUI.MultiIntSlider((Rect)_miGetSliderRect.Invoke(null, new object[2]
			{
				label.HasText(),
				options
			}), label, fieldName, sources, min, max);
		}

		/// <summary>Returns TRUE if there's mixed values. Auto-determines object type from the field's type</summary>
		public static bool MultiObjectField(GUIContent label, string fieldName, IList sources, bool allowSceneObjects, params GUILayoutOption[] options)
		{
			return DeGUI.MultiObjectField(EditorGUILayout.GetControlRect(label.HasText(), 18f, options), label, fieldName, sources, allowSceneObjects);
		}

		/// <summary>Returns TRUE if there's mixed values. Forces field to accept only objects of the given type</summary>
		public static bool MultiObjectField(GUIContent label, string fieldName, IList sources, Type objType, bool allowSceneObjects, params GUILayoutOption[] options)
		{
			return DeGUI.MultiObjectField(EditorGUILayout.GetControlRect(label.HasText(), 18f, options), label, fieldName, sources, objType, allowSceneObjects);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiRectField(GUIContent label, string fieldName, IList sources, params GUILayoutOption[] options)
		{
			return DeGUI.MultiRectField(EditorGUILayout.GetControlRect(label.HasText(), 36f, EditorStyles.numberField, options), label, fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiSlider(GUIContent label, string fieldName, IList sources, float min, float max, params GUILayoutOption[] options)
		{
			return DeGUI.MultiSlider((Rect)_miGetSliderRect.Invoke(null, new object[2]
			{
				label.HasText(),
				options
			}), label, fieldName, sources, min, max);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiTextArea(string fieldName, IList sources, params GUILayoutOption[] options)
		{
			return DeGUI.MultiTextArea(GUILayoutUtility.GetRect(new GUIContent((string)sources[0].GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(sources[0])), EditorStyles.textField, options), fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiTextField(GUIContent label, string fieldName, IList sources, params GUILayoutOption[] options)
		{
			return DeGUI.MultiTextField(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.textField, options), label, fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiToggleButton(GUIContent label, string fieldName, IList sources, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			return MultiToggleButton(null, label, fieldName, sources, null, null, null, null, guiStyle, options);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiToggleButton(GUIContent label, string fieldName, IList sources, Color? bgOffColor, Color? bgOnColor = null, Color? contentOffColor = null, Color? contenOnColor = null, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			return MultiToggleButton(null, label, fieldName, sources, bgOffColor, bgOnColor, contentOffColor, contenOnColor, guiStyle, options);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiToggleButton(bool? forceSetToggle, GUIContent label, string fieldName, IList sources, Color? bgOffColor, Color? bgOnColor = null, Color? contentOffColor = null, Color? contenOnColor = null, GUIStyle guiStyle = null, params GUILayoutOption[] options)
		{
			return DeGUI.MultiToggleButton(GUILayoutUtility.GetRect(label, (guiStyle == null) ? DeGUI.styles.button.bBlankBorder : guiStyle, options), forceSetToggle, label, fieldName, sources, bgOffColor, bgOnColor, contentOffColor, contenOnColor, guiStyle);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiUnityEvent(GUIContent label, string fieldName, IList sources, List<SerializedProperty> fieldsAsSerializedProperties, params GUILayoutOption[] options)
		{
			return DeGUI.MultiUnityEvent(EditorGUILayout.GetControlRect(label.HasText(), fieldsAsSerializedProperties[0].GetUnityEventHeight(), EditorStyles.label, options), label, fieldName, sources, fieldsAsSerializedProperties);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector2Field(GUIContent label, string fieldName, IList sources, params GUILayoutOption[] options)
		{
			return DeGUI.MultiVector2Field(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.numberField, options), label, fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector3Field(GUIContent label, string fieldName, IList sources, params GUILayoutOption[] options)
		{
			return DeGUI.MultiVector3Field(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.numberField, options), label, fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector4Field(GUIContent label, string fieldName, IList sources, params GUILayoutOption[] options)
		{
			return DeGUI.MultiVector4Field(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.numberField, options), label, fieldName, sources);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector2FieldAdvanced(GUIContent label, string fieldName, IList sources, bool xEnabled, bool yEnabled, bool lockAllToX, bool lockAllToY, params GUILayoutOption[] options)
		{
			return DeGUI.MultiVector2FieldAdvanced(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.numberField, options), label, fieldName, sources, xEnabled, yEnabled, lockAllToX, lockAllToY);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector3FieldAdvanced(GUIContent label, string fieldName, IList sources, bool xEnabled, bool yEnabled, bool zEnabled, bool lockAllToX, bool lockAllToY, bool lockAllToZ, params GUILayoutOption[] options)
		{
			return DeGUI.MultiVector3FieldAdvanced(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.numberField, options), label, fieldName, sources, xEnabled, yEnabled, zEnabled, lockAllToX, lockAllToY, lockAllToZ);
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector4FieldAdvanced(GUIContent label, string fieldName, IList sources, bool xEnabled, bool yEnabled, bool zEnabled, bool wEnabled, bool lockAllToX, bool lockAllToY, bool lockAllToZ, bool lockAllToW, params GUILayoutOption[] options)
		{
			return DeGUI.MultiVector4FieldAdvanced(EditorGUILayout.GetControlRect(label.HasText(), 18f, EditorStyles.numberField, options), label, fieldName, sources, xEnabled, yEnabled, zEnabled, wEnabled, lockAllToX, lockAllToY, lockAllToZ, lockAllToW);
		}

		/// <summary>Vertical box layout with style and color options</summary>
		public static void BeginVBox(GUIStyle style)
		{
			BeginVBox(null, style);
		}

		/// <summary>Vertical box layout with style and color options</summary>
		public static void BeginVBox(Color? color = null, GUIStyle style = null)
		{
			Color backgroundColor = ((!color.HasValue) ? Color.white : color.Value);
			if (style == null)
			{
				style = DeGUI.styles.box.def;
			}
			Color backgroundColor2 = GUI.backgroundColor;
			GUI.backgroundColor = backgroundColor;
			GUILayout.BeginVertical(style);
			GUI.backgroundColor = backgroundColor2;
		}

		/// <summary>End vertical box layout</summary>
		public static void EndVBox()
		{
			GUILayout.EndVertical();
		}

		/// <summary>Horizontal Divider</summary>
		public static void HorizontalDivider(Color? color = null, int height = 1, int topMargin = 5, int bottomMargin = 5)
		{
			GUILayout.Space(topMargin);
			DeGUI.DrawColoredSquare(GUILayoutUtility.GetRect(0f, height, GUILayout.ExpandWidth(expand: true)), (!color.HasValue) ? ((Color)DeGUI.colors.bg.divider) : color.Value);
			GUILayout.Space(bottomMargin);
		}

		/// <summary>
		/// A text field that becomes editable only on double-click
		/// </summary>
		/// <param name="editorWindow">EditorWindow reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		public static string DoubleClickTextField(EditorWindow editorWindow, string id, string text, GUIStyle defaultStyle, GUIStyle editingStyle = null, params GUILayoutOption[] options)
		{
			return DoDoubleClickTextField(null, editorWindow, id, text, null, -1, defaultStyle, editingStyle, options);
		}

		/// <summary>
		/// A text field that becomes editable only on double-click
		/// </summary>
		/// <param name="editor">Editor reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		public static string DoubleClickTextField(Editor editor, string id, string text, GUIStyle defaultStyle, GUIStyle editingStyle = null, params GUILayoutOption[] options)
		{
			return DoDoubleClickTextField(editor, null, id, text, null, -1, defaultStyle, editingStyle, options);
		}

		/// <summary>
		/// A text field that becomes editable only on double-click and can also be dragged
		/// </summary>
		/// <param name="editorWindow">EditorWindow reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="draggableList">List containing the dragged item and all other relative draggable items</param>
		/// <param name="draggedItemIndex">DraggableList index of the item being dragged</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		/// <returns></returns>
		public static string DoubleClickDraggableTextField(EditorWindow editorWindow, string id, string text, IList draggableList, int draggedItemIndex, GUIStyle defaultStyle, GUIStyle editingStyle = null, params GUILayoutOption[] options)
		{
			return DoDoubleClickTextField(null, editorWindow, id, text, draggableList, draggedItemIndex, defaultStyle, editingStyle, options);
		}

		/// <summary>
		/// A text field that becomes editable only on double-click and can also be dragged
		/// </summary>
		/// <param name="editor">Editor reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="draggableList">List containing the dragged item and all other relative draggable items</param>
		/// <param name="draggedItemIndex">DraggableList index of the item being dragged</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		/// <returns></returns>
		public static string DoubleClickDraggableTextField(Editor editor, string id, string text, IList draggableList, int draggedItemIndex, GUIStyle defaultStyle, GUIStyle editingStyle = null, params GUILayoutOption[] options)
		{
			return DoDoubleClickTextField(editor, null, id, text, draggableList, draggedItemIndex, defaultStyle, editingStyle, options);
		}

		private static string DoDoubleClickTextField(Editor editor, EditorWindow editorWindow, string id, string text, IList draggableList, int draggedItemIndex, GUIStyle defaultStyle, GUIStyle editingStyle = null, params GUILayoutOption[] options)
		{
			return DeGUI.DoDoubleClickTextField(GUILayoutUtility.GetRect(new GUIContent(""), defaultStyle, options), isTextArea: false, editor, editorWindow, id, text, draggableList, draggedItemIndex, defaultStyle, editingStyle);
		}

		/// <summary>
		/// Creates a Gradient field by using Unity 4.x hidden default one and Reflection.
		/// </summary>
		public static Gradient GradientField(string label, Gradient gradient, params GUILayoutOption[] options)
		{
			if (_miGradientField == null)
			{
				_miGradientField = typeof(EditorGUILayout).GetMethod("GradientField", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[3]
				{
					typeof(string),
					typeof(Gradient),
					typeof(GUILayoutOption[])
				}, null);
			}
			return _miGradientField.Invoke(null, new object[3] { label, gradient, options }) as Gradient;
		}

		/// <summary>Scene field</summary>
		public static UnityEngine.Object SceneField(string label, UnityEngine.Object obj)
		{
			if (obj != null && !obj.ToString().EndsWith(".SceneAsset)"))
			{
				obj = null;
			}
			return EditorGUILayout.ObjectField(label, obj, typeof(UnityEngine.Object), false);
		}
	}
}
