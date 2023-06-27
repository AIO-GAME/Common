using System.IO;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor
{
	public class UtilityWindowModificationProcessor : AssetModificationProcessor
	{
		private static AssetDeleteResult OnWillDeleteAsset(string asset, RemoveAssetOptions options)
		{
			string path = EditorUtils.ADBPathToFullPath(asset);
			if (!Directory.Exists(path))
			{
				return AssetDeleteResult.DidNotDelete;
			}
			string[] files = Directory.GetFiles(path, "DOTween.dll", SearchOption.AllDirectories);
			int num = files.Length;
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				if (files[i].EndsWith("DOTween.dll"))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return AssetDeleteResult.DidNotDelete;
			}
			Debug.Log("::: DOTween deleted");
			EditorPrefs.DeleteKey(string.Concat(Application.dataPath, "DOTweenVersion"));
			EditorPrefs.DeleteKey(string.Concat(Application.dataPath, "DOTweenProVersion"));
			return AssetDeleteResult.DidNotDelete;
		}
	}
}
