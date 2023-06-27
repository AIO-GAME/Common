using System.Reflection;
using UnityEditor;

namespace DG.DemiEditor
{
	public static class DeEditorGUIUtils
	{
		private static FieldInfo _fi_lastControlId;

		/// <summary>
		/// Precisely returns the last controlId assigned to a GUI element
		/// </summary>
		public static int GetLastControlId()
		{
			if (_fi_lastControlId == null)
			{
				_fi_lastControlId = typeof(EditorGUIUtility).GetField("s_LastControlID", BindingFlags.Static | BindingFlags.NonPublic);
				if (_fi_lastControlId == null)
				{
					_fi_lastControlId = typeof(EditorGUI).GetField("lastControlID", BindingFlags.Static | BindingFlags.NonPublic);
				}
			}
			return (int)_fi_lastControlId.GetValue(null);
		}
	}
}
