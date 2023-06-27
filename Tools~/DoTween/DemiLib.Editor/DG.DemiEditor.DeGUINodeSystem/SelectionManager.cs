using System.Collections.Generic;
using DG.DemiLib;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
	public class SelectionManager
	{
		public enum Mode
		{
			Default,
			Add,
			Subtract
		}

		public readonly List<IEditorGUINode> selectedNodes = new List<IEditorGUINode>();

		internal readonly List<IEditorGUINode> selectedNodesSnapshot = new List<IEditorGUINode>();

		internal Rect selectionRect;

		/// <summary>
		/// Set automatically when a selection ends up selecting a single node,
		/// reset when deselecting all nodes, selecting multiple nodes, or resetting the <see cref="T:DG.DemiEditor.DeGUINodeSystem.NodeProcess" />
		/// </summary>
		public IEditorGUINode focusedNode { get; private set; }

		public Mode selectionMode { get; internal set; }

		public bool IsSelected(IEditorGUINode node)
		{
			return selectedNodes.Contains(node);
		}

		public void Deselect(IEditorGUINode node)
		{
			selectedNodes.Remove(node);
		}

		/// <summary>
		/// Returns TRUE if something was actually deselected, FALSE if there were no selected nodes
		/// </summary>
		/// <returns></returns>
		public bool DeselectAll()
		{
			if (selectedNodes.Count == 0)
			{
				return false;
			}
			selectedNodes.Clear();
			focusedNode = null;
			return true;
		}

		public void Select(IEditorGUINode node, bool keepExistingSelections)
		{
			if (!keepExistingSelections)
			{
				selectedNodes.Clear();
			}
			if (!selectedNodes.Contains(node))
			{
				selectedNodes.Add(node);
			}
			focusedNode = ((selectedNodes.Count == 1) ? node : null);
		}

		public void Select(List<IEditorGUINode> nodes, bool keepExistingSelections)
		{
			focusedNode = null;
			if (!keepExistingSelections)
			{
				selectedNodes.Clear();
			}
			foreach (IEditorGUINode node in nodes)
			{
				if (!selectedNodes.Contains(node))
				{
					selectedNodes.Add(node);
				}
			}
			focusedNode = ((selectedNodes.Count == 1) ? selectedNodes[0] : null);
		}

		public T GetFocusedNode<T>() where T : IEditorGUINode
		{
			if (focusedNode != null)
			{
				return (T)focusedNode;
			}
			return default(T);
		}

		internal void Reset()
		{
			DeselectAll();
			ClearSnapshot();
			focusedNode = null;
		}

		internal void ClearSnapshot()
		{
			selectedNodesSnapshot.Clear();
		}

		internal void StoreSnapshot()
		{
			selectedNodesSnapshot.Clear();
			foreach (IEditorGUINode selectedNode in selectedNodes)
			{
				selectedNodesSnapshot.Add(selectedNode);
			}
		}
	}
}
