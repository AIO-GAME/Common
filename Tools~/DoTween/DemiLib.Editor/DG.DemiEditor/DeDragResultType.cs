namespace DG.DemiEditor
{
	public enum DeDragResultType
	{
		/// <summary>Nothing is being dragged</summary>
		NoDrag,
		/// <summary>Dragging</summary>
		Dragging,
		/// <summary>Dragging concluced and accepted</summary>
		Accepted,
		/// <summary>Dragging concluced but item position didn't change</summary>
		Ineffective,
		/// <summary>Dragging canceled</summary>
		Canceled,
		/// <summary>Dragging concluced but not accepted because too short</summary>
		Click
	}
}
