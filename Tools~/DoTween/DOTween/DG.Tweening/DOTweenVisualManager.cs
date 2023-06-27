using DG.Tweening.Core;
using UnityEngine;

namespace DG.Tweening
{
	[AddComponentMenu("")]
	public class DOTweenVisualManager : MonoBehaviour
	{
		public VisualManagerPreset preset;

		public OnEnableBehaviour onEnableBehaviour;

		public OnDisableBehaviour onDisableBehaviour;

		private bool _requiresRestartFromSpawnPoint;

		private ABSAnimationComponent _animComponent;

		private void Awake()
		{
			_animComponent = GetComponent<ABSAnimationComponent>();
		}

		private void Update()
		{
			if (_requiresRestartFromSpawnPoint && !(_animComponent == null))
			{
				_requiresRestartFromSpawnPoint = false;
				_animComponent.DORestart(fromHere: true);
			}
		}

		private void OnEnable()
		{
			switch (onEnableBehaviour)
			{
			case OnEnableBehaviour.Play:
				if (_animComponent != null)
				{
					_animComponent.DOPlay();
				}
				break;
			case OnEnableBehaviour.Restart:
				if (_animComponent != null)
				{
					_animComponent.DORestart();
				}
				break;
			case OnEnableBehaviour.RestartFromSpawnPoint:
				_requiresRestartFromSpawnPoint = true;
				break;
			}
		}

		private void OnDisable()
		{
			_requiresRestartFromSpawnPoint = false;
			switch (onDisableBehaviour)
			{
			case OnDisableBehaviour.Pause:
				if (_animComponent != null)
				{
					_animComponent.DOPause();
				}
				break;
			case OnDisableBehaviour.Rewind:
				if (_animComponent != null)
				{
					_animComponent.DORewind();
				}
				break;
			case OnDisableBehaviour.Kill:
				if (_animComponent != null)
				{
					_animComponent.DOKill();
				}
				break;
			case OnDisableBehaviour.KillAndComplete:
				if (_animComponent != null)
				{
					_animComponent.DOComplete();
					_animComponent.DOKill();
				}
				break;
			case OnDisableBehaviour.DestroyGameObject:
				if (_animComponent != null)
				{
					_animComponent.DOKill();
				}
				Object.Destroy(base.gameObject);
				break;
			}
		}
	}
}
