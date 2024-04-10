// using System;
// using System.Linq;
// using System.Reflection;
// using UnityEditor;
// using UnityEngine;
//
// namespace AIO.UEditor
// {
//     public static class EditorButtonExtensions
//     {
//         public static void DrawEasyButtons(this UnityEditor.Editor editor)
//         {
//             var methods = editor.target.GetType()
//                 .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
//                 .Where(m => m.GetParameters().Length == 0);
//             foreach (var method in methods)
//             {
//                 var ba = (EditorButtonAttribute)Attribute.GetCustomAttribute(method, typeof(EditorButtonAttribute));
//
//                 if (ba == null) continue;
//                 var wasEnabled = GUI.enabled;
//                 GUI.enabled = ba.Mode == EButtonMode.AlwaysEnabled
//                               || (EditorApplication.isPlaying ? ba.Mode == EButtonMode.EnabledInPlayMode : ba.Mode == EButtonMode.DisabledInPlayMode);
//
//                 if (((int)ba.Spacing & (int)EButtonSpacing.Before) != 0) GUILayout.Space(10);
//
//                 // Draw a button which invokes the method
//                 var buttonName = string.IsNullOrEmpty(ba.Name) ? ObjectNames.NicifyVariableName(method.Name) : ba.Name;
//                 if (GUILayout.Button(buttonName))
//                 {
//                     foreach (var t in editor.targets)
//                     {
//                         method.Invoke(t, null);
//                     }
//                 }
//
//                 if (((int)ba.Spacing & (int)EButtonSpacing.After) != 0) GUILayout.Space(10);
//                 GUI.enabled = wasEnabled;
//             }
//         }
//     }
//
//     [CanEditMultipleObjects]
//     [CustomEditor(typeof(MonoBehaviourEx), true)]
//     public class ButtonMonoBehaviourEx : UnityEditor.Editor
//     {
//         public override void OnInspectorGUI()
//         {
//             this.DrawEasyButtons();
//             // Draw the rest of the inspector as usual
//             DrawDefaultInspector();
//         }
//     }
//
//     [CanEditMultipleObjects]
//     [CustomEditor(typeof(ScriptableObjectEx), true)]
//     public class ButtonScriptableObjectEx : UnityEditor.Editor
//     {
//         public override void OnInspectorGUI()
//         {
//             this.DrawEasyButtons();
//             // Draw the rest of the inspector as usual
//             DrawDefaultInspector();
//         }
//     }
// }

