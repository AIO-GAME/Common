using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.DeGUINodeSystem.Core
{
	internal class Minimap
	{
		private class Styles
		{
			public GUIStyle visibleArea;

			public GUIStyle visibleAreaOverlay;

			private bool _initialized;

			public void Init()
			{
				if (!_initialized)
				{
					_initialized = true;
					visibleArea = DeGUI.styles.box.flat.Clone().Background(DeStylePalette.whiteSquareAlpha15);
					visibleAreaOverlay = new GUIStyle(GUI.skin.box);
				}
			}
		}

		private static readonly Styles _Styles = new Styles();

		private NodeProcess _process;

		private Texture2D _texture;

		private bool _requiresRefresh = true;

		private bool _draw;

		private Rect _area;

		private Rect _visibleArea;

		private Rect _relativeArea;

		private Rect _fullNodesArea;

		private Rect _fullZeroBasedArea;

		private Vector2 _shiftFromOriginalNodesAreaPos;

		public Minimap(NodeProcess process)
		{
			_process = process;
		}

		public void DrawButton()
		{
			Setup();
			if (!_draw || !_process.options.minimapClickToGoto)
			{
				return;
			}
			EditorGUIUtility.AddCursorRect(_area, MouseCursor.Arrow);
			if (Event.current.type == EventType.MouseDown && _area.Contains(Event.current.mousePosition))
			{
				Event.current.Use();
				JumpToMousePosition();
			}
			using (new DeGUI.ColorScope(null, null, Color.clear))
			{
				GUI.Button(_area, "");
			}
		}

		public void Draw()
		{
			if (!_draw)
			{
				return;
			}
			GUI.DrawTexture(_area, DeStylePalette.blackSquareAlpha80);
			if (_requiresRefresh || _texture == null)
			{
				_requiresRefresh = false;
				RefreshMapTexture(_fullZeroBasedArea, _shiftFromOriginalNodesAreaPos);
			}
			GUI.DrawTexture(_area, _texture, ScaleMode.StretchToFill);
			Rect position = new Rect(_area.x, _area.y, _area.width * _relativeArea.width / _fullZeroBasedArea.width, _area.height * _relativeArea.height / _fullZeroBasedArea.height);
			if (_fullNodesArea.x < _visibleArea.x)
			{
				float num = Mathf.Abs(_fullNodesArea.x);
				float num2 = num + Mathf.Max(0f, _fullNodesArea.xMax - _visibleArea.xMax);
				position.x += (_area.width - position.width) * (num / num2);
			}
			if (_fullNodesArea.y < _visibleArea.y)
			{
				float num3 = Mathf.Abs(_fullNodesArea.y);
				float num4 = num3 + Mathf.Max(0f, _fullNodesArea.yMax - _visibleArea.yMax);
				position.y += (_area.height - position.height) * (num3 / num4);
			}
			using (new DeGUI.ColorScope(null, null, new DeSkinColor(0.4f)))
			{
				GUI.Box(position, "", (_process.guiScale < 1f) ? DeGUI.styles.box.outline02 : DeGUI.styles.box.outline01);
			}
		}

		private void Setup()
		{
			if (Event.current.type != EventType.Repaint)
			{
				return;
			}
			_draw = true;
			if (_process.nodes.Count == 0)
			{
				_draw = false;
				return;
			}
			_visibleArea = new Rect(_process.relativeArea);
			_relativeArea = _visibleArea.ResetXY();
			_fullNodesArea = _process.EvaluateFullNodesArea();
			if (_fullNodesArea.width < 1f || _visibleArea.Includes(_fullNodesArea))
			{
				_draw = false;
				return;
			}
			_Styles.Init();
			int minimapMaxSize = _process.options.minimapMaxSize;
			_shiftFromOriginalNodesAreaPos = default(Vector2);
			_fullZeroBasedArea = default(Rect);
			_shiftFromOriginalNodesAreaPos = new Vector2((_fullNodesArea.x - _visibleArea.x < 0f) ? (0f - _fullNodesArea.x) : (0f - _visibleArea.x), (_fullNodesArea.y - _visibleArea.y < 0f) ? (0f - _fullNodesArea.y) : (0f - _visibleArea.y));
			_fullZeroBasedArea = new Rect(0f, 0f, Mathf.Abs(Mathf.Min(0f, _fullNodesArea.x - _visibleArea.x)) + Mathf.Max(_fullNodesArea.xMax - _visibleArea.x, _relativeArea.xMax), Mathf.Abs(Mathf.Min(0f, _fullNodesArea.y - _visibleArea.y)) + Mathf.Max(_fullNodesArea.yMax - _visibleArea.y, _relativeArea.yMax));
			float num;
			float num2;
			if (_fullZeroBasedArea.width > _fullZeroBasedArea.height)
			{
				num = minimapMaxSize;
				num2 = num * _fullZeroBasedArea.height / _fullZeroBasedArea.width;
			}
			else
			{
				num2 = minimapMaxSize;
				num = num2 * _fullZeroBasedArea.width / _fullZeroBasedArea.height;
			}
			num /= _process.guiScale;
			num2 /= _process.guiScale;
			_area = new Rect(_visibleArea.xMax - num - 3f, _visibleArea.yMax - num2 - 3f, num, num2);
		}

		public void RefreshMapTextureOnNextPass()
		{
			_requiresRefresh = true;
		}

		private void RefreshMapTexture(Rect fullZeroBasedArea, Vector2 shift)
		{
			int num = ((_process.options.minimapResolution == ProcessOptions.MinimapResolution.Big) ? 256 : ((_process.options.minimapResolution == ProcessOptions.MinimapResolution.Small) ? 64 : 128));
			if (_texture == null || _texture.width != num)
			{
				_texture = new Texture2D(num, num, TextureFormat.ARGB32, mipChain: true)
				{
					wrapMode = TextureWrapMode.Clamp,
					filterMode = FilterMode.Bilinear
				};
			}
			Color32[] array = new Color32[num * num];
			int num2 = array.Length;
			for (int i = 0; i < num2; i++)
			{
				array[i] = new Color32(0, 0, 0, 0);
			}
			_texture.SetPixels32(array);
			foreach (IEditorGUINode node in _process.nodes)
			{
				bool flag = false;
				if (_process.options.minimapEvidenceEndNodes && !_process.nodeToConnectionOptions[node].neverMarkAsEndNode)
				{
					foreach (string connectedNodesId in node.connectedNodesIds)
					{
						if (string.IsNullOrEmpty(connectedNodesId))
						{
							flag = true;
							break;
						}
					}
				}
				NodeGUIData nodeGUIData = _process.nodeToGUIData[node];
				Rect fullArea = nodeGUIData.fullArea;
				int num3 = (int)((fullArea.x + shift.x) * (float)num / fullZeroBasedArea.width);
				int num4 = (int)((fullArea.xMax + shift.x) * (float)num / fullZeroBasedArea.width);
				int num5 = (int)((fullArea.y + shift.y) * (float)num / fullZeroBasedArea.height);
				int num6 = (int)((fullArea.yMax + shift.y) * (float)num / fullZeroBasedArea.height);
				Color color = (flag ? Color.red : nodeGUIData.mainColor);
				for (int j = num3; j < num4; j++)
				{
					for (int k = num5; k < num6; k++)
					{
						_texture.SetPixel(j, num - k, color);
					}
				}
			}
			_texture.Apply();
		}

		private void JumpToMousePosition()
		{
			Vector2 vector = Event.current.mousePosition - new Vector2(_area.x, _area.y);
			float num = _fullZeroBasedArea.width / _area.width;
			Vector2 shift = _shiftFromOriginalNodesAreaPos - vector * num;
			shift += new Vector2(_visibleArea.width * 0.5f, _visibleArea.height * 0.5f);
			_process.ShiftAreaBy(shift);
		}
	}
}
