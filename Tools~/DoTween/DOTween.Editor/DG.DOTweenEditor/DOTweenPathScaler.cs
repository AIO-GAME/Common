using System;
using System.Collections.Generic;
using DG.DemiEditor;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor
{
	internal class DOTweenPathScaler : EditorWindow
	{
		private const string _Title = "DOTweenPathScaler";

		private static readonly Vector2 _WinSize = new Vector2(280f, 110f);

		private static DOTweenPathScaler _window;

		private DOTweenPath _src;

		private DOTweenPathInspector _inspector;

		private List<Vector3> _wpsOriginal;

		private float _scaleModeAll;

		private float _scaleModeX;

		private float _scaleModeY;

		private float _scaleModeZ;

		public static void Open(DOTweenPath src, DOTweenPathInspector inspector)
		{
			_window = EditorWindow.GetWindow<DOTweenPathScaler>(utility: true, "DOTweenPathScaler", focus: true);
			_window.minSize = _WinSize;
			_window.maxSize = _WinSize;
			_window.Setup(src, inspector);
			_window.ShowUtility();
		}

		private void Setup(DOTweenPath src, DOTweenPathInspector inspector)
		{
			_src = src;
			_inspector = inspector;
			_scaleModeAll = (_scaleModeX = (_scaleModeY = (_scaleModeZ = 0f)));
			_wpsOriginal = new List<Vector3>();
			for (int i = 0; i < _src.wps.Count; i++)
			{
				_wpsOriginal.Add(_src.wps[i]);
			}
		}

		private void DoClose(bool reset)
		{
			if (reset)
			{
				ResetPath();
			}
			_window.Close();
		}

		private void OnEnable()
		{
			Undo.undoRedoPerformed = (Undo.UndoRedoCallback)Delegate.Combine(Undo.undoRedoPerformed, new Undo.UndoRedoCallback(Repaint));
		}

		private void OnDisable()
		{
			Undo.undoRedoPerformed = (Undo.UndoRedoCallback)Delegate.Remove(Undo.undoRedoPerformed, new Undo.UndoRedoCallback(Repaint));
		}

		private void OnLostFocus()
		{
			DoClose(reset: true);
		}

		private void OnHierarchyChange()
		{
			Repaint();
		}

		private void OnGUI()
		{
			Undo.RecordObject(_src, "DOTweenPathScaler");
			DeGUI.BeginGUI();
			using (new DeGUI.LabelFieldWidthScope(40f))
			{
				EditorGUI.BeginChangeCheck();
				EditorGUI.BeginChangeCheck();
				_scaleModeAll = EditorGUILayout.Slider("X/Y/Z", _scaleModeAll, -10f, 10f);
				if (EditorGUI.EndChangeCheck())
				{
					_scaleModeX = _scaleModeAll;
					_scaleModeY = _scaleModeAll;
					_scaleModeZ = _scaleModeAll;
				}
				_scaleModeX = EditorGUILayout.Slider("x", _scaleModeX, -10f, 10f);
				_scaleModeY = EditorGUILayout.Slider("Y", _scaleModeY, -10f, 10f);
				_scaleModeZ = EditorGUILayout.Slider("Z", _scaleModeZ, -10f, 10f);
				if (EditorGUI.EndChangeCheck())
				{
					Vector3 scale = new Vector3((_scaleModeX >= 0f) ? (1f + _scaleModeX) : Mathf.Max(0.1f, 1f - (0f - _scaleModeX) * 0.1f), (_scaleModeY >= 0f) ? (1f + _scaleModeY) : Mathf.Max(0.1f, 1f - (0f - _scaleModeY) * 0.1f), (_scaleModeZ >= 0f) ? (1f + _scaleModeZ) : Mathf.Max(0.1f, 1f - (0f - _scaleModeZ) * 0.1f));
					ScalePath(scale);
					_inspector.RefreshPath(RepaintMode.SceneAndInspector, refreshWpIndexByDepth: false);
				}
			}
			GUILayout.Space(8f);
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Apply"))
			{
				DoClose(reset: false);
			}
			if (GUILayout.Button("Cancel"))
			{
				DoClose(reset: true);
			}
			GUILayout.EndHorizontal();
			if (GUI.changed)
			{
				EditorUtility.SetDirty(_src);
			}
		}

		private void ResetPath()
		{
			_src.wps = _wpsOriginal;
			_inspector.RefreshPath(RepaintMode.SceneAndInspector, refreshWpIndexByDepth: false);
		}

		private void ScalePath(Vector3 scale)
		{
			int count = _src.wps.Count;
			Vector3 vector = _src.transform.position;
			for (int i = 0; i < count; i++)
			{
				_src.wps[i] = new Vector3(vector.x + (_wpsOriginal[i].x - vector.x) * scale.x, vector.y + (_wpsOriginal[i].y - vector.y) * scale.y, vector.z + (_wpsOriginal[i].z - vector.z) * scale.z);
			}
		}
	}
}
