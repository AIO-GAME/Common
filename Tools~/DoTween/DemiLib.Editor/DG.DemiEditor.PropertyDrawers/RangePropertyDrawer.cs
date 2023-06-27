using DG.DemiLib;
using UnityEditor;
using UnityEngine;

namespace DG.DemiEditor.PropertyDrawers
{
	[CustomPropertyDrawer(typeof(Range))]
	[CustomPropertyDrawer(typeof(IntRange))]
	public class RangePropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			property.Next(enterChildren: true);
			SerializedProperty serializedProperty = property.Copy();
			property.Next(enterChildren: true);
			SerializedProperty serializedProperty2 = property.Copy();
			bool flag = serializedProperty.type == "int";
			float num = 0f;
			int num2 = 0;
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, label);
			Rect rect = new Rect(position.x, position.y, position.width * 0.5f, position.height);
			Rect rect2 = new Rect(rect.xMax + 3f, rect.y, rect.width - 3f, rect.height);
			EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
			float labelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 28f;
			GUIContent gUIContent = new GUIContent("Min");
			EditorGUI.BeginProperty(rect, gUIContent, serializedProperty);
			EditorGUI.BeginChangeCheck();
			if (flag)
			{
				num2 = EditorGUI.IntField(rect, gUIContent, serializedProperty.intValue);
			}
			else
			{
				num = EditorGUI.FloatField(rect, gUIContent, serializedProperty.floatValue);
			}
			if (EditorGUI.EndChangeCheck())
			{
				GUI.changed = true;
				if (flag)
				{
					if (num2 > serializedProperty2.intValue)
					{
						num2 = serializedProperty2.intValue;
					}
					serializedProperty.intValue = num2;
				}
				else
				{
					if (num > serializedProperty2.floatValue)
					{
						num = serializedProperty2.floatValue;
					}
					serializedProperty.floatValue = num;
				}
			}
			EditorGUI.EndProperty();
			gUIContent.text = "Max";
			EditorGUI.BeginProperty(rect2, gUIContent, serializedProperty2);
			EditorGUI.BeginChangeCheck();
			if (flag)
			{
				num2 = EditorGUI.IntField(rect2, gUIContent, serializedProperty2.intValue);
			}
			else
			{
				num = EditorGUI.FloatField(rect2, gUIContent, serializedProperty2.floatValue);
			}
			if (EditorGUI.EndChangeCheck())
			{
				GUI.changed = true;
				if (flag)
				{
					if (num2 < serializedProperty2.intValue)
					{
						num2 = serializedProperty.intValue;
					}
					serializedProperty2.intValue = num2;
				}
				else
				{
					if (num < serializedProperty2.floatValue)
					{
						num = serializedProperty.floatValue;
					}
					serializedProperty2.floatValue = num;
				}
			}
			EditorGUI.EndProperty();
			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUI.EndProperty();
		}
	}
}
