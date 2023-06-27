using System.Collections;
using UnityEngine;

namespace DG.DemiEditor
{
	internal class GUIDragData
	{
		public readonly object draggedItem;

		public readonly int draggedItemIndex;

		public IList draggableList;

		public int currDragIndex = -1;

		public bool currDragSet;

		public Rect lastRect;

		public bool hasHorizontalSet;

		public bool hasHorizontalEls;

		public object optionalData;

		public GUIDragData(IList draggableList, object draggedItem, int draggedItemIndex, object optionalData)
		{
			this.draggedItem = draggedItem;
			this.draggedItemIndex = draggedItemIndex;
			this.draggableList = draggableList;
			this.optionalData = optionalData;
		}
	}
}
