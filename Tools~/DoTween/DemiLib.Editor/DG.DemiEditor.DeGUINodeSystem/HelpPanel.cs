using System;
using System.Collections.Generic;
using System.Text;
using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
	/// <summary>
	/// You can attach to this
	/// </summary>
	public class HelpPanel
	{
		public struct ProjectNotes
		{
			/// <summary>Regular note</summary>
			public string note;

			/// <summary>Editable note (activated by setting <see cref="F:DG.DemiEditor.DeGUINodeSystem.HelpPanel.ProjectNotes.allowEditableNote" /> to TRUE
			/// (but you will have to save the result somewhere yourself)</summary>
			public string editableNote;

			/// <summary>If TRUE shows the <see cref="F:DG.DemiEditor.DeGUINodeSystem.HelpPanel.ProjectNotes.editableNote" /> textArea</summary>
			public bool allowEditableNote;
		}

		public class ContentGroup
		{
			internal readonly string title;

			internal readonly string description;

			internal readonly List<Definition> definitions = new List<Definition>();

			private bool _parsed;

			internal ContentGroup(string title, string description)
			{
				this.title = title;
				if (description != null)
				{
					description = description.Replace("[b]", "<color=#eeeeee><b>");
					description = description.Replace("[/b]", "</b></color>");
					this.description = description;
				}
			}

			/// <summary>
			/// Add definition. Supports rich-text but also these special tags:<para />
			/// - [b][/b]
			/// </summary>
			public Definition AppendDefinition(string value)
			{
				Definition definition = new Definition(value);
				definitions.Add(definition);
				return definition;
			}

			internal void Parse()
			{
				if (_parsed)
				{
					return;
				}
				_parsed = true;
				foreach (Definition definition in definitions)
				{
					definition.Parse();
				}
			}
		}

		public class Definition
		{
			internal readonly string definition;

			internal string keys;

			internal Definition(string value)
			{
				definition = value.Replace("[b]", "<color=#eeeeee><b>");
				definition = definition.Replace("[/b]", "</b></color>");
				definition = definition.Replace("[e]", "<color=#bcfdf6>");
				definition = definition.Replace("[/e]", "</color>");
				keys = "";
			}

			/// <summary>
			/// Add key, automatically formatting these special keys:<para />
			/// /<para />
			/// +<para />
			/// →
			/// </summary>
			/// <param name="newLine">If TRUE and there's other keys/targets, adds the new key on a new line preceded by a comma</param>
			public Definition AddKey(string value, bool newLine = true)
			{
				if (string.IsNullOrEmpty(keys))
				{
					keys = value;
				}
				else
				{
					keys = string.Concat(keys, newLine ? ",\n" : "", value);
				}
				return this;
			}

			public Definition AddKeyTarget(string value, bool addSpace = true)
			{
				keys = string.Format("{0}{1}<color=#ffffff>{2}</color>", keys, addSpace ? " " : "", value);
				return this;
			}

			internal void Parse()
			{
				if (string.IsNullOrEmpty(keys))
				{
					return;
				}
				_Strb.Length = 0;
				for (int i = 0; i < keys.Length; i++)
				{
					char c = keys[i];
					if (c != '\\' || (i != 0 && keys[i - 1] == '\\'))
					{
						if (c == '/' && (i == 0 || (keys[i - 1] != '\\' && keys[i - 1] != '<')))
						{
							_Strb.Append("<color=#ffffff>/</color>");
						}
						else if (c == '+' && (i == 0 || keys[i - 1] != '\\'))
						{
							_Strb.Append("<color=#ffffff>+</color>");
						}
						else if (c == '→' && (i == 0 || keys[i - 1] != '\\'))
						{
							_Strb.Append("<color=#ffffff>→</color>");
						}
						else if (c == ',' && (i == 0 || keys[i - 1] != '\\'))
						{
							_Strb.Append("<color=#ffffff>,</color>");
						}
						else
						{
							_Strb.Append(c);
						}
					}
				}
				keys = _Strb.ToString();
			}
		}

		private class Styles
		{
			public GUIStyle rowBox;

			public GUIStyle titleLabel;

			public GUIStyle groupTitleLabel;

			public GUIStyle descriptionLabel;

			public GUIStyle definitionLabel;

			public GUIStyle keysLabel;

			public GUIStyle projectNotes;

			public GUIStyle editableProjectNotes;

			public GUIStyle editableProjectNotesAsLabel;

			private bool _initialized;

			public void Init()
			{
				if (!_initialized)
				{
					_initialized = true;
					rowBox = new GUIStyle().Margin(0).Padding(0).Background(DeStylePalette.whiteSquare)
						.StretchWidth();
					titleLabel = new GUIStyle(GUI.skin.label).Add(16, Color.white, Format.RichText).Background(DeStylePalette.blueSquare).Padding(14, 14, 4, 4)
						.Margin(0)
						.StretchWidth();
					groupTitleLabel = new GUIStyle(GUI.skin.label).Add(Format.WordWrap, Format.RichText, Color.white).Background(DeStylePalette.blueSquare).Padding(14, 4, 8, 4)
						.Margin(0)
						.StretchWidth();
					descriptionLabel = new GUIStyle(GUI.skin.label).Add(Format.WordWrap, Format.RichText, _DescriptionColor).Background(DeStylePalette.whiteSquare).Padding(14, 4, 4, 4)
						.Margin(0)
						.StretchWidth();
					definitionLabel = new GUIStyle(GUI.skin.label).Add(Format.WordWrap, Format.RichText, new DeSkinColor(0.75f)).Padding(14, 4, 4, 4).Margin(0);
					keysLabel = new GUIStyle(GUI.skin.label).Add(Format.WordWrap, Format.RichText, _KeysColor).Padding(0, 14, 4, 4).Margin(0);
					projectNotes = DeGUI.styles.label.wordwrap.Clone(Color.white).Margin(0).Padding(14, 14, 4, 4)
						.Background(DeStylePalette.blackSquare)
						.StretchWidth();
					editableProjectNotes = EditorStyles.textArea.Clone(new Color(0.18f, 0.18f, 0.18f), Format.WordWrap).Margin(0).Padding(14, 14, 8, 8)
						.Background(DeStylePalette.whiteSquare);
					editableProjectNotesAsLabel = editableProjectNotes.Clone(Format.RichText, new DeSkinColor(0.95f)).Background(DeStylePalette.blackSquare);
				}
			}
		}

		public ProjectNotes notes;

		private const int _InnerPadding = 14;

		private static readonly Styles _Styles = new Styles();

		private static readonly Color _EvidenceColor = new Color(0.05490196f, 0.5960785f, 0.9725491f, 1f);

		private static readonly Color _DescriptionColor = new Color(0.5998054f, 0.7537874f, 129f / 136f, 1f);

		private static readonly Color _DescriptionBGColor = new Color(0.1386786f, 0.2625088f, 0.4191176f, 1f);

		private static readonly Color _KeysColor = new Color(1f, 0.8068966f, 0f, 1f);

		private static readonly Color _RowColor0 = new Color(0.08f, 0.08f, 0.08f, 0.94f);

		private static readonly Color _RowColor1 = new Color(0.16f, 0.16f, 0.16f, 0.94f);

		private NodeProcess _process;

		private readonly List<ContentGroup> _ContentGroups = new List<ContentGroup>();

		private bool _layoutReady;

		private string _editableTextAreaId;

		private Vector2 _scroll;

		private static readonly StringBuilder _Strb = new StringBuilder();

		public bool isOpen { get; private set; }

		public event Action<string> OnEditableNoteChanged;

		private void Dispatch_OnEditableNoteChanged(string changedNote)
		{
			if (this.OnEditableNoteChanged != null)
			{
				this.OnEditableNoteChanged(changedNote);
			}
		}

		public HelpPanel(NodeProcess nodeProcess)
		{
			_process = nodeProcess;
			_editableTextAreaId = string.Concat("HelpPanel_", UnityEngine.Random.Range(1, int.MaxValue).ToString());
			ContentGroup contentGroup = AddContentGroup("General");
			contentGroup.AppendDefinition("Open/Close [b]Help Panel[/b]").AddKey("F1");
			contentGroup.AppendDefinition("Pan area").AddKey("MMB → Drag");
			contentGroup.AppendDefinition("Zoom in/out (if allowed)").AddKey("CTRL+Scrollwheel");
			contentGroup.AppendDefinition("Show extra UI buttons").AddKey("ALT");
			contentGroup.AppendDefinition("Background context menu").AddKey("RMB");
			ContentGroup contentGroup2 = AddContentGroup("Selection");
			contentGroup2.AppendDefinition("Select all nodes").AddKey("CTRL+A");
			contentGroup2.AppendDefinition("Draw selection rect").AddKey("LMB → Drag").AddKeyTarget("on background");
			contentGroup2.AppendDefinition("Draw selection rect (add)").AddKey("SHIFT+LMB → Drag").AddKeyTarget("on background");
			contentGroup2.AppendDefinition("Add/Remove node from selection").AddKey("SHIFT+LMB").AddKeyTarget("on node");
			contentGroup2.AppendDefinition("Add node plus all forward connected nodes to selection").AddKey("SHIFT+ALT+LMB").AddKeyTarget("on node");
			ContentGroup contentGroup3 = AddContentGroup("Nodes Manipulation");
			contentGroup3.AppendDefinition("Delete selected nodes").AddKey("DELETE").AddKey("BACKSPACE");
			contentGroup3.AppendDefinition("Copy selected nodes").AddKey("CTRL+C");
			contentGroup3.AppendDefinition("Cut selected nodes").AddKey("CTRL+X");
			contentGroup3.AppendDefinition("Paste nodes").AddKey("CTRL+V");
			contentGroup3.AppendDefinition("Move selected nodes by 1 pixel").AddKey("ARROWS");
			contentGroup3.AppendDefinition("Move selected nodes by 10 pixel").AddKey("SHIFT+ARROWS");
			contentGroup3.AppendDefinition("Align and arrange selected nodes vertically").AddKey("CTRL+SHIFT+LEFT");
			contentGroup3.AppendDefinition("Align and arrange selected nodes horizontally").AddKey("CTRL+SHIFT+UP");
			contentGroup3.AppendDefinition("Disable snapping while dragging nodes").AddKey("ALT");
			contentGroup3.AppendDefinition("Drag connection from node (if allowed)").AddKey("CTRL+LMB → Drag");
			contentGroup3.AppendDefinition("Drag alternative connection from node (if allowed)").AddKey("CTRL+SPACE+LMB → Drag");
			contentGroup3.AppendDefinition("Clone selected nodes and drag them").AddKey("SHIFT+CTRL+LMB → Drag");
			contentGroup3.AppendDefinition("Node context menu").AddKey("RMB");
		}

		public bool Draw()
		{
			if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.F1)
			{
				Event.current.Use();
				return false;
			}
			if (!_layoutReady)
			{
				if (Event.current.type != EventType.Layout)
				{
					return true;
				}
				_layoutReady = true;
			}
			_Styles.Init();
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.TRS(_process.position.min - _process.position.min / _process.guiScale, Quaternion.identity, Vector3.one);
			Rect position = new Rect(_process.relativeArea.x, _process.relativeArea.y, _process.relativeArea.width * _process.guiScale, _process.relativeArea.height * _process.guiScale);
			using (new DeGUI.ColorScope(null, null, new Color(0.14f, 0.14f, 0.14f, 0.6f)))
			{
				GUI.DrawTexture(position, DeStylePalette.whiteSquare);
			}
			_scroll = GUILayout.BeginScrollView(_scroll);
			GUILayout.Label("Help Panel", _Styles.titleLabel);
			DeGUILayout.HorizontalDivider(_EvidenceColor, 1, 0, 0);
			if (notes.allowEditableNote || !string.IsNullOrEmpty(notes.note))
			{
				if (!string.IsNullOrEmpty(notes.note))
				{
					GUILayout.Label(notes.note, _Styles.projectNotes);
				}
				if (notes.allowEditableNote)
				{
					EditorGUI.BeginChangeCheck();
					bool flag = GUI.GetNameOfFocusedControl() == _editableTextAreaId;
					Rect rect = GUILayoutUtility.GetRect(new GUIContent(notes.editableNote), flag ? _Styles.editableProjectNotes : _Styles.editableProjectNotesAsLabel);
					using (new DeGUI.ColorScope(flag ? new Color(0.89f, 0.89f, 0.89f) : Color.white))
					{
						using (new DeGUI.CursorColorScope(flag ? Color.black : Color.white))
						{
							notes.editableNote = DeGUI.DoubleClickTextArea(rect, _process.editor, _editableTextAreaId, notes.editableNote, _Styles.editableProjectNotesAsLabel, _Styles.editableProjectNotes);
						}
					}
					if (EditorGUI.EndChangeCheck())
					{
						Dispatch_OnEditableNoteChanged(notes.editableNote);
					}
				}
			}
			int num = (int)Mathf.Min(position.width * 0.6f, 350f);
			foreach (ContentGroup contentGroup in _ContentGroups)
			{
				contentGroup.Parse();
				GUILayout.Label(contentGroup.title, _Styles.groupTitleLabel);
				if (!string.IsNullOrEmpty(contentGroup.description))
				{
					using (new DeGUI.ColorScope(_DescriptionBGColor))
					{
						GUILayout.Label(contentGroup.description, _Styles.descriptionLabel);
					}
				}
				for (int i = 0; i < contentGroup.definitions.Count; i++)
				{
					Definition definition = contentGroup.definitions[i];
					using (new DeGUI.ColorScope((i % 2 == 0) ? _RowColor0 : _RowColor1))
					{
						GUILayout.BeginHorizontal(_Styles.rowBox);
					}
					if (string.IsNullOrEmpty(definition.keys))
					{
						GUILayout.Label(definition.definition, _Styles.definitionLabel);
					}
					else
					{
						GUILayout.Label(definition.definition, _Styles.definitionLabel, GUILayout.Width(num));
						GUILayout.Label(definition.keys, _Styles.keysLabel);
					}
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndScrollView();
			GUI.matrix = matrix;
			return true;
		}

		/// <summary>
		/// Use this to add a content group to the Help Panel
		/// </summary>
		public ContentGroup AddContentGroup(string title, string description = null)
		{
			ContentGroup contentGroup = new ContentGroup(title, description);
			_ContentGroups.Add(contentGroup);
			return contentGroup;
		}

		internal void Open(bool doOpen)
		{
			isOpen = doOpen;
			if (doOpen)
			{
				_layoutReady = false;
			}
		}
	}
}
