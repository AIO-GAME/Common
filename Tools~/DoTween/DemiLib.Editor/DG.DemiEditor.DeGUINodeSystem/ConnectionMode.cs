namespace DG.DemiEditor.DeGUINodeSystem
{
	public enum ConnectionMode
	{
		Normal,
		/// <summary>
		/// The connectedNodesIds list will be automatically increased/decreased when adding/removing connections
		/// (otherwise connectedNodesIds will have to be increased via custom code)
		/// </summary>
		Flexible,
		/// <summary>
		/// Requires only two connectedNodesIds (no more, no less), 
		/// uses regular CTRL+Drag to connect connection 0, CTRL+SPACE+Drag to connect connection 1
		/// </summary>
		Dual,
		/// <summary>
		/// Like <see cref="F:DG.DemiEditor.DeGUINodeSystem.ConnectionMode.Normal" />, but with an extra connection as a last extra index, which is set when pressing CTRL+SPACE+Drag.
		/// Must always have at least one element in connectedNodesIds
		/// </summary>
		NormalPlus
	}
}
