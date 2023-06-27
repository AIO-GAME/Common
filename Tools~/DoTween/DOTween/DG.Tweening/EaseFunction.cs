namespace DG.Tweening
{
	/// <summary>
	/// Used for custom and animationCurve-based ease functions. Must return a value between 0 and 1.
	/// </summary>
	public delegate float EaseFunction(float time, float duration, float overshootOrAmplitude, float period);
}
