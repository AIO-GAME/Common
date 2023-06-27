using UnityEngine.Events;

namespace DG.DemiEditor
{
	public static class UnityEventExtensions
	{
		/// <summary>
		/// Returns a clone of the event
		/// </summary>
		public static UnityEvent Clone(this UnityEvent unityEvent)
		{
			return DeEditorReflectionUtils.DeepCopy(unityEvent);
		}
	}
}
