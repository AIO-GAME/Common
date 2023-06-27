using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;

namespace DG.DOTweenEditor
{
	public static class DOTweenEditorPreview
	{
		private static bool _isPreviewing;

		private static double _previewTime;

		private static Action _onPreviewUpdated;

		private static readonly List<Tween> _Tweens = new List<Tween>();

		/// <summary>
		/// Starts the update loop of tween in the editor. Has no effect during playMode.
		/// </summary>
		/// <param name="onPreviewUpdated">Eventual callback to call after every update</param>
		public static void Start(Action onPreviewUpdated = null)
		{
			if (!_isPreviewing && !EditorApplication.isPlayingOrWillChangePlaymode)
			{
				_isPreviewing = true;
				_onPreviewUpdated = onPreviewUpdated;
				_previewTime = EditorApplication.timeSinceStartup;
				EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Combine(EditorApplication.update, new EditorApplication.CallbackFunction(PreviewUpdate));
			}
		}

		/// <summary>
		/// Stops the update loop and clears the onPreviewUpdated callback.
		/// </summary>
		/// <param name="resetTweenTargets">If TRUE also resets the tweened objects to their original state</param>
		public static void Stop(bool resetTweenTargets = false)
		{
			_isPreviewing = false;
			EditorApplication.update = (EditorApplication.CallbackFunction)Delegate.Remove(EditorApplication.update, new EditorApplication.CallbackFunction(PreviewUpdate));
			_onPreviewUpdated = null;
			if (resetTweenTargets)
			{
				foreach (Tween tween in _Tweens)
				{
					try
					{
						if (tween.isFrom)
						{
							tween.Complete();
						}
						else
						{
							tween.Rewind();
						}
					}
					catch
					{
					}
				}
			}
			ValidateTweens();
		}

		/// <summary>
		/// Readies the tween for editor preview by setting its UpdateType to Manual plus eventual extra settings.
		/// </summary>
		/// <param name="t">The tween to ready</param>
		/// <param name="clearCallbacks">If TRUE (recommended) removes all callbacks (OnComplete/Rewind/etc)</param>
		/// <param name="preventAutoKill">If TRUE prevents the tween from being auto-killed at completion</param>
		/// <param name="andPlay">If TRUE starts playing the tween immediately</param>
		public static void PrepareTweenForPreview(Tween t, bool clearCallbacks = true, bool preventAutoKill = true, bool andPlay = true)
		{
			_Tweens.Add(t);
			t.SetUpdate(UpdateType.Manual);
			if (preventAutoKill)
			{
				t.SetAutoKill(autoKillOnCompletion: false);
			}
			if (clearCallbacks)
			{
				t.OnComplete(null).OnStart(null).OnPlay(null)
					.OnPause(null)
					.OnUpdate(null)
					.OnWaypointChange(null)
					.OnStepComplete(null)
					.OnRewind(null)
					.OnKill(null);
			}
			if (andPlay)
			{
				t.Play();
			}
		}

		private static void PreviewUpdate()
		{
			double previewTime = _previewTime;
			_previewTime = EditorApplication.timeSinceStartup;
			float num = (float)(_previewTime - previewTime);
			DOTween.ManualUpdate(num, num);
			if (_onPreviewUpdated != null)
			{
				_onPreviewUpdated();
			}
		}

		private static void ValidateTweens()
		{
			for (int num = _Tweens.Count - 1; num > -1; num--)
			{
				if (_Tweens[num] == null || !_Tweens[num].active)
				{
					_Tweens.RemoveAt(num);
				}
			}
		}
	}
}
