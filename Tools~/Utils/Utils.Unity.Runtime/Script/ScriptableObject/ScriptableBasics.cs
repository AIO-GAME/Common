﻿namespace AIO.Unity
{
    using System;
    using System.ComponentModel;

    using UnityEngine;

    /// <summary>
    /// Unity 可持续化数据
    /// </summary>
    public abstract class ScriptableBasics : ScriptableObject, ISerialize, IDisposable
    {
        /// <inheritdoc/>
        public byte[] Data
        {
            get
            {
                return data;
            }
            protected set
            {
                data = value;
            }
        }

        [Description("数据")]
        [SerializeReference, SerializeField] private byte[] data;

        /// <summary>
        /// 此函数在脚本启动时调用
        /// </summary>
        protected virtual void Awake()
        {

        }

        /// <summary>
        /// 当对象已启用并处于活动状态时调用此函数
        /// </summary>
        protected virtual void OnEnable()
        {
        }

        /// <summary>
        /// 要销毁对象时调用此函数
        /// </summary>
        protected virtual void OnDestroy()
        {

        }

        /// <summary>
        /// 对象变为禁用或非活跃状态时调用此函数
        /// </summary>
        protected virtual void OnDisable()
        {

        }

        /// <summary>
        /// 重置为默认值。
        /// </summary>
        protected virtual void Reset()
        {

        }

        /// <summary>
        /// 当该脚本被加载或检视面板的值被修改时调用此函数(仅在编辑器中调用）
        /// </summary>
        protected virtual void OnValidate()
        {

        }

        /// <inheritdoc/>
        public abstract void Deserialize();

        /// <inheritdoc/>
        public abstract void Serialize();

        /// <inheritdoc/>
        public abstract void Dispose();
    }

}
