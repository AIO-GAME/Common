using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AIO
{
    /// <summary>
    /// 命名空间
    /// </summary>
    public sealed class Namespace
    {
        private Namespace(in string fullName)
        {
            FullName = fullName;

            if (fullName != null)
            {
                var parts = fullName.Split('.');

                Name = parts[parts.Length - 1];

                if (parts.Length > 1)
                {
                    Root = parts[0];
                    Parent = fullName.Substring(0, fullName.LastIndexOf('.'));
                }
                else
                {
                    Root = this;
                    IsRoot = true;
                    Parent = Global;
                }
            }
            else
            {
                Root = this;
                IsRoot = true;
                IsGlobal = true;
            }
        }

        /// <summary>
        /// 根节点命名空间
        /// </summary>
        public Namespace Root { get; }

        /// <summary>
        /// 父命名空间
        /// </summary>
        public Namespace Parent { get; }

        /// <summary>
        /// 完整命名空间
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 是否为根节点
        /// </summary>
        public bool IsRoot { get; }

        /// <summary>
        /// 是否为全局
        /// </summary>
        public bool IsGlobal { get; }

        /// <summary>
        /// 获取所有父类命名空间
        /// </summary>
        public IEnumerable<Namespace> Ancestors
        {
            get
            {
                var ancestor = Parent;

                while (ancestor != null)
                {
                    yield return ancestor;
                    ancestor = ancestor.Parent;
                }
            }
        }

        /// <summary>
        /// 获取所有父类命名空间
        /// </summary>
        public IEnumerable<Namespace> AndAncestors()
        {
            yield return this;

            foreach (var ancestor in Ancestors)
            {
                yield return ancestor;
            }
        }


        /// <inheritdoc/>
        public override int GetHashCode()
        {
            if (FullName == null)
            {
                return 0;
            }

            return FullName.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return FullName;
        }

        static Namespace()
        {
            collection = new Collection();
        }

        private static readonly Collection collection;

        /// <summary>
        /// 全局命名空间
        /// </summary>
        public static Namespace Global { get; } = new Namespace(null);

        /// <summary>
        /// 转化为完整命名空间
        /// </summary>
        public static Namespace FromFullName(string fullName)
        {
            if (fullName == null)
            {
                return Global;
            }

            if (!collection.TryGetValue(fullName, out var @namespace))
            {
                @namespace = new Namespace(fullName);
                collection.Add(@namespace);
            }

            return @namespace;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Namespace;

            if (other == null)
            {
                return false;
            }

            return FullName == other.FullName;
        }
      
        /// <summary>
        /// 隐式转换
        /// </summary>
        public static implicit operator Namespace(in string fullName)
        {
            return FromFullName(fullName);
        }
   
        /// <summary>
        /// 隐式转换
        /// </summary>
        public static implicit operator string(in Namespace @namespace)
        {
            return @namespace.FullName;
        }
      
        /// <summary>
        /// 等于
        /// </summary>
        public static bool operator ==(in Namespace a, in Namespace b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        /// <summary>
        /// 不等于
        /// </summary>
        public static bool operator !=(in Namespace a, in Namespace b)
        {
            return !(a == b);
        }

        private class Collection : KeyedCollection<string, Namespace>, IKeyedCollection<string, Namespace>
        {
            protected override string GetKeyForItem(Namespace item)
            {
                return item.FullName;
            }

            /// <inheritdoc/>
            public bool TryGetValue(string key, out Namespace value)
            {
                if (Dictionary == null)
                {
                    value = default(Namespace);
                    return false;
                }

                return Dictionary.TryGetValue(key, out value);
            }
        }
    }
}