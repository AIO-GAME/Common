using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 轮询引用管理
    /// </summary>
    public class fsCyclicReferenceManager
    {
        private Dictionary<object, int> _objectIds = new Dictionary<object, int>(ObjectReferenceEqualityComparator.Instance);
        private int _nextId;

        private Dictionary<int, object> _marked = new Dictionary<int, object>();
        private int _depth;

        /// <summary>
        /// 进入
        /// </summary>
        public void Enter()
        {
            _depth++;
        }

        /// <summary>
        /// 退出
        /// </summary>
        public bool Exit()
        {
            _depth--;

            if (_depth == 0)
            {
                _objectIds = new Dictionary<object, int>(ObjectReferenceEqualityComparator.Instance);
                _nextId = 0;
                _marked = new Dictionary<int, object>();
            }

            if (_depth < 0)
            {
                _depth = 0;
                throw new InvalidOperationException("Internal Error - Mismatched Enter/Exit. Please report a bug at https://github.com/jacobdufault/fullserializer/issues with the serialization data.");
            }

            return _depth == 0;
        }

        /// <summary>
        /// 获取引用对象
        /// </summary>
        public object GetReferenceObject(in int id)
        {
            if (_marked.ContainsKey(id) == false)
            {
                throw new InvalidOperationException("Internal Deserialization Error - Object " +
                    "definition has not been encountered for object with id=" + id +
                    "; have you reordered or modified the serialized data? If this is an issue " +
                    "with an unmodified Full Serializer implementation and unmodified serialization " +
                    "data, please report an issue with an included test case.");
            }

            return _marked[id];
        }

        /// <summary>
        /// 添加引用对象ID
        /// </summary>
        public void AddReferenceWithId(in int id, in object reference)
        {
            _marked[id] = reference;
        }

        /// <summary>
        /// 获取引用对象ID
        /// </summary>
        public int GetReferenceId(in object item)
        {
            int id;
            if (_objectIds.TryGetValue(item, out id) == false)
            {
                id = _nextId++;
                _objectIds[item] = id;
            }
            return id;
        }

        /// <summary>
        /// 是否为引用
        /// </summary>
        public bool IsReference(in object item)
        {
            return _marked.ContainsKey(GetReferenceId(item));
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public void MarkSerialized(in object item)
        {
            var referenceId = GetReferenceId(item);

            if (_marked.ContainsKey(referenceId))
            {
                throw new InvalidOperationException("Internal Error - " + item +
                    " has already been marked as serialized");
            }

            _marked[referenceId] = item;
        }

        // We use the default ReferenceEquals when comparing two objects because
        // custom objects may override equals methods. These overriden equals may
        // treat equals differently; we want to serialize/deserialize the object
        // graph *identically* to how it currently exists.
        private class ObjectReferenceEqualityComparator : IEqualityComparer<object>
        {
            bool IEqualityComparer<object>.Equals(object x, object y)
            {
                return ReferenceEquals(x, y);
            }

            int IEqualityComparer<object>.GetHashCode(object obj)
            {
                return RuntimeHelpers.GetHashCode(obj);
            }

            public static readonly IEqualityComparer<object> Instance = new ObjectReferenceEqualityComparator();
        }
    }
}
