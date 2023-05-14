namespace AIO
{
    using System;

    /// <summary>
    /// 版本类型
    /// </summary>
    public struct fsVersionedType
    {
        /// <summary>
        /// The direct ancestors that this type can import.
        /// </summary>
        public fsVersionedType[] Ancestors;

        /// <summary>
        /// The identifying string that is unique among all ancestors.
        /// </summary>
        public string VersionString;

        /// <summary>
        /// The modeling type that this versioned type maps back to.
        /// </summary>
        public Type ModelType;

        /// <summary>
        /// Migrate from an instance of an ancestor.
        /// </summary>
        public object Migrate(object ancestorInstance)
        {
            return Activator.CreateInstance(ModelType, ancestorInstance);
        }

        /// <summary>
        /// 内容转化为字符串
        /// </summary>
        public override string ToString()
        {
            return "fsVersionedType [ModelType=" + ModelType + ", VersionString=" + VersionString + ", Ancestors.Length=" + Ancestors.Length + "]";
        }

        /// <summary>
        /// 相等
        /// </summary>
        public static bool operator ==(fsVersionedType a, fsVersionedType b)
        {
            return a.ModelType == b.ModelType;
        }

        /// <summary>
        /// 不相等
        /// </summary>
        public static bool operator !=(fsVersionedType a, fsVersionedType b)
        {
            return a.ModelType != b.ModelType;
        }

        /// <summary>
        /// 判断是否相等
        /// </summary>
        public override bool Equals(object obj)
        {
            return
                obj is fsVersionedType &&
                ModelType == ((fsVersionedType)obj).ModelType;
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public override int GetHashCode()
        {
            return ModelType.GetHashCode();
        }
    }
}
