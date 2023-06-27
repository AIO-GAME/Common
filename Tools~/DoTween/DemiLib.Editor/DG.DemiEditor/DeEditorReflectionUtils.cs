using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// Used code from Celtc on StackOverflow: https://stackoverflow.com/a/54044197/10151925
	/// </summary>
	public static class DeEditorReflectionUtils
	{
		/// <summary>
		/// Gets all fields from an object and its hierarchy inheritance
		/// </summary>
		public static List<FieldInfo> GetAllFields(this Type type, BindingFlags flags)
		{
			if (type == typeof(object))
			{
				return new List<FieldInfo>();
			}
			List<FieldInfo> allFields = type.BaseType.GetAllFields(flags);
			allFields.AddRange(type.GetFields(flags | BindingFlags.DeclaredOnly));
			return allFields;
		}

		/// <summary>
		/// Perform a deep copy of the class
		/// </summary>
		public static T DeepCopy<T>(T obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("Object cannot be null");
			}
			return (T)DoCopy(obj);
		}

		/// <summary>
		/// Does the copy
		/// </summary>
		private static object DoCopy(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			Type type = obj.GetType();
			if (type.IsValueType || type == typeof(string))
			{
				return obj;
			}
			if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				Array array = obj as Array;
				Array array2 = Array.CreateInstance(elementType, array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					array2.SetValue(DoCopy(array.GetValue(i)), i);
				}
				return Convert.ChangeType(array2, obj.GetType());
			}
			if (typeof(UnityEngine.Object).IsAssignableFrom(type))
			{
				return obj;
			}
			if (type.IsClass)
			{
				object obj2 = Activator.CreateInstance(obj.GetType());
				{
					foreach (FieldInfo allField in type.GetAllFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
					{
						object value = allField.GetValue(obj);
						if (value != null)
						{
							allField.SetValue(obj2, DoCopy(value));
						}
					}
					return obj2;
				}
			}
			throw new ArgumentException("Unknown type");
		}
	}
}
