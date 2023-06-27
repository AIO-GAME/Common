namespace DG.Tweening
{
	/// <summary>
	/// Type of path to use with DOPath tweens
	/// </summary>
	public enum PathType
	{
		/// <summary>Linear, composed of straight segments between each waypoint</summary>
		Linear,
		/// <summary>Curved path (which uses Catmull-Rom curves)</summary>
		CatmullRom,
		/// <summary><code>EXPERIMENTAL: </code>Curved path (which uses Cubic Bezier curves, where each point requires two extra control points)</summary>
		CubicBezier
	}
}
