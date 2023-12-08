/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 空 Inspector 物体属性
    /// </summary>
    //[CustomEditor(typeof(Class))]
    public abstract class NILInspector<E> : NILEditor where E : Object
    {
        /// <summary>
        /// 常量 撤销标识码
        /// </summary>
        protected const string UNDO = "Undo-";

        /// <summary>
        /// 目标对象
        /// </summary>
        private SerializedObject SerObj;

        /// <summary>
        /// 当前目标
        /// </summary>
        protected E Target;

        /// <summary>
        /// 当前的所有目标
        /// </summary>
        protected E[] Targets;

        /// <summary>
        /// 是否启用宽模式
        /// </summary>
        protected virtual bool IsWideMode => true;

        /// <summary>
        /// 是否启用基础属性展示
        /// </summary>
        protected virtual bool IsEnableBaseInspectorGUI => false;

        /// <inheritdoc />
        protected override void Awake()
        {
            Vector = new Vector2();
        }

        /// <inheritdoc />
        protected sealed override void OnEnable()
        {
            base.OnEnable();
            if (SerObj is null) SerObj = new SerializedObject(target);
            Target = target as E;
            if (targets != null)
            {
                var convertArray = new E[targets.Length];
                for (var i = 0; i < targets.Length; i++)
                    convertArray[i] = targets[i] as E;
                Targets = convertArray;
            }

            OnActivation();

            if (EditorApplication.isPlaying) OnRuntimeEnable();
        }

        protected virtual void OnActivation()
        {
        }

        /// <summary>
        /// 运行时 Enable
        /// </summary>
        protected virtual void OnRuntimeEnable()
        {
        }

        /// <summary>
        /// 运行时 Disable
        /// </summary>
        protected virtual void OnRuntimeDisable()
        {
        }

        /// <inheritdoc />
        protected sealed override void OnDisable() //脚本或对象禁用时调用
        {
            if (target is null) return;
            if (target.Equals(null)) return;
            EditorUtility.SetDirty(target);
            Undo.RecordObject(target, string.Concat(UNDO, UndoName));

            SerObj?.SetIsDifferentCacheDirty();
            SerObj?.ApplyModifiedProperties();
            SerObj?.Update();
            SerObj = null;

            OnInhibition();
        }

        protected virtual void OnInhibition()
        {
        }

        /// <inheritdoc />
        protected override void OnDestroy()
        {
        }

        protected override void OnHeaderGUI()
        {
            base.OnHeaderGUI();
        }

        /// <summary>
        /// 执行这一个函数来一个自定义检视面板
        /// </summary>
        public override void OnInspectorGUI() //首次进入 执行7次  相当于update
        {
            if (EditorGUIUtility.wideMode != IsWideMode) EditorGUIUtility.wideMode = IsWideMode;

            serializedObject.Update();
            // 更新序列化对象的表示，仅当对象自上次调用Update后被修改或它是一个脚本时。
            SerObj?.UpdateIfRequiredOrScript();
            if (URLArray != null)
            {
                // 在折叠栏边框上添加按钮
                var headerRect = new Rect(3, 2, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight);
                foreach (var item in URLArray)
                {
                    GUI.enabled = !string.IsNullOrEmpty(item.URL);
                    if (GUI.Button(new Rect(headerRect), item.Content, GEStyle.IconButton))
                    {
                        Application.OpenURL(item.URL);
                    }

                    headerRect.y += EditorGUIUtility.singleLineHeight;

                    GUI.enabled = true;
                }
            }

            if (IsEnableBaseInspectorGUI) base.OnInspectorGUI();

            // 显示并修改自定义面板
            OnGUI();
            if (IsEnableRuntimeData && EditorApplication.isPlaying)
            {
                GUI.backgroundColor = Color.cyan;
                GUI.color = Color.white;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Runtime Data", EditorStyles.boldLabel);
                EditorGUILayout.EndHorizontal();

                OnGUIRuntime();

                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();
            // 应用属性修改而不注册撤消操作。
            SerObj?.ApplyModifiedPropertiesWithoutUndo();
            // 在下一次调用Update()时更新hasMultipleDifferentValues缓存。
            SerObj?.SetIsDifferentCacheDirty();
            // 执行自定义面板操作
            if (GUI.changed)
            {
                OnChange();
                if (target is null) return;
                EditorUtility.SetDirty(target);
                Undo.RecordObject(target, string.Concat(UNDO, UndoName));
                SerObj?.SetIsDifferentCacheDirty();
                SerObj?.ApplyModifiedProperties();
                SerObj?.Update();
                Repaint(); //重新绘制
            }
        }

        protected virtual void OnGUIRuntime()
        {
        }

        /// <summary>
        /// 标记目标已改变
        /// </summary>
        /// <param name="markTarget">是否仅标记单个 target</param>
        protected void HasChanged(bool markTarget = false)
        {
            if (markTarget) EditorUtility.SetDirty(target);
            else
            {
                foreach (var item in targets)
                {
                    EditorUtility.SetDirty(item);
                }
            }

            if (EditorApplication.isPlaying) return;
            if (target is Component component && component.gameObject.scene.IsValid())
                EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
        }

        /// <summary>
        /// 绘制 Inspector 面板
        /// </summary>
        protected abstract void OnGUI();

        /// <summary>
        /// Inspector 发生改动时调用
        /// </summary>
        protected virtual void OnChange()
        {
        }
    }
}