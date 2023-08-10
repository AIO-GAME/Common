// using System.Reflection;
// using UnityEditor;
// using UnityEngine;
//
// namespace AIO.UEditor
// {
//     public static class DrawerUtils
//     {
//         /// <summary>
//         /// 重新绘制枚举类型属性
//         /// </summary>
//         /// <param name="fieldInfo"></param>
//         /// <param name="position"></param>
//         /// <param name="property"></param>
//         /// <param name="label"></param>
//         public static int DrawEnum<T>(FieldInfo fieldInfo, Rect position, SerializedProperty property, GUIContent label)
//             where T : LabelAttribute
//         {
//             var type = fieldInfo.FieldType;
//             var names = property.enumNames;
//             var values = new string[names.Length];
//             while (type != null && type.IsArray) type = type.GetElementType();
//
//             for (var i = 0; i < names.Length; ++i)
//             {
//                 if (type != null)
//                 {
//                     var info = type.GetField(names[i]);
//                     var enumAttributes = (T[])info.GetCustomAttributes(typeof(T), false);
//                     values[i] = enumAttributes.Length == 0 ? names[i] : enumAttributes[0].name;
//                 }
//                 else values[i] = "";
//             }
//
//             return EditorGUI.Popup(position, label.text, property.enumValueIndex, values);
//         }
//     }
// }
