using DG.Tweening.Core;
using UnityEditor;
using UnityEngine;

namespace DG.DOTweenEditor.UI
{
	[CustomEditor(typeof(DOTweenSettings))]
	public class DOTweenSettingsInspector : Editor
	{
		private DOTweenSettings _src;

		private void OnEnable()
		{
			_src = base.target as DOTweenSettings;
		}

		public override void OnInspectorGUI()
		{
			GUI.enabled = false;
			DrawDefaultInspector();
			GUI.enabled = true;
		}
	}
}
