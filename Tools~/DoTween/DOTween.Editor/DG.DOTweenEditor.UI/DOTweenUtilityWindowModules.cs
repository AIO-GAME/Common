using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DG.Tweening.Core;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor.UI
{
	public static class DOTweenUtilityWindowModules
	{
		private class ModuleInfo
		{
			public bool enabled;

			public string filePath;

			public readonly string id;

			public ModuleInfo(string filePath, string id)
			{
				this.filePath = filePath;
				this.id = id;
			}
		}

		private const string ModuleMarkerId = "MODULE_MARKER";

		private static readonly ModuleInfo _audioModule;

		private static readonly ModuleInfo _physicsModule;

		private static readonly ModuleInfo _physics2DModule;

		private static readonly ModuleInfo _spriteModule;

		private static readonly ModuleInfo _uiModule;

		private static readonly ModuleInfo _textMeshProModule;

		private static readonly ModuleInfo _tk2DModule;

		private static readonly ModuleInfo _deAudioModule;

		private static readonly ModuleInfo _deUnityExtendedModule;

		private static readonly string[] _ModuleDependentFiles;

		private static EditorWindow _editor;

		private static DOTweenSettings _src;

		private static bool _refreshed;

		private static bool _isWaitingForCompilation;

		private static readonly List<int> _LinesToChange;

		static DOTweenUtilityWindowModules()
		{
			_audioModule = new ModuleInfo("Modules/DOTweenModuleAudio.cs", "AUDIO");
			_physicsModule = new ModuleInfo("Modules/DOTweenModulePhysics.cs", "PHYSICS");
			_physics2DModule = new ModuleInfo("Modules/DOTweenModulePhysics2D.cs", "PHYSICS2D");
			_spriteModule = new ModuleInfo("Modules/DOTweenModuleSprite.cs", "SPRITE");
			_uiModule = new ModuleInfo("Modules/DOTweenModuleUI.cs", "UI");
			_textMeshProModule = new ModuleInfo("DOTweenTextMeshPro.cs", "TEXTMESHPRO");
			_tk2DModule = new ModuleInfo("DOTweenTk2D.cs", "TK2D");
			_deAudioModule = new ModuleInfo("DOTweenDeAudio.cs", "DEAUDIO");
			_deUnityExtendedModule = new ModuleInfo("DOTweenDeUnityExtended.cs", "DEUNITYEXTENDED");
			_ModuleDependentFiles = new string[7] { "DOTWEENDIR/Modules/DOTweenModuleUtils.cs", "DOTWEENPRODIR/DOTweenAnimation.cs", "DOTWEENPRODIR/DOTweenProShortcuts.cs", "DOTWEENPRODIR/Editor/DOTweenAnimationInspector.cs", "DOTWEENTIMELINEDIR/Scripts/Core/Plugins/DefaultActionPlugins.cs", "DOTWEENTIMELINEDIR/Scripts/Core/Plugins/DefaultTweenPlugins.cs", "DOTWEENTIMELINEDIR/Scripts/Core/Plugins/OptionalPlugins.cs" };
			_LinesToChange = new List<int>();
			for (int i = 0; i < _ModuleDependentFiles.Length; i++)
			{
				_ModuleDependentFiles[i] = _ModuleDependentFiles[i].Replace("DOTWEENDIR/", EditorUtils.dotweenDir);
				_ModuleDependentFiles[i] = _ModuleDependentFiles[i].Replace("DOTWEENPRODIR/", EditorUtils.dotweenProDir);
				_ModuleDependentFiles[i] = _ModuleDependentFiles[i].Replace("DOTWEENTIMELINEDIR/", EditorUtils.dotweenTimelineDir);
			}
			_audioModule.filePath = string.Concat(EditorUtils.dotweenDir, _audioModule.filePath);
			_physicsModule.filePath = string.Concat(EditorUtils.dotweenDir, _physicsModule.filePath);
			_physics2DModule.filePath = string.Concat(EditorUtils.dotweenDir, _physics2DModule.filePath);
			_spriteModule.filePath = string.Concat(EditorUtils.dotweenDir, _spriteModule.filePath);
			_uiModule.filePath = string.Concat(EditorUtils.dotweenDir, _uiModule.filePath);
			_textMeshProModule.filePath = string.Concat(EditorUtils.dotweenProDir, _textMeshProModule.filePath);
			_tk2DModule.filePath = string.Concat(EditorUtils.dotweenProDir, _tk2DModule.filePath);
			_deAudioModule.filePath = string.Concat(EditorUtils.dotweenProDir, _deAudioModule.filePath);
			_deUnityExtendedModule.filePath = string.Concat(EditorUtils.dotweenProDir, _deUnityExtendedModule.filePath);
		}

		public static bool Draw(EditorWindow editor, DOTweenSettings src)
		{
			_editor = editor;
			_src = src;
			if (!_refreshed)
			{
				Refresh(_src);
			}
			GUILayout.Label("Add/Remove Modules", EditorGUIUtils.titleStyle);
			GUILayout.BeginVertical();
			EditorGUI.BeginDisabledGroup(EditorApplication.isCompiling);
			GUILayout.BeginVertical(GUI.skin.box);
			GUILayout.Label("Unity", EditorGUIUtils.boldLabelStyle);
			_audioModule.enabled = EditorGUILayout.Toggle("Audio", _audioModule.enabled);
			_physicsModule.enabled = EditorGUILayout.Toggle("Physics", _physicsModule.enabled);
			_physics2DModule.enabled = EditorGUILayout.Toggle("Physics2D", _physics2DModule.enabled);
			_spriteModule.enabled = EditorGUILayout.Toggle("Sprites", _spriteModule.enabled);
			_uiModule.enabled = EditorGUILayout.Toggle("UI", _uiModule.enabled);
			EditorGUILayout.EndVertical();
			if (EditorUtils.hasPro)
			{
				GUILayout.BeginVertical(GUI.skin.box);
				GUILayout.Label("External Assets (Pro)", EditorGUIUtils.boldLabelStyle);
				GUILayout.Label("<b>IMPORTANT:</b> these modules are for external Unity assets.\n<i>DO NOT activate an external module</i> unless you have the relative asset in your project.", EditorGUIUtils.wordWrapRichTextLabelStyle);
				_deAudioModule.enabled = EditorGUILayout.Toggle("DeAudio", _deAudioModule.enabled);
				_deUnityExtendedModule.enabled = EditorGUILayout.Toggle("DeUnityExtended", _deUnityExtendedModule.enabled);
				_textMeshProModule.enabled = EditorGUILayout.Toggle("TextMesh Pro", _textMeshProModule.enabled);
				_tk2DModule.enabled = EditorGUILayout.Toggle("2D Toolkit (legacy)", _tk2DModule.enabled);
				EditorGUILayout.EndVertical();
			}
			GUILayout.Space(2f);
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Apply"))
			{
				Apply();
				Refresh(_src);
				return true;
			}
			if (GUILayout.Button("Cancel"))
			{
				return true;
			}
			GUILayout.EndHorizontal();
			EditorGUI.EndDisabledGroup();
			GUILayout.EndVertical();
			if (EditorApplication.isCompiling)
			{
				WaitForCompilation();
			}
			return false;
		}

		private static void WaitForCompilation()
		{
			if (!_isWaitingForCompilation)
			{
				_isWaitingForCompilation = true;
				EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Combine(EditorApplication.update, new EditorApplication.CallbackFunction(WaitForCompilation_Update));
				WaitForCompilation_Update();
			}
			EditorGUILayout.HelpBox("Waiting for Unity to finish the compilation process...", MessageType.Info);
		}

		private static void WaitForCompilation_Update()
		{
			if (!EditorApplication.isCompiling)
			{
				EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Remove(EditorApplication.update, new EditorApplication.CallbackFunction(WaitForCompilation_Update));
				_isWaitingForCompilation = false;
				Refresh(_src);
			}
			_editor.Repaint();
		}

		public static void ApplyModulesSettings()
		{
			DOTweenSettings dOTweenSettings = DOTweenUtilityWindow.GetDOTweenSettings();
			if (dOTweenSettings != null)
			{
				Refresh(dOTweenSettings, applySrcSettings: true);
			}
		}

		public static void Refresh(DOTweenSettings src, bool applySrcSettings = false)
		{
			_src = src;
			_refreshed = true;
			AssetDatabase.StartAssetEditing();
			_audioModule.enabled = ModuleIsEnabled(_audioModule);
			_physicsModule.enabled = ModuleIsEnabled(_physicsModule);
			_physics2DModule.enabled = ModuleIsEnabled(_physics2DModule);
			_spriteModule.enabled = ModuleIsEnabled(_spriteModule);
			_uiModule.enabled = ModuleIsEnabled(_uiModule);
			_textMeshProModule.enabled = ModuleIsEnabled(_textMeshProModule);
			_tk2DModule.enabled = ModuleIsEnabled(_tk2DModule);
			_deAudioModule.enabled = ModuleIsEnabled(_deAudioModule);
			_deUnityExtendedModule.enabled = ModuleIsEnabled(_deUnityExtendedModule);
			CheckAutoModuleSettings(applySrcSettings, _audioModule, ref src.modules.audioEnabled);
			CheckAutoModuleSettings(applySrcSettings, _physicsModule, ref src.modules.physicsEnabled);
			CheckAutoModuleSettings(applySrcSettings, _physics2DModule, ref src.modules.physics2DEnabled);
			CheckAutoModuleSettings(applySrcSettings, _spriteModule, ref src.modules.spriteEnabled);
			CheckAutoModuleSettings(applySrcSettings, _uiModule, ref src.modules.uiEnabled);
			CheckAutoModuleSettings(applySrcSettings, _textMeshProModule, ref src.modules.textMeshProEnabled);
			CheckAutoModuleSettings(applySrcSettings, _tk2DModule, ref src.modules.tk2DEnabled);
			CheckAutoModuleSettings(applySrcSettings, _deAudioModule, ref src.modules.deAudioEnabled);
			CheckAutoModuleSettings(applySrcSettings, _deUnityExtendedModule, ref src.modules.deUnityExtendedEnabled);
			AssetDatabase.StopAssetEditing();
			EditorUtility.SetDirty(_src);
		}

		private static void Apply()
		{
			AssetDatabase.StartAssetEditing();
			bool flag = ToggleModule(_audioModule, ref _src.modules.audioEnabled);
			bool flag2 = ToggleModule(_physicsModule, ref _src.modules.physicsEnabled);
			bool flag3 = ToggleModule(_physics2DModule, ref _src.modules.physics2DEnabled);
			bool flag4 = ToggleModule(_spriteModule, ref _src.modules.spriteEnabled);
			bool flag5 = ToggleModule(_uiModule, ref _src.modules.uiEnabled);
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			if (EditorUtils.hasPro)
			{
				flag6 = ToggleModule(_textMeshProModule, ref _src.modules.textMeshProEnabled);
				flag7 = ToggleModule(_tk2DModule, ref _src.modules.tk2DEnabled);
				flag8 = ToggleModule(_deAudioModule, ref _src.modules.deAudioEnabled);
				flag9 = ToggleModule(_deUnityExtendedModule, ref _src.modules.deUnityExtendedEnabled);
			}
			AssetDatabase.StopAssetEditing();
			EditorUtility.SetDirty(_src);
			if (flag || flag2 || flag3 || flag4 || flag5 || flag6 || flag7 || flag8 || flag9)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<b>DOTween module files modified ► </b>");
				if (flag)
				{
					Apply_AppendLog(stringBuilder, _src.modules.audioEnabled, "Audio");
				}
				if (flag2)
				{
					Apply_AppendLog(stringBuilder, _src.modules.physicsEnabled, "Physics");
				}
				if (flag3)
				{
					Apply_AppendLog(stringBuilder, _src.modules.physics2DEnabled, "Physics2D");
				}
				if (flag4)
				{
					Apply_AppendLog(stringBuilder, _src.modules.spriteEnabled, "Sprites");
				}
				if (flag5)
				{
					Apply_AppendLog(stringBuilder, _src.modules.uiEnabled, "UI");
				}
				if (flag6)
				{
					Apply_AppendLog(stringBuilder, _src.modules.textMeshProEnabled, "TextMesh Pro");
				}
				if (flag7)
				{
					Apply_AppendLog(stringBuilder, _src.modules.tk2DEnabled, "2D Toolkit");
				}
				if (flag8)
				{
					Apply_AppendLog(stringBuilder, _src.modules.deAudioEnabled, "DeAudio");
				}
				if (flag9)
				{
					Apply_AppendLog(stringBuilder, _src.modules.deUnityExtendedEnabled, "DeUnityExtended");
				}
				stringBuilder.Remove(stringBuilder.Length - 3, 3);
				Debug.Log(stringBuilder.ToString());
			}
			ASMDEFManager.RefreshExistingASMDEFFiles();
		}

		private static void Apply_AppendLog(StringBuilder strb, bool enabled, string id)
		{
			strb.Append("<color=#").Append(enabled ? "00ff00" : "ff0000").Append('>')
				.Append(id)
				.Append("</color>")
				.Append(" - ");
		}

		private static bool ModuleIsEnabled(ModuleInfo m)
		{
			if (!File.Exists(m.filePath))
			{
				return false;
			}
			using (StreamReader streamReader = new StreamReader(m.filePath))
			{
				for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
				{
					if (text.EndsWith("MODULE_MARKER") && text.StartsWith("#if"))
					{
						return text.Contains("true");
					}
				}
			}
			return true;
		}

		private static void CheckAutoModuleSettings(bool applySettings, ModuleInfo m, ref bool srcModuleEnabled)
		{
			if (m.enabled != srcModuleEnabled)
			{
				if (applySettings)
				{
					m.enabled = srcModuleEnabled;
					ToggleModule(m, ref srcModuleEnabled);
				}
				else
				{
					srcModuleEnabled = m.enabled;
					EditorUtility.SetDirty(_src);
				}
			}
		}

		private static bool ToggleModule(ModuleInfo m, ref bool srcSetting)
		{
			srcSetting = m.enabled;
			bool result = false;
			if (File.Exists(m.filePath))
			{
				_LinesToChange.Clear();
				string[] array = File.ReadAllLines(m.filePath);
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (text.EndsWith("MODULE_MARKER") && text.StartsWith("#if") && ((m.enabled && text.Contains("false")) || (!m.enabled && text.Contains("true"))))
					{
						_LinesToChange.Add(i);
					}
				}
				if (_LinesToChange.Count > 0)
				{
					result = true;
					using (StreamWriter streamWriter = new StreamWriter(m.filePath))
					{
						for (int j = 0; j < array.Length; j++)
						{
							string text2 = array[j];
							if (_LinesToChange.Contains(j))
							{
								text2 = (m.enabled ? text2.Replace("false", "true") : text2.Replace("true", "false"));
							}
							streamWriter.WriteLine(text2);
						}
					}
					AssetDatabase.ImportAsset(EditorUtils.FullPathToADBPath(m.filePath), ImportAssetOptions.Default);
				}
			}
			string marker = string.Concat("// ", m.id, "_MARKER");
			for (int k = 0; k < _ModuleDependentFiles.Length; k++)
			{
				if (ToggleModuleInDependentFile(_ModuleDependentFiles[k], m.enabled, marker))
				{
					result = true;
				}
			}
			_LinesToChange.Clear();
			return result;
		}

		private static bool ToggleModuleInDependentFile(string filePath, bool enable, string marker)
		{
			if (!File.Exists(filePath))
			{
				return false;
			}
			bool result = false;
			_LinesToChange.Clear();
			string[] array = File.ReadAllLines(filePath);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (text.EndsWith(marker) && text.StartsWith("#if") && ((enable && text.Contains("false")) || (!enable && text.Contains("true"))))
				{
					_LinesToChange.Add(i);
				}
			}
			if (_LinesToChange.Count > 0)
			{
				result = true;
				using (StreamWriter streamWriter = new StreamWriter(filePath))
				{
					for (int j = 0; j < array.Length; j++)
					{
						string text2 = array[j];
						if (_LinesToChange.Contains(j))
						{
							text2 = (enable ? text2.Replace("false", "true") : text2.Replace("true", "false"));
						}
						streamWriter.WriteLine(text2);
					}
				}
				AssetDatabase.ImportAsset(EditorUtils.FullPathToADBPath(filePath), ImportAssetOptions.Default);
			}
			return result;
		}
	}
}
