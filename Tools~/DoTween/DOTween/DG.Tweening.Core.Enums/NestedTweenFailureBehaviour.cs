namespace DG.Tweening.Core.Enums
{
	/// <summary>
	/// Behaviour in case a tween nested inside a Sequence fails
	/// </summary>
	public enum NestedTweenFailureBehaviour
	{
		/// <summary>If the Sequence contains other elements, kill the failed tween but preserve the rest</summary>
		TryToPreserveSequence,
		/// <summary>Kill the whole Sequence</summary>
		KillWholeSequence
	}
}
