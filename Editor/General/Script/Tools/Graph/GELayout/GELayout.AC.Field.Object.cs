/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        #region 物体文本框 FieldObject

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(bool allowSceneObjects, T Obj, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(Obj, typeof(T), allowSceneObjects, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(string name, T Obj, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(name, Obj, typeof(T), allowSceneObjects, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(GUIContent name, T Obj, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(name, Obj, typeof(T), allowSceneObjects, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(GUIContent name, T Obj, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(name, Obj, type, allowSceneObjects, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(bool allowSceneObjects, T Obj, Type type, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(Obj, type, allowSceneObjects, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(string name, T Obj, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(name, Obj, type, allowSceneObjects, options);
        }

        #endregion

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(T Obj, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(Obj, typeof(T), true, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(T Obj, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(Obj, typeof(T), allowSceneObjects, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(T Obj, Type type, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(Obj, type, allowSceneObjects, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(T Obj, Type type, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(Obj, type, true, options);
        }
    }
}