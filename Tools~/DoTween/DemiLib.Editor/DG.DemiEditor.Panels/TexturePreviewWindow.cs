using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.Panels
{
	internal class TexturePreviewWindow : EditorWindow
	{
		private const string _Title = "Texture Preview";

		private Texture2D _texture;

		private Vector2 _scrollPos;

		private void OnGUI()
		{
			if (_texture == null)
			{
				Close();
				return;
			}
			_scrollPos = GUILayout.BeginScrollView(_scrollPos);
			DeGUI.BeginGUI();
			Rect rect = new Rect(0f, 0f, _texture.width, _texture.height);
			GUILayoutUtility.GetRect(_texture.width, _texture.width, _texture.height, _texture.height);
			GUI.DrawTexture(rect, _texture, ScaleMode.StretchToFill);
			GUILayout.EndScrollView();
		}

		public static void Open(Texture2D textureToPreview)
		{
			EditorWindow.GetWindow<TexturePreviewWindow>(utility: true, "Texture Preview", focus: true)._texture = textureToPreview;
		}
	}
}
