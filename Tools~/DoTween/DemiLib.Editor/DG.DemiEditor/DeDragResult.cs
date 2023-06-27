namespace DG.DemiEditor
{
	public struct DeDragResult
	{
		public DeDragResultType outcome;

		public int movedFromIndex;

		public int movedToIndex;

		public DeDragResult(DeDragResultType outcome, int movedFromIndex = -1, int movedToIndex = -1)
		{
			this.outcome = outcome;
			this.movedFromIndex = movedFromIndex;
			this.movedToIndex = movedToIndex;
		}
	}
}
