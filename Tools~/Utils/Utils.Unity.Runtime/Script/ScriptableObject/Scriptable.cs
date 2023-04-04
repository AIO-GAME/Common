namespace AIO.Unity
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

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

    /// <summary>
    /// Unity 可持续化数据 
    /// 保存为二进制数据
    /// 只保存数据 需要需要编辑重写 
    /// 请使用Editor重写
    /// </summary>
    public class ScriptableData : ScriptableBasics
    {
        /// <inheritdoc/>
        protected sealed override void Awake()
        {
            base.Awake();
        }

        /// <inheritdoc/>
        protected sealed override void OnDestroy()
        {
            base.OnDestroy();
        }

        /// <inheritdoc/>
        protected sealed override void OnValidate()
        {
            base.OnValidate();
        }

        /// <inheritdoc/>
        protected sealed override void OnDisable()
        {
            base.OnDisable();
        }

        /// <inheritdoc/>
        protected sealed override void OnEnable()
        {
            base.OnEnable();
        }

        /// <inheritdoc/>
        public sealed override bool Equals(object other)
        {
            return base.Equals(other);
        }

        /// <inheritdoc/>
        public sealed override int GetHashCode()
        {
            return Utils.Hash.GetHashCode(Data);
        }

        /// <inheritdoc/>
        public sealed override string ToString()
        {
            return base.ToString();
        }

        /// <inheritdoc/>
        public sealed override void Deserialize()
        {
            OnDeserialize();
        }

        /// <inheritdoc/>
        public sealed override void Serialize()
        {
            OnSerialize();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        protected virtual void OnDeserialize()
        {

        }

        /// <summary>
        /// 序列化
        /// </summary>
        protected virtual void OnSerialize()
        {

        }

        /// <inheritdoc/>
        public sealed override void Dispose()
        {
            Serialize();
        }
    }
}
