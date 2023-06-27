namespace DG.DemiEditor
{
	public enum DeDragDirection
	{
		/// <summary>Automatically determines if dragged elements are horizontal, vertical, or both</summary>
		Auto,
		/// <summary>Forces vertical drag</summary>
		Vertical,
		/// <summary>Forces horizontal drag (useful to avoid initial wrong drag indicators
		/// if the users starts dragging an horizontal system vertically)</summary>
		Horizontal
	}
}
