using System.IO;
using DG.DOTweenEditor.UI;
using DG.Tweening.Core;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor
{
	internal static class ASMDEFManager
	{
		public enum ASMDEFType
		{
			Modules,
			DOTweenPro,
			DOTweenProEditor
		}

		private enum ChangeType
		{
			Deleted,
			Created,
			Overwritten
		}

		private const string _ModulesId = "DOTween.Modules";

		private const string _ProId = "DOTweenPro.Scripts";

		private const string _ProEditorId = "DOTweenPro.EditorScripts";

		private const string _ModulesASMDEFFile = "DOTween.Modules.asmdef";

		private const string _ProASMDEFFile = "DOTweenPro.Scripts.asmdef";

		private const string _ProEditorASMDEFFile = "DOTweenPro.EditorScripts.asmdef";

		private const string _RefTextMeshPro = "Unity.TextMeshPro";

		public static bool hasModulesASMDEF { get; private set; }

		public static bool hasProASMDEF { get; private set; }

		public static bool hasProEditorASMDEF { get; private set; }

		static ASMDEFManager()
		{
			Refresh();
		}

		public static void Refresh()
		{
			hasModulesASMDEF = File.Exists(string.Concat(EditorUtils.dotweenModulesDir, "DOTween.Modules.asmdef"));
			hasProASMDEF = File.Exists(string.Concat(EditorUtils.dotweenProDir, "DOTweenPro.Scripts.asmdef"));
			hasProEditorASMDEF = File.Exists(string.Concat(EditorUtils.dotweenProEditorDir, "DOTweenPro.EditorScripts.asmdef"));
		}

		public static void RefreshExistingASMDEFFiles()
		{
			Refresh();
			if (!hasModulesASMDEF)
			{
				if (hasProASMDEF)
				{
					RemoveASMDEF(ASMDEFType.DOTweenPro);
				}
				if (hasProEditorASMDEF)
				{
					RemoveASMDEF(ASMDEFType.DOTweenProEditor);
				}
			}
			else if (EditorUtils.hasPro)
			{
				if (!hasProASMDEF)
				{
					CreateASMDEF(ASMDEFType.DOTweenPro);
				}
				if (!hasProEditorASMDEF)
				{
					CreateASMDEF(ASMDEFType.DOTweenProEditor);
				}
				DOTweenSettings dOTweenSettings = DOTweenUtilityWindow.GetDOTweenSettings();
				if (!(dOTweenSettings == null))
				{
					ValidateProASMDEFReferences(dOTweenSettings, ASMDEFType.DOTweenPro, string.Concat(EditorUtils.dotweenProDir, "DOTweenPro.Scripts.asmdef"));
					ValidateProASMDEFReferences(dOTweenSettings, ASMDEFType.DOTweenProEditor, string.Concat(EditorUtils.dotweenProEditorDir, "DOTweenPro.EditorScripts.asmdef"));
				}
			}
		}

		public static void CreateAllASMDEF()
		{
			CreateASMDEF(ASMDEFType.Modules);
			if (EditorUtils.hasPro)
			{
				CreateASMDEF(ASMDEFType.DOTweenPro);
				CreateASMDEF(ASMDEFType.DOTweenProEditor);
			}
		}

		public static void RemoveAllASMDEF()
		{
			RemoveASMDEF(ASMDEFType.Modules);
			RemoveASMDEF(ASMDEFType.DOTweenPro);
			RemoveASMDEF(ASMDEFType.DOTweenProEditor);
		}

		private static void ValidateProASMDEFReferences(DOTweenSettings src, ASMDEFType asmdefType, string asmdefFilepath)
		{
			bool flag = false;
			using (StreamReader streamReader = new StreamReader(asmdefFilepath))
			{
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					if (text.Contains("Unity.TextMeshPro"))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag != src.modules.textMeshProEnabled)
			{
				CreateASMDEF(asmdefType, forceOverwrite: true);
			}
		}

		private static void LogASMDEFChange(ASMDEFType asmdefType, ChangeType changeType)
		{
			string arg = "";
			switch (asmdefType)
			{
			case ASMDEFType.Modules:
				arg = "DOTween/Modules/DOTween.Modules.asmdef";
				break;
			case ASMDEFType.DOTweenPro:
				arg = "DOTweenPro/DOTweenPro.Scripts.asmdef";
				break;
			case ASMDEFType.DOTweenProEditor:
				arg = "DOTweenPro/Editor/DOTweenPro.EditorScripts.asmdef";
				break;
			}
			object arg2;
			switch (changeType)
			{
			default:
				arg2 = "ff6600";
				break;
			case ChangeType.Created:
				arg2 = "00ff00";
				break;
			case ChangeType.Deleted:
				arg2 = "ff0000";
				break;
			}
			object arg3;
			switch (changeType)
			{
			default:
				arg3 = "changed";
				break;
			case ChangeType.Created:
				arg3 = "created";
				break;
			case ChangeType.Deleted:
				arg3 = "removed";
				break;
			}
			Debug.Log($"<b>DOTween ASMDEF file <color=#{arg2}>{arg3}</color></b> ► {arg}");
		}

		private static void CreateASMDEF(ASMDEFType type, bool forceOverwrite = false)
		{
			Refresh();
			bool flag = false;
			string arg = null;
			string text = null;
			string text2 = null;
			switch (type)
			{
			case ASMDEFType.Modules:
				flag = hasModulesASMDEF;
				arg = "DOTween.Modules";
				text = "DOTween.Modules.asmdef";
				text2 = EditorUtils.dotweenModulesDir;
				break;
			case ASMDEFType.DOTweenPro:
				flag = hasProASMDEF;
				arg = "DOTweenPro.Scripts";
				text = "DOTweenPro.Scripts.asmdef";
				text2 = EditorUtils.dotweenProDir;
				break;
			case ASMDEFType.DOTweenProEditor:
				flag = hasProEditorASMDEF;
				arg = "DOTweenPro.EditorScripts";
				text = "DOTweenPro.EditorScripts.asmdef";
				text2 = EditorUtils.dotweenProEditorDir;
				break;
			}
			if (flag && !forceOverwrite)
			{
				EditorUtility.DisplayDialog("Create ASMDEF", string.Concat(text, " already exists"), "Ok");
				return;
			}
			if (!Directory.Exists(text2))
			{
				EditorUtility.DisplayDialog("Create ASMDEF", $"Directory not found\n({text2})", "Ok");
				return;
			}
			string text3 = string.Concat(text2, text);
			using (StreamWriter streamWriter = File.CreateText(text3))
			{
				streamWriter.WriteLine("{");
				switch (type)
				{
				case ASMDEFType.Modules:
					streamWriter.WriteLine("\t\"name\": \"{0}\"", arg);
					break;
				case ASMDEFType.DOTweenPro:
				case ASMDEFType.DOTweenProEditor:
				{
					streamWriter.WriteLine("\t\"name\": \"{0}\",", arg);
					streamWriter.WriteLine("\t\"references\": [");
					DOTweenSettings dOTweenSettings = DOTweenUtilityWindow.GetDOTweenSettings();
					if (dOTweenSettings != null && dOTweenSettings.modules.textMeshProEnabled)
					{
						streamWriter.WriteLine("\t\t\"{0}\",", "Unity.TextMeshPro");
					}
					if (type == ASMDEFType.DOTweenProEditor)
					{
						streamWriter.WriteLine("\t\t\"{0}\",", "DOTween.Modules");
						streamWriter.WriteLine("\t\t\"{0}\"", "DOTweenPro.Scripts");
						streamWriter.WriteLine("\t],");
						streamWriter.WriteLine("\t\"includePlatforms\": [");
						streamWriter.WriteLine("\t\t\"Editor\"");
						streamWriter.WriteLine("\t],");
						streamWriter.WriteLine("\t\"autoReferenced\": false");
					}
					else
					{
						streamWriter.WriteLine("\t\t\"{0}\"", "DOTween.Modules");
						streamWriter.WriteLine("\t]");
					}
					break;
				}
				}
				streamWriter.WriteLine("}");
			}
			AssetDatabase.ImportAsset(EditorUtils.FullPathToADBPath(text3), ImportAssetOptions.ForceUpdate);
			Refresh();
			LogASMDEFChange(type, (!flag) ? ChangeType.Created : ChangeType.Overwritten);
		}

		private static void RemoveASMDEF(ASMDEFType type)
		{
			bool flag = false;
			string text = null;
			string text2 = null;
			switch (type)
			{
			case ASMDEFType.Modules:
				flag = hasModulesASMDEF;
				text2 = EditorUtils.dotweenModulesDir;
				text = "DOTween.Modules.asmdef";
				break;
			case ASMDEFType.DOTweenPro:
				flag = hasProASMDEF;
				text = "DOTweenPro.Scripts.asmdef";
				text2 = EditorUtils.dotweenProDir;
				break;
			case ASMDEFType.DOTweenProEditor:
				flag = hasProEditorASMDEF;
				text = "DOTweenPro.EditorScripts.asmdef";
				text2 = EditorUtils.dotweenProEditorDir;
				break;
			}
			Refresh();
			if (!flag)
			{
				EditorUtility.DisplayDialog("Remove ASMDEF", string.Concat(text, " not present"), "Ok");
				return;
			}
			AssetDatabase.DeleteAsset(EditorUtils.FullPathToADBPath(string.Concat(text2, text)));
			Refresh();
			LogASMDEFChange(type, ChangeType.Deleted);
		}
	}
}
