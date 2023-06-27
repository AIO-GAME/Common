using UnityEngine;

namespace DG.DemiEditor
{
	public static class GUIContentExtensions
	{
		public static bool HasText(this GUIContent content)
		{
			if (content != null)
			{
				return !string.IsNullOrEmpty(content.text);
			}
			return false;
		}
	}
}
