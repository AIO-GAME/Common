using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
	/// <summary>
	/// One per <see cref="T:DG.DemiEditor.DeGUINodeSystem.NodeProcess" />.
	/// Partially independent, mainly controlled by process.
	/// </summary>
	public class InteractionManager
	{
		public enum State
		{
			Inactive,
			Panning,
			DrawingSelection,
			DraggingNodes,
			DraggingConnector,
			ContextClick,
			DoubleClick
		}

		public enum ReadyFor
		{
			Unset,
			Panning,
			DrawingSelection,
			DraggingNodes,
			DraggingConnector
		}

		public enum TargetType
		{
			None,
			Background,
			Node
		}

		public enum NodeTargetType
		{
			None,
			DraggableArea,
			NonDraggableArea
		}

		private struct MouseSnapshot
		{
			public float time;

			public TargetType mouseTargetType;

			public string targetNodeId;

			public MouseSnapshot(float time, TargetType mouseTargetType, string targetNodeId)
			{
				this.time = time;
				this.mouseTargetType = mouseTargetType;
				this.targetNodeId = targetNodeId;
			}

			public void Reset()
			{
				time = 0f;
				mouseTargetType = TargetType.None;
				targetNodeId = null;
			}
		}

		private const float _DoubleClickTime = 0.4f;

		internal static readonly float MinDragStartupDistance = 10f;

		private readonly NodeProcess _process;

		private bool _isReadyToDragNodes;

		private bool _isDraggingNodes;

		private MouseCursor _currMouseCursor;

		private MouseSnapshot _lastLMBUpSnapshot;

		public State state { get; private set; }

		public ReadyFor readyForState { get; private set; }

		/// <summary>TRUE when read-to or dragging nodes</summary>
		public bool isDraggingNodes
		{
			get
			{
				if (!_isReadyToDragNodes)
				{
					return _isDraggingNodes;
				}
				return true;
			}
		}

		public TargetType mouseTargetType { get; private set; }

		public NodeTargetType nodeTargetType { get; private set; }

		public IEditorGUINode targetNode { get; internal set; }

		public Rect targetNodeConnectorArea { get; internal set; }

		public int targetNodeConnectorAreaIndex { get; internal set; }

		public Vector2 mousePositionOnLMBPress { get; internal set; }

		public bool mouseTargetIsLocked
		{
			get
			{
				State state = this.state;
				if (state == State.Panning || (uint)(state - 3) <= 1u)
				{
					return true;
				}
				ReadyFor readyFor = readyForState;
				if (readyFor == ReadyFor.Panning || (uint)(readyFor - 3) <= 1u)
				{
					return true;
				}
				return false;
			}
		}

		public InteractionManager(NodeProcess process)
		{
			_process = process;
		}

		/// <summary>Returns TRUE if the given node is currently being dragged</summary>
		public bool IsDragging(IEditorGUINode node)
		{
			if (state == State.DraggingNodes)
			{
				return targetNode == node;
			}
			return false;
		}

		internal void Reset()
		{
			SetState(State.Inactive, allowRepaint: false);
			mouseTargetType = TargetType.None;
			nodeTargetType = NodeTargetType.None;
			targetNode = null;
			targetNodeConnectorAreaIndex = 0;
			_currMouseCursor = MouseCursor.Arrow;
		}

		internal void SetState(State toState, bool allowRepaint = true)
		{
			State state = this.state;
			this.state = toState;
			readyForState = ReadyFor.Unset;
			_isReadyToDragNodes = false;
			if (this.state == State.DraggingNodes)
			{
				_isDraggingNodes = true;
			}
			else
			{
				_isDraggingNodes = false;
			}
			if (allowRepaint && (state == State.Panning || state == State.DraggingNodes))
			{
				_process.editor.Repaint();
			}
		}

		internal void SetReadyFor(ReadyFor value)
		{
			readyForState = value;
			if (readyForState == ReadyFor.DraggingNodes)
			{
				_isReadyToDragNodes = true;
			}
			else
			{
				_isReadyToDragNodes = false;
			}
		}

		internal void SetMouseTargetType(TargetType targetType, NodeTargetType nodeTargetType = NodeTargetType.None)
		{
			mouseTargetType = targetType;
			this.nodeTargetType = nodeTargetType;
		}

		internal bool EvaluateMouseUp()
		{
			if (Event.current.button != 0)
			{
				_lastLMBUpSnapshot.Reset();
				return false;
			}
			if (_lastLMBUpSnapshot.time > 0f && Time.realtimeSinceStartup - _lastLMBUpSnapshot.time <= 0.4f && _lastLMBUpSnapshot.mouseTargetType == mouseTargetType && (mouseTargetType != TargetType.Node || targetNode.id == _lastLMBUpSnapshot.targetNodeId))
			{
				_lastLMBUpSnapshot.Reset();
				return true;
			}
			_lastLMBUpSnapshot = new MouseSnapshot(Time.realtimeSinceStartup, mouseTargetType, (targetNode == null) ? null : targetNode.id);
			return false;
		}

		/// <summary>
		/// Returns TRUE if a repaint is required
		/// </summary>
		/// <returns></returns>
		internal bool Update()
		{
			MouseCursor currMouseCursor = _currMouseCursor;
			switch (state)
			{
			case State.Panning:
				_currMouseCursor = MouseCursor.Pan;
				break;
			case State.DrawingSelection:
				switch (_process.selection.selectionMode)
				{
				case SelectionManager.Mode.Add:
					_currMouseCursor = MouseCursor.ArrowPlus;
					break;
				case SelectionManager.Mode.Subtract:
					_currMouseCursor = MouseCursor.ArrowMinus;
					break;
				}
				break;
			case State.DraggingNodes:
				_currMouseCursor = MouseCursor.MoveArrow;
				break;
			default:
				_currMouseCursor = MouseCursor.Arrow;
				break;
			}
			if (_currMouseCursor != 0)
			{
				EditorGUIUtility.AddCursorRect(_process.relativeArea, _currMouseCursor);
			}
			return _currMouseCursor != currMouseCursor;
		}
	}
}
