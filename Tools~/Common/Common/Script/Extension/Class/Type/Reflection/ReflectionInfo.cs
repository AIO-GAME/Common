#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    ///     信息别名
    /// </summary>
    public abstract class ReflectionInfo : IDisposable
    {
        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     访问权限 public private protected internal
        /// </summary>
        public string Access { get; protected set; }

        /// <summary>
        ///     是否为不安全代码
        /// </summary>
        public bool IsUnsafe { get; protected set; }

        /// <summary>
        ///     是否为静态
        /// </summary>
        public bool IsStatic { get; protected set; }

        /// <summary>
        ///     是否为泛型
        /// </summary>
        public bool IsGeneric { get; protected set; }

        #region IDisposable Members

        /// <inheritdoc />
        public virtual void Dispose() { }

        #endregion


        /// <summary>
        ///     完整描述
        /// </summary>
        protected abstract string FullDescription();

        /// <inheritdoc />
        public sealed override string ToString()
        {
            return FullDescription();
        }

        /// <inheritdoc />
        public sealed override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <inheritdoc />
        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        ///     隐式转换为字符串
        /// </summary>
        /// <param name="info">
        ///     <see cref="ReflectionInfo" />
        /// </param>
        /// <returns>字符串</returns>
        public static implicit operator string(ReflectionInfo info)
        {
            return info.ToString();
        }
    }
}