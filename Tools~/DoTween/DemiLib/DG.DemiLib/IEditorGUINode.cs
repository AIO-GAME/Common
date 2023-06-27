using System.Collections.Generic;
using UnityEngine;

namespace DG.DemiLib
{
	public interface IEditorGUINode
	{
		/// <summary>Must be univocal</summary>
		string id { get; set; }

		/// <summary>Node position in editor GUI</summary>
		Vector2 guiPosition { get; set; }

		/// <summary>Ids of all forward connected nodes. Length indicates how many forward connections are allowed.
		/// Min length represents available connections from node.</summary>
		List<string> connectedNodesIds { get; }
	}
}
