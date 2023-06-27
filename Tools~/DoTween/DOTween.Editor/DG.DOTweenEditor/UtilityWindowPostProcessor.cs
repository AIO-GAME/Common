using System;
using DG.DOTweenEditor.UI;
using UnityEditor;

namespace DG.DOTweenEditor
{
	public class UtilityWindowPostProcessor : AssetPostprocessor
	{
		private static bool _setupDialogRequested;

		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			if (_setupDialogRequested)
			{
				return;
			}
			if (Array.FindAll(importedAssets, (string name) => name.Contains("DOTween") && !name.EndsWith(".meta") && !name.EndsWith(".jpg") && !name.EndsWith(".png")).Length != 0)
			{
				EditorUtils.DelayedCall(0.1f, delegate
				{
					DOTweenUtilityWindowModules.ApplyModulesSettings();
				});
			}
			if (Array.FindAll(importedAssets, (string name) => name.Contains("DOTweenPro") && !name.EndsWith(".meta") && !name.EndsWith(".jpg") && !name.EndsWith(".png")).Length != 0)
			{
				EditorUtils.DelayedCall(0.1f, delegate
				{
					ASMDEFManager.RefreshExistingASMDEFFiles();
				});
			}
		}
	}
}
