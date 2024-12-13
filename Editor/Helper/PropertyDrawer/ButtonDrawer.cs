// using System;
// using System.Linq;
// using System.Reflection;
// using UnityEditor;
// using UnityEngine;
// using AIO.UEngine;
//
// namespace AIO.UEditor
// {
//     [CustomPropertyDrawer(typeof(ButtonAttribute))]
//     public class ButtonDrawer : PropertyDrawer
//     {
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             DrawEasyButtons(property.serializedObject.targetObject);
//         }
//
//         public static void DrawEasyButtons<T>(T editor) where T : UnityEngine.Object
//         {
//             var methods = editor.GetType()
//                                 .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
//                                 .Where(m => m.GetParameters().Length == 0);
//             foreach (var method in methods)
//             {
//                 var ba = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
//
//                 if (ba == null) continue;
//                 var wasEnabled = GUI.enabled;
//                 GUI.enabled = ba.Mode == ButtonAttribute.EnableMode.Always
//                            || (EditorApplication.isPlaying
//                                   ? ba.Mode == ButtonAttribute.EnableMode.PlayMode
//                                   : ba.Mode == ButtonAttribute.EnableMode.Editor);
//
//                 if (((int)ba.Spacing & (int)ButtonAttribute.SpacingMode.Before) != 0) GUILayout.Space(10);
//
//                 // Draw a button which invokes the method
//                 var buttonName = string.IsNullOrEmpty(ba.Text) ? ObjectNames.NicifyVariableName(method.Name) : ba.Text;
//                 if (GUILayout.Button(buttonName))
//                 {
//                     method.Invoke(editor, null);
//                 }
//
//                 if (((int)ba.Spacing & (int)ButtonAttribute.SpacingMode.After) != 0) GUILayout.Space(10);
//                 GUI.enabled = wasEnabled;
//             }
//         }
//     }
//
//     // [CanEditMultipleObjects]
//     // [CustomEditor(typeof(MonoBehaviourEx), true)]
//     // public class ButtonMonoBehaviourEx : UnityEditor.Editor
//     // {
//     //     public override void OnInspectorGUI()
//     //     {
//     //         this.DrawEasyButtons();
//     //         // Draw the rest of the inspector as usual
//     //         DrawDefaultInspector();
//     //     }
//     // }
//     //
//     // [CanEditMultipleObjects]
//     // [CustomEditor(typeof(ScriptableObjectEx), true)]
//     // public class ButtonScriptableObjectEx : UnityEditor.Editor
//     // {
//     //     public override void OnInspectorGUI()
//     //     {
//     //         this.DrawEasyButtons();
//     //         // Draw the rest of the inspector as usual
//     //         DrawDefaultInspector();
//     //     }
//     // }
// }
