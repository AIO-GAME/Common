namespace AIO.Unity
{
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
        protected sealed override void Reset()
        {
            base.Reset();
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
            if (Data == null || Data.Length == 0) return;
            ToDeserialize(new BufferByte(Data));
        }

        /// <summary>
        /// 序列化
        /// </summary>
        protected virtual void OnSerialize()
        {
            var buffer = new BufferByte();
            ToSerialize(buffer);
            Data = buffer.ToArray();
        }

        /// <summary>
        /// 序列化
        /// </summary>
        protected virtual void ToSerialize(IWriteData buffer)
        {
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        protected virtual void ToDeserialize(IReadData buffer)
        {
        }


        /// <inheritdoc/>
        public override void Dispose()
        {
            Serialize();
        }
    }
}