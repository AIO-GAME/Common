using DG.DOTweenEditor.UI;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DG.DOTweenEditor
{
	[CustomEditor(typeof(DOTweenVisualManager))]
	public class DOTweenVisualManagerInspector : Editor
	{
		private DOTweenVisualManager _src;

		private void OnEnable()
		{
			_src = base.target as DOTweenVisualManager;
			if (Application.isPlaying)
			{
				return;
			}
			MonoBehaviour[] components = _src.GetComponents<MonoBehaviour>();
			int num = ArrayUtility.IndexOf(components, _src);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (components[i] is ABSAnimationComponent)
				{
					num2++;
				}
			}
			if (num2 > 0)
			{
				while (num2 > 0)
				{
					num2--;
					ComponentUtility.MoveComponentUp(_src);
				}
				EditorUtility.SetDirty(_src.gameObject);
			}
		}

		public override void OnInspectorGUI()
		{
			EditorGUIUtils.SetGUIStyles();
			Undo.RecordObject(_src, "DOTween Visual Manager");
			EditorGUIUtility.labelWidth = 80f;
			EditorGUIUtils.InspectorLogo();
			VisualManagerPreset preset = _src.preset;
			_src.preset = (VisualManagerPreset)(object)EditorGUILayout.EnumPopup("Preset", _src.preset);
			if (preset != _src.preset && _src.preset == VisualManagerPreset.PoolingSystem)
			{
				_src.onEnableBehaviour = OnEnableBehaviour.RestartFromSpawnPoint;
				_src.onDisableBehaviour = OnDisableBehaviour.Rewind;
			}
			GUILayout.Space(6f);
			bool flag = _src.preset != VisualManagerPreset.Custom;
			OnEnableBehaviour onEnableBehaviour = _src.onEnableBehaviour;
			OnDisableBehaviour onDisableBehaviour = _src.onDisableBehaviour;
			_src.onEnableBehaviour = (OnEnableBehaviour)(object)EditorGUILayout.EnumPopup(new GUIContent("On Enable", "Eventual actions to perform when this gameObject is activated"), _src.onEnableBehaviour);
			_src.onDisableBehaviour = (OnDisableBehaviour)(object)EditorGUILayout.EnumPopup(new GUIContent("On Disable", "Eventual actions to perform when this gameObject is deactivated"), _src.onDisableBehaviour);
			if ((flag && onEnableBehaviour != _src.onEnableBehaviour) || onDisableBehaviour != _src.onDisableBehaviour)
			{
				_src.preset = VisualManagerPreset.Custom;
			}
			if (GUI.changed)
			{
				EditorUtility.SetDirty(_src);
			}
		}
	}
}
