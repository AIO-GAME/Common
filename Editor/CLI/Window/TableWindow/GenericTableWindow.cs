#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UObject = UnityEngine.Object;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 通用表格绘制器
    /// </summary>
    [HelpURL("https://wanderer.blog.csdn.net/article/details/120796924")]
    public sealed class GenericTableWindow : GraphicWindow
    {
        private const int                           Border      = 10;
        private const int                           TitleHeight = 20;
        private       Dictionary<string, FieldInfo> _fieldInfos = new Dictionary<string, FieldInfo>();
        private       TableView<object>             _tableView;
        private       UObject                       _target;
        private       string                        _targetName;

        /// <summary>
        /// 打开通用表格绘制器
        /// </summary>
        /// <param name="target">表格数据目标实例</param>
        /// <param name="fieldName">表格数据的字段名称</param>
        public static void OpenWindow(UObject target, string fieldName)
        {
            var window = GetWindow<GenericTableWindow>();
            window.titleContent.image = EditorGUIUtility.IconContent("ScriptableObject Icon").image;
            window.titleContent.text  = "Generic Table";
            window.OnInit(target, fieldName);
        }

        private void OnInit(UObject target, string fieldName)
        {
            var fieldInfo = target.GetType()
                                  .GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (fieldInfo == null)
            {
                Debug.LogWarning($"通用表格绘制器：未从 {target.GetType().FullName} 中找到字段 {fieldName}！");
                Close();
                return;
            }

            var dataList = GetDatas(fieldInfo.GetValue(target));
            if (dataList.Count <= 0)
            {
                Debug.LogWarning($"通用表格绘制器：{target.GetType().FullName} 的字段 {fieldName} 长度为0，或不是数组、集合类型！");
                Close();
                return;
            }

            var columns = GetColumns(dataList[0].GetType());
            if (columns.Count <= 0)
            {
                Debug.LogWarning($"通用表格绘制器：{target.GetType().FullName} 的字段 {fieldName} 不是复杂类型，或类型中不含有可序列化字段！");
                Close();
                return;
            }

            _tableView = new TableView<object>(dataList, columns)
            {
                IsEnableContextClick = false
            };
            _target     = target;
            _targetName = $"{_target.GetType().FullName}.{fieldName} ({_target.name})";
        }

        private void OnTitleGUI()
        {
            if (GUILayout.Button(_targetName, EditorStyles.toolbarButton))
            {
                Selection.activeObject = _target;
                EditorGUIUtility.PingObject(_target);
            }
        }

        protected override void OnDraw()
        {
            OnTitleGUI();
            OnBodyGUI();
        }

        private void OnBodyGUI()
        {
            var rect = new Rect(0, 0, position.width, position.height);
            rect.x      += Border;
            rect.y      += Border + TitleHeight;
            rect.width  -= Border * 2;
            rect.height -= Border * 2 + TitleHeight;
            _tableView.OnGUI(rect);
        }

        protected override void OnUpdate()
        {
            if (EditorApplication.isCompiling || _tableView == null || _target == null) Close();
        }

        private static List<object> GetDatas(object field)
        {
            var datas = new List<object>();
            switch (field)
            {
                case Array array:
                    datas.AddRange(array.Cast<object>());
                    break;
                case IEnumerable<object> list:
                    datas.AddRange(list);
                    break;
            }

            return datas;
        }

        private List<TableColumn<object>> GetColumns(Type type)
        {
            _fieldInfos.Clear();
            var fieldInfos =
                type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var fieldInfo in fieldInfos)
            {
                if (!fieldInfo.IsPublic && !fieldInfo.IsDefined(typeof(SerializeField), true)) continue;
                if (!_fieldInfos.ContainsKey(fieldInfo.Name)) _fieldInfos.Add(fieldInfo.Name, fieldInfo);
            }

            var columns = new List<TableColumn<object>>();
            foreach (var item in _fieldInfos)
            {
                TableColumn<object> column = null;
                var                 field  = item.Value;
                if (field.FieldType.IsEnum)
                    column = GetEnumColumn(field);
                else if (field.FieldType == typeof(string))
                    column = GetStringColumn(field);
                else if (field.FieldType == typeof(int))
                    column = GetIntColumn(field);
                else if (field.FieldType == typeof(float))
                    column = GetFloatColumn(field);
                else if (field.FieldType == typeof(bool))
                    column = GetBoolColumn(field);
                else if (field.FieldType == typeof(Vector2))
                    column = GetVector2Column(field);
                else if (field.FieldType == typeof(Vector3))
                    column = GetVector3Column(field);
                else if (field.FieldType == typeof(Color))
                    column                                                     = GetColorColumn(field);
                else if (field.FieldType.IsSubclassOf(typeof(UObject))) column = GetObjectColumn(field);

                if (column != null)
                {
                    column.autoResize    = false;
                    column.headerContent = new GUIContent(field.Name);
                    columns.Add(column);
                }
            }

            return columns;
        }

        private TableColumn<object> GetEnumColumn(FieldInfo field)
        {
            var column = new TableColumn<object>
            {
                width   = 100,
                canSort = false,
                Compare = null,
                DrawCell = (rect, data, rowIndex, isSelected, isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = EditorGUI.EnumPopup(rect, (Enum)field.GetValue(data));
                    if (EditorGUI.EndChangeCheck())
                    {
                        field.SetValue(data, value);
                        HasChanged(_target);
                    }
                }
            };
            return column;
        }

        private TableColumn<object> GetStringColumn(FieldInfo field)
        {
            var column = new TableColumn<object>
            {
                width   = 100,
                canSort = true,
                Compare = (a, b) =>
                {
                    var x = (string)field.GetValue(a);
                    var y = (string)field.GetValue(b);
                    return string.Compare(x, y, StringComparison.CurrentCulture);
                },
                DrawCell = (rect, data, rowIndex, isSelected, isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = EditorGUI.TextField(rect, (string)field.GetValue(data));
                    if (EditorGUI.EndChangeCheck())
                    {
                        field.SetValue(data, value);
                        HasChanged(_target);
                    }
                }
            };
            return column;
        }

        private TableColumn<object> GetIntColumn(FieldInfo field)
        {
            var column = new TableColumn<object>
            {
                width   = 100,
                canSort = true,
                Compare = (a, b) =>
                {
                    var x = (int)field.GetValue(a);
                    var y = (int)field.GetValue(b);
                    return x.CompareTo(y);
                },
                DrawCell = (rect, data, rowIndex, isSelected, isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = EditorGUI.IntField(rect, (int)field.GetValue(data));
                    if (EditorGUI.EndChangeCheck())
                    {
                        field.SetValue(data, value);
                        HasChanged(_target);
                    }
                }
            };
            return column;
        }

        private TableColumn<object> GetFloatColumn(FieldInfo field)
        {
            var column = new TableColumn<object>
            {
                width   = 100,
                canSort = true,
                Compare = (a, b) =>
                {
                    var x = (float)field.GetValue(a);
                    var y = (float)field.GetValue(b);
                    return x.CompareTo(y);
                },
                DrawCell = (rect, data, rowIndex, isSelected, isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = EditorGUI.FloatField(rect, (float)field.GetValue(data));
                    if (EditorGUI.EndChangeCheck())
                    {
                        field.SetValue(data, value);
                        HasChanged(_target);
                    }
                }
            };
            return column;
        }

        private TableColumn<object> GetBoolColumn(FieldInfo field)
        {
            var column = new TableColumn<object>
            {
                width   = 40,
                canSort = false,
                Compare = null,
                DrawCell = (rect, data, rowIndex, isSelected, isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = EditorGUI.Toggle(rect, (bool)field.GetValue(data));
                    if (EditorGUI.EndChangeCheck())
                    {
                        field.SetValue(data, value);
                        HasChanged(_target);
                    }
                }
            };
            return column;
        }

        private TableColumn<object> GetVector2Column(FieldInfo field)
        {
            var column = new TableColumn<object>
            {
                width   = 100,
                canSort = false,
                Compare = null,
                DrawCell = (rect, data, rowIndex, isSelected, isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = EditorGUI.Vector2Field(rect, "", (Vector2)field.GetValue(data));
                    if (EditorGUI.EndChangeCheck())
                    {
                        field.SetValue(data, value);
                        HasChanged(_target);
                    }
                }
            };
            return column;
        }

        private TableColumn<object> GetVector3Column(FieldInfo field)
        {
            var column = new TableColumn<object>
            {
                width   = 150,
                canSort = false,
                Compare = null,
                DrawCell = (rect, data, rowIndex, isSelected, isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = EditorGUI.Vector3Field(rect, "", (Vector3)field.GetValue(data));
                    if (EditorGUI.EndChangeCheck())
                    {
                        field.SetValue(data, value);
                        HasChanged(_target);
                    }
                }
            };
            return column;
        }

        private TableColumn<object> GetColorColumn(FieldInfo field)
        {
            var column = new TableColumn<object>
            {
                width   = 100,
                canSort = false,
                Compare = null,
                DrawCell = (rect, data, rowIndex, isSelected, isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = EditorGUI.ColorField(rect, (Color)field.GetValue(data));
                    if (EditorGUI.EndChangeCheck())
                    {
                        field.SetValue(data, value);
                        HasChanged(_target);
                    }
                }
            };
            return column;
        }

        private TableColumn<object> GetObjectColumn(FieldInfo field)
        {
            var column = new TableColumn<object>
            {
                width   = 150,
                canSort = false,
                Compare = null,
                DrawCell = (rect, data, rowIndex, isSelected, isFocused) =>
                {
                    EditorGUI.BeginChangeCheck();
                    var value = EditorGUI.ObjectField(rect, field.GetValue(data) as UObject, field.FieldType, true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        field.SetValue(data, value);
                        HasChanged(_target);
                    }
                }
            };
            return column;
        }
    }
}