using System.Collections.Generic;
using DG.DemiLib;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
	public class NodeProcessScope<T> : DeScope where T : class, IEditorGUINode, new()
	{
		private NodeProcess _process;

		/// <summary>
		/// Use this to encapsulate node GUI operations.<para />
		/// Automatically manages various operations (press F1 to see them).<para />
		/// Sets <code>GUI.changed</code> to TRUE if the area is panned, a node is dragged, controlNodes change sorting or are deleted.<para />
		/// Wraps all content inside a GUILayout Area (nodeArea).
		/// </summary>
		/// <param name="process">The <see cref="T:DG.DemiEditor.DeGUINodeSystem.NodeProcess" /> to use</param>
		/// <param name="nodeArea">Area within which the nodes will be drawn</param>
		/// <param name="refAreaShift">Area shift (caused by dragging)</param>
		/// <param name="controlNodes">This list will be sorted based on current node draw order,
		/// and changed in case one of its nodes is deleted.<para />
		/// <code>IMPORTANT:</code> this list should be part of your serialized class (MonoBehaviour or ScriptableObject),
		/// so it will be stored as a reference and modifying one will modify the other.<para />
		/// Usually you want to pass all nodes to this except the eventual start node (or nodes that can't be sorted nor deleted).</param>
		public NodeProcessScope(NodeProcess process, Rect nodeArea, ref Vector2 refAreaShift, IList<T> controlNodes)
		{
			_process = process;
			_process.BeginGUI(nodeArea, ref refAreaShift, controlNodes);
		}

		protected override void CloseScope()
		{
			_process.EndGUI();
			_process = null;
		}
	}
}
