namespace DG.Tweening
{
	/// <summary>
	/// Types of autoPlay behaviours
	/// </summary>
	public enum AutoPlay
	{
		/// <summary>No tween is automatically played</summary>
		None,
		/// <summary>Only Sequences are automatically played</summary>
		AutoPlaySequences,
		/// <summary>Only Tweeners are automatically played</summary>
		AutoPlayTweeners,
		/// <summary>All tweens are automatically played</summary>
		All
	}
}
