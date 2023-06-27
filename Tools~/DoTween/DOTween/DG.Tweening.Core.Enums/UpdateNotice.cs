namespace DG.Tweening.Core.Enums
{
	/// <summary>
	/// Additional notices passed to plugins when updating.
	/// Public so it can be used by custom plugins. Internally, only PathPlugin uses it
	/// </summary>
	public enum UpdateNotice
	{
		/// <summary>
		/// None
		/// </summary>
		None,
		/// <summary>
		/// Lets the plugin know that we restarted or rewinded
		/// </summary>
		RewindStep
	}
}
