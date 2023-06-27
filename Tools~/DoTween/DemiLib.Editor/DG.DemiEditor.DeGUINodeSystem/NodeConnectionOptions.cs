using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
	public struct NodeConnectionOptions
	{
		public bool allowManualConnections;

		public ConnectionMode connectionMode;

		public ConnectorMode connectorMode;

		public Color startColor;

		public Gradient gradientColor;

		public bool neverMarkAsEndNode;

		public NodeConnectionOptions(bool allowManualConnections, ConnectorMode connectorMode = ConnectorMode.Smart)
		{
			this.allowManualConnections = allowManualConnections;
			this.connectorMode = connectorMode;
			connectionMode = ConnectionMode.Normal;
			startColor = Color.clear;
			gradientColor = null;
			neverMarkAsEndNode = false;
		}

		public NodeConnectionOptions(bool allowManualConnections, ConnectionMode connectionMode, ConnectorMode connectorMode = ConnectorMode.Smart)
		{
			this.allowManualConnections = allowManualConnections;
			this.connectionMode = connectionMode;
			this.connectorMode = connectorMode;
			startColor = Color.clear;
			gradientColor = null;
			neverMarkAsEndNode = false;
		}

		public NodeConnectionOptions(bool allowManualConnections, ConnectorMode connectorMode, Gradient gradientColor)
		{
			this.allowManualConnections = allowManualConnections;
			this.connectorMode = connectorMode;
			this.gradientColor = gradientColor;
			connectionMode = ConnectionMode.Normal;
			startColor = Color.clear;
			neverMarkAsEndNode = false;
		}

		public NodeConnectionOptions(bool allowManualConnections, ConnectionMode connectionMode, ConnectorMode connectorMode, Gradient gradientColor)
		{
			this.allowManualConnections = allowManualConnections;
			this.connectionMode = connectionMode;
			this.connectorMode = connectorMode;
			this.gradientColor = gradientColor;
			startColor = Color.clear;
			neverMarkAsEndNode = false;
		}

		public NodeConnectionOptions(bool allowManualConnections, ConnectorMode connectorMode, Color startColor, Gradient gradientColor = null)
		{
			this.allowManualConnections = allowManualConnections;
			this.connectorMode = connectorMode;
			this.startColor = startColor;
			this.gradientColor = gradientColor;
			connectionMode = ConnectionMode.Normal;
			neverMarkAsEndNode = false;
		}

		public NodeConnectionOptions(bool allowManualConnections, ConnectionMode connectionMode, ConnectorMode connectorMode, Color startColor, Gradient gradientColor = null)
		{
			this.allowManualConnections = allowManualConnections;
			this.connectionMode = connectionMode;
			this.connectorMode = connectorMode;
			this.startColor = startColor;
			this.gradientColor = gradientColor;
			neverMarkAsEndNode = false;
		}
	}
}
