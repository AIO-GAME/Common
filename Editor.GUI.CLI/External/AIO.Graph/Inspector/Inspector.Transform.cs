﻿/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    [GithubURL("https://github.com/AIO-GAME/Common#readme")]
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Transform))]
    internal sealed class TransformNilInspector : NILInspector<Transform>
    {
        private static bool _copyQuaternion = false;

        [MenuItem("CONTEXT/Transform/Copy/Location")]
        public static void CopyLocation(MenuCommand cmd)
        {
            if (cmd.context is Transform trans)
            {
                GUIUtility.systemCopyBuffer = trans.GetLocation().LocationToJson();
            }
        }

        [MenuItem("CONTEXT/Transform/Paste/Location")]
        public static void PasteLocation(MenuCommand cmd)
        {
            if (!(cmd.context is Transform trans) || string.IsNullOrEmpty(GUIUtility.systemCopyBuffer)) return;
            var location = GUIUtility.systemCopyBuffer.JsonToLocation();
            if (location == Location.Null) return;
            Undo.RecordObject(trans, "Paste Location");
            trans.SetLocation(location);
            EditorUtility.SetDirty(trans);
        }

        private PagePainter _pagePainter;
        private object _rotationGUI;
        private MethodInfo _onEnable;
        private MethodInfo _rotationField;
        private bool _onlyShowLocal = false;
        private string _lockSource;
        private bool _isLock = false;
        private bool _isLockPosition = false;
        private bool _isLockRotation = false;
        private bool _isLockScale = false;
        private GUIContent _lpGC;
        private GUIContent _lrGC;
        private GUIContent _lsGC;

        protected override void OnActivation()
        {
            _pagePainter = new PagePainter(this);
            _pagePainter.AddPage("Property", EditorGUIUtility.IconContent("ToolHandleLocal").image, PropertyGUI);
            _pagePainter.AddPage("Hierarchy", EditorGUIUtility.IconContent("ToolHandlePivot").image, HierarchyGUI);
            _pagePainter.AddPage("Copy", EditorGUIUtility.IconContent("ToolHandleCenter").image, CopyGUI);
            if (_rotationGUI == null)
            {
                var type = Type.GetType("UnityEditor.TransformRotationGUI,UnityEditor");
                if (type != null)
                {
                    _rotationGUI = Activator.CreateInstance(type);
                    _onEnable = type.GetMethod("OnEnable", BindingFlags.Instance | BindingFlags.Public);
                    _rotationField = type.GetMethod("RotationField", new Type[] { });
                }
            }

            _onEnable?.Invoke(_rotationGUI,
                new object[] { serializedObject.FindProperty("m_LocalRotation"), new GUIContent() });
            _onlyShowLocal = EditorPrefs.GetBool(EditorPrefsTable.Transform_OnlyShowLocal, false);
            _lpGC = new GUIContent
            {
                text = "LP",
                tooltip = "Local Position"
            };
            _lrGC = new GUIContent
            {
                text = "LR",
                tooltip = "Local Rotation"
            };
            _lsGC = new GUIContent
            {
                text = "LS",
                tooltip = "Local Scale"
            };
        }

        protected override void OnInhibition()
        {
            Tools.hidden = false;
        }

        protected override void OnGUI()
        {
            GUILayout.Space(5);

            _pagePainter.Painting();
        }

        private void PropertyGUI()
        {
            if (_isLock)
            {
                EditorGUILayout.HelpBox(_lockSource, MessageType.None);
            }

            if (!_onlyShowLocal && Targets.Length == 1)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("P", GUILayout.Width(20));
                GUI.enabled = false;
                EditorGUILayout.Vector3Field("", Target.position);
                GUI.enabled = true;
                if (GUILayout.Button(GCCopyPaste, "InvisibleButton", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    var gm = new GenericMenu();
                    gm.AddItem(new GUIContent("Copy"), false,
                        () => { GUIUtility.systemCopyBuffer = Target.position.ToCopyString("F4"); });
                    gm.AddDisabledItem(new GUIContent("Paste"));
                    gm.ShowAsContext();
                }

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("R", GUILayout.Width(20));
                GUI.enabled = false;
                EditorGUILayout.Vector3Field("", Target.rotation.eulerAngles);
                GUI.enabled = true;
                if (GUILayout.Button(GCCopyPaste, "InvisibleButton", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    var gm = new GenericMenu();
                    gm.AddItem(new GUIContent("Copy"), false, () =>
                    {
                        if (_copyQuaternion)
                        {
                            GUIUtility.systemCopyBuffer = Target.rotation.ToCopyString("F4");
                        }
                        else
                        {
                            var temp = Target.rotation.eulerAngles;
                            var x = ClampAngle(temp.x);
                            var y = ClampAngle(temp.y);
                            var z = ClampAngle(temp.z);
                            var angle = new Vector3(x, y, z);
                            GUIUtility.systemCopyBuffer = angle.ToCopyString("F1");
                        }
                    });
                    gm.AddDisabledItem(new GUIContent("Paste"));
                    gm.ShowAsContext();
                }

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("S", GUILayout.Width(20));
                GUI.enabled = false;
                EditorGUILayout.Vector3Field("", Target.lossyScale);
                GUI.enabled = true;
                if (GUILayout.Button(GCCopyPaste, "InvisibleButton", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    var gm = new GenericMenu();
                    gm.AddItem(
                        new GUIContent("Copy"),
                        false,
                        () => { GUIUtility.systemCopyBuffer = Target.lossyScale.ToCopyString("F4"); }
                    );
                    gm.AddDisabledItem(new GUIContent("Paste"));
                    gm.ShowAsContext();
                }

                GUILayout.EndHorizontal();
            }

            GUI.enabled = !_isLockPosition;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_lpGC, "Label", GUILayout.Width(20)))
            {
                if (Targets.Length == 1)
                {
                    var gm = new GenericMenu();
                    gm.AddItem(new GUIContent("Reset LocalPosition But Ignore Child"), false, () =>
                    {
                        Undo.RecordObject(Target, "Reset LocalPosition But Ignore Child");
                        var dir = Vector3.zero - Target.localPosition;
                        Target.localPosition = Vector3.zero;
                        HasChanged();

                        for (var i = 0; i < Target.childCount; i++)
                        {
                            var child = Target.GetChild(i);
                            Undo.RecordObject(child, "Reset LocalPosition But Ignore Child");
                            child.position -= dir;
                            EditorUtility.SetDirty(child);
                        }
                    });
                    gm.ShowAsContext();
                }
            }

            PropertyField("m_LocalPosition", "");
            GUILayout.EndHorizontal();

            GUI.enabled = !_isLockRotation;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_lrGC, "Label", GUILayout.Width(20)))
            {
                if (Targets.Length == 1)
                {
                    var gm = new GenericMenu();
                    gm.AddItem(new GUIContent("Reset LocalRotation But Ignore Child"), false, () =>
                    {
                        var pos = new Vector3[Target.childCount];
                        var rot = new Quaternion[pos.Length];
                        for (var i = 0; i < Target.childCount; i++)
                        {
                            pos[i] = Target.GetChild(i).position;
                            rot[i] = Target.GetChild(i).rotation;
                        }

                        Undo.RecordObject(Target, "Reset LocalRotation But Ignore Child");
                        Target.localRotation = Quaternion.identity;
                        HasChanged();

                        for (var i = 0; i < Target.childCount; i++)
                        {
                            var child = Target.GetChild(i);
                            Undo.RecordObject(child, "Reset LocalRotation But Ignore Child");
                            child.position = pos[i];
                            child.rotation = rot[i];
                            EditorUtility.SetDirty(child);
                        }
                    });
                    gm.ShowAsContext();
                }
            }

            _rotationField.Invoke(_rotationGUI, null);
            if (GUILayout.Button(GCCopyPaste, "InvisibleButton", GUILayout.Width(20), GUILayout.Height(20)))
            {
                GenericMenu gm = new GenericMenu();
                if (Targets.Length == 1)
                {
                    gm.AddItem(new GUIContent("Copy"), false, () =>
                    {
                        if (_copyQuaternion)
                        {
                            GUIUtility.systemCopyBuffer = Target.localRotation.ToCopyString("F4");
                        }
                        else
                        {
                            var temp = Target.localRotation.eulerAngles;
                            var x = ClampAngle(temp.x);
                            var y = ClampAngle(temp.y);
                            var z = ClampAngle(temp.z);
                            var angle = new Vector3(x, y, z);
                            GUIUtility.systemCopyBuffer = angle.ToCopyString("F1");
                        }
                    });
                    gm.AddItem(new GUIContent("Paste"), false, () =>
                    {
                        if (!string.IsNullOrEmpty(GUIUtility.systemCopyBuffer))
                        {
                            if (_copyQuaternion)
                            {
                                Undo.RecordObject(Target, "Paste localRotation value");
                                Target.localRotation =
                                    GUIUtility.systemCopyBuffer.ToPasteQuaternion(Quaternion.identity);
                                HasChanged();
                            }
                            else
                            {
                                Undo.RecordObject(Target, "Paste localRotation value");
                                Target.localRotation = GUIUtility.systemCopyBuffer.ToPasteVector3(Vector3.zero)
                                    .ToQuaternion();
                                HasChanged();
                            }
                        }
                    });
                }
                else
                {
                    gm.AddDisabledItem(new GUIContent("Copy"));
                    gm.AddDisabledItem(new GUIContent("Paste"));
                }

                gm.ShowAsContext();
            }

            GUILayout.EndHorizontal();

            GUI.enabled = !_isLockScale;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_lsGC, "Label", GUILayout.Width(20)))
            {
                if (Targets.Length == 1)
                {
                    var gm = new GenericMenu();
                    gm.AddItem(new GUIContent("Reset LocalScale But Ignore Child"), false, () =>
                    {
                        var pos = new Vector3[Target.childCount];
                        var scale = new Vector3[pos.Length];
                        for (var i = 0; i < Target.childCount; i++)
                        {
                            pos[i] = Target.GetChild(i).position;
                            scale[i] = Target.GetChild(i).lossyScale;
                        }

                        Undo.RecordObject(Target, "Reset LocalScale But Ignore Child");
                        Target.localScale = Vector3.one;
                        HasChanged();

                        for (var i = 0; i < Target.childCount; i++)
                        {
                            var child = Target.GetChild(i);
                            Undo.RecordObject(child, "Reset LocalScale But Ignore Child");
                            child.position = pos[i];
                            child.localScale = scale[i];
                            EditorUtility.SetDirty(child);
                        }
                    });
                    gm.ShowAsContext();
                }
            }

            PropertyField("m_LocalScale", "");
            GUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        private void HierarchyGUI()
        {
            if (Targets.Length > 1)
            {
                EditorGUILayout.HelpBox("Hierarchy page cannot be multi-edited.", MessageType.None);
                return;
            }

            if (_isLock)
            {
                EditorGUILayout.HelpBox(_lockSource, MessageType.None);
            }

            GUI.enabled = !_isLock;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Root", GUILayout.Width(LabelWidth));
            EditorGUILayout.ObjectField(Target.root, typeof(Transform), true);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Parent", GUILayout.Width(LabelWidth));
            GUI.color = Target.parent ? Color.white : Color.gray;
            Transform parent = EditorGUILayout.ObjectField(Target.parent, typeof(Transform), true) as Transform;
            if (parent != Target.parent)
            {
                Undo.RecordObject(Target, "Change Parent " + Target.name);
                Target.SetParent(parent);
                HasChanged();
            }

            GUI.color = Color.white;
            GUILayout.EndHorizontal();

            GUI.enabled = !_isLock && Target.childCount > 0;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Child Count", GUILayout.Width(LabelWidth));
            GUILayout.Label(Target.childCount.ToString());
            GUILayout.FlexibleSpace();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Detach", EditorStyles.miniButton))
            {
                if (EditorUtility.DisplayDialog("Prompt", "Are you sure you want to detach all children?", "Yes", "No"))
                {
                    Undo.RecordObject(Target, "Detach Children");
                    Target.DetachChildren();
                    HasChanged();
                }
            }

            GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();

            GUI.backgroundColor = Color.yellow;

            GUI.enabled = !_isLock;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Empty Parent", EditorStyles.miniButton))
            {
                CreateEmptyParent();
            }

            GUILayout.EndHorizontal();

            GUI.enabled = true;

            GUI.backgroundColor = Color.white;
        }

        private void CopyGUI()
        {
            if (Targets.Length > 1)
            {
                EditorGUILayout.HelpBox("Copy page cannot be multi-edited.", MessageType.None);
                return;
            }

            GUI.backgroundColor = Color.yellow;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("  Name  ", EditorStyles.miniButtonLeft))
            {
                GUIUtility.systemCopyBuffer = Target.name;
            }

            if (GUILayout.Button("FullName", EditorStyles.miniButtonRight))
            {
                GUIUtility.systemCopyBuffer = Target.GetType().FullName;
            }

            GUILayout.EndHorizontal();

            GUI.backgroundColor = Color.green;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(" Public Field", EditorStyles.miniButtonLeft))
            {
                GUIUtility.systemCopyBuffer = ToCSPublicField();
            }

            if (GUILayout.Button("Private Field", EditorStyles.miniButtonRight))
            {
                GUIUtility.systemCopyBuffer = ToCSPrivateField();
            }

            GUILayout.EndHorizontal();

            GUI.backgroundColor = Color.white;

            GUILayout.BeginHorizontal();
            _copyQuaternion = GUILayout.Toggle(_copyQuaternion, "Copy Quaternion");
            GUILayout.EndHorizontal();
        }

        private void CreateEmptyParent()
        {
            var parent = new GameObject("EmptyParent");
            parent.transform.SetParent(Target.parent);
            parent.transform.localPosition = Target.localPosition;
            parent.transform.localRotation = Quaternion.identity;
            parent.transform.localScale = Vector3.one;
            parent.transform.SetSiblingIndex(Target.GetSiblingIndex());
            Target.SetParent(parent.transform);
            Selection.activeGameObject = parent;
            EditorGUIUtility.PingObject(parent);
        }

        private static float ClampAngle(float angle)
        {
            if (angle > 180) angle -= 360;
            else if (angle < -180) angle += 360;
            return angle;
        }

        private string ToCSPublicField()
        {
            var fieldName = Target.name.Trim().Replace(" ", "");
            var field = $"[InspectorName(\"{Target.name}\")] public GameObject {fieldName};";
            return field;
        }

        private string ToCSPrivateField()
        {
            var fieldName = Target.name.Trim().Replace(" ", "");
            var fieldNames = fieldName.ToCharArray();
            fieldNames[0] = char.ToLower(fieldNames[0]);
            var field =
                $"[InspectorName(\"{Target.GetType().FullName}\")] private GameObject _{new string(fieldNames)};";
            return field;
        }
    }
}