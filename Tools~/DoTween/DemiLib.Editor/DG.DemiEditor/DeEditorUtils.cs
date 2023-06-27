using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor
{
	public static class DeEditorUtils
	{
		public static class List
		{
			/// <summary>
			/// Shifts an item from an index to another, without modifying the list except than by moving elements around
			/// </summary>
			public static void Shift<T>(IList<T> list, int fromIndex, int toIndex)
			{
				if (toIndex != fromIndex)
				{
					int num = fromIndex;
					T value = list[fromIndex];
					while (num > toIndex)
					{
						num--;
						list[num + 1] = list[num];
						list[num] = value;
					}
					while (num < toIndex)
					{
						num++;
						list[num - 1] = list[num];
						list[num] = value;
					}
				}
			}
		}

		public static class Array
		{
			/// <summary>
			/// Expands the given array and adds the given element as the last one
			/// </summary>
			public static void ExpandAndAdd<T>(ref T[] array, T element)
			{
				int num = array.Length;
				System.Array.Resize(ref array, num + 1);
				array[num] = element;
			}

			/// <summary>
			/// Removes the element at index from the given array, shifts everything after by -1 position and resizes the array
			/// </summary>
			public static void RemoveAtIndexAndContract<T>(ref T[] array, int index)
			{
				int num = array.Length;
				for (int i = index + 1; i < num; i++)
				{
					array[i - 1] = array[i];
				}
				System.Array.Resize(ref array, num - 1);
			}
		}

		private static readonly List<DelayedCall> _DelayedCalls = new List<DelayedCall>();

		private static MethodInfo _clearConsoleMI;

		private static readonly List<GameObject> _RootGOs = new List<GameObject>(500);

		private static readonly StringBuilder _Strb = new StringBuilder();

		private static float _editorUiScaling = -1f;

		private static MethodInfo _miGetTargetStringFromBuildTargetGroup;

		private static MethodInfo _miGetPlatformNameFromBuildTargetGroup;

		private static MethodInfo _miGetAnnotations;

		private static MethodInfo _miSetGizmoEnabled;

		private static int _miSetGizmoEnabledTotParms;

		private static MethodInfo _miSetIconEnabled;

		/// <summary>Calls the given action after the given delay</summary>
		public static DelayedCall DelayedCall(float delay, Action callback)
		{
			DelayedCall delayedCall = new DelayedCall(delay, callback);
			_DelayedCalls.Add(delayedCall);
			return delayedCall;
		}

		public static void ClearAllDelayedCalls()
		{
			foreach (DelayedCall delayedCall in _DelayedCalls)
			{
				delayedCall.Clear();
			}
			_DelayedCalls.Clear();
		}

		public static void ClearDelayedCall(DelayedCall call)
		{
			call.Clear();
			if (_DelayedCalls.IndexOf(call) != -1)
			{
				_DelayedCalls.Remove(call);
			}
		}

		/// <summary>
		/// Return the size of the editor game view, eventual extra bars excluded (meaning the true size of the game area)
		/// </summary>
		public static Vector2 GetGameViewSize()
		{
			return Handles.GetMainGameViewSize();
		}

		/// <summary>
		/// Returns a value from 1 to N (2 for 200% scaling) indicating the UI Scaling of Unity's editor.
		/// The first time this is called it will store the scaling and keep it without refreshing,
		/// since you need to restart Unity in order to apply a scaling change
		/// </summary>
		public static float GetEditorUIScaling()
		{
			if (_editorUiScaling < 0f)
			{
				PropertyInfo property = typeof(GUIUtility).GetProperty("pixelsPerPoint", BindingFlags.Static | BindingFlags.NonPublic);
				if (property != null)
				{
					_editorUiScaling = (float)property.GetValue(null, null);
				}
				else
				{
					_editorUiScaling = 1f;
				}
			}
			return _editorUiScaling;
		}

		/// <summary>
		/// Clears all logs from Unity's console
		/// </summary>
		public static void ClearConsole()
		{
			if (_clearConsoleMI == null)
			{
				Type type = Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
				if (type != null)
				{
					_clearConsoleMI = type.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
				}
				if (_clearConsoleMI == null)
				{
					return;
				}
			}
			_clearConsoleMI.Invoke(null, null);
		}

		/// <summary>
		/// Adds the given global define (if it's not already present) to all the <see cref="T:UnityEditor.BuildTargetGroup" />
		/// or only to the given <see cref="T:UnityEditor.BuildTargetGroup" />, depending on passed parameters,
		/// and returns TRUE if it was added, FALSE otherwise.<para />
		/// NOTE: when adding to all of them some legacy warnings might appear, which you can ignore.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="buildTargetGroup"><see cref="T:UnityEditor.BuildTargetGroup" />to use. Leave NULL to add to all of them.</param>
		public static bool AddGlobalDefine(string id, BuildTargetGroup? buildTargetGroup = null)
		{
			bool result = false;
			BuildTargetGroup[] array = ((!buildTargetGroup.HasValue) ? ((BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup))) : new BuildTargetGroup[1] { buildTargetGroup.Value });
			foreach (BuildTargetGroup buildTargetGroup2 in array)
			{
				if (IsValidBuildTargetGroup(buildTargetGroup2))
				{
					string scriptingDefineSymbolsForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup2);
					if (System.Array.IndexOf(scriptingDefineSymbolsForGroup.Split(';'), id) == -1)
					{
						result = true;
						scriptingDefineSymbolsForGroup = string.Concat(scriptingDefineSymbolsForGroup, (scriptingDefineSymbolsForGroup.Length > 0) ? string.Concat(";", id) : id);
						PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup2, scriptingDefineSymbolsForGroup);
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Removes the given global define (if present) from all the <see cref="T:UnityEditor.BuildTargetGroup" />
		/// or only from the given <see cref="T:UnityEditor.BuildTargetGroup" />, depending on passed parameters,
		/// and returns TRUE if it was removed, FALSE otherwise.<para />
		/// NOTE: when removing from all of them some legacy warnings might appear, which you can ignore.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="buildTargetGroup"><see cref="T:UnityEditor.BuildTargetGroup" />to use. Leave NULL to remove from all of them.</param>
		public static bool RemoveGlobalDefine(string id, BuildTargetGroup? buildTargetGroup = null)
		{
			bool result = false;
			BuildTargetGroup[] array = ((!buildTargetGroup.HasValue) ? ((BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup))) : new BuildTargetGroup[1] { buildTargetGroup.Value });
			foreach (BuildTargetGroup buildTargetGroup2 in array)
			{
				if (!IsValidBuildTargetGroup(buildTargetGroup2))
				{
					continue;
				}
				string[] array2 = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup2).Split(';');
				if (System.Array.IndexOf(array2, id) == -1)
				{
					continue;
				}
				result = true;
				_Strb.Length = 0;
				for (int j = 0; j < array2.Length; j++)
				{
					if (!(array2[j] == id))
					{
						if (_Strb.Length > 0)
						{
							_Strb.Append(';');
						}
						_Strb.Append(array2[j]);
					}
				}
				PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup2, _Strb.ToString());
			}
			_Strb.Length = 0;
			return result;
		}

		/// <summary>
		/// Returns TRUE if the given global define is present in all the <see cref="T:UnityEditor.BuildTargetGroup" />
		/// or only in the given <see cref="T:UnityEditor.BuildTargetGroup" />, depending on passed parameters.<para />
		/// </summary>
		/// <param name="id"></param>
		/// <param name="buildTargetGroup"><see cref="T:UnityEditor.BuildTargetGroup" />to use. Leave NULL to check in all of them.</param>
		public static bool HasGlobalDefine(string id, BuildTargetGroup? buildTargetGroup = null)
		{
			BuildTargetGroup[] array = ((!buildTargetGroup.HasValue) ? ((BuildTargetGroup[])Enum.GetValues(typeof(BuildTargetGroup))) : new BuildTargetGroup[1] { buildTargetGroup.Value });
			foreach (BuildTargetGroup buildTargetGroup2 in array)
			{
				if (IsValidBuildTargetGroup(buildTargetGroup2) && System.Array.IndexOf(PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup2).Split(';'), id) != -1)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Sets the gizmos icon visibility in the Scene and Game view for the given class names
		/// </summary>
		/// <param name="visible">Visibility</param>
		/// <param name="classNames">Class names (no namespace), as many as you want separated by a comma</param>
		public static void SetGizmosIconVisibility(bool visible, params string[] classNames)
		{
			if (!StoreAnnotationsReflectionMethods())
			{
				return;
			}
			int num = (visible ? 1 : 0);
			foreach (object item in (IEnumerable)_miGetAnnotations.Invoke(null, null))
			{
				Type type = item.GetType();
				FieldInfo field = type.GetField("classID", BindingFlags.Instance | BindingFlags.Public);
				FieldInfo field2 = type.GetField("scriptClass", BindingFlags.Instance | BindingFlags.Public);
				if (field == null || field2 == null)
				{
					continue;
				}
				string text = (string)field2.GetValue(item);
				bool flag = false;
				for (int i = 0; i < classNames.Length; i++)
				{
					if (!(classNames[i] != text))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					int num2 = (int)field.GetValue(item);
					if (_miSetGizmoEnabledTotParms == 4)
					{
						_miSetGizmoEnabled.Invoke(null, new object[4] { num2, text, num, true });
					}
					else
					{
						_miSetGizmoEnabled.Invoke(null, new object[3] { num2, text, num });
					}
					_miSetIconEnabled.Invoke(null, new object[3] { num2, text, num });
				}
			}
		}

		/// <summary>
		/// Sets the gizmos icon visibility in the Scene and Game view for all custom icons
		/// (for example icons created with HOTools)
		/// </summary>
		/// <param name="visible">Visibility</param>
		public static void SetGizmosIconVisibilityForAllCustomIcons(bool visible)
		{
			if (!StoreAnnotationsReflectionMethods())
			{
				return;
			}
			int num = (visible ? 1 : 0);
			foreach (object item in (IEnumerable)_miGetAnnotations.Invoke(null, null))
			{
				Type type = item.GetType();
				FieldInfo field = type.GetField("classID", BindingFlags.Instance | BindingFlags.Public);
				FieldInfo field2 = type.GetField("scriptClass", BindingFlags.Instance | BindingFlags.Public);
				if (field == null || field2 == null)
				{
					continue;
				}
				int num2 = (int)field.GetValue(item);
				if (num2 == 114)
				{
					string text = (string)field2.GetValue(item);
					if (_miSetGizmoEnabledTotParms == 4)
					{
						_miSetGizmoEnabled.Invoke(null, new object[4] { num2, text, num, true });
					}
					else
					{
						_miSetGizmoEnabled.Invoke(null, new object[3] { num2, text, num });
					}
					_miSetIconEnabled.Invoke(null, new object[3] { num2, text, num });
				}
			}
		}

		/// <summary>
		/// Returns all components of type T in the currently open scene, or NULL if none could be found.<para />
		/// If you're on Unity 5 or later, and have <code>DeEditorTools</code>, use <code>DeEditorToolsUtils.FindAllComponentsOfType</code>
		/// instead, which is more efficient.
		/// </summary>
		public static List<T> FindAllComponentsOfType<T>() where T : Component
		{
			if (!(Resources.FindObjectsOfTypeAll(typeof(GameObject)) is GameObject[] array))
			{
				return null;
			}
			List<T> list = null;
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if (gameObject.hideFlags == HideFlags.NotEditable || gameObject.hideFlags == HideFlags.HideAndDontSave)
				{
					continue;
				}
				T[] componentsInChildren = gameObject.GetComponentsInChildren<T>();
				if (componentsInChildren.Length != 0)
				{
					if (list == null)
					{
						list = new List<T>();
					}
					T[] array3 = componentsInChildren;
					foreach (T item in array3)
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		private static bool IsValidBuildTargetGroup(BuildTargetGroup group)
		{
			if (group == BuildTargetGroup.Unknown)
			{
				return false;
			}
			if (_miGetTargetStringFromBuildTargetGroup == null)
			{
				_miGetTargetStringFromBuildTargetGroup = Type.GetType("UnityEditor.Modules.ModuleManager, UnityEditor.dll").GetMethod("GetTargetStringFromBuildTargetGroup", BindingFlags.Static | BindingFlags.NonPublic);
				_miGetPlatformNameFromBuildTargetGroup = typeof(PlayerSettings).GetMethod("GetPlatformName", BindingFlags.Static | BindingFlags.NonPublic);
			}
			string value = (string)_miGetTargetStringFromBuildTargetGroup.Invoke(null, new object[1] { group });
			string value2 = (string)_miGetPlatformNameFromBuildTargetGroup.Invoke(null, new object[1] { group });
			if (string.IsNullOrEmpty(value))
			{
				return !string.IsNullOrEmpty(value2);
			}
			return true;
		}

		private static bool StoreAnnotationsReflectionMethods()
		{
			if (_miGetAnnotations != null)
			{
				return true;
			}
			Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.AnnotationUtility");
			if (type == null)
			{
				return false;
			}
			_miGetAnnotations = type.GetMethod("GetAnnotations", BindingFlags.Static | BindingFlags.NonPublic);
			_miSetGizmoEnabled = type.GetMethod("SetGizmoEnabled", BindingFlags.Static | BindingFlags.NonPublic);
			if (_miSetGizmoEnabled != null)
			{
				_miSetGizmoEnabledTotParms = _miSetGizmoEnabled.GetParameters().Length;
			}
			_miSetIconEnabled = type.GetMethod("SetIconEnabled", BindingFlags.Static | BindingFlags.NonPublic);
			return true;
		}
	}
}
