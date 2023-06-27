using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.DemiEditor.Panels;
using DG.DemiLib;
using DG.DemiLib.Core;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Global Demigiant GUI manager. Call <see cref="M:DG.DemiEditor.DeGUI.BeginGUI(DG.DemiLib.DeColorPalette,DG.DemiEditor.DeStylePalette)" /> to initialize it inside GUI calls.
	/// </summary>
	public static class DeGUI
	{
		private enum ButtonState
		{
			Normal,
			Hover,
			Pressed
		}

		public class MixedValueScope : DeScope
		{
			private readonly bool _prevMixedValue;

			public MixedValueScope(bool showMixedValue = true)
			{
				_prevMixedValue = EditorGUI.showMixedValue;
				EditorGUI.showMixedValue = showMixedValue;
			}

			protected override void CloseScope()
			{
				EditorGUI.showMixedValue = _prevMixedValue;
			}
		}

		public class LabelFieldWidthScope : DeScope
		{
			private readonly float _prevLabelWidth;

			private readonly float _prevFieldWidth;

			public LabelFieldWidthScope(float? labelWidth, float? fieldWidth = null)
			{
				_prevLabelWidth = EditorGUIUtility.labelWidth;
				_prevFieldWidth = EditorGUIUtility.fieldWidth;
				if (labelWidth.HasValue)
				{
					EditorGUIUtility.labelWidth = labelWidth.Value;
				}
				if (fieldWidth.HasValue)
				{
					EditorGUIUtility.fieldWidth = fieldWidth.Value;
				}
			}

			protected override void CloseScope()
			{
				EditorGUIUtility.labelWidth = _prevLabelWidth;
				EditorGUIUtility.fieldWidth = _prevFieldWidth;
			}
		}

		public class ColorScope : DeScope
		{
			private readonly Color _prevBackground;

			private readonly Color _prevContent;

			private readonly Color _prevMain;

			public ColorScope(Color? background, Color? content = null, Color? main = null)
			{
				_prevBackground = GUI.backgroundColor;
				_prevContent = GUI.contentColor;
				_prevMain = GUI.color;
				if (background.HasValue)
				{
					GUI.backgroundColor = background.Value;
				}
				if (content.HasValue)
				{
					GUI.contentColor = content.Value;
				}
				if (main.HasValue)
				{
					GUI.color = main.Value;
				}
			}

			protected override void CloseScope()
			{
				GUI.backgroundColor = _prevBackground;
				GUI.contentColor = _prevContent;
				GUI.color = _prevMain;
			}
		}

		/// <summary>
		/// Sets the GUI cursor color to the given ones
		/// </summary>
		public class CursorColorScope : DeScope
		{
			private readonly Color _prevColor;

			public CursorColorScope(Color color)
			{
				_prevColor = GUI.skin.settings.cursorColor;
				GUI.skin.settings.cursorColor = color;
			}

			protected override void CloseScope()
			{
				GUI.skin.settings.cursorColor = _prevColor;
			}
		}

		/// <summary>
		/// Sets the GUI matrix to the given ones
		/// </summary>
		public class MatrixScope : DeScope
		{
			private readonly Matrix4x4 _prevMatrix;

			public MatrixScope(Matrix4x4 matrix)
			{
				_prevMatrix = GUI.matrix;
				GUI.matrix = matrix;
			}

			protected override void CloseScope()
			{
				GUI.matrix = _prevMatrix;
			}
		}

		/// <summary>
		/// Wrapper to set serialized fields with multiple sources selected: automatically sets GUI to show mixed values when necessary
		/// and contains a fieldInfo <see cref="T:System.Reflection.FieldInfo" /> which is set within the wrapper.<para />
		/// Note that you must set the <see cref="F:DG.DemiEditor.DeGUI.MultiPropertyScope.value" /> property within the wrapper so that it's assigned correctly when closing the scope.
		/// </summary>
		public class MultiPropertyScope : DeScope
		{
			public readonly FieldInfo fieldInfo;

			public readonly bool hasMixedValue;

			public readonly bool isGenericSerializedProperty;

			public object value;

			private readonly IList _sources;

			private readonly bool _prevShowMixedValue;

			private readonly bool _isSerializedPropertyMode;

			private readonly bool _requiresSpecialEndChangeCheck;

			private readonly List<SerializedProperty> _fieldsAsSerializedProperties;

			/// <summary>Multi property scope</summary>
			/// <param name="fieldName">Name of the field so it can be found and set/get via Reflection</param>
			/// <param name="sources">List of the sources containing the given field</param>
			/// <param name="requiresSpecialEndChangeCheck">If TRUE validates EditorGUI.EndChangeCheck before calling it
			/// (fixes an issue which happens with advanced Undo usage in DOTween Timeline and ColorFields)</param>
			public MultiPropertyScope(string fieldName, IList sources, List<SerializedProperty> fieldsAsSerializedProperties = null, bool requiresSpecialEndChangeCheck = false)
			{
				_sources = sources;
				fieldInfo = GetFieldInfo(fieldName, sources);
				_prevShowMixedValue = EditorGUI.showMixedValue;
				_requiresSpecialEndChangeCheck = requiresSpecialEndChangeCheck;
				EditorGUI.BeginChangeCheck();
				EditorGUI.showMixedValue = (hasMixedValue = HasMixedValue(fieldInfo, _sources));
				if (fieldsAsSerializedProperties != null)
				{
					_isSerializedPropertyMode = true;
					_fieldsAsSerializedProperties = fieldsAsSerializedProperties;
					isGenericSerializedProperty = _fieldsAsSerializedProperties[0].propertyType == SerializedPropertyType.Generic;
					for (int i = 0; i < _fieldsAsSerializedProperties.Count; i++)
					{
						_fieldsAsSerializedProperties[i].serializedObject.Update();
					}
				}
			}

			protected override void CloseScope()
			{
				EditorGUI.showMixedValue = _prevShowMixedValue;
				if (_requiresSpecialEndChangeCheck)
				{
					if (((Stack<bool>)_fiChangedStack.GetValue(null)).Count > 0 && !EditorGUI.EndChangeCheck())
					{
						return;
					}
				}
				else if (!EditorGUI.EndChangeCheck())
				{
					return;
				}
				if (_isSerializedPropertyMode)
				{
					if (isGenericSerializedProperty)
					{
						_fieldsAsSerializedProperties[0].serializedObject.ApplyModifiedProperties();
						return;
					}
					object obj = null;
					for (int i = 0; i < _fieldsAsSerializedProperties.Count; i++)
					{
						if (i == 0)
						{
							obj = _fieldsAsSerializedProperties[i].GetValue();
						}
						else
						{
							_fieldsAsSerializedProperties[i].SetValue(obj);
						}
						_fieldsAsSerializedProperties[i].serializedObject.ApplyModifiedProperties();
					}
					return;
				}
				try
				{
					for (int j = 0; j < _sources.Count; j++)
					{
						fieldInfo.SetValue(_sources[j], value);
					}
				}
				catch
				{
				}
			}

			private static FieldInfo GetFieldInfo(string fieldName, IList targets)
			{
				return targets[0].GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}

			private static bool HasMixedValue(FieldInfo fInfo, IList targets)
			{
				object obj = null;
				for (int i = 0; i < targets.Count; i++)
				{
					if (i == 0)
					{
						obj = fInfo.GetValue(targets[i]);
					}
					else if (!fInfo.GetValue(targets[i]).Equals(obj))
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>
		/// Default color palette
		/// </summary>
		public static DeColorPalette colors;

		/// <summary>
		/// Default style palette
		/// </summary>
		public static DeStylePalette styles;

		/// <summary>TRUE if we're using the PRO skin</summary>
		public static readonly bool IsProSkin;

		public static Color defaultGUIColor;

		public static Color defaultGUIBackgroundColor;

		public static Color defaultGUIContentColor;

		public static readonly GUIContent MixedValueLabel;

		private static DeColorPalette _defaultColorPalette;

		private static DeStylePalette _defaultStylePalette;

		private static string _doubleClickTextFieldId;

		private static int _activePressButtonId;

		private static int _pressFrame;

		private static bool _hasEditorPressUpdateActive;

		private static readonly Dictionary<Rect, ButtonState> _ButtonRectToState;

		private static MethodInfo _foo_miDefaultPropertyField;

		private static FieldInfo _foo_fiChangedStack;

		public static int defaultFontSize { get; private set; }

		public static bool usesInterFont { get; private set; }

		public static bool isMouseDown { get; private set; }

		private static MethodInfo _miDefaultPropertyField
		{
			get
			{
				if (_foo_miDefaultPropertyField == null)
				{
					_foo_miDefaultPropertyField = typeof(EditorGUI).GetMethod("DefaultPropertyField", BindingFlags.Static | BindingFlags.NonPublic);
				}
				return _foo_miDefaultPropertyField;
			}
		}

		private static FieldInfo _fiChangedStack
		{
			get
			{
				if (_foo_fiChangedStack == null)
				{
					_foo_fiChangedStack = typeof(EditorGUI).GetField("s_ChangedStack", BindingFlags.Static | BindingFlags.NonPublic);
				}
				return _foo_fiChangedStack;
			}
		}

		static DeGUI()
		{
			MixedValueLabel = new GUIContent("—");
			_activePressButtonId = -1;
			_pressFrame = -1;
			_hasEditorPressUpdateActive = false;
			_ButtonRectToState = new Dictionary<Rect, ButtonState>();
			GUIUtils.isProSkin = (IsProSkin = EditorGUIUtility.isProSkin);
			EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Remove(EditorApplication.update, new EditorApplication.CallbackFunction(OnEditorPressUpdate));
			_hasEditorPressUpdateActive = false;
		}

		private static void OnEditorPressUpdate()
		{
			if (GUIUtility.hotControl < 2)
			{
				_pressFrame = -1;
				EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Remove(EditorApplication.update, new EditorApplication.CallbackFunction(OnEditorPressUpdate));
				_hasEditorPressUpdateActive = false;
			}
			else
			{
				_pressFrame++;
			}
		}

		/// <summary>
		/// Call this at the beginning of GUI methods.
		/// Returns TRUE if the styles were initialized or re-initialized
		/// </summary>
		/// <param name="guiColorPalette">Eventual <see cref="T:DG.DemiLib.DeColorPalette" /> to use</param>
		/// <param name="guiStylePalette">Eventual <see cref="T:DG.DemiEditor.DeStylePalette" /> to use</param>
		public static bool BeginGUI(DeColorPalette guiColorPalette = null, DeStylePalette guiStylePalette = null)
		{
			bool result = ChangePalette(guiColorPalette, guiStylePalette);
			defaultGUIColor = GUI.color;
			defaultGUIBackgroundColor = GUI.backgroundColor;
			defaultGUIContentColor = GUI.contentColor;
			defaultFontSize = GUI.skin.label.fontSize;
			usesInterFont = DeUnityEditorVersion.Version >= 2019.3f && GUI.skin.label.font == null;
			switch (Event.current.rawType)
			{
			case EventType.MouseDown:
				isMouseDown = true;
				break;
			case EventType.MouseUp:
				isMouseDown = false;
				break;
			}
			return result;
		}

		/// <summary>
		/// Better implementation of GUI.BeginScrollView.
		/// Returns the modified scrollView struct.<para />
		/// Must be closed by a DeGUI.<see cref="M:DG.DemiEditor.DeGUI.EndScrollView" />.
		/// </summary>
		/// <param name="scrollViewArea">Area used by the scrollView</param>
		/// <param name="scrollView"><see cref="T:DG.DemiEditor.DeScrollView" /> target</param>
		/// <param name="resetContentHeightToZero">If TRUE (default) resets <see cref="P:DG.DemiEditor.DeScrollView.fullContentArea" />.height to 0
		/// after beginning the ScrollView</param>
		public static DeScrollView BeginScrollView(Rect scrollViewArea, DeScrollView scrollView, bool resetContentHeightToZero = true)
		{
			scrollView.Begin(scrollViewArea, resetContentHeightToZero);
			return scrollView;
		}

		/// <summary>
		/// Closes a DeGUI.<see cref="M:DG.DemiEditor.DeGUI.BeginScrollView(UnityEngine.Rect,DG.DemiEditor.DeScrollView,System.Boolean)" /> correctly
		/// </summary>
		public static void EndScrollView()
		{
			DeScrollView.Current().End();
		}

		/// <summary>
		/// Exits the current event correctly, also taking care of eventual drag operations
		/// </summary>
		public static void ExitCurrentEvent()
		{
			DeGUIDrag.EndDrag(applyDrag: false);
			Event.current.type = EventType.Used;
		}

		/// <summary>
		/// Changes the active palettes to the given ones
		/// (or resets them to the default ones if NULL).
		/// Returns TRUE if the styles were initialized or re-initialized
		/// </summary>
		public static bool ChangePalette(DeColorPalette newColorPalette, DeStylePalette newStylePalette)
		{
			if (newColorPalette != null)
			{
				colors = newColorPalette;
			}
			else
			{
				if (_defaultColorPalette == null)
				{
					_defaultColorPalette = new DeColorPalette();
				}
				colors = _defaultColorPalette;
			}
			if (newStylePalette != null)
			{
				styles = newStylePalette;
			}
			else
			{
				if (_defaultStylePalette == null)
				{
					_defaultStylePalette = new DeStylePalette();
				}
				styles = _defaultStylePalette;
			}
			return styles.Init();
		}

		/// <summary>
		/// Resets the GUI colors to the default ones (only available if BeginGUI was called first)
		/// </summary>
		public static void ResetGUIColors(bool resetBackground = true, bool resetContent = true, bool resetMain = true)
		{
			if (resetBackground)
			{
				GUI.backgroundColor = defaultGUIBackgroundColor;
			}
			if (resetContent)
			{
				GUI.contentColor = defaultGUIContentColor;
			}
			if (resetMain)
			{
				GUI.color = defaultGUIColor;
			}
		}

		/// <summary>
		/// Sets the GUI colors to the given ones
		/// </summary>
		public static void SetGUIColors(Color? background, Color? content, Color? main)
		{
			if (background.HasValue)
			{
				GUI.backgroundColor = background.Value;
			}
			if (content.HasValue)
			{
				GUI.contentColor = content.Value;
			}
			if (main.HasValue)
			{
				GUI.color = main.Value;
			}
		}

		/// <summary>
		/// Opens a panel that previews the given texture (if not NULL)
		/// </summary>
		public static void ShowTexturePreview(Texture2D texture)
		{
			TexturePreviewWindow.Open(texture);
		}

		private static ButtonState GetButtonState(Rect r)
		{
			if (!_ButtonRectToState.ContainsKey(r))
			{
				_ButtonRectToState.Add(r, ButtonState.Normal);
			}
			return _ButtonRectToState[r];
		}

		private static void SetButtonState(Rect r, ButtonState state)
		{
			if (_ButtonRectToState.ContainsKey(r))
			{
				_ButtonRectToState[r] = state;
			}
			else
			{
				_ButtonRectToState.Add(r, state);
			}
		}

		/// <summary>
		/// A button that triggers an immediate repaint when hovered/pressed/unhovered
		/// (which otherwise doesn't happen if you apply a background to the button's GUIStyle).<para />
		/// Requires <see cref="P:UnityEditor.EditorWindow.wantsMouseMove" /> to be activated.
		/// </summary>
		public static bool ActiveButton(Rect rect, GUIContent content, GUIStyle guiStyle = null)
		{
			ButtonState buttonState = GetButtonState(rect);
			ButtonState buttonState2 = (rect.Contains(Event.current.mousePosition) ? ((!isMouseDown) ? ButtonState.Hover : ButtonState.Pressed) : ButtonState.Normal);
			bool result = GUI.Button(rect, content, guiStyle);
			if (buttonState != buttonState2)
			{
				SetButtonState(rect, buttonState2);
				DeEditorPanelUtils.RepaintCurrentEditor();
			}
			return result;
		}

		/// <summary>
		/// A button that triggers an immediate repaint when hovered/pressed/unhovered
		/// (which otherwise doesn't happen if you apply a background to the button's GUIStyle)
		/// and also assigns different GUI colors based on the button's state and the given one.<para />
		/// Requires <see cref="P:UnityEditor.EditorWindow.wantsMouseMove" /> to be activated.
		/// </summary>
		/// <param name="rect">Rect</param>
		/// <param name="content">Content</param>
		/// <param name="onNormal">Default color</param>
		/// <param name="guiStyle">Style</param>
		public static bool ActiveButton(Rect rect, GUIContent content, Color onNormal, GUIStyle guiStyle)
		{
			return ActiveButton(rect, content, onNormal, null, null, guiStyle);
		}

		/// <summary>
		/// A button that triggers an immediate repaint when hovered/pressed/unhovered
		/// (which otherwise doesn't happen if you apply a background to the button's GUIStyle)
		/// and also assigns different GUI colors based on the button's state with options to eventually auto-generate them.<para />
		/// Requires <see cref="P:UnityEditor.EditorWindow.wantsMouseMove" /> to be activated.
		/// </summary>
		/// <param name="rect">Rect</param>
		/// <param name="content">Content</param>
		/// <param name="onNormal">Default color</param>
		/// <param name="onHover">Hover color (if NULL auto-generates it from the given one by making it brighter</param>
		/// <param name="onPressed">Pressed color (if NULL auto-generates it from the given one by making it even brighter</param>
		/// <param name="guiStyle">Style</param>
		public static bool ActiveButton(Rect rect, GUIContent content, Color onNormal, Color? onHover = null, Color? onPressed = null, GUIStyle guiStyle = null)
		{
			Color value = onNormal;
			ButtonState buttonState = GetButtonState(rect);
			ButtonState buttonState2 = (rect.Contains(Event.current.mousePosition) ? ((!isMouseDown) ? ButtonState.Hover : ButtonState.Pressed) : ButtonState.Normal);
			switch (buttonState2)
			{
			case ButtonState.Hover:
				value = (onHover.HasValue ? onHover.Value : onNormal.CloneAndChangeBrightness(1.15f));
				break;
			case ButtonState.Pressed:
				value = (onPressed.HasValue ? onPressed.Value : onNormal.CloneAndChangeBrightness(1.3f));
				break;
			}
			bool result;
			using (new ColorScope(value))
			{
				result = GUI.Button(rect, content, guiStyle);
			}
			if (buttonState != buttonState2)
			{
				SetButtonState(rect, buttonState2);
				DeEditorPanelUtils.RepaintCurrentEditor();
			}
			return result;
		}

		/// <summary>Shaded button</summary>
		public static bool ShadedButton(Rect rect, Color shade, string text)
		{
			return ShadedButton(rect, shade, new GUIContent(text, ""), null);
		}

		/// <summary>Shaded button</summary>
		public static bool ShadedButton(Rect rect, Color shade, string text, GUIStyle guiStyle)
		{
			return ShadedButton(rect, shade, new GUIContent(text, ""), guiStyle);
		}

		/// <summary>Shaded button</summary>
		public static bool ShadedButton(Rect rect, Color shade, GUIContent content)
		{
			return ShadedButton(rect, shade, content, null);
		}

		/// <summary>Shaded button</summary>
		public static bool ShadedButton(Rect rect, Color shade, GUIContent content, GUIStyle guiStyle)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = shade;
			bool result = ((guiStyle == null) ? GUI.Button(rect, content) : GUI.Button(rect, content, guiStyle));
			GUI.backgroundColor = backgroundColor;
			return result;
		}

		/// <summary>Colored button</summary>
		public static bool ColoredButton(Rect rect, Color shade, Color contentColor, string text)
		{
			return ColoredButton(rect, shade, contentColor, new GUIContent(text, ""), null);
		}

		/// <summary>Colored button</summary>
		public static bool ColoredButton(Rect rect, Color shade, Color contentColor, string text, GUIStyle guiStyle)
		{
			return ColoredButton(rect, shade, contentColor, new GUIContent(text, ""), guiStyle);
		}

		/// <summary>Colored button</summary>
		public static bool ColoredButton(Rect rect, Color shade, Color contentColor, GUIContent content)
		{
			return ColoredButton(rect, shade, contentColor, content, null);
		}

		/// <summary>Colored button</summary>
		public static bool ColoredButton(Rect rect, Color shade, Color contentColor, GUIContent content, GUIStyle guiStyle)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = shade;
			if (guiStyle == null)
			{
				guiStyle = styles.button.def;
			}
			bool result = GUI.Button(rect, content, guiStyle.Clone(contentColor));
			GUI.backgroundColor = backgroundColor;
			return result;
		}

		/// <summary>Toolbar foldout button which allows clicking even on its label</summary>
		public static bool FoldoutButton(Rect rect, bool toggled, string text = null, bool isLarge = false, bool stretchedLabel = false)
		{
			GUIStyle style = ((!isLarge) ? ((!string.IsNullOrEmpty(text)) ? ((!toggled) ? (stretchedLabel ? styles.button.toolFoldoutClosedWStretchedLabel : styles.button.toolFoldoutClosedWLabel) : (stretchedLabel ? styles.button.toolFoldoutOpenWStretchedLabel : styles.button.toolFoldoutOpenWLabel)) : (toggled ? styles.button.toolFoldoutOpen : styles.button.toolFoldoutClosed)) : ((!string.IsNullOrEmpty(text)) ? ((!toggled) ? (stretchedLabel ? styles.button.toolLFoldoutClosedWStretchedLabel : styles.button.toolLFoldoutClosedWLabel) : (stretchedLabel ? styles.button.toolLFoldoutOpenWStretchedLabel : styles.button.toolLFoldoutOpenWLabel)) : (toggled ? styles.button.toolLFoldoutOpen : styles.button.toolLFoldoutClosed)));
			if (GUI.Button(rect, text, style))
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			return toggled;
		}

		/// <summary>Foldout button + label (not intended to be used in toolbar) which allows click-to-foldout/foldin</summary>
		public static bool FoldoutLabel(Rect rect, bool toggled, GUIContent label = null)
		{
			bool flag = label != null && !string.IsNullOrEmpty(label.text);
			GUIStyle style = (flag ? (toggled ? styles.button.foldoutOpenWLabel : styles.button.foldoutClosedWLabel) : (toggled ? styles.button.toolFoldoutOpen : styles.button.toolFoldoutClosed));
			if (GUI.Button(rect, flag ? label : GUIContent.none, style))
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			return toggled;
		}

		/// <summary>
		/// Draws a button that returns TRUE the first time it's pressed, instead than when its released.
		/// </summary>
		public static bool PressButton(Rect rect, string text, GUIStyle guiStyle)
		{
			return PressButton(rect, new GUIContent(text, ""), guiStyle);
		}

		/// <summary>
		/// Draws a button that returns TRUE the first time it's pressed, instead than when its released.
		/// </summary>
		public static bool PressButton(Rect rect, GUIContent content, GUIStyle guiStyle)
		{
			if (GUI.enabled && Event.current.type == EventType.MouseUp && _activePressButtonId != -1)
			{
				GUIUtility.hotControl = 0;
				Event.current.Use();
				_pressFrame = -1;
				_activePressButtonId = -1;
			}
			GUI.Button(rect, content, guiStyle);
			int lastControlId = DeEditorGUIUtils.GetLastControlId();
			int hotControl = GUIUtility.hotControl;
			int num;
			if (GUI.enabled && _activePressButtonId == -1)
			{
				num = ((hotControl > 1) ? 1 : 0);
				if (num != 0 && _pressFrame == -1)
				{
					_pressFrame++;
					if (!_hasEditorPressUpdateActive)
					{
						_hasEditorPressUpdateActive = true;
						EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Remove(EditorApplication.update, new EditorApplication.CallbackFunction(OnEditorPressUpdate));
						EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Combine(EditorApplication.update, new EditorApplication.CallbackFunction(OnEditorPressUpdate));
					}
				}
			}
			else
			{
				num = 0;
			}
			bool flag = num != 0 && _pressFrame < 2 && rect.Contains(Event.current.mousePosition);
			if (flag && _activePressButtonId == -1 && _activePressButtonId != lastControlId)
			{
				GUIUtility.hotControl = lastControlId;
				_activePressButtonId = lastControlId;
				return true;
			}
			if (!flag && hotControl < 1)
			{
				_activePressButtonId = -1;
			}
			return false;
		}

		/// <summary>Toolbar foldout button</summary>
		public static bool ToolbarFoldoutButton(Rect rect, bool toggled, string text = null, bool isLarge = false, bool stretchedLabel = false, Color? forceLabelColor = null)
		{
			GUIStyle style = ((!isLarge) ? ((!string.IsNullOrEmpty(text)) ? ((!toggled) ? (stretchedLabel ? styles.button.toolFoldoutClosedWStretchedLabel : styles.button.toolFoldoutClosedWLabel) : (stretchedLabel ? styles.button.toolFoldoutOpenWStretchedLabel : styles.button.toolFoldoutOpenWLabel)) : (toggled ? styles.button.toolFoldoutOpen : styles.button.toolFoldoutClosed)) : ((!string.IsNullOrEmpty(text)) ? ((!toggled) ? (stretchedLabel ? styles.button.toolLFoldoutClosedWStretchedLabel : styles.button.toolLFoldoutClosedWLabel) : (stretchedLabel ? styles.button.toolLFoldoutOpenWStretchedLabel : styles.button.toolLFoldoutOpenWLabel)) : (toggled ? styles.button.toolLFoldoutOpen : styles.button.toolLFoldoutClosed)));
			if (forceLabelColor.HasValue)
			{
				style = style.Clone(forceLabelColor.Value);
			}
			if (GUI.Button(rect, text, style))
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			return toggled;
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(Rect rect, bool toggled, string text)
		{
			return ToggleButton(rect, toggled, new GUIContent(text, ""), null, null);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(Rect rect, bool toggled, string text, GUIStyle guiStyle)
		{
			return ToggleButton(rect, toggled, new GUIContent(text, ""), null, guiStyle);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(Rect rect, bool toggled, string text, DeColorPalette colorPalette, GUIStyle guiStyle = null)
		{
			return ToggleButton(rect, toggled, new GUIContent(text, ""), colorPalette, guiStyle);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(Rect rect, bool toggled, GUIContent content)
		{
			return ToggleButton(rect, toggled, content, null, null);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(Rect rect, bool toggled, GUIContent content, GUIStyle guiStyle)
		{
			return ToggleButton(rect, toggled, content, null, guiStyle);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(Rect rect, bool toggled, GUIContent content, DeColorPalette colorPalette, GUIStyle guiStyle = null)
		{
			DeColorPalette deColorPalette = colorPalette ?? colors;
			return ToggleButton(rect, toggled, content, deColorPalette.bg.toggleOff, deColorPalette.bg.toggleOn, deColorPalette.content.toggleOff, deColorPalette.content.toggleOn, guiStyle);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(Rect rect, bool toggled, string text, Color bgOnColor, Color contentOnColor, GUIStyle guiStyle = null)
		{
			return ToggleButton(rect, toggled, new GUIContent(text, ""), colors.bg.toggleOff, bgOnColor, colors.content.toggleOff, contentOnColor, guiStyle);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(Rect rect, bool toggled, GUIContent content, Color bgOnColor, Color contentOnColor, GUIStyle guiStyle = null)
		{
			return ToggleButton(rect, toggled, content, colors.bg.toggleOff, bgOnColor, colors.content.toggleOff, contentOnColor, guiStyle);
		}

		/// <summary>Button that can be toggled on and off</summary>
		public static bool ToggleButton(Rect rect, bool toggled, GUIContent content, Color bgOffColor, Color bgOnColor, Color contentOffColor, Color contenOnColor, GUIStyle guiStyle = null)
		{
			Color backgroundColor = GUI.backgroundColor;
			Color contentColor = GUI.contentColor;
			GUI.backgroundColor = (toggled ? bgOnColor : bgOffColor);
			GUI.contentColor = (toggled ? contenOnColor : contentOffColor);
			if (guiStyle == null)
			{
				guiStyle = styles.button.bBlankBorder;
			}
			if (GUI.Button(rect, content, guiStyle))
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			SetGUIColors(backgroundColor, contentColor, null);
			return toggled;
		}

		/// <summary>Scene field</summary>
		public static UnityEngine.Object SceneField(Rect rect, string label, UnityEngine.Object obj)
		{
			if (obj != null && !obj.ToString().EndsWith(".SceneAsset)"))
			{
				obj = null;
			}
			return EditorGUI.ObjectField(rect, label, obj, typeof(UnityEngine.Object), allowSceneObjects: false);
		}

		/// <summary>
		/// Draws a background grid using the given grid texture
		/// </summary>
		/// <param name="area">Area rect</param>
		/// <param name="offset">Offset from 0, 0 position (used when area has been dragged)</param>
		/// <param name="texture">Texture to use for the grid</param>
		/// <param name="scale">Eventual scale to apply to the grid</param>
		public static void BackgroundGrid(Rect area, Vector2 offset, Texture2D texture, float scale = 1f)
		{
			if (Event.current.type == EventType.Repaint)
			{
				int num = (int)((float)texture.width * scale);
				int num2 = (int)((float)texture.height * scale);
				int num3 = (int)((float)num - offset.x % (float)num);
				if (num3 < 0)
				{
					num3 = num + num3;
				}
				int num4 = (int)((float)num2 - offset.y % (float)num2);
				if (num4 < 0)
				{
					num4 = num2 + num4;
				}
				Rect position = new Rect(area.x - (float)num3, area.yMax, area.width + (float)num3, 0f - (area.height + (float)num4));
				GUI.DrawTextureWithTexCoords(position, texture, new Rect(0f, 0f, position.width / (float)num, position.height / (float)num2));
			}
		}

		/// <summary>
		/// Draws a background grid using default grid textures
		/// </summary>
		/// <param name="area">Area rect</param>
		/// <param name="offset">Offset from 0, 0 position (used when area has been dragged)</param>
		/// <param name="forceDarkSkin">If TRUE forces a dark skin, otherwise uses a skin that fits with the current Unity's one</param>
		/// <param name="scale">Eventual scale to apply to the grid</param>
		public static void BackgroundGrid(Rect area, Vector2 offset, bool forceDarkSkin = false, float scale = 1f)
		{
			Texture2D texture = ((forceDarkSkin || IsProSkin) ? DeStylePalette.grid_dark : DeStylePalette.grid_bright);
			BackgroundGrid(area, offset, texture, scale);
		}

		/// <summary>Box with style and color options</summary>
		public static void Box(Rect rect, Color color, GUIStyle style = null)
		{
			if (style == null)
			{
				style = styles.box.def;
			}
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
			GUI.Box(rect, "", style);
			GUI.backgroundColor = backgroundColor;
		}

		/// <summary>
		/// Can be used instead of EditorGUI.PropertyField, to draw a serializedProperty without its attributes
		/// (very useful in case you want to use this from within a PropertyDrawer for that same property,
		/// since otherwise bad infinite loops might happen)
		/// </summary>
		public static void DefaultPropertyField(Rect position, SerializedProperty property, GUIContent label)
		{
			_miDefaultPropertyField.Invoke(null, new object[3] { position, property, label });
		}

		/// <summary>Draws a colored square</summary>
		public static void DrawColoredSquare(Rect rect, Color color)
		{
			Color color2 = GUI.color;
			GUI.color = color;
			GUI.DrawTexture(rect, DeStylePalette.whiteSquare);
			GUI.color = color2;
		}

		/// <summary>Draws the given texture tiled within the given rect</summary>
		/// <param name="rect">Rect</param>
		/// <param name="texture">Texture</param>
		/// <param name="scale">Eventual scale to apply</param>
		/// <param name="color">If not NULL, colorizes the texture with this color</param>
		public static void DrawTiledTexture(Rect rect, Texture2D texture, float scale = 1f, Color? color = null)
		{
			float num = (float)texture.width * scale;
			float num2 = (float)texture.height * scale;
			if (!color.HasValue)
			{
				GUI.DrawTextureWithTexCoords(rect, texture, new Rect(0f, 0f, rect.width / num, rect.height / num2));
				return;
			}
			Color color2 = GUI.color;
			GUI.color = color.Value;
			GUI.DrawTextureWithTexCoords(rect, texture, new Rect(0f, 0f, rect.width / num, rect.height / num2));
			GUI.color = color2;
		}

		/// <summary>
		/// A text field that becomes editable only on double-click
		/// </summary>
		/// <param name="rect">Area</param>
		/// <param name="editorWindow">EditorWindow reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		public static string DoubleClickTextField(Rect rect, EditorWindow editorWindow, string id, string text, GUIStyle defaultStyle = null, GUIStyle editingStyle = null)
		{
			return DoDoubleClickTextField(rect, isTextArea: false, null, editorWindow, id, text, null, -1, defaultStyle, editingStyle);
		}

		/// <summary>
		/// A text field that becomes editable only on double-click
		/// </summary>
		/// <param name="rect">Area</param>
		/// <param name="editor">Editor reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		public static string DoubleClickTextField(Rect rect, Editor editor, string id, string text, GUIStyle defaultStyle = null, GUIStyle editingStyle = null)
		{
			return DoDoubleClickTextField(rect, isTextArea: false, editor, null, id, text, null, -1, defaultStyle, editingStyle);
		}

		/// <summary>
		/// A text field that becomes editable only on double-click and can also be dragged
		/// </summary>
		/// <param name="rect">Area</param>
		/// <param name="editorWindow">EditorWindow reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="draggableList">List containing the dragged item and all other relative draggable items</param>
		/// <param name="draggedItemIndex">DraggableList index of the item being dragged</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		public static string DoubleClickDraggableTextField(Rect rect, EditorWindow editorWindow, string id, string text, IList draggableList, int draggedItemIndex, GUIStyle defaultStyle = null, GUIStyle editingStyle = null)
		{
			return DoDoubleClickTextField(rect, isTextArea: false, null, editorWindow, id, text, draggableList, draggedItemIndex, defaultStyle, editingStyle);
		}

		/// <summary>
		/// A text field that becomes editable only on double-click and can also be dragged
		/// </summary>
		/// <param name="rect">Area</param>
		/// <param name="editor">Editor reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="draggableList">List containing the dragged item and all other relative draggable items</param>
		/// <param name="draggedItemIndex">DraggableList index of the item being dragged</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		public static string DoubleClickDraggableTextField(Rect rect, Editor editor, string id, string text, IList draggableList, int draggedItemIndex, GUIStyle defaultStyle = null, GUIStyle editingStyle = null)
		{
			return DoDoubleClickTextField(rect, isTextArea: false, editor, null, id, text, draggableList, draggedItemIndex, defaultStyle, editingStyle);
		}

		/// <summary>
		/// A textArea that becomes editable only on double-click
		/// </summary>
		/// <param name="rect">Area</param>
		/// <param name="editorWindow">EditorWindow reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		public static string DoubleClickTextArea(Rect rect, EditorWindow editorWindow, string id, string text, GUIStyle defaultStyle = null, GUIStyle editingStyle = null)
		{
			return DoDoubleClickTextField(rect, isTextArea: true, null, editorWindow, id, text, null, -1, defaultStyle, editingStyle);
		}

		/// <summary>
		/// A textArea that becomes editable only on double-click
		/// </summary>
		/// <param name="rect">Area</param>
		/// <param name="editor">Editor reference</param>
		/// <param name="id">A unique ID to use in order to determine if the text is selected or not</param>
		/// <param name="text">Text</param>
		/// <param name="defaultStyle">Style for default (non-editing mode) appearance</param>
		/// <param name="editingStyle">Style for editing mode</param>
		public static string DoubleClickTextArea(Rect rect, Editor editor, string id, string text, GUIStyle defaultStyle = null, GUIStyle editingStyle = null)
		{
			return DoDoubleClickTextField(rect, isTextArea: true, editor, null, id, text, null, -1, defaultStyle, editingStyle);
		}

		internal static string DoDoubleClickTextField(Rect rect, bool isTextArea, Editor editor, EditorWindow editorWindow, string id, string text, IList draggableList, int draggedItemIndex, GUIStyle defaultStyle = null, GUIStyle editingStyle = null)
		{
			if (defaultStyle == null)
			{
				defaultStyle = EditorStyles.label;
			}
			if (editingStyle == null)
			{
				editingStyle = EditorStyles.textField;
			}
			GUI.SetNextControlName(id);
			bool num = Event.current.rawType == EventType.MouseDown;
			bool flag = num && rect.Contains(Event.current.mousePosition);
			bool flag2 = num && ((_doubleClickTextFieldId == id && !flag) || (_doubleClickTextFieldId != id && flag));
			if (flag && _doubleClickTextFieldId != id)
			{
				if (Event.current.clickCount == 2)
				{
					flag2 = false;
					_doubleClickTextFieldId = id;
					EditorGUI.FocusTextInControl(id);
					if (editor != null)
					{
						editor.Repaint();
					}
					else
					{
						editorWindow.Repaint();
					}
				}
				else if (draggableList != null)
				{
					if (editor != null)
					{
						DeGUIDrag.StartDrag(editor, draggableList, draggedItemIndex);
					}
					else
					{
						DeGUIDrag.StartDrag(editorWindow, draggableList, draggedItemIndex);
					}
				}
			}
			bool flag3 = _doubleClickTextFieldId == id && (Event.current.type != EventType.Layout || GUI.GetNameOfFocusedControl() == id);
			if (!flag3)
			{
				EditorGUIUtility.AddCursorRect(rect, MouseCursor.Arrow);
				if (GUI.GetNameOfFocusedControl() == id)
				{
					GUIUtility.keyboardControl = 0;
				}
			}
			text = (isTextArea ? EditorGUI.TextArea(rect, text, flag3 ? editingStyle : defaultStyle) : EditorGUI.TextField(rect, text, flag3 ? editingStyle : defaultStyle));
			if (!flag2)
			{
				if (!flag3)
				{
					return text;
				}
				bool flag4 = Event.current.type == EventType.MouseDown && !rect.Contains(Event.current.mousePosition);
				if (!flag4 && Event.current.isKey)
				{
					flag4 = (!isTextArea && Event.current.keyCode == KeyCode.Return) || (Event.current.control && Event.current.keyCode == KeyCode.Return) || Event.current.keyCode == KeyCode.Escape;
				}
				if (!flag4)
				{
					goto IL_01ea;
				}
			}
			GUIUtility.keyboardControl = 0;
			_doubleClickTextFieldId = null;
			if (editor != null)
			{
				editor.Repaint();
			}
			else
			{
				editorWindow.Repaint();
			}
			goto IL_01ea;
			IL_01ea:
			return text;
		}

		/// <summary>Divider</summary>
		public static void FlatDivider(Rect rect, Color? color = null)
		{
			Color backgroundColor = GUI.backgroundColor;
			if (color.HasValue)
			{
				GUI.backgroundColor = color.Value;
			}
			GUI.Box(rect, "", styles.box.flat);
			GUI.backgroundColor = backgroundColor;
		}

		/// <summary>Draws a Vector3Field that can have single axes disabled</summary>
		public static Vector2 Vector2FieldAdvanced(Rect rect, GUIContent label, Vector2 value, bool xEnabled, bool yEnabled, bool lockAllToX, bool lockAllToY)
		{
			if (label.HasText())
			{
				rect = EditorGUI.PrefixLabel(rect, label);
			}
			Rect rect2 = rect.SetWidth((rect.width - 2f) / 2f);
			using (new LabelFieldWidthScope(10f))
			{
				for (int i = 0; i < 2; i++)
				{
					bool enabled = GUI.enabled;
					switch (i)
					{
					case 0:
						GUI.enabled = enabled && xEnabled;
						EditorGUI.BeginChangeCheck();
						value.x = EditorGUI.FloatField(rect2, "X", value.x);
						if (EditorGUI.EndChangeCheck() && lockAllToX)
						{
							value.y = value.x;
						}
						break;
					case 1:
						GUI.enabled = enabled && yEnabled;
						EditorGUI.BeginChangeCheck();
						value.y = EditorGUI.FloatField(rect2, "Y", value.y);
						if (EditorGUI.EndChangeCheck() && lockAllToY)
						{
							value.x = value.y;
						}
						break;
					}
					GUI.enabled = enabled;
					rect2 = rect2.Shift(rect2.width + 2f, 0f, 0f, 0f);
				}
				return value;
			}
		}

		/// <summary>Draws a Vector3Field that can have single axes disabled</summary>
		public static Vector3 Vector3FieldAdvanced(Rect rect, GUIContent label, Vector3 value, bool xEnabled, bool yEnabled, bool zEnabled, bool lockAllToX, bool lockAllToY, bool lockAllToZ)
		{
			if (label.HasText())
			{
				rect = EditorGUI.PrefixLabel(rect, label);
			}
			Rect rect2 = rect.SetWidth((rect.width - 4f) / 3f);
			using (new LabelFieldWidthScope(10f))
			{
				for (int i = 0; i < 3; i++)
				{
					bool enabled = GUI.enabled;
					switch (i)
					{
					case 0:
						GUI.enabled = enabled && xEnabled;
						EditorGUI.BeginChangeCheck();
						value.x = EditorGUI.FloatField(rect2, "X", value.x);
						if (EditorGUI.EndChangeCheck() && lockAllToX)
						{
							value.y = (value.z = value.x);
						}
						break;
					case 1:
						GUI.enabled = enabled && yEnabled;
						EditorGUI.BeginChangeCheck();
						value.y = EditorGUI.FloatField(rect2, "Y", value.y);
						if (EditorGUI.EndChangeCheck() && lockAllToY)
						{
							value.x = (value.z = value.y);
						}
						break;
					case 2:
						GUI.enabled = enabled && zEnabled;
						EditorGUI.BeginChangeCheck();
						value.z = EditorGUI.FloatField(rect2, "Z", value.z);
						if (EditorGUI.EndChangeCheck() && lockAllToZ)
						{
							value.x = (value.y = value.z);
						}
						break;
					}
					GUI.enabled = enabled;
					rect2 = rect2.Shift(rect2.width + 2f, 0f, 0f, 0f);
				}
				return value;
			}
		}

		/// <summary>Draws a Vector3Field that can have single axes disabled</summary>
		public static Vector4 Vector4FieldAdvanced(Rect rect, GUIContent label, Vector4 value, bool xEnabled, bool yEnabled, bool zEnabled, bool wEnabled, bool lockAllToX, bool lockAllToY, bool lockAllToZ, bool lockAllToW)
		{
			if (label.HasText())
			{
				rect = EditorGUI.PrefixLabel(rect, label);
			}
			Rect rect2 = rect.SetWidth((rect.width - 6f) / 4f);
			using (new LabelFieldWidthScope(10f))
			{
				for (int i = 0; i < 4; i++)
				{
					bool enabled = GUI.enabled;
					switch (i)
					{
					case 0:
						GUI.enabled = enabled && xEnabled;
						EditorGUI.BeginChangeCheck();
						value.x = EditorGUI.FloatField(rect2, "X", value.x);
						if (EditorGUI.EndChangeCheck() && lockAllToX)
						{
							value.y = (value.z = (value.w = value.x));
						}
						break;
					case 1:
						GUI.enabled = enabled && yEnabled;
						EditorGUI.BeginChangeCheck();
						value.y = EditorGUI.FloatField(rect2, "Y", value.y);
						if (EditorGUI.EndChangeCheck() && lockAllToY)
						{
							value.x = (value.z = (value.w = value.y));
						}
						break;
					case 2:
						GUI.enabled = enabled && zEnabled;
						EditorGUI.BeginChangeCheck();
						value.z = EditorGUI.FloatField(rect2, "Z", value.z);
						if (EditorGUI.EndChangeCheck() && lockAllToZ)
						{
							value.x = (value.y = (value.w = value.z));
						}
						break;
					case 3:
						GUI.enabled = enabled && wEnabled;
						EditorGUI.BeginChangeCheck();
						value.w = EditorGUI.FloatField(rect2, "W", value.w);
						if (EditorGUI.EndChangeCheck() && lockAllToW)
						{
							value.x = (value.y = (value.z = value.w));
						}
						break;
					}
					GUI.enabled = enabled;
					rect2 = rect2.Shift(rect2.width + 2f, 0f, 0f, 0f);
				}
				return value;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiColorField(Rect rect, GUIContent label, string fieldName, IList sources)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources, null, requiresSpecialEndChangeCheck: true))
			{
				multiPropertyScope.value = EditorGUI.ColorField(rect, label, (Color)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiColorFieldAdvanced(Rect rect, GUIContent label, string fieldName, IList sources, bool alphaOnly)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources, null, requiresSpecialEndChangeCheck: true))
			{
				if (alphaOnly)
				{
					Color color = (Color)multiPropertyScope.fieldInfo.GetValue(sources[0]);
					color.a = EditorGUI.Slider(rect, label, ((Color)multiPropertyScope.fieldInfo.GetValue(sources[0])).a, 0f, 1f);
					multiPropertyScope.value = color;
				}
				else
				{
					multiPropertyScope.value = EditorGUI.ColorField(rect, label, (Color)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				}
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiCurveField(Rect rect, GUIContent label, string fieldName, IList sources)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.CurveField(rect, label, (AnimationCurve)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				if (multiPropertyScope.hasMixedValue)
				{
					DrawTiledTexture(rect.Contract(2f), DeStylePalette.tileBars_empty, 0.25f, new Color(0.97f, 0.81f, 0.02f, 0.5f));
				}
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values. Supports using an int as an enum</summary>
		public static bool MultiEnumPopup<T>(Rect rect, GUIContent label, string fieldName, IList sources) where T : Enum
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				bool num = multiPropertyScope.fieldInfo.FieldType == typeof(int);
				multiPropertyScope.value = EditorGUI.EnumPopup(rect, label, (T)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				if (num)
				{
					multiPropertyScope.value = (int)multiPropertyScope.value;
				}
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values. Supports using an int as an enum</summary>
		public static bool MultiEnumPopup(Rect rect, GUIContent label, Type enumType, string fieldName, IList sources)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				bool num = multiPropertyScope.fieldInfo.FieldType == typeof(int);
				object obj = Enum.Parse(enumType, multiPropertyScope.fieldInfo.GetValue(sources[0]).ToString());
				multiPropertyScope.value = EditorGUI.EnumPopup(rect, label, obj as Enum);
				if (num)
				{
					multiPropertyScope.value = (int)multiPropertyScope.value;
				}
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiFloatField(Rect rect, GUIContent label, string fieldName, IList sources, float? min = null, float? max = null)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				EditorGUI.BeginChangeCheck();
				multiPropertyScope.value = EditorGUI.FloatField(rect, label, (float)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				if (EditorGUI.EndChangeCheck())
				{
					if (min.HasValue && (float)multiPropertyScope.value < min.Value)
					{
						multiPropertyScope.value = min;
					}
					else if (max.HasValue && (float)multiPropertyScope.value > max.Value)
					{
						multiPropertyScope.value = max;
					}
				}
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values. Supports also uint fields</summary>
		public static bool MultiIntField(Rect rect, GUIContent label, string fieldName, IList sources, int? min = null, int? max = null)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				bool flag = multiPropertyScope.fieldInfo.FieldType == typeof(uint);
				bool flag2 = !flag && multiPropertyScope.fieldInfo.FieldType == typeof(float);
				EditorGUI.BeginChangeCheck();
				multiPropertyScope.value = EditorGUI.IntField(rect, label, flag ? ((int)(uint)multiPropertyScope.fieldInfo.GetValue(sources[0])) : (flag2 ? ((int)(float)multiPropertyScope.fieldInfo.GetValue(sources[0])) : ((int)multiPropertyScope.fieldInfo.GetValue(sources[0]))));
				if (EditorGUI.EndChangeCheck())
				{
					if (min.HasValue && (int)multiPropertyScope.value < min.Value)
					{
						multiPropertyScope.value = min;
					}
					else if (max.HasValue && (int)multiPropertyScope.value > max.Value)
					{
						multiPropertyScope.value = max;
					}
					if (flag)
					{
						if ((int)multiPropertyScope.value < 0)
						{
							multiPropertyScope.value = 0u;
						}
						else
						{
							multiPropertyScope.value = (uint)(int)multiPropertyScope.value;
						}
					}
					else if (flag2)
					{
						multiPropertyScope.value = (float)(int)multiPropertyScope.value;
					}
				}
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiIntSlider(Rect rect, GUIContent label, string fieldName, IList sources, int min, int max)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.IntSlider(rect, label, (int)multiPropertyScope.fieldInfo.GetValue(sources[0]), min, max);
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values. Auto-determines object type from the field's type</summary>
		public static bool MultiObjectField(Rect rect, GUIContent label, string fieldName, IList sources, bool allowSceneObjects)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.ObjectField(rect, label, (UnityEngine.Object)multiPropertyScope.fieldInfo.GetValue(sources[0]), multiPropertyScope.fieldInfo.FieldType, allowSceneObjects);
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values. Forces field to accept only objects of the given type</summary>
		public static bool MultiObjectField(Rect rect, GUIContent label, string fieldName, IList sources, Type objType, bool allowSceneObjects)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.ObjectField(rect, label, (UnityEngine.Object)multiPropertyScope.fieldInfo.GetValue(sources[0]), objType, allowSceneObjects);
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiRectField(Rect rect, GUIContent label, string fieldName, IList sources)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.RectField(rect, label, (Rect)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiSlider(Rect rect, GUIContent label, string fieldName, IList sources, float min, float max)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.Slider(rect, label, (float)multiPropertyScope.fieldInfo.GetValue(sources[0]), min, max);
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiTextArea(Rect rect, string fieldName, IList sources)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.TextArea(rect, (string)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiTextField(Rect rect, GUIContent label, string fieldName, IList sources)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.TextField(rect, label, (string)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values. Supports passing int values as bool (1 = true, 0 = false)</summary>
		public static bool MultiToggleButton(Rect rect, GUIContent label, string fieldName, IList sources, GUIStyle guiStyle = null)
		{
			return MultiToggleButton(rect, null, label, fieldName, sources, null, null, null, null, guiStyle);
		}

		/// <summary>Returns TRUE if there's mixed values. Supports passing int values as bool (1 = true, 0 = false)</summary>
		public static bool MultiToggleButton(Rect rect, GUIContent label, string fieldName, IList sources, Color? bgOffColor, Color? bgOnColor = null, Color? contentOffColor = null, Color? contenOnColor = null, GUIStyle guiStyle = null)
		{
			return MultiToggleButton(rect, null, label, fieldName, sources, bgOffColor, bgOnColor, contentOffColor, contenOnColor, guiStyle);
		}

		/// <summary>Returns TRUE if there's mixed values. Supports passing int values as bool (1 = true, 0 = false)</summary>
		public static bool MultiToggleButton(Rect rect, bool? forceSetToggle, GUIContent label, string fieldName, IList sources, Color? bgOffColor, Color? bgOnColor = null, Color? contentOffColor = null, Color? contenOnColor = null, GUIStyle guiStyle = null)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				bool flag = multiPropertyScope.fieldInfo.FieldType == typeof(int);
				multiPropertyScope.value = ToggleButton(rect, forceSetToggle.HasValue ? forceSetToggle.Value : (flag ? ((int)multiPropertyScope.fieldInfo.GetValue(sources[0]) == 1) : ((bool)multiPropertyScope.fieldInfo.GetValue(sources[0]))), multiPropertyScope.hasMixedValue ? MixedValueLabel : label, (!bgOffColor.HasValue) ? ((Color)colors.bg.toggleOff) : bgOffColor.Value, (!bgOnColor.HasValue) ? ((Color)colors.bg.toggleOn) : bgOnColor.Value, (!contentOffColor.HasValue) ? ((Color)colors.content.toggleOff) : contentOffColor.Value, (!contenOnColor.HasValue) ? ((Color)colors.content.toggleOn) : contenOnColor.Value, guiStyle);
				if (flag)
				{
					multiPropertyScope.value = (((bool)multiPropertyScope.value) ? 1 : 0);
				}
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values. Requires a SerializedProperty representation of each UnityEven field</summary>
		public static bool MultiUnityEvent(Rect rect, GUIContent label, string fieldName, IList sources, List<SerializedProperty> fieldsAsSerializedProperties)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources, fieldsAsSerializedProperties))
			{
				EditorGUI.BeginDisabledGroup(multiPropertyScope.hasMixedValue);
				if (label.HasText())
				{
					EditorGUI.PropertyField(rect, fieldsAsSerializedProperties[0], label);
				}
				else
				{
					EditorGUI.PropertyField(rect, fieldsAsSerializedProperties[0]);
				}
				EditorGUI.EndDisabledGroup();
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector2Field(Rect rect, GUIContent label, string fieldName, IList sources)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.Vector2Field(rect, label, (Vector2)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector3Field(Rect rect, GUIContent label, string fieldName, IList sources)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.Vector3Field(rect, label, (Vector3)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector4Field(Rect rect, GUIContent label, string fieldName, IList sources)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = EditorGUI.Vector4Field(rect, label.text, (Vector4)multiPropertyScope.fieldInfo.GetValue(sources[0]));
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector2FieldAdvanced(Rect rect, GUIContent label, string fieldName, IList sources, bool xEnabled, bool yEnabled, bool lockAllToX, bool lockAllToY)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = Vector2FieldAdvanced(rect, label, (Vector2)multiPropertyScope.fieldInfo.GetValue(sources[0]), xEnabled, yEnabled, lockAllToX, lockAllToY);
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector3FieldAdvanced(Rect rect, GUIContent label, string fieldName, IList sources, bool xEnabled, bool yEnabled, bool zEnabled, bool lockAllToX, bool lockAllToY, bool lockAllToZ)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = Vector3FieldAdvanced(rect, label, (Vector3)multiPropertyScope.fieldInfo.GetValue(sources[0]), xEnabled, yEnabled, zEnabled, lockAllToX, lockAllToY, lockAllToZ);
				return multiPropertyScope.hasMixedValue;
			}
		}

		/// <summary>Returns TRUE if there's mixed values</summary>
		public static bool MultiVector4FieldAdvanced(Rect rect, GUIContent label, string fieldName, IList sources, bool xEnabled, bool yEnabled, bool zEnabled, bool wEnabled, bool lockAllToX, bool lockAllToY, bool lockAllToZ, bool lockAllToW)
		{
			using (MultiPropertyScope multiPropertyScope = new MultiPropertyScope(fieldName, sources))
			{
				multiPropertyScope.value = Vector4FieldAdvanced(rect, label, (Vector4)multiPropertyScope.fieldInfo.GetValue(sources[0]), xEnabled, yEnabled, zEnabled, wEnabled, lockAllToX, lockAllToY, lockAllToZ, lockAllToW);
				return multiPropertyScope.hasMixedValue;
			}
		}
	}
}
