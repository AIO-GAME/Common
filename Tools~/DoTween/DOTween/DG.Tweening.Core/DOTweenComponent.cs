using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace DG.Tweening.Core
{
	/// <summary>
	/// Used to separate DOTween class from the MonoBehaviour instance (in order to use static constructors on DOTween).
	/// Contains all instance-based methods
	/// </summary>
	[AddComponentMenu("")]
	public class DOTweenComponent : MonoBehaviour, IDOTweenInit
	{
		/// <summary>Used internally inside Unity Editor, as a trick to update DOTween's inspector at every frame</summary>
		public int inspectorUpdater;

		private float _unscaledTime;

		private float _unscaledDeltaTime;

		private bool _paused;

		private float _pausedTime;

		private bool _isQuitting;

		private bool _duplicateToDestroy;

		private void Awake()
		{
			if (DOTween.instance == null)
			{
				DOTween.instance = this;
				inspectorUpdater = 0;
				_unscaledTime = Time.realtimeSinceStartup;
				Type looseScriptType = Utils.GetLooseScriptType("DG.Tweening.DOTweenModuleUtils");
				if (looseScriptType == null)
				{
					Debugger.LogError("Couldn't load Modules system");
				}
				else
				{
					looseScriptType.GetMethod("Init", BindingFlags.Static | BindingFlags.Public).Invoke(null, null);
				}
			}
			else
			{
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("Duplicate DOTweenComponent instance found in scene: destroying it");
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		private void Start()
		{
			if (DOTween.instance != this)
			{
				_duplicateToDestroy = true;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		private void Update()
		{
			_unscaledDeltaTime = Time.realtimeSinceStartup - _unscaledTime;
			if (DOTween.useSmoothDeltaTime && _unscaledDeltaTime > DOTween.maxSmoothUnscaledTime)
			{
				_unscaledDeltaTime = DOTween.maxSmoothUnscaledTime;
			}
			if (TweenManager.hasActiveDefaultTweens)
			{
				TweenManager.Update(UpdateType.Normal, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, _unscaledDeltaTime * DOTween.timeScale);
			}
			_unscaledTime = Time.realtimeSinceStartup;
			if (!TweenManager.isUnityEditor)
			{
				return;
			}
			inspectorUpdater++;
			if (DOTween.showUnityEditorReport && TweenManager.hasActiveTweens)
			{
				if (TweenManager.totActiveTweeners > DOTween.maxActiveTweenersReached)
				{
					DOTween.maxActiveTweenersReached = TweenManager.totActiveTweeners;
				}
				if (TweenManager.totActiveSequences > DOTween.maxActiveSequencesReached)
				{
					DOTween.maxActiveSequencesReached = TweenManager.totActiveSequences;
				}
			}
		}

		private void LateUpdate()
		{
			if (TweenManager.hasActiveLateTweens)
			{
				TweenManager.Update(UpdateType.Late, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, _unscaledDeltaTime * DOTween.timeScale);
			}
		}

		private void FixedUpdate()
		{
			if (TweenManager.hasActiveFixedTweens && Time.timeScale > 0f)
			{
				TweenManager.Update(UpdateType.Fixed, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) / Time.timeScale * DOTween.timeScale);
			}
		}

		private void OnDrawGizmos()
		{
			if (!DOTween.drawGizmos || !TweenManager.isUnityEditor)
			{
				return;
			}
			int count = DOTween.GizmosDelegates.Count;
			if (count != 0)
			{
				for (int i = 0; i < count; i++)
				{
					DOTween.GizmosDelegates[i]();
				}
			}
		}

		private void OnDestroy()
		{
			if (_duplicateToDestroy)
			{
				return;
			}
			if (DOTween.showUnityEditorReport)
			{
				Debugger.LogReport(string.Concat("Max overall simultaneous active Tweeners/Sequences: ", DOTween.maxActiveTweenersReached.ToString(), "/", DOTween.maxActiveSequencesReached.ToString()));
			}
			if (DOTween.useSafeMode)
			{
				int totErrors = DOTween.safeModeReport.GetTotErrors();
				if (totErrors > 0)
				{
					string text = $"DOTween's safe mode captured {totErrors} errors. This is usually ok (it's what safe mode is there for) but if your game is encountering issues you should set Log Behaviour to Default in DOTween Utility Panel in order to get detailed warnings when an error is captured (consider that these errors are always on the user side).";
					if (DOTween.safeModeReport.totMissingTargetOrFieldErrors > 0)
					{
						text = string.Concat(text, "\n- ", DOTween.safeModeReport.totMissingTargetOrFieldErrors.ToString(), " missing target or field errors");
					}
					if (DOTween.safeModeReport.totStartupErrors > 0)
					{
						text = string.Concat(text, "\n- ", DOTween.safeModeReport.totStartupErrors.ToString(), " startup errors");
					}
					if (DOTween.safeModeReport.totCallbackErrors > 0)
					{
						text = string.Concat(text, "\n- ", DOTween.safeModeReport.totCallbackErrors.ToString(), " errors inside callbacks (these might be important)");
					}
					if (DOTween.safeModeReport.totUnsetErrors > 0)
					{
						text = string.Concat(text, "\n- ", DOTween.safeModeReport.totUnsetErrors.ToString(), " undetermined errors (these might be important)");
					}
					Debugger.LogSafeModeReport(text);
				}
			}
			if (DOTween.instance == this)
			{
				DOTween.instance = null;
			}
			DOTween.Clear(destroy: true, _isQuitting);
		}

		public void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				_paused = true;
				_pausedTime = Time.realtimeSinceStartup;
			}
			else if (_paused)
			{
				_paused = false;
				_unscaledTime += Time.realtimeSinceStartup - _pausedTime;
			}
		}

		private void OnApplicationQuit()
		{
			_isQuitting = true;
		}

		/// <summary>
		/// Directly sets the current max capacity of Tweeners and Sequences
		/// (meaning how many Tweeners and Sequences can be running at the same time),
		/// so that DOTween doesn't need to automatically increase them in case the max is reached
		/// (which might lead to hiccups when that happens).
		/// Sequences capacity must be less or equal to Tweeners capacity
		/// (if you pass a low Tweener capacity it will be automatically increased to match the Sequence's).
		/// Beware: use this method only when there are no tweens running.
		/// </summary>
		/// <param name="tweenersCapacity">Max Tweeners capacity.
		/// Default: 200</param>
		/// <param name="sequencesCapacity">Max Sequences capacity.
		/// Default: 50</param>
		public IDOTweenInit SetCapacity(int tweenersCapacity, int sequencesCapacity)
		{
			TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
			return this;
		}

		internal IEnumerator WaitForCompletion(Tween t)
		{
			while (t.active && !t.isComplete)
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForRewind(Tween t)
		{
			while (t.active && (!t.playedOnce || t.position * (float)(t.completedLoops + 1) > 0f))
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForKill(Tween t)
		{
			while (t.active)
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForElapsedLoops(Tween t, int elapsedLoops)
		{
			while (t.active && t.completedLoops < elapsedLoops)
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForPosition(Tween t, float position)
		{
			while (t.active && t.position * (float)(t.completedLoops + 1) < position)
			{
				yield return null;
			}
		}

		internal IEnumerator WaitForStart(Tween t)
		{
			while (t.active && !t.playedOnce)
			{
				yield return null;
			}
		}

		internal static void Create()
		{
			if (!(DOTween.instance != null))
			{
				GameObject obj = new GameObject("[DOTween]");
				UnityEngine.Object.DontDestroyOnLoad(obj);
				DOTween.instance = obj.AddComponent<DOTweenComponent>();
			}
		}

		internal static void DestroyInstance()
		{
			if (DOTween.instance != null)
			{
				UnityEngine.Object.Destroy(DOTween.instance.gameObject);
			}
			DOTween.instance = null;
		}
	}
}
