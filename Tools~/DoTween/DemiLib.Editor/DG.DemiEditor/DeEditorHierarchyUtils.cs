using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	public static class DeEditorHierarchyUtils
	{
		private static MethodInfo _miExpand;

		public static void ExpandGameObject(GameObject go)
		{
			if (_miExpand == null)
			{
				_miExpand = Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor.dll").GetMethod("ExpandTreeViewItem", BindingFlags.Instance | BindingFlags.NonPublic);
			}
			EditorApplication.ExecuteMenuItem("Window/Hierarchy");
			EditorWindow focusedWindow = EditorWindow.focusedWindow;
			_miExpand.Invoke(focusedWindow, new object[2]
			{
				go.GetInstanceID(),
				true
			});
		}
	}
}
