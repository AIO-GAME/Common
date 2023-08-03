namespace AIO
{
    using System;

    using System.Reflection;

    /// <summary>
    /// 分散程序集名称
    /// </summary>
    public struct LooseAssemblyName
    {
        /// <summary>
        /// 名称
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// 分散程序集名称
        /// </summary>
        public LooseAssemblyName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is LooseAssemblyName))
            {
                return false;
            }

            return ((LooseAssemblyName)obj).Name == Name;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Utils.Hash.GetHashCode(Name);
        }

        /// <inheritdoc/>
        public static bool operator ==(LooseAssemblyName a, LooseAssemblyName b)
        {
            return a.Equals(b);
        }

        /// <inheritdoc/>
        public static bool operator !=(LooseAssemblyName a, LooseAssemblyName b)
        {
            return !(a == b);
        }

        /// <inheritdoc/>
        public static implicit operator LooseAssemblyName(string name)
        {
            return new LooseAssemblyName(name);
        }

        /// <inheritdoc/>
        public static implicit operator string(LooseAssemblyName name)
        {
            return name.Name;
        }

        /// <inheritdoc/>
        public static explicit operator LooseAssemblyName(AssemblyName strongAssemblyName)
        {
            return new LooseAssemblyName(strongAssemblyName.Name);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }
    }
}
