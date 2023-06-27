namespace DG.Tweening
{
	/// <summary>
	/// Rotation mode used with DORotate methods
	/// </summary>
	public enum RotateMode
	{
		/// <summary>
		/// Fastest way that never rotates beyond 360°
		/// </summary>
		Fast,
		/// <summary>
		/// Fastest way that rotates beyond 360°
		/// </summary>
		FastBeyond360,
		/// <summary>
		/// Adds the given rotation to the transform using world axis and an advanced precision mode
		/// (like when using transform.Rotate(Space.World)).
		/// <para>In this mode the end value is is always considered relative</para>
		/// </summary>
		WorldAxisAdd,
		/// <summary>
		/// Adds the given rotation to the transform's local axis
		/// (like when rotating an object with the "local" switch enabled in Unity's editor or using transform.Rotate(Space.Self)).
		/// <para>In this mode the end value is is always considered relative</para>
		/// </summary>
		LocalAxisAdd
	}
}
