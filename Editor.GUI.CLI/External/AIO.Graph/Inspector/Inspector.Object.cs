// /*|============|*|
// |*|Author:     |*| Star fire
// |*|Date:       |*| 2023-12-07
// |*|E-Mail:     |*| xinansky99@foxmail.com
// |*|============|*/
//
// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using UnityEditor;
// using UnityEditor.IMGUI.Controls;
// using UnityEngine;
// using UObject = UnityEngine.Object;
// using UReorderableList = UnityEditorInternal.ReorderableList;
//
// namespace AIO.UEditor
// {
//     /// <summary>
//     /// 对象检视器
//     /// </summary>
//     [GithubURL("https://github.com/AIO-GAME/Common#readme")]
//     [CanEditMultipleObjects]
//     [CustomEditor(typeof(UObject), true)]
//     internal sealed class ObjectInspector : NILInspector<UObject>
//     {
//         private readonly List<FieldInspector> _fields = new List<FieldInspector>();
//         private readonly List<PropertyInspector> _properties = new List<PropertyInspector>();
//         private readonly List<EventInspector> _events = new List<EventInspector>();
//         private readonly List<MethodInspector> _methods = new List<MethodInspector>();
//
//         protected override bool IsEnableRuntimeData => false;
//
//         protected override void OnActivation()
//         {
//             try
//             {
//                 using (var iterator = serializedObject.GetIterator())
//                 {
//                     var fieldPaths = new HashSet<string>();
//                     while (iterator.NextVisible(true))
//                     {
//                         var property = serializedObject.FindProperty(iterator.name);
//                         if (property == null || fieldPaths.Contains(property.propertyPath)) continue;
//                         fieldPaths.Add(property.propertyPath);
//                         _fields.Add(new FieldInspector(property));
//                     }
//
//                     fieldPaths.Clear();
//                 }
//
//                 var properties = target.GetType()
//                     .GetProperties((property) => property.IsDefined(typeof(PropertyDisplayAttribute), true));
//                 foreach (var property in properties)
//                 {
//                     _properties.Add(new PropertyInspector(property));
//                 }
//
//                 var events = target.GetType().GetFields((field) =>
//                     field.FieldType.IsSubclassOf(typeof(Delegate[])) &&
//                     field.IsDefined(typeof(EventAttribute), true));
//                 foreach (var fieldInfo in events)
//                 {
//                     _events.Add(new EventInspector(fieldInfo));
//                 }
//
//                 var methods = target.GetType().GetMethods((method) => method.IsDefined(typeof(ButtonAttribute), true));
//                 foreach (var method in methods)
//                 {
//                     _methods.Add(new MethodInspector(method));
//                 }
//
//                 _methods.Sort((a, b) => a.Attribute.Order - b.Attribute.Order);
//             }
//             catch
//             {
//             }
//         }
//
//         protected override void OnGUI()
//         {
//             FieldGUI();
//             PropertyGUI();
//             EventGUI();
//             MethodGUI();
//         }
//
//         private void OnSceneGUI()
//         {
//             FieldSceneHandle();
//         }
//
//         /// <summary>
//         /// 绘制字段
//         /// </summary>
//         private void FieldGUI()
//         {
//             var drawer = true;
//             var indent = 0;
//             foreach (var field in _fields)
//             {
//                 if (field.Drawer != null)
//                 {
//                     if (field.IsDisplayDrawer)
//                     {
//                         EditorGUI.indentLevel = 0;
//                         indent = 1;
//
//                         if (string.IsNullOrEmpty(field.Drawer.Style))
//                         {
//                             EditorGUILayout.BeginHorizontal();
//                         }
//                         else
//                         {
//                             EditorGUILayout.BeginHorizontal(field.Drawer.Style);
//                             GUILayout.Space(10);
//                         }
//
//                         field.DrawerValue = EditorGUILayout.Foldout(field.DrawerValue, field.Drawer.Name,
//                             field.Drawer.ToggleOnLabelClick);
//                         drawer = field.DrawerValue;
//                         EditorGUILayout.EndHorizontal();
//                     }
//                     else
//                     {
//                         drawer = false;
//                     }
//                 }
//
//                 if (drawer)
//                 {
//                     EditorGUI.indentLevel = indent;
//                     field.Painting(this);
//                 }
//             }
//
//             EditorGUI.indentLevel = 0;
//         }
//
//         /// <summary>
//         /// 绘制属性
//         /// </summary>
//         private void PropertyGUI()
//         {
//             GUI.color = Color.yellow;
//             foreach (var property in _properties) property.Painting(this);
//             GUI.color = Color.white;
//         }
//
//         /// <summary>
//         /// 绘制事件
//         /// </summary>
//         private void EventGUI()
//         {
//             foreach (var _event in _events) _event.Painting(this);
//         }
//
//         /// <summary>
//         /// 绘制方法
//         /// </summary>
//         private void MethodGUI()
//         {
//             foreach (var _method in _methods) _method.Painting(this);
//         }
//
//         /// <summary>
//         /// 场景中处理字段
//         /// </summary>
//         private void FieldSceneHandle()
//         {
//             var drawer = true;
//             foreach (var _field in _fields)
//             {
//                 if (_field.Drawer != null)
//                 {
//                     drawer = _field.IsDisplayDrawer && _field.DrawerValue;
//                 }
//
//                 if (drawer)
//                 {
//                     _field.SceneHandle(this);
//                 }
//             }
//         }
//
//         #region Field
//
//         /// <summary>
//         /// 字段检视器
//         /// </summary>
//         private sealed class FieldInspector
//         {
//             public FieldInfo Field;
//             public SerializedProperty Property;
//             public List<FieldPainter> Painters = new List<FieldPainter>();
//             public List<FieldSceneHandler> SceneHandlers = new List<FieldSceneHandler>();
//             public MethodInfo EnableCondition;
//             public MethodInfo DisplayCondition;
//             public string Label;
//             public Color UseColor = Color.white;
//             public bool IsReadOnly;
//             public DrawerAttribute Drawer;
//             public MethodInfo DrawerCondition;
//             public bool DrawerValue = true;
//             public bool HasPreview;
//             public float PreviewSize;
//
//             /// <summary>
//             /// 是否激活
//             /// </summary>
//             public bool IsEnable
//             {
//                 get
//                 {
//                     if (EnableCondition == null) return !IsReadOnly;
//                     bool condition;
//                     if (EnableCondition.IsStatic)
//                     {
//                         condition = (bool)EnableCondition.Invoke(null, null);
//                     }
//                     else
//                     {
//                         condition = (bool)EnableCondition.Invoke(Property.serializedObject.targetObject, null);
//                     }
//
//                     return !IsReadOnly && condition;
//                 }
//             }
//
//             /// <summary>
//             /// 是否显示
//             /// </summary>
//             public bool IsDisplay
//             {
//                 get
//                 {
//                     if (DisplayCondition == null) return true;
//                     bool condition;
//                     if (DisplayCondition.IsStatic)
//                     {
//                         condition = (bool)DisplayCondition.Invoke(null, null);
//                     }
//                     else
//                     {
//                         condition = (bool)DisplayCondition.Invoke(Property.serializedObject.targetObject, null);
//                     }
//
//                     return condition;
//                 }
//             }
//
//             /// <summary>
//             /// 是否显示整个抽屉组
//             /// </summary>
//             public bool IsDisplayDrawer
//             {
//                 get
//                 {
//                     if (DrawerCondition == null) return true;
//                     bool condition;
//                     if (DrawerCondition.IsStatic)
//                     {
//                         condition = (bool)DrawerCondition.Invoke(null, null);
//                     }
//                     else
//                     {
//                         condition = (bool)DrawerCondition.Invoke(Property.serializedObject.targetObject, null);
//                     }
//
//                     return condition;
//                 }
//             }
//
//             public FieldInspector(SerializedProperty property)
//             {
//                 var flags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public |
//                             BindingFlags.NonPublic;
//                 Field = property.serializedObject.targetObject.GetType().GetField(property.name, flags);
//                 Property = property;
//                 Label = property.displayName;
//
//                 if (Field != null)
//                 {
//                     InspectorAttribute[] iattributes =
//                         (InspectorAttribute[])Field.GetCustomAttributes(typeof(InspectorAttribute), true);
//                     for (int i = 0; i < iattributes.Length; i++)
//                     {
//                         switch (iattributes[i])
//                         {
//                             case DropdownAttribute _:
//                                 Painters.Add(new DropdownPainter(iattributes[i]));
//                                 break;
//                             case LayerAttribute _:
//                                 Painters.Add(new LayerPainter(iattributes[i]));
//                                 break;
//                             case ReorderableListAttribute _:
//                                 Painters.Add(new ReorderableListPainter(iattributes[i]));
//                                 break;
//                             case PasswordAttribute _:
//                                 Painters.Add(new PasswordPainter(iattributes[i]));
//                                 break;
//                             case HyperlinkAttribute _:
//                                 Painters.Add(new HyperlinkPainter(iattributes[i]));
//                                 break;
//                             case FilePathAttribute _:
//                                 Painters.Add(new FilePathPainter(iattributes[i]));
//                                 break;
//                             case FolderPathAttribute _:
//                                 Painters.Add(new FolderPathPainter(iattributes[i]));
//                                 break;
//                             case ClassTypeAttribute _:
//                                 Painters.Add(new ClassTypePainter(iattributes[i]));
//                                 break;
//                             case EnableAttribute _:
//                             {
//                                 EnableCondition = property.serializedObject.targetObject.GetType()
//                                     .GetMethod(iattributes[i].To<EnableAttribute>().Condition, flags);
//                                 if (EnableCondition != null && EnableCondition.ReturnType != typeof(bool))
//                                 {
//                                     EnableCondition = null;
//                                 }
//
//                                 break;
//                             }
//                             case DisplayAttribute _:
//                             {
//                                 DisplayCondition = property.serializedObject.targetObject.GetType()
//                                     .GetMethod(iattributes[i].To<DisplayAttribute>().Condition, flags);
//                                 if (DisplayCondition != null && DisplayCondition.ReturnType != typeof(bool))
//                                 {
//                                     DisplayCondition = null;
//                                 }
//
//                                 break;
//                             }
//                             case LabelAttribute _:
//                                 Label = iattributes[i].To<LabelAttribute>().Name;
//                                 break;
//                             case ColorAttribute _:
//                             {
//                                 var attribute = (ColorAttribute)iattributes[i];
//                                 UseColor = new Color(attribute.R, attribute.G, attribute.B, attribute.A);
//                                 break;
//                             }
//                             case ReadOnlyAttribute _:
//                                 IsReadOnly = true;
//                                 break;
//                             case PreviewAttribute _:
//                                 HasPreview = true;
//                                 PreviewSize = iattributes[i].To<PreviewAttribute>().Size;
//                                 break;
//                             case GenericMenuAttribute _:
//                                 Painters.Add(new GenericMenuPainter(iattributes[i]));
//                                 break;
//                             case GenericTableAttribute _:
//                                 Painters.Add(new GenericTablePainter(iattributes[i]));
//                                 break;
//                             case DrawerAttribute _:
//                             {
//                                 Drawer = iattributes[i] as DrawerAttribute;
//                                 if (!string.IsNullOrEmpty(Drawer.Condition))
//                                 {
//                                     DrawerCondition = property.serializedObject.targetObject.GetType()
//                                         .GetMethod(Drawer.Condition, flags);
//                                     if (DrawerCondition != null && DrawerCondition.ReturnType != typeof(bool))
//                                     {
//                                         DrawerCondition = null;
//                                     }
//                                 }
//
//                                 DrawerValue = Drawer.DefaultOpened;
//                                 break;
//                             }
//                         }
//                     }
//
//                     var sattributes =
//                         (SceneHandlerAttribute[])Field.GetCustomAttributes(typeof(SceneHandlerAttribute), true);
//                     foreach (var sceneHandler in sattributes)
//                     {
//                         switch (sceneHandler)
//                         {
//                             case MoveHandlerAttribute _:
//                                 SceneHandlers.Add(new MoveHandler(sceneHandler));
//                                 break;
//                             case RadiusHandlerAttribute _:
//                                 SceneHandlers.Add(new RadiusHandler(sceneHandler));
//                                 break;
//                             case BoundsHandlerAttribute _:
//                                 SceneHandlers.Add(new BoundsHandler(sceneHandler));
//                                 break;
//                             case DirectionHandlerAttribute _:
//                                 SceneHandlers.Add(new DirectionHandler(sceneHandler));
//                                 break;
//                             case CircleAreaHandlerAttribute _:
//                                 SceneHandlers.Add(new CircleAreaHandler(sceneHandler));
//                                 break;
//                         }
//                     }
//                 }
//             }
//
//             public void Painting(ObjectInspector nilInspector)
//             {
//                 if (!IsDisplay) return;
//                 GUI.color = UseColor;
//                 if (Painters.Count > 0 && nilInspector.targets.Length == 1)
//                 {
//                     GUI.enabled = IsEnable;
//                     foreach (var painter in Painters) painter.Painting(nilInspector, this);
//                     GUI.enabled = true;
//                 }
//                 else
//                 {
//                     if (Property.name == "m_Script")
//                     {
//                         GUI.enabled = false;
//                         EditorGUILayout.BeginHorizontal();
//                         EditorGUILayout.PropertyField(Property);
//                         EditorGUILayout.EndHorizontal();
//                         GUI.enabled = true;
//                     }
//                     else
//                     {
//                         GUI.enabled = IsEnable;
//                         EditorGUILayout.BeginHorizontal();
//                         EditorGUILayout.PropertyField(Property, new GUIContent(Label), true);
//                         nilInspector.DrawCopyPaste(Property);
//                         EditorGUILayout.EndHorizontal();
//                         GUI.enabled = true;
//                     }
//                 }
//
//                 GUI.color = Color.white;
//
//                 if (HasPreview)
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     GUILayout.Space(nilInspector.LabelWidth);
//                     var preview = Property.propertyType == SerializedPropertyType.ObjectReference
//                         ? AssetPreview.GetAssetPreview(Property.objectReferenceValue)
//                         : null;
//                     var gc = preview != null ? new GUIContent(preview) : new GUIContent("No Preview");
//                     EditorGUILayout.LabelField(gc, EditorStyles.helpBox, GUILayout.Width(PreviewSize),
//                         GUILayout.Height(PreviewSize));
//                     GUILayout.FlexibleSpace();
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//
//             public void SceneHandle(ObjectInspector nilInspector)
//             {
//                 if (!IsDisplay) return;
//                 if (SceneHandlers.Count <= 0) return;
//                 foreach (var SceneHandler in SceneHandlers)
//                 {
//                     SceneHandler.SceneHandle(nilInspector, this);
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器
//         /// </summary>
//         private abstract class FieldPainter
//         {
//             private InspectorAttribute IAttribute;
//
//             protected FieldPainter(InspectorAttribute attribute)
//             {
//                 IAttribute = attribute;
//             }
//
//             public abstract void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector);
//         }
//
//         /// <summary>
//         /// 字段绘制器 - 下拉菜单
//         /// </summary>
//         private sealed class DropdownPainter : FieldPainter
//         {
//             public DropdownAttribute DAttribute;
//
//             public DropdownPainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 DAttribute = attribute as DropdownAttribute;
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (DAttribute.ValueType == fieldInspector.Field.FieldType)
//                 {
//                     object value = fieldInspector.Field.GetValue(nilInspector.target);
//                     int selectIndex = Array.IndexOf(DAttribute.Values, value);
//                     if (selectIndex < 0)
//                     {
//                         selectIndex = 0;
//                         fieldInspector.Field.SetValue(nilInspector.target, DAttribute.Values[selectIndex]);
//                         nilInspector.HasChanged();
//                     }
//
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUI.BeginChangeCheck();
//                     int newIndex = EditorGUILayout.Popup(fieldInspector.Label, selectIndex, DAttribute.DisplayOptions);
//                     if (EditorGUI.EndChangeCheck())
//                     {
//                         Undo.RecordObject(nilInspector.target, "Dropdown");
//                         fieldInspector.Field.SetValue(nilInspector.target, DAttribute.Values[newIndex]);
//                         nilInspector.HasChanged();
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox($"[{fieldInspector.Field.Name}] used a mismatched Dropdown!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器 - 层级检视
//         /// </summary>
//         private sealed class LayerPainter : FieldPainter
//         {
//             public LayerAttribute LAttribute;
//
//             public LayerPainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 LAttribute = attribute as LayerAttribute;
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(string))
//                 {
//                     string value = (string)fieldInspector.Field.GetValue(nilInspector.target);
//                     int layer = LayerMask.NameToLayer(value);
//                     if (layer < 0) layer = 0;
//                     if (layer > 31) layer = 31;
//
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUI.BeginChangeCheck();
//                     int newLayer = EditorGUILayout.LayerField(fieldInspector.Label, layer);
//                     if (EditorGUI.EndChangeCheck())
//                     {
//                         Undo.RecordObject(nilInspector.target, "Layer");
//                         fieldInspector.Field.SetValue(nilInspector.target, LayerMask.LayerToName(newLayer));
//                         nilInspector.HasChanged();
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else if (fieldInspector.Field.FieldType == typeof(int))
//                 {
//                     int layer = (int)fieldInspector.Field.GetValue(nilInspector.target);
//                     if (layer < 0) layer = 0;
//                     if (layer > 31) layer = 31;
//
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUI.BeginChangeCheck();
//                     int newLayer = EditorGUILayout.LayerField(fieldInspector.Label, layer);
//                     if (EditorGUI.EndChangeCheck())
//                     {
//                         Undo.RecordObject(nilInspector.target, "Layer");
//                         fieldInspector.Field.SetValue(nilInspector.target, newLayer);
//                         nilInspector.HasChanged();
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox(
//                         $"[{fieldInspector.Field.Name}] can't used Layer! because the types don't match!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器 - 可排序列表
//         /// </summary>
//         private sealed class ReorderableListPainter : FieldPainter
//         {
//             private ReorderableListAttribute RAttribute;
//             private UReorderableList List;
//
//             public ReorderableListPainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 RAttribute = attribute as ReorderableListAttribute;
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Property.isArray)
//                 {
//                     if (List == null) GenerateList(fieldInspector);
//                     List?.DoLayoutList();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox($"[{fieldInspector.Field.Name}] can't use the ReorderableList!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//
//             private void GenerateList(FieldInspector fieldInspector)
//             {
//                 List = new UReorderableList(fieldInspector.Property.serializedObject, fieldInspector.Property, true,
//                     true, true, true)
//                 {
//                     drawHeaderCallback = rect =>
//                     {
//                         EditorGUI.LabelField(rect, $"{fieldInspector.Label}: {fieldInspector.Property.arraySize}",
//                             EditorStyles.boldLabel);
//                     },
//                     drawElementCallback = (rect, index, isActive, isFocused) =>
//                     {
//                         var element = fieldInspector.Property.GetArrayElementAtIndex(index);
//                         rect.x += 10;
//                         rect.y += 2;
//                         rect.width -= 10;
//                         EditorGUI.PropertyField(rect, element, true);
//                     },
//                     drawElementBackgroundCallback = (rect, index, isActive, isFocused) =>
//                     {
//                         if (Event.current.type != EventType.Repaint) return;
//                         GUIStyle gUIStyle = index % 2 != 0 ? "CN EntryBackEven" : "Box";
//                         gUIStyle = (!isActive && !isFocused) ? gUIStyle : "RL Element";
//                         rect.x += 2;
//                         rect.width -= 6;
//                         gUIStyle.Draw(rect, false, isActive, isActive, isFocused);
//                     },
//                     elementHeightCallback = index =>
//                         EditorGUI.GetPropertyHeight(fieldInspector.Property.GetArrayElementAtIndex(index)) + 6
//                 };
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器 - 密码
//         /// </summary>
//         private sealed class PasswordPainter : FieldPainter
//         {
//             public PasswordAttribute PAttribute;
//
//             public PasswordPainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 PAttribute = attribute as PasswordAttribute;
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(string))
//                 {
//                     var value = (string)fieldInspector.Field.GetValue(nilInspector.target);
//                     if (value == null) value = "";
//
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUI.BeginChangeCheck();
//                     var newValue = EditorGUILayout.PasswordField(fieldInspector.Label, value);
//                     if (EditorGUI.EndChangeCheck())
//                     {
//                         Undo.RecordObject(nilInspector.target, "Password");
//                         fieldInspector.Field.SetValue(nilInspector.target, newValue);
//                         nilInspector.HasChanged();
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox(
//                         $"[{fieldInspector.Field.Name}] can't used Password! because the types don't match!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器 - 超链接
//         /// </summary>
//         private sealed class HyperlinkPainter : FieldPainter
//         {
//             public HyperlinkAttribute HAttribute;
//
//             public HyperlinkPainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 HAttribute = attribute as HyperlinkAttribute;
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(string))
//                 {
//                     EditorGUILayout.BeginHorizontal();
// #if UNITY_2021_1_OR_NEWER
//                     if (EditorGUILayout.LinkButton(HAttribute.Name))
// #else
//                     if (GELayout.Button(HAttribute.Name))
// #endif
//                     {
//                         Application.OpenURL((string)fieldInspector.Field.GetValue(nilInspector.target));
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox(
//                         $"[{fieldInspector.Field.Name}] can't used Hyperlink! because the types don't match!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器 - 文件路径
//         /// </summary>
//         private sealed class FilePathPainter : FieldPainter
//         {
//             public FilePathAttribute FAttribute;
//             public GUIContent OpenGC;
//
//             public FilePathPainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 FAttribute = attribute as FilePathAttribute;
//                 OpenGC = new GUIContent();
//                 OpenGC.image = EditorGUIUtility.IconContent("Folder Icon").image;
//                 OpenGC.tooltip = "Browse file path";
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(string))
//                 {
//                     string value = (string)fieldInspector.Field.GetValue(nilInspector.target);
//                     if (value == null) value = "";
//
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUI.BeginChangeCheck();
//                     string newValue = EditorGUILayout.TextField(fieldInspector.Label, value);
//                     if (EditorGUI.EndChangeCheck())
//                     {
//                         Undo.RecordObject(nilInspector.target, "FilePath");
//                         fieldInspector.Field.SetValue(nilInspector.target, newValue);
//                         nilInspector.HasChanged();
//                     }
//
//                     if (GUILayout.Button(OpenGC, GEStyle.IconButton, GUILayout.Width(20),
//                             GUILayout.Height(EditorGUIUtility.singleLineHeight)))
//                     {
//                         string path = EditorUtility.OpenFilePanel("Select File", Application.dataPath,
//                             FAttribute.Extension);
//                         if (path.Length != 0)
//                         {
//                             Undo.RecordObject(nilInspector.target, "FilePath");
//                             fieldInspector.Field.SetValue(nilInspector.target,
//                                 "Assets" + path.Replace(Application.dataPath, ""));
//                             nilInspector.HasChanged();
//                         }
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox(
//                         $"[{fieldInspector.Field.Name}] can't used FilePath! because the types don't match!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器 - 文件夹路径
//         /// </summary>
//         private sealed class FolderPathPainter : FieldPainter
//         {
//             private FolderPathAttribute FAttribute;
//             private readonly GUIContent OpenGC;
//
//             public FolderPathPainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 FAttribute = attribute as FolderPathAttribute;
//                 OpenGC = new GUIContent
//                 {
//                     image = EditorGUIUtility.IconContent("Folder Icon").image,
//                     tooltip = "Browse folder path"
//                 };
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(string))
//                 {
//                     var value = (string)fieldInspector.Field.GetValue(nilInspector.target);
//                     if (value == null) value = "";
//
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUI.BeginChangeCheck();
//                     var newValue = EditorGUILayout.TextField(fieldInspector.Label, value);
//                     if (EditorGUI.EndChangeCheck())
//                     {
//                         Undo.RecordObject(nilInspector.target, "FolderPath");
//                         fieldInspector.Field.SetValue(nilInspector.target, newValue);
//                         nilInspector.HasChanged();
//                     }
//
//                     if (GUILayout.Button(OpenGC, GEStyle.IconButton, GUILayout.Width(20),
//                             GUILayout.Height(EditorGUIUtility.singleLineHeight)))
//                     {
//                         var path = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, "");
//                         if (path.Length != 0)
//                         {
//                             Undo.RecordObject(nilInspector.target, "FolderPath");
//                             fieldInspector.Field.SetValue(nilInspector.target,
//                                 "Assets" + path.Replace(Application.dataPath, ""));
//                             nilInspector.HasChanged();
//                         }
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox(
//                         $"[{fieldInspector.Field.Name}] can't used FolderPath! because the types don't match!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器 - Class类型
//         /// </summary>
//         private sealed class ClassTypePainter : FieldPainter
//         {
//             private readonly ClassTypeAttribute CAttribute;
//
//             public ClassTypePainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 CAttribute = attribute as ClassTypeAttribute;
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(string))
//                 {
//                     var value = (string)fieldInspector.Field.GetValue(nilInspector.target);
//
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.LabelField(fieldInspector.Label, GUILayout.Width(nilInspector.LabelWidth));
//                     if (GUILayout.Button(value, EditorStyles.popup))
//                     {
//                         var gm = new GenericMenu();
//                         Type[] types;
//                         if (CAttribute.IsOnlyRuntime)
//                         {
//                             if (CAttribute.IsIgnoreAbstract)
//                             {
//                                 types = AHelper.Reflect.GetTypesInRunTimeAssemblies(type =>
//                                     type.IsSubclassOf(CAttribute.ParentClass) && !type.IsAbstract);
//                             }
//                             else
//                             {
//                                 types = AHelper.Reflect.GetTypesInRunTimeAssemblies(type =>
//                                     type.IsSubclassOf(CAttribute.ParentClass));
//                             }
//                         }
//                         else
//                         {
//                             if (CAttribute.IsIgnoreAbstract)
//                             {
//                                 types = AHelper.Reflect.GetTypesInAllAssemblies(type =>
//                                     type.IsSubclassOf(CAttribute.ParentClass) && !type.IsAbstract);
//                             }
//                             else
//                             {
//                                 types = AHelper.Reflect.GetTypesInAllAssemblies(type =>
//                                     type.IsSubclassOf(CAttribute.ParentClass));
//                             }
//                         }
//
//                         gm.AddItem(new GUIContent("<None>"), value == "<None>", () =>
//                         {
//                             Undo.RecordObject(nilInspector.target, "ClassType");
//                             fieldInspector.Field.SetValue(nilInspector.target, "<None>");
//                             nilInspector.HasChanged();
//                         });
//                         for (var i = 0; i < types.Length; i++)
//                         {
//                             var j = i;
//                             gm.AddItem(new GUIContent(types[j].FullName), value == types[j].FullName, () =>
//                             {
//                                 Undo.RecordObject(nilInspector.target, "ClassType");
//                                 fieldInspector.Field.SetValue(nilInspector.target, types[j].FullName);
//                                 nilInspector.HasChanged();
//                             });
//                         }
//
//                         gm.ShowAsContext();
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox(
//                         $"[{fieldInspector.Field.Name}] can't used ClassType! because the types don't match!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器 - 通用菜单
//         /// </summary>
//         private sealed class GenericMenuPainter : FieldPainter
//         {
//             private GenericMenuAttribute GAttribute;
//             private MethodInfo GenerateMenu;
//             private MethodInfo ChooseMenu;
//             private bool IsReady = false;
//
//             public GenericMenuPainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 GAttribute = attribute as GenericMenuAttribute;
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(string))
//                 {
//                     if (!IsReady)
//                     {
//                         Ready(fieldInspector);
//                     }
//
//                     var value = (string)fieldInspector.Field.GetValue(nilInspector.target);
//
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.LabelField(fieldInspector.Label, GUILayout.Width(nilInspector.LabelWidth));
//                     if (GUILayout.Button(value, EditorStyles.popup))
//                     {
//                         if (GenerateMenu != null)
//                         {
//                             var menus = CallGenerateMenu(fieldInspector);
//                             if (menus != null && menus.Length > 0)
//                             {
//                                 var gm = new GenericMenu();
//                                 for (var i = 0; i < menus.Length; i++)
//                                 {
//                                     var j = i;
//                                     gm.AddItem(new GUIContent(menus[j]), value == menus[j], () =>
//                                     {
//                                         Undo.RecordObject(nilInspector.target, "GenericMenu");
//                                         value = menus[j];
//                                         fieldInspector.Field.SetValue(nilInspector.target, value);
//                                         if (ChooseMenu != null)
//                                         {
//                                             CallChooseMenu(fieldInspector, value);
//                                         }
//
//                                         nilInspector.HasChanged();
//                                     });
//                                 }
//
//                                 gm.ShowAsContext();
//                             }
//                         }
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox(
//                         $"[{fieldInspector.Field.Name}] can't used GenericMenu! because the types don't match!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//
//             public void Ready(FieldInspector fieldInspector)
//             {
//                 IsReady = true;
//                 BindingFlags flags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public |
//                                      BindingFlags.NonPublic;
//                 if (!string.IsNullOrEmpty(GAttribute.GenerateMenu))
//                 {
//                     GenerateMenu = fieldInspector.Property.serializedObject.targetObject.GetType()
//                         .GetMethod(GAttribute.GenerateMenu, flags);
//                     if (GenerateMenu != null && GenerateMenu.ReturnType != typeof(string[]))
//                     {
//                         GenerateMenu = null;
//                     }
//                 }
//
//                 if (!string.IsNullOrEmpty(GAttribute.ChooseMenu))
//                 {
//                     ChooseMenu = fieldInspector.Property.serializedObject.targetObject.GetType()
//                         .GetMethod(GAttribute.ChooseMenu, flags);
//                     if (ChooseMenu != null)
//                     {
//                         ParameterInfo[] parameters = ChooseMenu.GetParameters();
//                         if (parameters.Length != 1)
//                         {
//                             GenerateMenu = null;
//                         }
//                         else if (parameters[0].ParameterType != typeof(string))
//                         {
//                             GenerateMenu = null;
//                         }
//                     }
//                 }
//             }
//
//             public string[] CallGenerateMenu(FieldInspector fieldInspector)
//             {
//                 if (GenerateMenu.IsStatic)
//                 {
//                     return GenerateMenu.Invoke(null, null) as string[];
//                 }
//                 else
//                 {
//                     return GenerateMenu.Invoke(fieldInspector.Property.serializedObject.targetObject, null) as string[];
//                 }
//             }
//
//             public void CallChooseMenu(FieldInspector fieldInspector, string value)
//             {
//                 if (ChooseMenu.IsStatic)
//                 {
//                     ChooseMenu.Invoke(null, new object[] { value });
//                 }
//                 else
//                 {
//                     ChooseMenu.Invoke(fieldInspector.Property.serializedObject.targetObject, new object[] { value });
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段绘制器 - 通用表格
//         /// </summary>
//         private sealed class GenericTablePainter : FieldPainter
//         {
//             private GenericTableAttribute GAttribute;
//             private readonly GUIContent OpenGC;
//
//             public GenericTablePainter(InspectorAttribute attribute) : base(attribute)
//             {
//                 GAttribute = attribute as GenericTableAttribute;
//                 OpenGC = new GUIContent
//                 {
//                     image = EditorGUIUtility.IconContent("Clipboard").image,
//                     tooltip = "Edit with GenericTableWindow"
//                 };
//             }
//
//             public override void Painting(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 var list = fieldInspector.Field.GetValue(nilInspector.target) as IEnumerable<object>;
//                 if (fieldInspector.Field.FieldType.IsArray || list != null)
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.PropertyField(fieldInspector.Property, new GUIContent(fieldInspector.Label), true);
//                     if (GUILayout.Button(OpenGC, GEStyle.IconButton, GUILayout.Width(16),
//                             GUILayout.Height(16)))
//                     {
//                         GenericTableWindow.OpenWindow(nilInspector.target, fieldInspector.Field.Name);
//                     }
//
//                     EditorGUILayout.EndHorizontal();
//                 }
//                 else
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     EditorGUILayout.HelpBox(
//                         $"[{fieldInspector.Field.Name}] can't used GenericTable! because the types don't match!",
//                         MessageType.Error);
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段场景处理器
//         /// </summary>
//         private abstract class FieldSceneHandler
//         {
//             private SceneHandlerAttribute SAttribute;
//
//             protected FieldSceneHandler(SceneHandlerAttribute attribute)
//             {
//                 SAttribute = attribute;
//             }
//
//             public abstract void SceneHandle(ObjectInspector nilInspector, FieldInspector fieldInspector);
//         }
//
//         /// <summary>
//         /// 字段场景处理器 - 移动手柄
//         /// </summary>
//         private sealed class MoveHandler : FieldSceneHandler
//         {
//             public MoveHandlerAttribute MAttribute;
//
//             public MoveHandler(SceneHandlerAttribute attribute) : base(attribute)
//             {
//                 MAttribute = attribute as MoveHandlerAttribute;
//             }
//
//             public override void SceneHandle(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(Vector3))
//                 {
//                     Vector3 value = (Vector3)fieldInspector.Field.GetValue(nilInspector.target);
//
//                     using (new Handles.DrawingScope(fieldInspector.UseColor))
//                     {
//                         EditorGUI.BeginChangeCheck();
//                         Vector3 newValue = Handles.PositionHandle(value, Quaternion.identity);
//                         if (EditorGUI.EndChangeCheck())
//                         {
//                             Undo.RecordObject(nilInspector.target, "Move Handler");
//                             fieldInspector.Field.SetValue(nilInspector.target, newValue);
//                             nilInspector.HasChanged(true);
//                         }
//
//                         if (MAttribute.Display != null)
//                         {
//                             Handles.Label(newValue, MAttribute.Display);
//                         }
//                     }
//                 }
//                 else if (fieldInspector.Field.FieldType == typeof(Vector2))
//                 {
//                     Vector2 value = (Vector2)fieldInspector.Field.GetValue(nilInspector.target);
//
//                     using (new Handles.DrawingScope(fieldInspector.UseColor))
//                     {
//                         EditorGUI.BeginChangeCheck();
//                         Vector2 newValue = Handles.PositionHandle(value, Quaternion.identity);
//                         if (EditorGUI.EndChangeCheck())
//                         {
//                             Undo.RecordObject(nilInspector.target, "Move Handler");
//                             fieldInspector.Field.SetValue(nilInspector.target, newValue);
//                             nilInspector.HasChanged(true);
//                         }
//
//                         if (MAttribute.Display != null)
//                         {
//                             Handles.Label(newValue, MAttribute.Display);
//                         }
//                     }
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段场景处理器 - 半径手柄
//         /// </summary>
//         private sealed class RadiusHandler : FieldSceneHandler
//         {
//             public RadiusHandlerAttribute RAttribute;
//
//             public RadiusHandler(SceneHandlerAttribute attribute) : base(attribute)
//             {
//                 RAttribute = attribute as RadiusHandlerAttribute;
//             }
//
//             public override void SceneHandle(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(float))
//                 {
//                     Component component = nilInspector.target as Component;
//                     Vector3 center = component != null ? component.transform.position : Vector3.zero;
//                     float value = (float)fieldInspector.Field.GetValue(nilInspector.target);
//
//                     using (new Handles.DrawingScope(fieldInspector.UseColor))
//                     {
//                         EditorGUI.BeginChangeCheck();
//                         float newValue = Handles.RadiusHandle(Quaternion.identity, center, value);
//                         if (EditorGUI.EndChangeCheck())
//                         {
//                             Undo.RecordObject(nilInspector.target, "Radius Handler");
//                             fieldInspector.Field.SetValue(nilInspector.target, newValue);
//                             nilInspector.HasChanged(true);
//                         }
//
//                         if (RAttribute.Display != null)
//                         {
//                             Handles.Label(center, RAttribute.Display);
//                         }
//                     }
//                 }
//                 else if (fieldInspector.Field.FieldType == typeof(int))
//                 {
//                     Component component = nilInspector.target as Component;
//                     Vector3 center = component != null ? component.transform.position : Vector3.zero;
//                     int value = (int)fieldInspector.Field.GetValue(nilInspector.target);
//
//                     using (new Handles.DrawingScope(fieldInspector.UseColor))
//                     {
//                         EditorGUI.BeginChangeCheck();
//                         int newValue = (int)Handles.RadiusHandle(Quaternion.identity, center, value);
//                         if (EditorGUI.EndChangeCheck())
//                         {
//                             Undo.RecordObject(nilInspector.target, "Radius Handler");
//                             fieldInspector.Field.SetValue(nilInspector.target, newValue);
//                             nilInspector.HasChanged(true);
//                         }
//
//                         if (RAttribute.Display != null)
//                         {
//                             Handles.Label(center, RAttribute.Display);
//                         }
//                     }
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段场景处理器 - 包围盒
//         /// </summary>
//         private sealed class BoundsHandler : FieldSceneHandler
//         {
//             public BoundsHandlerAttribute BAttribute;
//             public BoxBoundsHandle BoundsHandle;
//
//             public BoundsHandler(SceneHandlerAttribute attribute) : base(attribute)
//             {
//                 BAttribute = attribute as BoundsHandlerAttribute;
//                 BoundsHandle = new BoxBoundsHandle();
//             }
//
//             public override void SceneHandle(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(Bounds))
//                 {
//                     Bounds value = (Bounds)fieldInspector.Field.GetValue(nilInspector.target);
//                     BoundsHandle.center = value.center;
//                     BoundsHandle.size = value.size;
//
//                     using (new Handles.DrawingScope(fieldInspector.UseColor))
//                     {
//                         EditorGUI.BeginChangeCheck();
//                         BoundsHandle.DrawHandle();
//                         if (EditorGUI.EndChangeCheck())
//                         {
//                             Undo.RecordObject(nilInspector.target, "Bounds Handler");
//                             value.center = BoundsHandle.center;
//                             value.size = BoundsHandle.size;
//                             fieldInspector.Field.SetValue(nilInspector.target, value);
//                             nilInspector.HasChanged(true);
//                         }
//
//                         if (BAttribute.Display != null)
//                         {
//                             Handles.Label(BoundsHandle.center, BAttribute.Display);
//                         }
//                     }
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段场景处理器 - 方向
//         /// </summary>
//         private sealed class DirectionHandler : FieldSceneHandler
//         {
//             public DirectionHandlerAttribute DAttribute;
//             public Transform Target;
//             public Vector3 Position;
//             public float ExternalSize;
//             public float InternalSize;
//             public float DynamicMultiple;
//
//             public DirectionHandler(SceneHandlerAttribute attribute) : base(attribute)
//             {
//                 DAttribute = attribute as DirectionHandlerAttribute;
//                 DynamicMultiple = 1;
//             }
//
//             public override void SceneHandle(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(Vector3))
//                 {
//                     Vector3 value = (Vector3)fieldInspector.Field.GetValue(nilInspector.target);
//
//                     if (value != Vector3.zero)
//                     {
//                         using (new Handles.DrawingScope(fieldInspector.UseColor))
//                         {
//                             ExternalSize = GetExternalSize(nilInspector.target);
//                             InternalSize = GetInternalSize(nilInspector.target);
//                             Handles.CircleHandleCap(0, Position, Quaternion.FromToRotation(Vector3.forward, value),
//                                 ExternalSize, EventType.Repaint);
//                             Handles.CircleHandleCap(0, Position, Quaternion.FromToRotation(Vector3.forward, value),
//                                 InternalSize, EventType.Repaint);
//                             Handles.Slider(Position, value);
//                         }
//                     }
//                 }
//                 else if (fieldInspector.Field.FieldType == typeof(Vector2))
//                 {
//                     Vector2 value = (Vector2)fieldInspector.Field.GetValue(nilInspector.target);
//
//                     if (value != Vector2.zero)
//                     {
//                         using (new Handles.DrawingScope(fieldInspector.UseColor))
//                         {
//                             ExternalSize = GetExternalSize(nilInspector.target);
//                             InternalSize = GetInternalSize(nilInspector.target);
//                             Handles.CircleHandleCap(0, Position, Quaternion.FromToRotation(Vector3.forward, value),
//                                 ExternalSize, EventType.Repaint);
//                             Handles.CircleHandleCap(0, Position, Quaternion.FromToRotation(Vector3.forward, value),
//                                 InternalSize, EventType.Repaint);
//                             Handles.Slider(Position, value);
//                         }
//                     }
//                 }
//             }
//
//             public float GetExternalSize(UObject target)
//             {
//                 if (Target == null)
//                 {
//                     Component component = target as Component;
//                     if (component)
//                     {
//                         Target = component.transform;
//                     }
//                 }
//
//                 if (Target != null)
//                 {
//                     Position = Target.position;
//                     return HandleUtility.GetHandleSize(Target.TransformPoint(Position)) * 1;
//                 }
//                 else
//                 {
//                     return 1;
//                 }
//             }
//
//             public float GetInternalSize(UObject target)
//             {
//                 if (Target == null)
//                 {
//                     Component component = target as Component;
//                     if (component)
//                     {
//                         Target = component.transform;
//                     }
//                 }
//
//                 if (DAttribute.IsDynamic)
//                 {
//                     if (DynamicMultiple < 2)
//                     {
//                         DynamicMultiple += 0.005f;
//                     }
//                     else
//                     {
//                         DynamicMultiple = 0;
//                     }
//
//                     GUI.changed = true;
//                 }
//
//                 if (Target != null)
//                 {
//                     Position = Target.position;
//                     return HandleUtility.GetHandleSize(Target.TransformPoint(Position)) * 0.5f * DynamicMultiple;
//                 }
//                 else
//                 {
//                     return 0.5f * DynamicMultiple;
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 字段场景处理器 - 圆形区域
//         /// </summary>
//         private sealed class CircleAreaHandler : FieldSceneHandler
//         {
//             public CircleAreaHandlerAttribute CAttribute;
//             public Transform Target;
//             public Vector3 Position;
//             public Quaternion Rotation;
//             public float Size;
//             public float DynamicMultiple;
//
//             public CircleAreaHandler(SceneHandlerAttribute attribute) : base(attribute)
//             {
//                 CAttribute = attribute as CircleAreaHandlerAttribute;
//                 Rotation = GetRotation();
//                 DynamicMultiple = 1;
//             }
//
//             public override void SceneHandle(ObjectInspector nilInspector, FieldInspector fieldInspector)
//             {
//                 if (fieldInspector.Field.FieldType == typeof(float))
//                 {
//                     float value = (float)fieldInspector.Field.GetValue(nilInspector.target);
//
//                     using (new Handles.DrawingScope(fieldInspector.UseColor))
//                     {
//                         Position = GetPosition(nilInspector.target);
//                         Size = GetSize(nilInspector.target, value);
//                         Handles.CircleHandleCap(0, Position, Rotation, Size, EventType.Repaint);
//                         if (Target)
//                         {
//                             Handles.Slider(Position, Target.forward);
//                         }
//                     }
//                 }
//             }
//
//             public Vector3 GetPosition(UObject target)
//             {
//                 if (Target == null)
//                 {
//                     Component component = target as Component;
//                     if (component)
//                     {
//                         Target = component.transform;
//                     }
//                 }
//
//                 return Target != null ? Target.position : Vector3.zero;
//             }
//
//             public Quaternion GetRotation()
//             {
//                 if (CAttribute.Direction == CircleAreaHandlerAttribute.Axis.X)
//                 {
//                     return Quaternion.FromToRotation(Vector3.forward, Vector3.right);
//                 }
//                 else if (CAttribute.Direction == CircleAreaHandlerAttribute.Axis.Y)
//                 {
//                     return Quaternion.FromToRotation(Vector3.forward, Vector3.up);
//                 }
//                 else
//                 {
//                     return Quaternion.identity;
//                 }
//             }
//
//             private float GetSize(UObject target, float value)
//             {
//                 if (!CAttribute.IsDynamic) return value * DynamicMultiple;
//                 if (DynamicMultiple < 1) DynamicMultiple += 0.0025f;
//                 else DynamicMultiple = 0;
//                 GUI.changed = true;
//                 return value * DynamicMultiple;
//             }
//         }
//
//         #endregion
//
//         #region Property
//
//         /// <summary>
//         /// 属性检视器
//         /// </summary>
//         private sealed class PropertyInspector
//         {
//             public PropertyInfo Property;
//             public PropertyDisplayAttribute Attribute;
//             public string Name;
//
//             public PropertyInspector(PropertyInfo property)
//             {
//                 Property = property;
//                 Attribute = property.GetCustomAttribute<PropertyDisplayAttribute>(true);
//                 Name = string.IsNullOrEmpty(Attribute.Text) ? property.Name : Attribute.Text;
//             }
//
//             public void Painting(ObjectInspector nilInspector)
//             {
//                 if (nilInspector.targets.Length > 1)
//                     return;
//
//                 if (!Property.CanRead)
//                     return;
//
//                 if (Attribute.DisplayOnlyRuntime && !EditorApplication.isPlaying)
//                     return;
//
//                 if (Property.CanWrite)
//                 {
//                     CanWritePainting(nilInspector);
//                 }
//                 else
//                 {
//                     ReadOnlyPainting(nilInspector);
//                 }
//             }
//
//             private void CanWritePainting(ObjectInspector nilInspector)
//             {
//                 EditorGUILayout.BeginHorizontal();
//                 EditorGUI.BeginChangeCheck();
//                 object value = Property.GetValue(nilInspector.target);
//                 object newValue = value;
//                 if (Property.PropertyType.IsEnum)
//                 {
//                     Enum realValue = EditorGUILayout.EnumPopup(Name, (Enum)value);
//                     if (EditorGUI.EndChangeCheck()) newValue = realValue;
//                 }
//                 else if (Property.PropertyType == typeof(string))
//                 {
//                     string realValue = EditorGUILayout.TextField(Name, (string)value);
//                     if (EditorGUI.EndChangeCheck()) newValue = realValue;
//                 }
//                 else if (Property.PropertyType == typeof(int))
//                 {
//                     int realValue = EditorGUILayout.IntField(Name, (int)value);
//                     if (EditorGUI.EndChangeCheck()) newValue = realValue;
//                 }
//                 else if (Property.PropertyType == typeof(float))
//                 {
//                     float realValue = EditorGUILayout.FloatField(Name, (float)value);
//                     if (EditorGUI.EndChangeCheck()) newValue = realValue;
//                 }
//                 else if (Property.PropertyType == typeof(bool))
//                 {
//                     bool realValue = EditorGUILayout.Toggle(Name, (bool)value);
//                     if (EditorGUI.EndChangeCheck()) newValue = realValue;
//                 }
//                 else if (Property.PropertyType == typeof(Vector2))
//                 {
//                     Vector2 realValue = EditorGUILayout.Vector2Field(Name, (Vector2)value);
//                     if (EditorGUI.EndChangeCheck()) newValue = realValue;
//                 }
//                 else if (Property.PropertyType == typeof(Vector3))
//                 {
//                     Vector3 realValue = EditorGUILayout.Vector3Field(Name, (Vector3)value);
//                     if (EditorGUI.EndChangeCheck()) newValue = realValue;
//                 }
//                 else if (Property.PropertyType == typeof(Color))
//                 {
//                     Color realValue = EditorGUILayout.ColorField(Name, (Color)value);
//                     if (EditorGUI.EndChangeCheck()) newValue = realValue;
//                 }
//                 else if (Property.PropertyType.IsSubclassOf(typeof(UObject)))
//                 {
//                     UObject realValue =
//                         EditorGUILayout.ObjectField(Name, value as UObject, Property.PropertyType, true);
//                     if (EditorGUI.EndChangeCheck()) newValue = realValue;
//                 }
//                 else
//                 {
//                     EditorGUILayout.TextField(Name, value != null ? value.ToString() : "null");
//                     EditorGUI.EndChangeCheck();
//                 }
//
//                 EditorGUILayout.EndHorizontal();
//
//                 if (value != newValue)
//                 {
//                     Undo.RecordObject(nilInspector.target, "Property Changed");
//                     Property.SetValue(nilInspector.target, newValue);
//                     nilInspector.HasChanged();
//                 }
//             }
//
//             private void ReadOnlyPainting(ObjectInspector nilInspector)
//             {
//                 GUI.enabled = false;
//
//                 EditorGUILayout.BeginHorizontal();
//                 object value = Property.GetValue(nilInspector.target);
//                 if (Property.PropertyType.IsEnum)
//                 {
//                     EditorGUILayout.EnumPopup(Name, (Enum)value);
//                 }
//                 else if (Property.PropertyType == typeof(string))
//                 {
//                     EditorGUILayout.TextField(Name, (string)value);
//                 }
//                 else if (Property.PropertyType == typeof(int))
//                 {
//                     EditorGUILayout.IntField(Name, (int)value);
//                 }
//                 else if (Property.PropertyType == typeof(float))
//                 {
//                     EditorGUILayout.FloatField(Name, (float)value);
//                 }
//                 else if (Property.PropertyType == typeof(bool))
//                 {
//                     EditorGUILayout.Toggle(Name, (bool)value);
//                 }
//                 else if (Property.PropertyType == typeof(Vector2))
//                 {
//                     EditorGUILayout.Vector2Field(Name, (Vector2)value);
//                 }
//                 else if (Property.PropertyType == typeof(Vector3))
//                 {
//                     EditorGUILayout.Vector3Field(Name, (Vector3)value);
//                 }
//                 else if (Property.PropertyType == typeof(Color))
//                 {
//                     EditorGUILayout.ColorField(Name, (Color)value);
//                 }
//                 else if (Property.PropertyType.IsSubclassOf(typeof(UObject)))
//                 {
//                     EditorGUILayout.ObjectField(Name, value as UObject, Property.PropertyType, false);
//                 }
//                 else
//                 {
//                     EditorGUILayout.TextField(Name, value != null ? value.ToString() : "null");
//                 }
//
//                 EditorGUILayout.EndHorizontal();
//
//                 GUI.enabled = true;
//             }
//         }
//
//         #endregion
//
//         #region Event
//
//         /// <summary>
//         /// 事件检视器
//         /// </summary>
//         private sealed class EventInspector
//         {
//             public FieldInfo Field;
//             public EventAttribute Attribute;
//             public string Name;
//             public bool IsFoldout;
//
//             public EventInspector(FieldInfo field)
//             {
//                 Field = field;
//                 Attribute = field.GetCustomAttribute<EventAttribute>(true);
//                 Name = string.IsNullOrEmpty(Attribute.Text) ? field.Name : Attribute.Text;
//                 IsFoldout = true;
//             }
//
//             public void Painting(Editor nilInspector)
//             {
//                 if (nilInspector.targets.Length > 1) return;
//
//                 var delegates = Field.GetValue(nilInspector.target) as Delegate[];
//
//                 EditorGUILayout.BeginHorizontal();
//                 GUILayout.Space(10);
//                 IsFoldout = EditorGUILayout.Foldout(IsFoldout,
//                     string.Format("{0} [{1}]", Name, delegates != null ? delegates.Length : 0), true);
//                 EditorGUILayout.EndHorizontal();
//
//                 if (!IsFoldout || delegates == null) return;
//                 foreach (var t in delegates)
//                 {
//                     EditorGUILayout.BeginHorizontal();
//                     GUILayout.Space(30);
//                     EditorGUILayout.LabelField($"{t.Target}->{t.Method}", "Textfield");
//                     EditorGUILayout.EndHorizontal();
//                 }
//             }
//         }
//
//         #endregion
//
//         #region Method
//
//         /// <summary>
//         /// 函数检视器
//         /// </summary>
//         private sealed class MethodInspector
//         {
//             public MethodInfo Method;
//             public ButtonAttribute Attribute;
//             public string Name;
//
//             public MethodInspector(MethodInfo method)
//             {
//                 Method = method;
//                 Attribute = method.GetCustomAttribute<ButtonAttribute>(true);
//                 Name = string.IsNullOrEmpty(Attribute.Text) ? Method.Name : Attribute.Text;
//             }
//
//             public void Painting(ObjectInspector nilInspector)
//             {
//                 if (nilInspector.targets.Length > 1)
//                     return;
//
//                 GUI.enabled = Attribute.Mode == ButtonAttribute.EnableMode.Always
//                               || (Attribute.Mode == ButtonAttribute.EnableMode.Editor && !EditorApplication.isPlaying)
//                               || (Attribute.Mode == ButtonAttribute.EnableMode.Playmode && EditorApplication.isPlaying);
//
//                 EditorGUILayout.BeginHorizontal();
//                 if (GUILayout.Button(Name, Attribute.Style))
//                 {
//                     nilInspector.HasChanged();
//
//                     if (Method.ReturnType.Name != "Void")
//                     {
//                         var result = Method.Invoke(Method.IsStatic ? null : nilInspector.target, null);
//                         Debug.Log($"点击按钮 {Name} 后，存在返回值：{result}");
//                     }
//                     else
//                     {
//                         Method.Invoke(Method.IsStatic ? null : nilInspector.target, null);
//                     }
//                 }
//
//                 EditorGUILayout.EndHorizontal();
//
//                 GUI.enabled = true;
//             }
//         }
//
//         #endregion
//     }
// }