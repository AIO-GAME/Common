// /*|============|*|
// |*|Author:     |*| xinan                
// |*|Date:       |*| 2024-01-11               
// |*|E-Mail:     |*| xinansky99@gmail.com
// |*|============|*/
//
// using System;
// using System.Linq;
// using System.Reflection;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UIElements;
//
// namespace AIO.UEditor
// {
//     /// <summary>
//     /// 通过GUID查找资源
//     /// </summary>
//     public class ProjectFindByGuid
//     {
//         private const BindingFlags BINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;
//
//         private static readonly Type ProjectBrowser = Type.GetType("UnityEditor.ProjectBrowser,UnityEditor.dll");
//         private static readonly FieldInfo searchFieldText = ProjectBrowser.GetField("m_SearchFieldText", BINDING_FLAGS);
//         private static readonly IMGUIContainer container = new IMGUIContainer(OnGUI);
//         private static EditorWindow projectWindow;
//
//         [InitializeOnLoadMethod]
//         public static void OnLoad()
//         {
//             AssemblyReloadEvents.afterAssemblyReload += Reload;
//             Reload();
//         }
//
//         private static void Reload()
//         {
//             EditorApplication.update -= Update;
//             EditorApplication.update += Update;
//         }
//
//         private static void Update()
//         {
//             if (EditorApplication.isCompiling)
//             {
//                 EditorApplication.update -= Update;
//                 return;
//             }
//
//
//             if (EditorWindow.focusedWindow != null && EditorWindow.focusedWindow.GetType() == ProjectBrowser)
//             {
//                 projectWindow = EditorWindow.focusedWindow;
//                 if (!projectWindow.rootVisualElement.parent.hierarchy.Children().Contains(container))
//                 {
//                     projectWindow.rootVisualElement.parent.hierarchy.Insert(1, container);
//                     container.StretchToParentWidth();
//                 }
//             }
//
//             if (projectWindow != null & !string.IsNullOrEmpty(errorMsg))
//             {
//                 projectWindow.Repaint();
//             }
//         }
//
//         private static GUIContent btnTitle;
//         private static GUIStyle btnStyle;
//         private static GUIStyle errorStyle;
//         private static string errorMsg;
//         private static double errorMsgStartTime;
//
//         private static void OnGUI()
//         {
//             if (projectWindow == null) return;
//             btnTitle = btnTitle ?? new GUIContent(" Search GUID", EditorGUIUtility.IconContent("d_search_icon").image);
//             btnStyle = btnStyle ?? new GUIStyle(GUI.skin.button);
//             
//             var calcSize = GUI.skin.button.CalcSize(btnTitle);
//
//             var rect = new Rect(new Vector2(0, 22 - calcSize.y), calcSize);
//             var xMin = 45;
//             var xRight = 389;
//             if ((int)ProjectBrowser?.GetField("m_ViewMode", BINDING_FLAGS)?.GetValue(projectWindow) == 1)
//             {
//                 xRight = 415;
//             }
//
//             if (Screen.width < xMin + xRight) rect.x = xMin;
//             else rect.x = Screen.width - xRight;
//
//             container.style.height = rect.y + rect.height;
//             var value = (string)searchFieldText.GetValue(projectWindow);
//             if (!string.IsNullOrEmpty(value))
//             {
//                 if (GUI.Button(rect, btnTitle))
//                 {
//                     var assetPath = AssetDatabase.GUIDToAssetPath(value);
//                     if (!string.IsNullOrEmpty(assetPath))
//                     {
//                         GUI.FocusControl(null);
//                         var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
//                         EditorGUIUtility.PingObject(asset);
//                         Selection.activeObject = asset;
//                     }
//                     else
//                     {
//                         errorMsg = $"Not Found GUID : {value}";
//                         errorMsgStartTime = EditorApplication.timeSinceStartup;
//                     }
//                 }
//             }
//
//
//             if (!string.IsNullOrEmpty(errorMsg))
//             {
//                 var lerp = Mathf.Clamp01(1 - (float)(EditorApplication.timeSinceStartup - errorMsgStartTime - 1));
//
//                 if (lerp <= 0)
//                 {
//                     errorMsg = null;
//                     return;
//                 }
//
//                 errorStyle = errorStyle ?? "OL Ping";
//                 var color = Color.white;
//                 color.a = lerp;
//                 GUI.color = color;
//                 GUI.backgroundColor = color;
//                 rect.size = errorStyle.CalcSize(new GUIContent(errorMsg));
//                 rect.x += calcSize.x + 5;
//                 GUI.Label(rect, errorMsg, errorStyle);
//             }
//
//             GUI.backgroundColor = Color.white;
//             GUI.color = Color.white;
//         }
//     }
// }