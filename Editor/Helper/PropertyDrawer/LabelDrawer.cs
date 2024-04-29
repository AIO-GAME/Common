//
// using System.Text.RegularExpressions;
// using AIO.Unity.Runtime;
// using UnityEditor;
// using UnityEngine;
//
// namespace AIO.UEditor
// {
//     /// <summary>
//     /// 只支持 数值类型
//     /// </summary>
//     [CustomPropertyDrawer(typeof(LabelRangeAttribute))]
//     [CustomPropertyDrawer(typeof(LabelRangeIntAttribute))]
//     public class LabelRangeDrawer : LabelDrawer
//     {
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             switch (property.propertyType)
//             {
//                 case SerializedPropertyType.Float:
//                 {
//                     if (!(attribute is LabelRangeAttribute attributes))
//                     {
//                         base.OnGUI(position, property, label);
//                         return;
//                     }
//
//                     var name = attributes.name ?? "";
//                     property.floatValue = EditorGUI.Slider(position, name, property.floatValue, attributes.min, attributes.max);
//                     return;
//                 }
//                 case SerializedPropertyType.Integer:
//                 {
//                     if (!(attribute is LabelRangeIntAttribute attributes))
//                     {
//                         base.OnGUI(position, property, label);
//                         return;
//                     }
//
//                     var name = attributes.name ?? "";
//                     property.floatValue  = EditorGUI.IntSlider(position, name, property.intValue, attributes.min, attributes.max);
//                     return;
//                 }
//                 default:
//                     base.OnGUI(position, property, label);
//                     return;
//             }
//         }
//     }
//
//     /// <summary>
//     /// 使字段在Inspector中显示自定义的名称。
//     /// 支持 Enum
//     /// 支持 Boolean
//     /// 支持 Integer
//     /// 支持 String
//     /// 支持 Class
//     /// ---------
//     /// 不支持 数组类型 (Odin 插件冲突)
//     /// </summary>
//     [CustomPropertyDrawer(typeof(LabelAttribute))]
//     public class LabelDrawer : PropertyDrawer
//     {
//         private GUIContent Label;
//
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             switch (property.propertyType)
//             {
//                 case SerializedPropertyType.Enum:
//                 {
//                     if (Label == null && attribute is LabelAttribute attributes)
//                     {
//                         Label = new GUIContent(attributes.name ?? "");
//                         var isElement = Regex.IsMatch(property.displayName, "Element \\d+");
//                         if (isElement) Label.text = property.displayName;
//                     }
//
//                     EditorGUI.BeginChangeCheck();
//                     var index = DrawerUtils.DrawEnum<LabelAttribute>(fieldInfo, position, property, Label ?? label);
//                     if (EditorGUI.EndChangeCheck() && index != -1) property.enumValueIndex = index;
//                     break;
//                 }
//                 case SerializedPropertyType.ObjectReference:
//                 case SerializedPropertyType.String:
//                 case SerializedPropertyType.Integer:
//                 case SerializedPropertyType.Boolean:
//                 default:
//                 {
//                     if (Label == null && attribute is LabelAttribute attributes)
//                         Label = new GUIContent(attributes.name ?? "");
//                     EditorGUI.PropertyField(position, property, Label ?? label);
//                     break;
//                 }
//             }
//         }
//     }
// }

