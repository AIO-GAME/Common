using System.Collections.Generic;
using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core
{
	internal class NodeDragManager
	{
		public readonly Dictionary<IEditorGUINode, Vector2> nodeToFullDragPosition = new Dictionary<IEditorGUINode, Vector2>();

		private readonly NodeProcess _process;

		private readonly SnappingManager _snapper = new SnappingManager();

		private IEditorGUINode _mainNode;

		private Rect _nodeArea;

		private List<IEditorGUINode> _allNodesRef;

		private Dictionary<IEditorGUINode, NodeGUIData> _nodeToGuiDataRef;

		private List<IEditorGUINode> _draggedNodesRef;

		private bool _lastDragWasSnappedOnX;

		private bool _lastDragWasSnappedOnY;

		public NodeDragManager(NodeProcess process)
		{
			_process = process;
		}

		public void BeginDrag(IEditorGUINode mainNode, List<IEditorGUINode> draggedNodes, List<IEditorGUINode> allNodes, Dictionary<IEditorGUINode, NodeGUIData> nodeToGuiData)
		{
			_mainNode = mainNode;
			_allNodesRef = allNodes;
			_nodeToGuiDataRef = nodeToGuiData;
			_draggedNodesRef = draggedNodes;
			_lastDragWasSnappedOnX = false;
			_lastDragWasSnappedOnY = false;
			nodeToFullDragPosition.Clear();
			foreach (IEditorGUINode item in _draggedNodesRef)
			{
				nodeToFullDragPosition.Add(item, item.guiPosition);
			}
		}

		public void ApplyDrag(Vector2 delta)
		{
			Vector2 vector = nodeToFullDragPosition[_mainNode];
			_nodeArea = _nodeToGuiDataRef[_mainNode].fullArea;
			_nodeArea.x = vector.x + _process.areaShift.x;
			_nodeArea.y = vector.y + _process.areaShift.y;
			Vector2 vector2 = delta;
			_snapper.EvaluateSnapping(_mainNode, _nodeArea, _allNodesRef, _draggedNodesRef, _nodeToGuiDataRef, _process.relativeArea);
			if (_snapper.hasSnapX)
			{
				_lastDragWasSnappedOnX = true;
				vector2.x = _snapper.snapX - (_mainNode.guiPosition.x + _process.areaShift.x);
			}
			else if (_lastDragWasSnappedOnX)
			{
				Vector2 vector3 = new Vector2(vector.x - _mainNode.guiPosition.x, 0f);
				foreach (IEditorGUINode item in _draggedNodesRef)
				{
					item.guiPosition += vector3;
				}
				_lastDragWasSnappedOnX = false;
			}
			if (_snapper.hasSnapY)
			{
				_lastDragWasSnappedOnY = true;
				vector2.y = _snapper.snapY - (_mainNode.guiPosition.y + _process.areaShift.y);
			}
			else if (_lastDragWasSnappedOnY)
			{
				Vector2 vector4 = new Vector2(0f, vector.y - _mainNode.guiPosition.y);
				foreach (IEditorGUINode item2 in _draggedNodesRef)
				{
					item2.guiPosition += vector4;
				}
				_lastDragWasSnappedOnY = false;
			}
			foreach (IEditorGUINode item3 in _draggedNodesRef)
			{
				nodeToFullDragPosition[item3] += delta;
				item3.guiPosition += vector2;
			}
		}

		public void EndGUI()
		{
			float width = 2f / _process.guiScale;
			if (_snapper.showHorizontalGuide)
			{
				float y = ((_snapper.snapYPosition == 0) ? _snapper.snapY : (_snapper.snapY + _nodeArea.height));
				Vector2 vector = new Vector2(0f, y);
				Vector2 vector2 = new Vector2(_process.relativeArea.width, y);
				Handles.DrawBezier(vector, vector2, vector, vector2, Color.cyan, null, width);
			}
			if (_snapper.showVerticalGuide)
			{
				float x = ((_snapper.snapXPosition == 0) ? _snapper.snapX : (_snapper.snapX + _nodeArea.width));
				Vector2 vector3 = new Vector2(x, 0f);
				Vector2 vector4 = new Vector2(x, _process.relativeArea.height);
				Handles.DrawBezier(vector3, vector4, vector3, vector4, Color.cyan, null, width);
			}
		}
	}
}
