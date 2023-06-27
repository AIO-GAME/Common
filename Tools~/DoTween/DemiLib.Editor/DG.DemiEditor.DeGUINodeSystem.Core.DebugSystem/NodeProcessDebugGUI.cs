using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core.DebugSystem
{
	internal static class NodeProcessDebugGUI
	{
		private static class Styles
		{
			public static GUIStyle fpsLabel;

			static Styles()
			{
				fpsLabel = new GUIStyle(GUI.skin.label).Add(TextAnchor.MiddleLeft, Color.white, Format.RichText).Padding(0).Margin(0);
			}
		}

		private static readonly string[] _LabelsTxts = new string[3];

		private static readonly Rect[] _LabelsRects = new Rect[3];

		public static void Draw(NodeProcessDebug debug, Rect processArea)
		{
			if (Event.current.type != EventType.Repaint)
			{
				return;
			}
			_LabelsTxts[0] = "<b><color=#ff0000>NodeProcess DEBUG</color></b>";
			_LabelsTxts[1] = $"<b>Panning Avrg <color=#4290f5>FPS</color> (Layout/Repaint/Layout+Repaint):</b> <color=#ffd845>{debug.panningData.avrgFps_Layout:N2}</color> / <color=#ffd845>{debug.panningData.avrgFps_Repaint:N2}</color> / <color=#ffd845>{debug.panningData.avrgFps_LayoutAndRepaint:N2}</color>";
			_LabelsTxts[2] = $"<b>Panning Avrg <color=#4290f5>MS</color> (Layout/Repaint/Layout+Repaint):</b> <color=#ffd845>{debug.panningData.avrgDrawTime_Layout:N2}</color> / <color=#ffd845>{debug.panningData.avrgDrawTime_Repaint:N2}</color> / <color=#ffd845>{debug.panningData.avrgDrawTime_LayoutAndRepaint:N2}</color>";
			Rect rect = new Rect(processArea.x, processArea.y, 0f, 12f);
			for (int i = 0; i < _LabelsTxts.Length; i++)
			{
				Vector2 vector = Styles.fpsLabel.CalcSize(new GUIContent(_LabelsTxts[i]));
				_LabelsRects[i] = new Rect(rect.x + 6f, rect.yMax - 6f, vector.x, vector.y);
				if (rect.width < vector.x + 12f)
				{
					rect.width = vector.x + 12f;
				}
				if (i > 0)
				{
					rect.height += 2f;
				}
				rect.height += vector.y;
			}
			DeGUI.DrawColoredSquare(rect, new Color(0f, 0f, 0f, 0.8f));
			for (int j = 0; j < _LabelsTxts.Length; j++)
			{
				GUI.Label(_LabelsRects[j], _LabelsTxts[j], Styles.fpsLabel);
			}
		}
	}
}
