using DG.DOTweenEditor.UI;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEditor;

namespace DG.DOTweenEditor
{
	[InitializeOnLoad]
	internal static class Initializer
	{
		static Initializer()
		{
			DOTweenPath.OnReset += OnReset;
		}

		private static void OnReset(DOTweenPath src)
		{
			DOTweenSettings dOTweenSettings = DOTweenUtilityWindow.GetDOTweenSettings();
			if (!(dOTweenSettings == null))
			{
				Undo.RecordObject(src, "DOTweenPath");
				src.autoPlay = dOTweenSettings.defaultAutoPlay == AutoPlay.All || dOTweenSettings.defaultAutoPlay == AutoPlay.AutoPlayTweeners;
				src.autoKill = dOTweenSettings.defaultAutoKill;
				EditorUtility.SetDirty(src);
			}
		}
	}
}
