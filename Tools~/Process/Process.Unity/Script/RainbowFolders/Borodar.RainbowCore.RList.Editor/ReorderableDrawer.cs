using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIO.RainbowCore.RList.Editor
{
	[CustomPropertyDrawer(typeof(ReorderableAttribute))]
	internal class ReorderableDrawer : PropertyDrawer
	{
		private struct SurrogateCallback
		{
			private string property;

			internal SurrogateCallback(string property)
			{
				this.property = property;
			}

			internal void SetReference(SerializedProperty element, Object objectReference, ReorderableList list)
			{
				SerializedProperty serializedProperty = ((!string.IsNullOrEmpty(property)) ? element.FindPropertyRelative(property) : null);
				if (serializedProperty != null && serializedProperty.propertyType == SerializedPropertyType.ObjectReference)
				{
					serializedProperty.objectReferenceValue = objectReference;
				}
			}
		}

		public const string ARRAY_PROPERTY_NAME = "array";

		private static Dictionary<int, ReorderableList> lists = new Dictionary<int, ReorderableList>();

		public override bool CanCacheInspectorGUI(SerializedProperty property)
		{
			return false;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return GetList(property, base.attribute as ReorderableAttribute, "array")?.GetHeight() ?? EditorGUIUtility.singleLineHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			ReorderableList list = GetList(property, base.attribute as ReorderableAttribute, "array");
			if (list != null)
			{
				list.DoList(EditorGUI.IndentedRect(position), label);
			}
			else
			{
				GUI.Label(position, "Array must extend from ReorderableArray", EditorStyles.label);
			}
		}

		public static int GetListId(SerializedProperty property)
		{
			if (property != null)
			{
				int hashCode = property.serializedObject.targetObject.GetHashCode();
				int hashCode2 = property.propertyPath.GetHashCode();
				return ((hashCode << 5) + hashCode) ^ hashCode2;
			}
			return 0;
		}

		public static ReorderableList GetList(SerializedProperty property, string arrayPropertyName)
		{
			return GetList(property, null, GetListId(property), arrayPropertyName);
		}

		public static ReorderableList GetList(SerializedProperty property, ReorderableAttribute attrib, string arrayPropertyName)
		{
			return GetList(property, attrib, GetListId(property), arrayPropertyName);
		}

		public static ReorderableList GetList(SerializedProperty property, int id, string arrayPropertyName)
		{
			return GetList(property, null, id, arrayPropertyName);
		}

		public static ReorderableList GetList(SerializedProperty property, ReorderableAttribute attrib, int id, string arrayPropertyName)
		{
			if (property == null)
			{
				return null;
			}
			ReorderableList value = null;
			SerializedProperty serializedProperty = property.FindPropertyRelative(arrayPropertyName);
			if (serializedProperty != null && serializedProperty.isArray)
			{
				if (!lists.TryGetValue(id, out value))
				{
					if (attrib != null)
					{
						Texture elementIcon = ((!string.IsNullOrEmpty(attrib.elementIconPath)) ? AssetDatabase.GetCachedIcon(attrib.elementIconPath) : null);
						ReorderableList.ElementDisplayType elementDisplayType = (attrib.singleLine ? ReorderableList.ElementDisplayType.SingleLine : ReorderableList.ElementDisplayType.Auto);
						value = new ReorderableList(serializedProperty, attrib.add, attrib.remove, attrib.draggable, elementDisplayType, attrib.elementNameProperty, attrib.elementNameOverride, elementIcon);
						value.paginate = attrib.paginate;
						value.pageSize = attrib.pageSize;
						value.sortable = attrib.sortable;
						if (attrib.surrogateType != null)
						{
							SurrogateCallback surrogateCallback = new SurrogateCallback(attrib.surrogateProperty);
							value.surrogate = new ReorderableList.Surrogate(attrib.surrogateType, surrogateCallback.SetReference);
						}
					}
					else
					{
						value = new ReorderableList(serializedProperty, canAdd: true, canRemove: true, draggable: true);
					}
					lists.Add(id, value);
				}
				else
				{
					value.List = serializedProperty;
				}
			}
			return value;
		}
	}
}
