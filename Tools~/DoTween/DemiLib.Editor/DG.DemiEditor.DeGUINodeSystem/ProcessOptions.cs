using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem
{
	public class ProcessOptions
	{
		public enum EvidenceEndNodesMode
		{
			None,
			Icon,
			Invasive
		}

		public enum MinimapResolution
		{
			Normal,
			Small,
			Big
		}

		public bool allowDeletion = true;

		public bool allowCopyPaste = true;

		public bool drawBackgroundGrid = true;

		public Texture2D gridTextureOverride;

		public bool forceDarkSkin;

		public bool evidenceSelectedNodes = true;

		public bool evidenceSelectedNodesArea = true;

		public Color evidenceSelectedNodesColor = new Color(0.13f, 0.48f, 0.91f);

		public EvidenceEndNodesMode evidenceEndNodes = EvidenceEndNodesMode.Icon;

		public int evidenceEndNodesBackgroundBorder = 26;

		public Color evidenceEndNodesBackgroundColor = new Color(1f, 0f, 0f, 0.5f);

		public float connectorsThickness = 3f;

		public bool connectorsShadow = true;

		public bool showMinimap = true;

		public int minimapMaxSize = 150;

		public MinimapResolution minimapResolution;

		public bool minimapEvidenceEndNodes = true;

		public bool minimapClickToGoto = true;

		public bool mouseWheelScalesGUI = true;

		public float[] guiScaleValues = new float[6] { 1f, 0.8f, 0.6f, 0.4f, 0.2f, 0.1f };

		public bool debug_showFps;
	}
}
