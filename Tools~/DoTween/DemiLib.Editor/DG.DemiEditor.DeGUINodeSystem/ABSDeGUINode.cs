using DG.DemiLib;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
	/// <summary>
	/// Abstract dynamic class used for every node of the same type
	/// (meaning there is only a single recycled instance for all same-type nodes)
	/// </summary>
	public abstract class ABSDeGUINode
	{
		internal NodeProcess process;

		/// <summary>Used to fill <see cref="T:DG.DemiEditor.DeGUINodeSystem.NodeGUIData" /></summary>
		protected internal abstract NodeGUIData GetAreas(Vector2 position, IEditorGUINode iNode);

		/// <summary>Called when the node needs to be drawn</summary>
		protected internal abstract void OnGUI(NodeGUIData nodeGuiData, IEditorGUINode iNode);
	}
}
