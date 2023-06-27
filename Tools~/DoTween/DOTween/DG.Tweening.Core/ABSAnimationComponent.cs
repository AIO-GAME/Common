using System;
using UnityEngine;
using UnityEngine.Events;

namespace DG.Tweening.Core
{
	[AddComponentMenu("")]
	public abstract class ABSAnimationComponent : MonoBehaviour
	{
		public UpdateType updateType;

		public bool isSpeedBased;

		public bool hasOnStart;

		public bool hasOnPlay;

		public bool hasOnUpdate;

		public bool hasOnStepComplete;

		public bool hasOnComplete;

		public bool hasOnTweenCreated;

		public bool hasOnRewind;

		public UnityEvent onStart;

		public UnityEvent onPlay;

		public UnityEvent onUpdate;

		public UnityEvent onStepComplete;

		public UnityEvent onComplete;

		public UnityEvent onTweenCreated;

		public UnityEvent onRewind;

		[NonSerialized]
		public Tween tween;

		public abstract void DOPlay();

		public abstract void DOPlayBackwards();

		public abstract void DOPlayForward();

		public abstract void DOPause();

		public abstract void DOTogglePause();

		public abstract void DORewind();

		/// <summary>
		/// Restarts the tween
		/// </summary>
		public abstract void DORestart();

		/// <summary>
		/// Restarts the tween
		/// </summary>
		/// <param name="fromHere">If TRUE, re-evaluates the tween's start and end values from its current position.
		/// Set it to TRUE when spawning the same DOTweenPath in different positions (like when using a pooling system)</param>
		public abstract void DORestart(bool fromHere);

		public abstract void DOComplete();

		public abstract void DOKill();
	}
}
