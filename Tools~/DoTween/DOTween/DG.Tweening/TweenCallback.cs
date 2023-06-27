namespace DG.Tweening
{
	/// <summary>
	/// Used for tween callbacks
	/// </summary>
	public delegate void TweenCallback();
	/// <summary>
	/// Used for tween callbacks
	/// </summary>
	public delegate void TweenCallback<in T>(T value);
}
