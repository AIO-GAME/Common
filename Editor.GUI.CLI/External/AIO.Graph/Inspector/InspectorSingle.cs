/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

namespace AIO.UEditor
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 修改单个Inspertor物体属性
    /// </summary>
    //[CustomEditor(typeof(Class))]
    public abstract class InspectorSingle<T> : EmptyEditor where T : Object
    {
        /// <summary>
        /// 常量 撤销标识码
        /// </summary>
        protected const string UNDO = "Undo-";

        /// <summary>
        /// 目标对象
        /// </summary>
        protected SerializedObject SerObj;

        protected T Target;

        /// <inheritdoc />
        protected override void Awake()
        {
            Vector = new Vector2();
        }

        /// <inheritdoc />
        protected sealed override void OnEnable()
        {
            if (SerObj is null) SerObj = new SerializedObject(target);
            Target = (T)target;
            OnActivation();
        }

        protected virtual void OnActivation()
        {
        }

        /// <inheritdoc />
        protected override void OnDisable() //脚本或对象禁用时调用
        {
            EditorUtility.SetDirty(SerObj?.targetObject);
            Undo.RecordObject(SerObj?.targetObject, string.Concat(UNDO, UndoName));
        }

        /// <inheritdoc />
        protected override void OnDestroy()
        {
        }

        /// <summary>
        /// 执行这一个函数来一个自定义检视面板
        /// </summary>
        public override void OnInspectorGUI() //首次进入 执行7次  相当于updata
        {
            // 更新序列化对象的表示，仅当对象自上次调用Update后被修改或它是一个脚本时。
            SerObj?.UpdateIfRequiredOrScript();
            // 显示并修改自定义面板
            OnGUI();
            // 应用属性修改而不注册撤消操作。
            SerObj?.ApplyModifiedPropertiesWithoutUndo();
            // 在下一次调用Update()时更新hasMultipleDifferentValues缓存。
            SerObj?.SetIsDifferentCacheDirty();
            // 执行自定义面板操作
            if (GUI.changed)
            {
                OnChange();
                EditorUtility.SetDirty(SerObj?.targetObject);
                Undo.RecordObject(SerObj?.targetObject, string.Concat(UNDO, UndoName));
                Repaint(); //重新绘制
            }
        }

        /// <summary>
        /// 绘制 Inspertor 面板
        /// </summary>
        protected abstract void OnGUI();

        /// <summary>
        /// Inspertor 发生改动时调用
        /// </summary>
        protected virtual void OnChange()
        {
        }
    }
}