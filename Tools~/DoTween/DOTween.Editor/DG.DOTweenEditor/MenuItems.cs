using DG.Tweening.Core;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor
{
	internal static class MenuItems
	{
		[MenuItem("GameObject/Demigiant/DOTween Manager", false, 20)]
		private static void CreateDOTweenComponent(MenuCommand menuCommand)
		{
			GameObject gameObject = new GameObject("[DOTween]");
			gameObject.AddComponent<DOTweenComponent>();
			GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);
			Undo.RegisterCreatedObjectUndo(gameObject, string.Concat("Create ", gameObject.name));
			Selection.activeObject = gameObject;
		}
	}
}
