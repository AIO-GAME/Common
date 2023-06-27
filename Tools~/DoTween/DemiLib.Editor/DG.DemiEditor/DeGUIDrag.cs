using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Manages the dragging of GUI elements
	/// </summary>
	public static class DeGUIDrag
	{
		public static readonly Color DefaultDragColor = new Color(0.1720873f, 0.4236527f, 0.7686567f, 0.65f);

		private const float _DragDelay = 0.15f;

		private static GUIDragData _dragData;

		private static int _dragId;

		private static bool _waitingToApplyDrag;

		private static DelayedCall _dragDelayedCall;

		private static bool _dragDelayElapsed;

		private static Editor _editor;

		private static EditorWindow _editorWindow;

		/// <summary>
		/// True if a GUI element is currently being dragged
		/// </summary>
		public static bool isDragging => _dragData != null;

		/// <summary>
		/// Return the current item being dragged, or NULL if there is none
		/// </summary>
		public static object draggedItem
		{
			get
			{
				if (_dragData == null)
				{
					return null;
				}
				return _dragData.draggedItem;
			}
		}

		/// <summary>
		/// Type of current item being dragged, or NULL if there is none
		/// </summary>
		public static Type draggedItemType
		{
			get
			{
				if (_dragData == null)
				{
					return null;
				}
				return _dragData.draggedItem.GetType();
			}
		}

		/// <summary>
		/// Starting index of current item being dragged, or NULL if there is none
		/// </summary>
		public static int draggedItemOriginalIndex
		{
			get
			{
				if (_dragData == null)
				{
					return -1;
				}
				return _dragData.draggedItemIndex;
			}
		}

		/// <summary>
		/// Retrieves the eventual optional data stored via the StartDrag method
		/// </summary>
		public static object optionalDragData
		{
			get
			{
				if (_dragData == null)
				{
					return null;
				}
				return _dragData.optionalData;
			}
		}

		/// <summary>
		/// Starts a drag operation on a GUI element.
		/// </summary>
		/// <param name="editor">Reference to the current editor drawing the GUI (used when a Repaint is needed)</param>
		/// <param name="draggableList">List containing the dragged item and all other relative draggable items</param>
		/// <param name="draggedItemIndex">DraggableList index of the item being dragged</param>
		/// <param name="optionalData">Optional data that can be retrieved via the <see cref="P:DG.DemiEditor.DeGUIDrag.optionalDragData" /> static property</param>
		public static void StartDrag(Editor editor, IList draggableList, int draggedItemIndex, object optionalData = null)
		{
			DoStartDrag(-1, editor, null, draggableList, draggedItemIndex, optionalData);
		}

		/// <summary>
		/// Starts a drag operation on a GUI element.
		/// </summary>
		/// <param name="dragId">ID for this drag operation (must be the same for both StartDrag and Drag</param>
		/// <param name="editor">Reference to the current editor drawing the GUI (used when a Repaint is needed)</param>
		/// <param name="draggableList">List containing the dragged item and all other relative draggable items</param>
		/// <param name="draggedItemIndex">DraggableList index of the item being dragged</param>
		/// <param name="optionalData">Optional data that can be retrieved via the <see cref="P:DG.DemiEditor.DeGUIDrag.optionalDragData" /> static property</param>
		[Obsolete("Use overload that doesn't require dragId instead")]
		public static void StartDrag(int dragId, Editor editor, IList draggableList, int draggedItemIndex, object optionalData = null)
		{
			DoStartDrag(dragId, editor, null, draggableList, draggedItemIndex, optionalData);
		}

		/// <summary>
		/// Starts a drag operation on a GUI element.
		/// </summary>
		/// <param name="editorWindow">Reference to the current editor drawing the GUI (used when a Repaint is needed)</param>
		/// <param name="draggableList">List containing the dragged item and all other relative draggable items</param>
		/// <param name="draggedItemIndex">DraggableList index of the item being dragged</param>
		/// <param name="optionalData">Optional data that can be retrieved via the <see cref="P:DG.DemiEditor.DeGUIDrag.optionalDragData" /> static property</param>
		public static void StartDrag(EditorWindow editorWindow, IList draggableList, int draggedItemIndex, object optionalData = null)
		{
			DoStartDrag(-1, null, editorWindow, draggableList, draggedItemIndex, optionalData);
		}

		/// <summary>
		/// Starts a drag operation on a GUI element.
		/// </summary>
		/// <param name="dragId">ID for this drag operation (must be the same for both StartDrag and Drag</param>
		/// <param name="editorWindow">Reference to the current editor drawing the GUI (used when a Repaint is needed)</param>
		/// <param name="draggableList">List containing the dragged item and all other relative draggable items</param>
		/// <param name="draggedItemIndex">DraggableList index of the item being dragged</param>
		/// <param name="optionalData">Optional data that can be retrieved via the <see cref="P:DG.DemiEditor.DeGUIDrag.optionalDragData" /> static property</param>
		[Obsolete("Use overload that doesn't require dragId instead")]
		public static void StartDrag(int dragId, EditorWindow editorWindow, IList draggableList, int draggedItemIndex, object optionalData = null)
		{
			DoStartDrag(dragId, null, editorWindow, draggableList, draggedItemIndex, optionalData);
		}

		private static void DoStartDrag(int dragId, Editor editor, EditorWindow editorWindow, IList draggableList, int draggedItemIndex, object optionalData)
		{
			if (_dragData == null)
			{
				Reset();
				_editor = editor;
				_editorWindow = editorWindow;
				_dragId = dragId;
				_dragData = new GUIDragData(draggableList, draggableList[draggedItemIndex], draggedItemIndex, optionalData);
				ClearDragDelayedCall();
				_dragDelayedCall = DeEditorUtils.DelayedCall(0.15f, delegate
				{
					_dragDelayElapsed = true;
				});
			}
		}

		/// <summary>
		/// Call this after each draggable GUI block, to calculate and draw the current drag state
		/// (or complete it if the mouse was released).
		/// </summary>
		/// <param name="dragId">ID for this drag operation (must be the same for both StartDrag and Drag</param>
		/// <param name="draggableList">List containing the draggable item and all other relative draggable items</param>
		/// <param name="currDraggableItemIndex">Current index of the draggable item being drawn</param>
		/// <param name="lastGUIRect">If NULL will calculate this automatically using <see cref="M:UnityEngine.GUILayoutUtility.GetLastRect" />.
		/// Pass this if you're creating a drag between elements that don't use GUILayout</param>
		/// <param name="direction">Drag direction. You can leave it to <see cref="F:DG.DemiEditor.DeDragDirection.Auto" />
		/// unless you want to skip eventual layout calculations</param>
		[Obsolete("Use overload that doesn't require dragId instead")]
		public static DeDragResult Drag(int dragId, IList draggableList, int currDraggableItemIndex, Rect? lastGUIRect = null, DeDragDirection direction = DeDragDirection.Auto)
		{
			return Drag(draggableList, currDraggableItemIndex, DefaultDragColor, lastGUIRect, direction);
		}

		/// <summary>
		/// Call this after each draggable GUI block, to calculate and draw the current drag state
		/// (or complete it if the mouse was released).
		/// </summary>
		/// <param name="draggableList">List containing the draggable item and all other relative draggable items</param>
		/// <param name="currDraggableItemIndex">Current index of the draggable item being drawn</param>
		/// <param name="lastGUIRect">If NULL will calculate this automatically using <see cref="M:UnityEngine.GUILayoutUtility.GetLastRect" />.
		/// Pass this if you're creating a drag between elements that don't use GUILayout</param>
		/// <param name="direction">Drag direction. You can leave it to <see cref="F:DG.DemiEditor.DeDragDirection.Auto" />
		/// unless you want to skip eventual layout calculations</param>
		public static DeDragResult Drag(IList draggableList, int currDraggableItemIndex, Rect? lastGUIRect = null, DeDragDirection direction = DeDragDirection.Auto)
		{
			return Drag(draggableList, currDraggableItemIndex, DefaultDragColor, lastGUIRect, direction);
		}

		/// <summary>
		/// Call this after each draggable GUI block, to calculate and draw the current drag state
		/// (or complete it if the mouse was released).
		/// </summary>
		/// <param name="dragId">ID for this drag operation (must be the same for both StartDrag and Drag</param>
		/// <param name="draggableList">List containing the draggable item and all other relative draggable items</param>
		/// <param name="currDraggableItemIndex">Current index of the draggable item being drawn</param>
		/// <param name="dragEvidenceColor">Color to use for drag divider and selection</param>
		/// <param name="lastGUIRect">If NULL will calculate this automatically using <see cref="M:UnityEngine.GUILayoutUtility.GetLastRect" />.
		/// Pass this if you're creating a drag between elements that don't use GUILayout</param>
		/// <param name="direction">Drag direction. You can leave it to <see cref="F:DG.DemiEditor.DeDragDirection.Auto" />
		/// unless you want to skip eventual layout calculations</param>
		[Obsolete("Use overload that doesn't require dragId instead")]
		public static DeDragResult Drag(int dragId, IList draggableList, int currDraggableItemIndex, Color dragEvidenceColor, Rect? lastGUIRect = null, DeDragDirection direction = DeDragDirection.Auto)
		{
			return Drag(draggableList, currDraggableItemIndex, dragEvidenceColor, lastGUIRect, direction);
		}

		/// <summary>
		/// Call this after each draggable GUI block, to calculate and draw the current drag state
		/// (or complete it if the mouse was released).
		/// </summary>
		/// <param name="draggableList">List containing the draggable item and all other relative draggable items</param>
		/// <param name="currDraggableItemIndex">Current index of the draggable item being drawn</param>
		/// <param name="dragEvidenceColor">Color to use for drag divider and selection</param>
		/// <param name="lastGUIRect">If NULL will calculate this automatically using <see cref="M:UnityEngine.GUILayoutUtility.GetLastRect" />.
		/// Pass this if you're creating a drag between elements that don't use GUILayout</param>
		/// <param name="direction">Drag direction. You can leave it to <see cref="F:DG.DemiEditor.DeDragDirection.Auto" />
		/// unless you want to skip eventual layout calculations</param>
		public static DeDragResult Drag(IList draggableList, int currDraggableItemIndex, Color dragEvidenceColor, Rect? lastGUIRect = null, DeDragDirection direction = DeDragDirection.Auto)
		{
			if (_dragData == null || _dragData.draggableList == null || _dragData.draggableList != draggableList)
			{
				return new DeDragResult(DeDragResultType.NoDrag);
			}
			if (_waitingToApplyDrag)
			{
				if (Event.current.type == EventType.Repaint)
				{
					Event.current.type = EventType.Used;
				}
				if (Event.current.type == EventType.Used)
				{
					ApplyDrag();
				}
				return new DeDragResult(DeDragResultType.Dragging, _dragData.draggedItemIndex, _dragData.currDragIndex);
			}
			_dragData.draggableList = draggableList;
			int count = _dragData.draggableList.Count;
			if (currDraggableItemIndex == 0 && Event.current.type == EventType.Repaint)
			{
				_dragData.currDragSet = false;
			}
			if (!_dragData.currDragSet)
			{
				Rect lastRect = ((!lastGUIRect.HasValue) ? GUILayoutUtility.GetLastRect() : lastGUIRect.Value);
				if (!_dragData.hasHorizontalSet && Event.current.type == EventType.Repaint)
				{
					switch (direction)
					{
					case DeDragDirection.Auto:
						if (currDraggableItemIndex == 0)
						{
							_dragData.lastRect = lastRect;
						}
						else if (_dragData.lastRect.width > 0f)
						{
							_dragData.hasHorizontalSet = true;
							_dragData.hasHorizontalEls = !Mathf.Approximately(_dragData.lastRect.xMin, lastRect.xMin);
						}
						break;
					case DeDragDirection.Vertical:
						_dragData.hasHorizontalSet = true;
						_dragData.hasHorizontalEls = false;
						break;
					case DeDragDirection.Horizontal:
						_dragData.hasHorizontalSet = true;
						_dragData.hasHorizontalEls = true;
						break;
					}
				}
				Vector2 center = lastRect.center;
				Vector2 mousePosition = Event.current.mousePosition;
				if (currDraggableItemIndex <= count - 1 && lastRect.Contains(mousePosition) && ((_dragData.hasHorizontalEls && mousePosition.x <= center.x) || (!_dragData.hasHorizontalEls && mousePosition.y <= center.y)))
				{
					if (_dragDelayElapsed)
					{
						DeGUI.FlatDivider(_dragData.hasHorizontalEls ? new Rect(lastRect.xMin, lastRect.yMin, 5f, lastRect.height) : new Rect(lastRect.xMin, lastRect.yMin, lastRect.width, 5f), dragEvidenceColor);
					}
					_dragData.currDragIndex = currDraggableItemIndex;
					_dragData.currDragSet = true;
				}
				else if (currDraggableItemIndex <= count - 1 && lastRect.Contains(mousePosition) && ((_dragData.hasHorizontalEls && mousePosition.x > center.x) || (!_dragData.hasHorizontalEls && mousePosition.y > center.y)))
				{
					if (_dragDelayElapsed)
					{
						DeGUI.FlatDivider(_dragData.hasHorizontalEls ? new Rect(lastRect.xMax - 5f, lastRect.yMin, 5f, lastRect.height) : new Rect(lastRect.xMin, lastRect.yMax - 5f, lastRect.width, 5f), dragEvidenceColor);
					}
					_dragData.currDragIndex = currDraggableItemIndex + 1;
					_dragData.currDragSet = true;
				}
				else if (currDraggableItemIndex == 0 && !lastRect.Contains(mousePosition) && ((_dragData.hasHorizontalEls && (mousePosition.x <= lastRect.x || mousePosition.y < lastRect.y)) || (!_dragData.hasHorizontalEls && mousePosition.y <= center.y)))
				{
					if (_dragDelayElapsed)
					{
						DeGUI.FlatDivider(_dragData.hasHorizontalEls ? new Rect(lastRect.xMin, lastRect.yMin, 5f, lastRect.height) : new Rect(lastRect.xMin, lastRect.yMin, lastRect.width, 5f), dragEvidenceColor);
					}
					_dragData.currDragIndex = currDraggableItemIndex;
					_dragData.currDragSet = true;
				}
				else if (currDraggableItemIndex >= count - 1 && ((_dragData.hasHorizontalEls && (mousePosition.x > center.x || mousePosition.y > lastRect.yMax)) || (!_dragData.hasHorizontalEls && mousePosition.y > center.y)))
				{
					if (_dragDelayElapsed)
					{
						DeGUI.FlatDivider(_dragData.hasHorizontalEls ? new Rect(lastRect.xMax - 5f, lastRect.yMin, 5f, lastRect.height) : new Rect(lastRect.xMin, lastRect.yMax - 5f, lastRect.width, 5f), dragEvidenceColor);
					}
					_dragData.currDragIndex = count;
					_dragData.currDragSet = true;
				}
			}
			if (_dragData.draggedItemIndex == currDraggableItemIndex)
			{
				Color value = dragEvidenceColor;
				value.a = 0.35f;
				if (_dragDelayElapsed)
				{
					DeGUI.FlatDivider((!lastGUIRect.HasValue) ? GUILayoutUtility.GetLastRect() : lastGUIRect.Value, value);
				}
			}
			if (GUIUtility.hotControl < 1)
			{
				if (_dragDelayElapsed)
				{
					return EndDrag(applyDrag: true);
				}
				EndDrag(applyDrag: false);
				return new DeDragResult(DeDragResultType.Click);
			}
			return new DeDragResult(DeDragResultType.Dragging, _dragData.draggedItemIndex, _dragData.currDragIndex);
		}

		/// <summary>
		/// Ends the drag operations, and eventually applies the drag outcome.
		/// Returns TRUE if the position of the dragged item actually changed.
		/// Called automatically by Drag method. Use it only if you want to force the end of a drag operation.
		/// </summary>
		/// <param name="applyDrag">If TRUE applies the drag results, otherwise simply cancels the drag</param>
		public static DeDragResult EndDrag(bool applyDrag)
		{
			if (_dragData == null)
			{
				return new DeDragResult(DeDragResultType.NoDrag);
			}
			int draggedItemIndex = _dragData.draggedItemIndex;
			int movedToIndex = ((_dragData.currDragIndex > _dragData.draggedItemIndex) ? (_dragData.currDragIndex - 1) : _dragData.currDragIndex);
			if (applyDrag)
			{
				bool num = _dragData.currDragIndex < _dragData.draggedItemIndex || _dragData.currDragIndex > _dragData.draggedItemIndex + 1;
				if (Event.current.type == EventType.Repaint)
				{
					Event.current.type = EventType.Used;
				}
				else if (Event.current.type == EventType.Used)
				{
					ApplyDrag();
				}
				else
				{
					_waitingToApplyDrag = true;
				}
				return new DeDragResult(num ? DeDragResultType.Accepted : DeDragResultType.Ineffective, draggedItemIndex, movedToIndex);
			}
			Reset();
			return new DeDragResult(DeDragResultType.Canceled, draggedItemIndex, movedToIndex);
		}

		private static void ApplyDrag()
		{
			if (_dragData == null)
			{
				return;
			}
			int draggedItemIndex = _dragData.draggedItemIndex;
			int num = ((_dragData.currDragIndex > _dragData.draggedItemIndex) ? (_dragData.currDragIndex - 1) : _dragData.currDragIndex);
			if (num != draggedItemIndex)
			{
				int num2 = draggedItemIndex;
				while (num2 > num)
				{
					num2--;
					_dragData.draggableList[num2 + 1] = _dragData.draggableList[num2];
					_dragData.draggableList[num2] = _dragData.draggedItem;
				}
				while (num2 < num)
				{
					num2++;
					_dragData.draggableList[num2 - 1] = _dragData.draggableList[num2];
					_dragData.draggableList[num2] = _dragData.draggedItem;
				}
			}
			Reset();
			Repaint();
		}

		private static void Repaint()
		{
			if (_editor != null)
			{
				_editor.Repaint();
			}
			else if (_editorWindow != null)
			{
				_editorWindow.Repaint();
			}
		}

		private static void Reset()
		{
			_dragData = null;
			_dragId = -1;
			_waitingToApplyDrag = false;
			_dragDelayElapsed = false;
			ClearDragDelayedCall();
		}

		private static void ClearDragDelayedCall()
		{
			if (_dragDelayedCall != null)
			{
				DeEditorUtils.ClearDelayedCall(_dragDelayedCall);
				_dragDelayedCall = null;
			}
		}
	}
}
