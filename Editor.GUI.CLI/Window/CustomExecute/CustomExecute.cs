#region

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UObject = UnityEngine.Object;

#endregion

namespace AIO.UEditor
{
    [HelpURL("https://wanderer.blog.csdn.net/article/details/102971712"), GWindow("自定义执行器", "执行器",
                                                                                  IconResource = "Editor/Icon/App/AIO",
                                                                                  Group = "Tools",
                                                                                  Menu = "AIO/Window/Custom Execute",
                                                                                  MinSizeWidth = 600, MinSizeHeight = 600
     )]
    public class CustomExecute : GraphicWindow
    {
        public enum ExecuterMode
        {
            Dynamic,
            Static
        }

        private static string       _projectPath;
        private        ExecuterMode _mode = ExecuterMode.Dynamic;

        /// <summary>
        /// 项目路径（也即是 Application.dataPath 路径的末尾去掉了 Assets）
        /// </summary>
        public static string ProjectPath
        {
            get
            {
                if (string.IsNullOrEmpty(_projectPath))
                    _projectPath = Application.dataPath.Substring(0,
                                                                  Application.dataPath.LastIndexOf("/", StringComparison.CurrentCulture) + 1);

                return _projectPath;
            }
        }

        protected override void OnDisable()
        {
            if (_csharpCodeProvider == null) return;
            _csharpCodeProvider.Dispose();
            _csharpCodeProvider = null;
        }

        public void Initialization()
        {
            _assembliesPath =
                EditorApplication.applicationPath.Substring(0,
                                                            EditorApplication.applicationPath.LastIndexOf("/", StringComparison.CurrentCulture)) +
                "/Data/Managed";

            _assemblies.Add(typeof(Type).Assembly.Location);
            _assemblies.Add(typeof(Button).Assembly.Location);
            _assemblies.Add(typeof(Editor).Assembly.Location);
            _assemblies.Add(typeof(GameObject).Assembly.Location);
            _assemblies.Add(string.Concat(ProjectPath, "Library/ScriptAssemblies/Assembly-CSharp.dll"));
            _assemblies.Add(string.Concat(ProjectPath, "Library/ScriptAssemblies/Assembly-CSharp-Editor.dll"));

            _namespace =
                "using System;\r\nusing System.Collections;\r\nusing System.Collections.Generic;\r\nusing UnityEngine;\r\nusing UnityEngine.UI;\r\nusing UnityEditor;\r\nusing DG.Tweening;\r\n";
            _code = "Debug.Log(\"Hello world!\");";

            _codeTemplate = @"#NAMESPACE#

namespace AIO
{
    public static class DynamicClass
    {
        private static void DynamicMethod()
        {
            #CODE#
        }
    }
}";
        }

        protected override void OnActivation()
        {
            _csharpCodeProvider = new CSharpCodeProvider();
            Entity              = null;
            _methodName         = "<None>";
            Initialization();
        }

        protected override void OnDraw()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Toggle(_mode == ExecuterMode.Dynamic, "Dynamic", GEStyle.LargeButtonLeft)) _mode = ExecuterMode.Dynamic;

            if (GUILayout.Toggle(_mode == ExecuterMode.Static, "Static", GEStyle.LargeButtonRight)) _mode = ExecuterMode.Static;

            GUILayout.EndHorizontal();

            if (_mode == ExecuterMode.Dynamic)
                DynamicGUI();
            else
                StaticGUI();
        }


        private void DynamicGUI()
        {
            #region Namespace

            GUILayout.BeginHorizontal("AC BoldHeader");
            _isShowNamespace = EditorGUILayout.Foldout(_isShowNamespace, "Namespace", true);
            GUILayout.EndHorizontal();

            if (_isShowNamespace)
            {
                _scrollNamespace = GUILayout.BeginScrollView(_scrollNamespace, "TextField", GUILayout.Height(150));
                _namespace       = EditorGUILayout.TextArea(_namespace, "Label");
                GUILayout.EndScrollView();
            }

            #endregion

            #region Assembly

            GUILayout.BeginHorizontal("AC BoldHeader");
            _isShowAssemblies = EditorGUILayout.Foldout(_isShowAssemblies, "Assembly", true);
            GUILayout.EndHorizontal();

            if (_isShowAssemblies)
            {
                GUILayout.BeginVertical("Box", GUILayout.Height(150));

                _scrollAssemblies = GUILayout.BeginScrollView(_scrollAssemblies);
                for (var i = 0; i < _assemblies.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(i + ".", GUILayout.Width(25));
                    _assemblies[i] = EditorGUILayout.TextField(_assemblies[i]);
                    if (GUILayout.Button("Browse", EditorStyles.miniButtonLeft, GUILayout.Width(50)))
                    {
                        var initialPath = File.Exists(_assemblies[i])
                            ? Path.GetDirectoryName(_assemblies[i])
                            : _assembliesPath;
                        var path = EditorUtility.OpenFilePanel("Browse Assembly Path", initialPath, "*.dll");
                        if (path.Length != 0)
                        {
                            _assemblies[i] = path;
                            GUI.FocusControl(null);
                        }
                    }

                    if (GUILayout.Button("Delete", EditorStyles.miniButtonRight, GUILayout.Width(50)))
                    {
                        _assemblies.RemoveAt(i);
                        GUI.FocusControl(null);
                    }

                    GUILayout.EndHorizontal();
                }

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Add", EditorStyles.miniButton, GUILayout.Width(50))) _assemblies.Add("");

                GUILayout.EndHorizontal();
                GUILayout.EndScrollView();

                GUILayout.EndVertical();
            }

            #endregion

            #region Code

            GUILayout.BeginHorizontal("AC BoldHeader");
            _isShowCode = EditorGUILayout.Foldout(_isShowCode, "Code", true);
            GUILayout.EndHorizontal();

            if (_isShowCode)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Clear Code", EditorStyles.miniButtonLeft))
                {
                    _code = "";
                    GUI.FocusControl(null);
                }

                if (GUILayout.Button("Clear Console", EditorStyles.miniButtonRight))
                {
                    var logEntries = Type.GetType("UnityEditor.LogEntries,UnityEditor");
                    var clearMethod = logEntries?.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
                    clearMethod?.Invoke(null, null);
                    GUI.FocusControl(null);
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                _scrollCode = GUILayout.BeginScrollView(_scrollCode, "TextField");
                _code       = EditorGUILayout.TextArea(_code, "Label");
                GUILayout.EndScrollView();
            }

            #endregion

            #region Execute

            GUILayout.BeginHorizontal();
            GUI.enabled         = _code != "";
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Execute", GEStyle.LargeButton)) DynamicExecute();

            GUI.backgroundColor = Color.white;
            GUI.enabled         = true;
            GUILayout.EndHorizontal();

            #endregion
        }

        private void StaticGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Entity:", GUILayout.Width(60));
            Entity = EditorGUILayout.ObjectField(Entity, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUI.enabled = Entity != null;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Target:", GUILayout.Width(60));
            var content = Target != null
                ? EditorGUIUtility.ObjectContent(Target, Target.GetType())
                : new GUIContent("<None>");
            if (GUILayout.Button(content, GEStyle.MiniPopup))
            {
                var gm = new GenericMenu();
                var components = Entity.GetComponents<Component>();
                gm.AddItem(new GUIContent("<None>"), Target == null, () => { Target = null; });
                for (var i = 0; i < components.Length; i++)
                {
                    var component = components[i];
                    gm.AddItem(new GUIContent(component.GetType().FullName), Target == component,
                               () => { Target = component; });
                }

                gm.ShowAsContext();
            }

            GUILayout.EndHorizontal();
            GUI.enabled = true;

            GUI.enabled = Target != null;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Method:", GUILayout.Width(60));
            if (GUILayout.Button(_methodName, GEStyle.MiniPopup))
            {
                var gm = new GenericMenu();
                var methods = Target.GetType().GetMethods(BindingFlags.Static | BindingFlags.Instance |
                                                          BindingFlags.Public | BindingFlags.NonPublic);
                gm.AddItem(new GUIContent("<None>"), Method == null, () => { Method = null; });
                for (var i = 0; i < methods.Length; i++)
                {
                    var method = methods[i];
                    if (method.Name.Contains("get_") || method.Name.Contains("set_")) continue;

                    gm.AddItem(new GUIContent(method.Name + "()"), Method == method, () => { Method = method; });
                }

                gm.ShowAsContext();
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            GUILayout.Label("Parameters:");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            var isValid = true;
            for (var i = 0; i < _parameters.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"{i + 1}.{_parameters[i].Name}:", GUILayout.Width(200));
                switch (_parameters[i].Type)
                {
                    case "String":
                        _parameters[i].StringValue = EditorGUILayout.TextField(_parameters[i].StringValue);
                        break;
                    case "Int32":
                        _parameters[i].IntValue = EditorGUILayout.IntField(_parameters[i].IntValue);
                        break;
                    case "Single":
                        _parameters[i].FloatValue = EditorGUILayout.FloatField(_parameters[i].FloatValue);
                        break;
                    case "Double":
                        _parameters[i].DoubleValue = EditorGUILayout.DoubleField(_parameters[i].DoubleValue);
                        break;
                    case "Boolean":
                        _parameters[i].BoolValue = EditorGUILayout.Toggle(_parameters[i].BoolValue);
                        break;
                    case "Vector2":
                        _parameters[i].Vector2Value = EditorGUILayout.Vector2Field("", _parameters[i].Vector2Value);
                        break;
                    case "Vector3":
                        _parameters[i].Vector3Value = EditorGUILayout.Vector3Field("", _parameters[i].Vector3Value);
                        break;
                    case "Color":
                        _parameters[i].ColorValue = EditorGUILayout.ColorField(_parameters[i].ColorValue);
                        break;
                    default:
                        if (_parameters[i].IsEnum)
                        {
                            _parameters[i].EnumValue = EditorGUILayout.EnumPopup(_parameters[i].EnumValue);
                        }
                        else if (_parameters[i].IsObject)
                        {
                            _parameters[i].ObjectValue = EditorGUILayout.ObjectField(_parameters[i].ObjectValue,
                                                                                     _parameters[i].ObjectType, true);
                        }
                        else
                        {
                            GUILayout.Label("Unknown type!");
                            isValid = false;
                        }

                        break;
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUI.enabled = true;

            #region Execute

            GUILayout.BeginHorizontal();
            GUI.enabled         = Method != null && isValid;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Execute", GEStyle.LargeButton)) StaticExecute();

            GUI.backgroundColor = Color.white;
            GUI.enabled         = true;
            GUILayout.EndHorizontal();

            #endregion
        }

        private void DynamicExecute()
        {
            var results =
                _csharpCodeProvider.CompileAssemblyFromSource(GenerateParameters(), GenerateCode());
            if (results.Errors.HasErrors)
            {
                Debug.LogError("执行动态工具失败：工具代码存在如下编译错误！");
                for (var i = 0; i < results.Errors.Count; i++) Debug.LogError(results.Errors[i].ToString());
            }
            else
            {
                var assembly = results.CompiledAssembly;
                Type type = assembly.DefinedTypes.First(t => t.Name == "DynamicClass");
                var method = type.GetMethod("DynamicMethod", BindingFlags.Static | BindingFlags.NonPublic);
                method?.Invoke(null, null);
            }
        }

        private CompilerParameters GenerateParameters()
        {
            var compilerParameters = new CompilerParameters();
            foreach (var assembly in _assemblies) compilerParameters.ReferencedAssemblies.Add(assembly);

            compilerParameters.GenerateExecutable = false;
            compilerParameters.GenerateInMemory   = true;
            compilerParameters.CompilerOptions    = "/unsafe /optimize";
            return compilerParameters;
        }

        private string GenerateCode()
        {
            if (string.IsNullOrEmpty(_codeTemplate)) return string.Empty;
            return _codeTemplate.Replace("#NAMESPACE#", _namespace).Replace("#CODE#", _code);
        }

        private void StaticExecute()
        {
            var parameters = new object[_parameters.Count];
            for (var i = 0; i < _parameters.Count; i++) parameters[i] = _parameters[i].GetValue();

            if (Method.IsStatic)
            {
                var returnValue = Method.Invoke(null, parameters);
                if (Method.ReturnType.Name != "Void")
                    Debug.Log($"Execute {_methodName}, Return value is: " +
                              (returnValue != null ? returnValue.ToString() : "null"));
            }
            else
            {
                var returnValue = Method.Invoke(Target, parameters);
                if (Method.ReturnType.Name != "Void")
                    Debug.Log($"Execute {_methodName}, Return value is: " +
                              (returnValue != null ? returnValue.ToString() : "null"));
            }
        }

        private void FormatMethodName()
        {
            var builder = new StringBuilder();
            builder.Append(Method.IsFamily ? "protected " : Method.IsPublic ? "public " : "private ");
            if (Method.IsStatic) builder.Append("static ");
            if (Method.IsAssembly) builder.Append("internal ");
            if (Method.IsAbstract) builder.Append("abstract ");
            if (Method.IsVirtual) builder.Append("virtual ");
            builder.Append(Method.ReturnType.Name);
            builder.Append(" ");
            builder.Append(Method.Name);
            builder.Append("(");
            for (var i = 0; i < _parameters.Count; i++)
            {
                if (i != 0) builder.Append(", ");

                if (_parameters[i].Info.IsIn) builder.Append("in ");
                if (_parameters[i].Info.IsOut) builder.Append("out ");
                builder.Append(_parameters[i].Type);
                builder.Append(" ");
                builder.Append(_parameters[i].Name);
            }

            builder.Append(")");
            _methodName = builder.ToString();
        }

        public class Parameter
        {
            public bool    BoolValue;
            public Color   ColorValue;
            public double  DoubleValue;
            public Enum    EnumValue;
            public float   FloatValue;
            public int     IntValue;
            public UObject ObjectValue;

            public string  StringValue;
            public Vector2 Vector2Value;
            public Vector3 Vector3Value;

            public Parameter(ParameterInfo parameterInfo)
            {
                Info   = parameterInfo;
                Name   = parameterInfo.Name;
                Type   = parameterInfo.ParameterType.Name;
                IsEnum = parameterInfo.ParameterType.IsEnum;
                IsObject = parameterInfo.ParameterType == typeof(UObject) ||
                           parameterInfo.ParameterType.IsSubclassOf(typeof(UObject));
                ObjectType = IsObject ? parameterInfo.ParameterType : null;

                if (IsEnum) EnumValue = (Enum)Enum.ToObject(parameterInfo.ParameterType, 0);

                if (IsObject) ObjectValue = null;
            }

            public ParameterInfo Info       { get; private set; }
            public string        Name       { get; private set; }
            public string        Type       { get; private set; }
            public bool          IsEnum     { get; private set; }
            public bool          IsObject   { get; private set; }
            public Type          ObjectType { get; private set; }

            public object GetValue()
            {
                switch (Type)
                {
                    case "String":
                        return StringValue;
                    case "Int32":
                        return IntValue;
                    case "Single":
                        return FloatValue;
                    case "Double":
                        return DoubleValue;
                    case "Boolean":
                        return BoolValue;
                    case "Vector2":
                        return Vector2Value;
                    case "Vector3":
                        return Vector3Value;
                    case "Color":
                        return ColorValue;
                    default:
                        if (IsEnum)
                            return EnumValue;
                        if (IsObject)
                            return ObjectValue;
                        return null;
                }
            }
        }

        #region Dynamic Field

        private string             _assembliesPath;
        private List<string>       _assemblies = new List<string>();
        private string             _namespace;
        private string             _code;
        private CSharpCodeProvider _csharpCodeProvider;
        private string             _codeTemplate;
        private Vector2            _scrollNamespace;
        private Vector2            _scrollAssemblies;
        private Vector2            _scrollCode;
        private bool               _isShowNamespace;
        private bool               _isShowAssemblies;
        private bool               _isShowCode = true;

        #endregion

        #region Static Field

        private GameObject      _entity;
        private Component       _target;
        private MethodInfo      _method;
        private List<Parameter> _parameters = new List<Parameter>();
        private string          _methodName = "<None>";

        private GameObject Entity
        {
            get => _entity;
            set
            {
                if (_entity == value) return;
                _entity = value;
                Target  = null;
            }
        }

        private Component Target
        {
            get => _target;
            set
            {
                if (_target == value) return;
                _target = value;
                Method  = null;
            }
        }

        private MethodInfo Method
        {
            get => _method;
            set
            {
                if (_method == value) return;
                _method = value;
                if (_method != null)
                {
                    _parameters.Clear();
                    foreach (var t in _method.GetParameters()) _parameters.Add(new Parameter(t));

                    FormatMethodName();
                }
                else
                {
                    _parameters.Clear();
                    _methodName = "<None>";
                }
            }
        }

        #endregion
    }
}