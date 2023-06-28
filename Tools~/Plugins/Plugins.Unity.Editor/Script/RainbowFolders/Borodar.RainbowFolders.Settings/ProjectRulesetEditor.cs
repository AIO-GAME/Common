using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Borodar.RainbowCore;
using Borodar.RainbowCore.RList.Editor;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowFolders.Settings
{
	[CustomEditor(typeof(ProjectRuleset))]
	public class ProjectRulesetEditor : Editor
	{
		private enum Filter
		{
			All,
			Name,
			Path
		}

		public static readonly HashSet<ProjectRulesetEditor> EDITORS = new HashSet<ProjectRulesetEditor>();

		private const string PROP_NAME_FOLDERS = "Rules";

		private const string NEGATIVE_LOOKAHEAD = "(?!.*)";

		private SerializedProperty _foldersProperty;

		private ReorderableList _reorderableList;

		private string _query = string.Empty;

		private Enum _filter = Filter.All;

		private bool _matchCase;

		private bool _useRegex;

		private string _warningMessage;

		public DefaultAsset Asset { get; set; }

		public int SearchTab { get; set; }

		public bool ForceUpdate { get; set; }

		protected void OnEnable()
		{
			EDITORS.Add(this);
			_foldersProperty = base.serializedObject.FindProperty("Rules");
			_reorderableList = new ReorderableList(_foldersProperty)
			{
				label = new GUIContent("Search Results"),
				elementDisplayType = ReorderableList.ElementDisplayType.SingleLine,
				expandable = false,
				headerHeight = 4f,
				paginate = true,
				pageSize = 10
			};
			_reorderableList.onChangedCallback += delegate
			{
				OnRulesetChange();
			};
			Undo.undoRedoPerformed = (Undo.UndoRedoCallback)Delegate.Remove(Undo.undoRedoPerformed, new Undo.UndoRedoCallback(OnRulesetChange));
			Undo.undoRedoPerformed = (Undo.UndoRedoCallback)Delegate.Combine(Undo.undoRedoPerformed, new Undo.UndoRedoCallback(OnRulesetChange));
		}

		protected void OnDisable()
		{
			EDITORS.Remove(this);
			ClearHiddenFlags();
			Undo.undoRedoPerformed = (Undo.UndoRedoCallback)Delegate.Remove(Undo.undoRedoPerformed, new Undo.UndoRedoCallback(OnRulesetChange));
		}

		public override void OnInspectorGUI()
		{
			GUILayout.Space(6f);
			int searchTab = SearchTab;
			SearchTab = GUILayout.Toolbar(SearchTab, new string[2] { "Filter by folder", "Filter by key" });
			ForceUpdate |= SearchTab != searchTab;
			EditorGUILayout.BeginVertical("AvatarMappingBox");
			GUILayout.Space(6f);
			switch (SearchTab)
			{
			case 0:
				DrawSearchByFolderPanel(ForceUpdate);
				break;
			case 1:
				DrawSearchByKeyPanel(ForceUpdate);
				break;
			default:
				throw new ArgumentOutOfRangeException("SearchTab", SearchTab, null);
			}
			if (!string.IsNullOrEmpty(_warningMessage))
			{
				EditorGUILayout.HelpBox(_warningMessage, MessageType.Warning);
			}
			GUILayout.Space(4f);
			EditorGUILayout.EndVertical();
			GUILayout.Space(2f);
			base.serializedObject.Update();
			DrawReorderableList();
			ForceUpdate = false;
		}

		private static void OnRulesetChange()
		{
			ProjectRuleset.OnRulesetChange();
		}

		private void DrawSearchByFolderPanel(bool forceUpdate)
		{
			DefaultAsset asset = Asset;
			Asset = (DefaultAsset)EditorGUILayout.ObjectField(Asset, typeof(DefaultAsset), false);
			if (forceUpdate || !(Asset == asset))
			{
				if (Asset == null)
				{
					ClearHiddenFlags();
				}
				else
				{
					ApplyHiddenFlagsByAsset();
				}
			}
		}

		private void DrawSearchByKeyPanel(bool forceUpdate)
		{
			EditorGUILayout.BeginHorizontal();
			bool flag = CoreEditorUtility.SearchField(ref _query, ref _filter, Filter.All);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			if (!object.Equals(_filter, Filter.All))
			{
				Rect rect = GUILayoutUtility.GetRect(GUIContent.none, "MiniLabel");
				rect.y += 1f;
				rect.width = 55f;
				GUI.Label(rect, $"➔ {_filter}", "MiniLabel");
			}
			GUILayout.FlexibleSpace();
			bool matchCase = _matchCase;
			_matchCase = EditorGUILayout.ToggleLeft("Match case", _matchCase, "MiniLabel", GUILayout.Width(83f));
			bool flag2 = _matchCase != matchCase;
			bool useRegex = _useRegex;
			_useRegex = EditorGUILayout.ToggleLeft("Regex", _useRegex, "MiniLabel", GUILayout.Width(58f));
			bool flag3 = _useRegex != useRegex;
			EditorGUILayout.EndHorizontal();
			if (forceUpdate || flag || flag2 || flag3)
			{
				_warningMessage = string.Empty;
				ApplyFilters();
			}
		}

		private void ApplyFilters()
		{
			bool flag = object.Equals(Filter.All, _filter);
			if (string.IsNullOrEmpty(_query) && flag)
			{
				ClearHiddenFlags();
			}
			else
			{
				ApplyHiddenFlagsByKey();
			}
		}

		private void ClearHiddenFlags()
		{
			if (_foldersProperty != null)
			{
				for (int i = 0; i < _foldersProperty.arraySize; i++)
				{
					_foldersProperty.GetArrayElementAtIndex(i).FindPropertyRelative("IsHidden").boolValue = false;
				}
				_foldersProperty.serializedObject.ApplyModifiedProperties();
				_reorderableList.canAdd = true;
				_reorderableList.headerHeight = 4f;
				_reorderableList.paginate = true;
			}
		}

		private void ApplyHiddenFlagsByAsset()
		{
			string assetPath = AssetDatabase.GetAssetPath(Asset);
			string fileName = Path.GetFileName(assetPath);
			foreach (ProjectRule rule in ((ProjectRuleset)base.target).Rules)
			{
				bool flag;
				switch (rule.Type)
				{
				case ProjectRule.KeyType.Name:
					flag = rule.Key.Equals(fileName) || (rule.IsRecursive() && assetPath.Contains(string.Concat("/", rule.Key, "/")));
					break;
				case ProjectRule.KeyType.Path:
					flag = rule.Key.Equals(assetPath) || (rule.IsRecursive() && assetPath.StartsWith(string.Concat(rule.Key, "/")));
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				rule.IsHidden = !flag;
			}
			_reorderableList.canAdd = false;
			_reorderableList.headerHeight = 18f;
			_reorderableList.paginate = false;
		}

		private void ApplyHiddenFlagsByKey()
		{
			Regex regex = (_useRegex ? MakeRegexFromQuery() : null);
			for (int i = 0; i < _foldersProperty.arraySize; i++)
			{
				SerializedProperty arrayElementAtIndex = _foldersProperty.GetArrayElementAtIndex(i);
				SerializedProperty serializedProperty = arrayElementAtIndex.FindPropertyRelative("IsHidden");
				Enum filter = _filter;
				if (filter is Filter)
				{
					switch ((Filter)(object)filter)
					{
					case Filter.All:
						serializedProperty.boolValue = !KeyContainsQuery(arrayElementAtIndex, regex);
						continue;
					case Filter.Name:
						serializedProperty.boolValue = !KeyHasSameType(arrayElementAtIndex, ProjectRule.KeyType.Name) || !KeyContainsQuery(arrayElementAtIndex, regex);
						continue;
					case Filter.Path:
						serializedProperty.boolValue = !KeyHasSameType(arrayElementAtIndex, ProjectRule.KeyType.Path) || !KeyContainsQuery(arrayElementAtIndex, regex);
						continue;
					}
				}
				throw new ArgumentOutOfRangeException("_filter", _filter, null);
			}
			_foldersProperty.serializedObject.ApplyModifiedProperties();
			_reorderableList.canAdd = false;
			_reorderableList.headerHeight = 18f;
			_reorderableList.paginate = false;
		}

		private Regex MakeRegexFromQuery()
		{
			RegexOptions options = ((!_matchCase) ? RegexOptions.IgnoreCase : RegexOptions.None);
			try
			{
				return new Regex(_query, options);
			}
			catch (ArgumentException ex)
			{
				_warningMessage = ex.Message;
				return new Regex("(?!.*)");
			}
		}

		private static bool KeyHasSameType(SerializedProperty item, ProjectRule.KeyType keyType)
		{
			return item.FindPropertyRelative("Type").enumValueIndex == (int)keyType;
		}

		private bool KeyContainsQuery(SerializedProperty item, Regex regex)
		{
			string stringValue = item.FindPropertyRelative("Key").stringValue;
			if (_useRegex)
			{
				return regex.Match(stringValue).Success;
			}
			StringComparison comparisonType = (_matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
			return stringValue.IndexOf(_query, comparisonType) >= 0;
		}

		private void DrawReorderableList()
		{
			EditorGUI.BeginChangeCheck();
			_reorderableList.DoLayoutList();
			if (EditorGUI.EndChangeCheck())
			{
				ProjectRuleset.OnRulesetChange();
				base.serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
