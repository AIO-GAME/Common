using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DG.DemiEditor
{
	public static class SerializedPropertyExtensions
	{
		/// <summary>
		/// Returns the value of the given property (works like a cast to type).
		/// <para>
		/// Improved from HiddenMonk's functions (http://answers.unity3d.com/questions/627090/convert-serializedproperty-to-custom-class.html)
		/// </para>
		/// </summary>
		public static T CastTo<T>(this SerializedProperty property)
		{
			object obj = property.serializedObject.targetObject;
			string[] array = property.propertyPath.Split('.');
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				string text = array[i];
				if (text == "Array")
				{
					if (i >= num - 2)
					{
						int indexInArray = property.GetIndexInArray();
						IList<T> list = (IList<T>)obj;
						if (list.Count - 1 >= indexInArray)
						{
							return list[indexInArray];
						}
						return default(T);
					}
					string text2 = array[i + 1];
					text2 = text2.Substring(text2.IndexOf("[") + 1);
					text2 = text2.Substring(0, text2.Length - 1);
					obj = ((IList)obj)[Convert.ToInt32(text2)];
					i++;
				}
				else
				{
					obj = GetFieldOrPropertyValue<object>(text, obj);
				}
			}
			return (T)obj;
		}

		/// <summary>
		/// Returns TRUE if this property is inside an array
		/// </summary>
		public static bool IsArrayElement(this SerializedProperty property)
		{
			return property.propertyPath.Contains("Array");
		}

		/// <summary>
		/// Returns -1 if the property is not inside an array, otherwise returns its index inside the array
		/// </summary>
		public static int GetIndexInArray(this SerializedProperty property)
		{
			if (!property.IsArrayElement())
			{
				return -1;
			}
			int num = property.propertyPath.LastIndexOf('[') + 1;
			int length = property.propertyPath.LastIndexOf(']') - num;
			return int.Parse(property.propertyPath.Substring(num, length));
		}

		/// <summary>
		/// Returns the height of a UnityEvent serializedProperty
		/// </summary>
		public static float GetUnityEventHeight(this SerializedProperty property)
		{
			if (property.propertyType != SerializedPropertyType.Generic)
			{
				return 18f;
			}
			ReorderableList reorderableList = new ReorderableList(property.serializedObject, property.FindPropertyRelative("m_PersistentCalls.m_Calls"), draggable: false, displayHeader: true, displayAddButton: true, displayRemoveButton: true);
			return reorderableList.GetHeight() + (float)(Mathf.Max(1, reorderableList.count) * 26);
		}

		/// <summary>
		/// Uses code from FlaShG's GitMerge: https://github.com/FlaShG/GitMerge-for-Unity/blob/master/Editor/SerializedPropertyExtensions.cs
		/// </summary>
		public static object GetValue(this SerializedProperty p)
		{
			switch (p.propertyType)
			{
			case SerializedPropertyType.AnimationCurve:
				return p.animationCurveValue;
			case SerializedPropertyType.ArraySize:
				return p.intValue;
			case SerializedPropertyType.Boolean:
				return p.boolValue;
			case SerializedPropertyType.Bounds:
				return p.boundsValue;
			case SerializedPropertyType.Character:
				return p.stringValue;
			case SerializedPropertyType.Color:
				return p.colorValue;
			case SerializedPropertyType.Enum:
				return p.enumValueIndex;
			case SerializedPropertyType.Float:
				return p.floatValue;
			case SerializedPropertyType.Generic:
				Debug.LogWarning("Get/Set of Generic SerializedProperty not supported");
				return null;
			case SerializedPropertyType.Gradient:
				Debug.LogWarning("Get/Set of Gradient SerializedProperty not supported");
				return 0;
			case SerializedPropertyType.Integer:
				return p.intValue;
			case SerializedPropertyType.LayerMask:
				return p.intValue;
			case SerializedPropertyType.ObjectReference:
				return p.objectReferenceValue;
			case SerializedPropertyType.Quaternion:
				return p.quaternionValue;
			case SerializedPropertyType.Rect:
				return p.rectValue;
			case SerializedPropertyType.String:
				return p.stringValue;
			case SerializedPropertyType.Vector2:
				return p.vector2Value;
			case SerializedPropertyType.Vector3:
				return p.vector3Value;
			case SerializedPropertyType.Vector4:
				return p.vector4Value;
			default:
				return 0;
			}
		}

		/// <summary>
		/// Uses code from FlaShG's GitMerge: https://github.com/FlaShG/GitMerge-for-Unity/blob/master/Editor/SerializedPropertyExtensions.cs
		/// </summary>
		public static void SetValue(this SerializedProperty p, object value)
		{
			switch (p.propertyType)
			{
			case SerializedPropertyType.AnimationCurve:
				p.animationCurveValue = value as AnimationCurve;
				break;
			case SerializedPropertyType.ArraySize:
				p.intValue = (int)value;
				break;
			case SerializedPropertyType.Boolean:
				p.boolValue = (bool)value;
				break;
			case SerializedPropertyType.Bounds:
				p.boundsValue = (Bounds)value;
				break;
			case SerializedPropertyType.Character:
				p.stringValue = (string)value;
				break;
			case SerializedPropertyType.Color:
				p.colorValue = (Color)value;
				break;
			case SerializedPropertyType.Enum:
				p.enumValueIndex = (int)value;
				break;
			case SerializedPropertyType.Float:
				p.floatValue = (float)value;
				break;
			case SerializedPropertyType.Generic:
				Debug.LogWarning("Get/Set of Generic SerializedProperty not supported");
				break;
			case SerializedPropertyType.Gradient:
				Debug.LogWarning("Get/Set of Gradient SerializedProperty not supported");
				break;
			case SerializedPropertyType.Integer:
				p.intValue = (int)value;
				break;
			case SerializedPropertyType.LayerMask:
				p.intValue = (int)value;
				break;
			case SerializedPropertyType.ObjectReference:
				p.objectReferenceValue = value as UnityEngine.Object;
				break;
			case SerializedPropertyType.Quaternion:
				p.quaternionValue = (Quaternion)value;
				break;
			case SerializedPropertyType.Rect:
				p.rectValue = (Rect)value;
				break;
			case SerializedPropertyType.String:
				p.stringValue = (string)value;
				break;
			case SerializedPropertyType.Vector2:
				p.vector2Value = (Vector2)value;
				break;
			case SerializedPropertyType.Vector3:
				p.vector3Value = (Vector3)value;
				break;
			case SerializedPropertyType.Vector4:
				p.vector4Value = (Vector4)value;
				break;
			}
		}

		private static T GetFieldOrPropertyValue<T>(string fieldName, object obj)
		{
			for (Type type = obj.GetType(); type != null; type = type.BaseType)
			{
				FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null)
				{
					return (T)field.GetValue(obj);
				}
				PropertyInfo property = type.GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (property != null)
				{
					return (T)property.GetValue(obj, null);
				}
			}
			return default(T);
		}
	}
}
