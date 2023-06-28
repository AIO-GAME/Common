using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowCore
{
	public abstract class CoreDraggablePopupWindow : EditorWindow
	{
		private Vector2 _offset;

		public static T GetDraggableWindow<T>() where T : CoreDraggablePopupWindow
		{
			T[] array = Resources.FindObjectsOfTypeAll(typeof(T)) as T[];
			T val = ((array.Length == 0) ? null : array[0]);
			if (!(Object)val)
			{
				return ScriptableObject.CreateInstance<T>();
			}
			return val;
		}

		public void Show<T>(Rect inPosition, bool focus = true) where T : CoreDraggablePopupWindow
		{
			base.minSize = inPosition.size;
			base.position = inPosition;
			ShowPopup();
			if (focus)
			{
				Focus();
			}
		}

		public virtual void OnGUI()
		{
			Event current = Event.current;
			if (current.button == 0 && current.type == EventType.MouseDown)
			{
				_offset = base.position.position - GUIUtility.GUIToScreenPoint(current.mousePosition);
			}
			if (current.button == 0 && current.type == EventType.MouseDrag)
			{
				Vector2 vector = GUIUtility.GUIToScreenPoint(current.mousePosition);
				base.position = new Rect(vector + _offset, base.position.size);
			}
		}
	}
}
