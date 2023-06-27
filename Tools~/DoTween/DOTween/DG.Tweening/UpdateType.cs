namespace DG.Tweening
{
	/// <summary>
	/// Update type
	/// </summary>
	public enum UpdateType
	{
		/// <summary>Updates every frame during Update calls</summary>
		Normal,
		/// <summary>Updates every frame during LateUpdate calls</summary>
		Late,
		/// <summary>Updates using FixedUpdate calls</summary>
		Fixed,
		/// <summary>Updates using manual update calls</summary>
		Manual
	}
}
