namespace DG.Tweening.Core
{
	public abstract class ABSSequentiable
	{
		internal TweenType tweenType;

		internal float sequencedPosition;

		internal float sequencedEndPosition;

		/// <summary>Called the first time the tween is set in a playing state, after any eventual delay</summary>
		internal TweenCallback onStart;
	}
}
