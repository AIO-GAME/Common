/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Internal;
using Object = UnityEngine.Object;

namespace UnityEditor
{
    public partial class GELayout
    {
        #region 物体文本框 FieldObject

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(T Obj, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(Obj, typeof(T), allowSceneObjects, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(T Obj, string name, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(name, Obj, typeof(T), allowSceneObjects, options);
        }

        /// <summary> 物体文本框 FieldObject </summary>
        public static T Field<T>(T Obj, GUIContent name, bool allowSceneObjects, params GUILayoutOption[] options) where T : Object
        {
            return (T)EditorGUILayout.ObjectField(name, Obj, typeof(T), allowSceneObjects, options);
        }

        #endregion
    }
}