/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-26

|||✩ - - - - - |*/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// Editor 基类 无预览窗口 数据类
    /// </summary>
    public abstract partial class NILEditor : Editor
    {
        /// <summary>
        /// 复制、粘贴按钮的GUIContent
        /// </summary>
        protected GUIContent GCCopyPaste;

        /// <summary>
        /// 开启预览窗口
        /// </summary>
        protected bool Preview { get; set; } = false;

        /// <summary>
        /// scorll pos
        /// </summary>
        protected Vector2 Vector;

        /// <summary>
        /// 撤销操作名字
        /// </summary>
        protected virtual string UndoName => GetType().FullName;

        protected IEnumerable<URLAttribute> URLArray { get; private set; }

        /// <summary>
        /// 控件标签的标准宽度
        /// </summary>
        protected virtual float LabelWidth => EditorGUIUtility.labelWidth;

        /// <summary>
        /// 是否启用运行时调试数据
        /// </summary>
        protected virtual bool IsEnableRuntimeData => true;

        /// <summary>
        /// 鼠标点击进入调用
        /// </summary>
        protected abstract void Awake(); //最先调用 修改脚本后 该方法不会重新调用

        /// <summary>
        /// 每次开启调用
        /// </summary>
        protected virtual void OnEnable()
        {
            SerializedPropertyDic.Clear();
            GCCopyPaste = new GUIContent
            {
                image = EditorGUIUtility.IconContent("d_editicon.sml").image,
                tooltip = "Copy or Paste"
            };
            URLArray = GetType().GetCustomAttributes<URLAttribute>(false);
            foreach (var attribute in URLArray) attribute.OnEnable();
        } //在awake之后调用 修改脚本后 该方法会自动重新调用

        /// <summary>
        ///
        /// </summary>
        protected abstract void OnDisable(); //脚本或对象禁用时调用

        /// <summary>
        /// 销毁时调用
        /// </summary>
        protected abstract void OnDestroy(); //并且鼠标点击其他实例 且也会调用该方法

        #region Property

        private readonly Dictionary<string, SerializedProperty> SerializedPropertyDic =
            new Dictionary<string, SerializedProperty>();

        /// <summary>
        /// 根据名字获取序列化属性
        /// </summary>
        /// <param name="propertyName">序列化属性名字</param>
        /// <returns>序列化属性</returns>
        protected SerializedProperty GetProperty(string propertyName)
        {
            SerializedProperty serializedProperty;
            if (SerializedPropertyDic.ContainsKey(propertyName))
            {
                serializedProperty = SerializedPropertyDic[propertyName];
            }
            else
            {
                serializedProperty = serializedObject.FindProperty(propertyName);
                if (serializedProperty != null)
                {
                    SerializedPropertyDic.Add(propertyName, serializedProperty);
                }
            }

            return serializedProperty;
        }

        /// <summary>
        /// 在属性字段的后面绘制复制、粘贴按钮
        /// </summary>
        /// <param name="property">属性</param>
        protected void DrawCopyPaste(SerializedProperty property)
        {
            if (!IsSupportCopyPaste(property)) return;

            if (GUILayout.Button(GCCopyPaste, "InvisibleButton", GUILayout.Width(20), GUILayout.Height(20)))
            {
                var gm = new GenericMenu();
                if (targets.Length == 1)
                {
                    gm.AddItem(new GUIContent("Copy"), false, () => { CopyValue(property); });
                    gm.AddItem(new GUIContent("Paste"), false, () => { PasteValue(property); });
                }
                else
                {
                    gm.AddDisabledItem(new GUIContent("Copy"));
                    gm.AddDisabledItem(new GUIContent("Paste"));
                }

                gm.ShowAsContext();
            }
        }

        /// <summary>
        /// 属性的类型是否支持复制粘贴
        /// </summary>
        private static bool IsSupportCopyPaste(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.Vector2
                || property.propertyType == SerializedPropertyType.Vector3
                || property.propertyType == SerializedPropertyType.Vector4
                || property.propertyType == SerializedPropertyType.Vector2Int
                || property.propertyType == SerializedPropertyType.Vector3Int
                || property.propertyType == SerializedPropertyType.Quaternion
                || property.propertyType == SerializedPropertyType.Bounds
                || property.propertyType == SerializedPropertyType.BoundsInt
                || (property.propertyType == SerializedPropertyType.Generic && property.hasChildren &&
                    property.type == "Location"))
                return true;
            return false;
        }

        /// <summary>
        /// 复制属性的值
        /// </summary>
        private void CopyValue(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                GUIUtility.systemCopyBuffer = property.vector2Value.ToCopyString("F4");
            }
            else if (property.propertyType == SerializedPropertyType.Vector3)
            {
                GUIUtility.systemCopyBuffer = property.vector3Value.ToCopyString("F4");
            }
            else if (property.propertyType == SerializedPropertyType.Vector4)
            {
                GUIUtility.systemCopyBuffer = property.vector4Value.ToCopyString("F4");
            }
            else if (property.propertyType == SerializedPropertyType.Vector2Int)
            {
                GUIUtility.systemCopyBuffer = property.vector2IntValue.ToCopyString();
            }
            else if (property.propertyType == SerializedPropertyType.Vector3Int)
            {
                GUIUtility.systemCopyBuffer = property.vector3IntValue.ToCopyString();
            }
            else if (property.propertyType == SerializedPropertyType.Quaternion)
            {
                GUIUtility.systemCopyBuffer = property.quaternionValue.ToCopyString("F4");
            }
            else if (property.propertyType == SerializedPropertyType.Bounds)
            {
                GUIUtility.systemCopyBuffer = property.boundsValue.ToCopyString("F4");
            }
            else if (property.propertyType == SerializedPropertyType.BoundsInt)
            {
                GUIUtility.systemCopyBuffer = property.boundsIntValue.ToCopyString();
            }
            else if (property.propertyType == SerializedPropertyType.Generic && property.hasChildren &&
                     property.type == "Location")
            {
                SerializedProperty position = property.FindPropertyRelative("Position");
                SerializedProperty rotation = property.FindPropertyRelative("Rotation");
                SerializedProperty scale = property.FindPropertyRelative("Scale");

                if (position != null && rotation != null && scale != null)
                {
                    Location location = new Location();
                    location.Position = position.vector3Value;
                    location.Rotation = rotation.vector3Value;
                    location.Scale = scale.vector3Value;
                    GUIUtility.systemCopyBuffer = location.LocationToJson();
                }
            }
        }

        /// <summary>
        /// 粘贴值到属性
        /// </summary>
        private static void PasteValue(SerializedProperty property)
        {
            if (string.IsNullOrEmpty(GUIUtility.systemCopyBuffer)) return;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    property.vector2Value = GUIUtility.systemCopyBuffer.ToPasteVector2(Vector3.zero);
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = GUIUtility.systemCopyBuffer.ToPasteVector3(Vector3.zero);
                    break;
                case SerializedPropertyType.Vector4:
                    property.vector4Value = GUIUtility.systemCopyBuffer.ToPasteVector4(Vector4.zero);
                    break;
                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = GUIUtility.systemCopyBuffer.ToPasteVector2Int(Vector2Int.zero);
                    break;
                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = GUIUtility.systemCopyBuffer.ToPasteVector3Int(Vector3Int.zero);
                    break;
                case SerializedPropertyType.Quaternion:
                    property.quaternionValue = GUIUtility.systemCopyBuffer.ToPasteQuaternion(Quaternion.identity);
                    break;
                case SerializedPropertyType.Bounds:
                    property.boundsValue = GUIUtility.systemCopyBuffer.ToPasteBounds();
                    break;
                case SerializedPropertyType.BoundsInt:
                    property.boundsIntValue = GUIUtility.systemCopyBuffer.ToPasteBoundsInt();
                    break;
                case SerializedPropertyType.Generic when property.hasChildren &&
                                                         property.type == "Location":
                {
                    var position = property.FindPropertyRelative("Position");
                    var rotation = property.FindPropertyRelative("Rotation");
                    var scale = property.FindPropertyRelative("Scale");

                    if (position != null && rotation != null && scale != null)
                    {
                        var location = GUIUtility.systemCopyBuffer.JsonToLocation();
                        if (location != Location.Null)
                        {
                            position.vector3Value = location.Position;
                            rotation.vector3Value = location.Rotation;
                            scale.vector3Value = location.Scale;
                        }
                    }

                    break;
                }
                case SerializedPropertyType.Integer:
                case SerializedPropertyType.Boolean:
                case SerializedPropertyType.Float:
                case SerializedPropertyType.String:
                case SerializedPropertyType.Color:
                case SerializedPropertyType.ObjectReference:
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.Enum:
                case SerializedPropertyType.Rect:
                case SerializedPropertyType.ArraySize:
                case SerializedPropertyType.Character:
                case SerializedPropertyType.AnimationCurve:
                case SerializedPropertyType.Gradient:
                case SerializedPropertyType.ExposedReference:
                case SerializedPropertyType.FixedBufferSize:
                case SerializedPropertyType.RectInt:
                case SerializedPropertyType.ManagedReference:
                default:
                    break;
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 制作一个序列化属性字段
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="isLine">自动水平布局并占用一行</param>
        /// <param name="options">布局操作</param>
        protected void PropertyField(string propertyName, bool isLine = true, params GUILayoutOption[] options)
        {
            if (isLine) EditorGUILayout.BeginHorizontal();

            var serializedProperty = GetProperty(propertyName);
            if (serializedProperty != null)
            {
                EditorGUILayout.PropertyField(serializedProperty, true, options);
                DrawCopyPaste(serializedProperty);
            }
            else
            {
                EditorGUILayout.HelpBox($"Property [{propertyName}] not found!", MessageType.Error);
            }

            if (isLine) EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 制作一个序列化属性字段
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="content">显示名称</param>
        /// <param name="isLine">自动水平布局并占用一行</param>
        /// <param name="options">布局操作</param>
        protected void PropertyField(string propertyName, string content, bool isLine = true,
            params GUILayoutOption[] options)
        {
            if (isLine) EditorGUILayout.BeginHorizontal();

            var serializedProperty = GetProperty(propertyName);
            if (serializedProperty != null)
            {
                EditorGUILayout.PropertyField(serializedProperty, new GUIContent(content), true, options);
                DrawCopyPaste(serializedProperty);
            }
            else
            {
                EditorGUILayout.HelpBox($"Property [{propertyName}] not found!", MessageType.Error);
            }

            if (isLine) EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 制作一个序列化属性字段
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="includeChildren">包含子级</param>
        /// <param name="isLine">自动水平布局并占用一行</param>
        /// <param name="options">布局操作</param>
        protected void PropertyField(string propertyName, bool includeChildren, bool isLine = true,
            params GUILayoutOption[] options)
        {
            if (isLine) EditorGUILayout.BeginHorizontal();

            var serializedProperty = GetProperty(propertyName);
            if (serializedProperty != null)
            {
                EditorGUILayout.PropertyField(serializedProperty, includeChildren, options);
                DrawCopyPaste(serializedProperty);
            }
            else
            {
                EditorGUILayout.HelpBox($"Property [{propertyName}] not found!", MessageType.Error);
            }

            if (isLine) EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 制作一个序列化属性字段
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="name">显示名称</param>
        /// <param name="includeChildren">包含子级</param>
        /// <param name="isLine">自动水平布局并占用一行</param>
        /// <param name="options">布局操作</param>
        protected void PropertyField(string propertyName, string name, bool includeChildren, bool isLine = true,
            params GUILayoutOption[] options)
        {
            if (isLine) EditorGUILayout.BeginHorizontal();

            var serializedProperty = GetProperty(propertyName);
            if (serializedProperty != null)
            {
                EditorGUILayout.PropertyField(serializedProperty, new GUIContent(name), includeChildren, options);
                DrawCopyPaste(serializedProperty);
            }
            else
            {
                EditorGUILayout.HelpBox($"Property [{propertyName}] not found!", MessageType.Error);
            }

            if (isLine) EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}
