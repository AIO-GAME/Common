using System;
using System.Collections.Generic;

namespace AIO
{
    public interface IPoolItem : IDisposable
    {
        int UUID { get; }
    }

    public abstract class ObjPool<T>
    where T : IPoolItem
    {
        #region class

        /// <summary>
        /// 双向链表节点
        /// </summary>
        protected class Node
        {
            /// <summary>
            /// 节点值
            /// </summary>
            public T Value;

            /// <summary>
            /// 上一个节点
            /// </summary>
            public Node Prev;

            /// <summary>
            /// 下一个节点
            /// </summary>
            public Node Next;

            public Node(T value) { Value = value; }

            public override string ToString() { return $"Node(值: {Value})"; }
        }

        #endregion

        /// <summary>
        /// 哈希表，快速查找
        /// </summary>
        protected readonly Dictionary<int, Node> _cache = new Dictionary<int, Node>();

        /// <summary>
        /// 双向链表的头尾指针
        /// </summary>
        protected Node _head, _tail;

        /// <summary>
        /// 容量限制
        /// </summary>
        public int Capacity
        {
            get => _capacity;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                _capacity = value;
                while (_cache.Count > _capacity) RemoveNode(); // 如果新容量小于当前缓存数量，移除多余的对象
            }
        }

        private int _capacity;

        /// <summary>
        /// 释放对象时触发
        /// </summary>
        public event Action<T> OnRelease;

        /// <summary>
        /// 创建对象时触发
        /// </summary>
        public event Func<int, T> OnCreate;

        public ObjPool(int capacity)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            _capacity = capacity;
        }

        public override string ToString()
        {
            var current = _head.Next;
            var str     = new System.Text.StringBuilder();
            str.AppendLine($"{nameof(ObjPool<T>)} (Capacity: {_capacity}, Count: {_cache.Count})");
            str.AppendLine("Nodes:");
            while (current != null && current != _tail)
            {
                str.AppendLine(current.ToString());
                current = current.Next;
            }

            return str.ToString();
        }

        protected void InvokeOnRelease(T obj) { OnRelease?.Invoke(obj); }

        /// <summary>
        /// 允许默认值
        /// </summary>
        public bool AllowDefaultValue { get; set; } = false;

        protected T InvokeOnCreate(int uuid)
        {
            if (OnCreate is null)
            {
                if (AllowDefaultValue) return default(T);
                throw new Exception("ObjPool 未注册 OnCreate 事件");
            }

            return OnCreate.Invoke(uuid);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public abstract T Get(int key);

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="value">值</param>
        public abstract void Add(T value);

        /// <summary>
        /// 移动节点到头部
        /// </summary>
        /// <param name="node">节点</param>
        protected virtual void MoveToHead(Node node)
        {
            RemoveNode(node);
            AddNode(node);
        }

        /// <summary>
        /// 添加节点到头部
        /// </summary>
        /// <param name="node">节点</param>
        protected virtual void AddNode(Node node)
        {
            node.Next       = _head.Next;
            node.Prev       = _head;
            _head.Next.Prev = node;
            _head.Next      = node;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node">节点</param>
        protected virtual void RemoveNode(Node node)
        {
            var prevNode = node.Prev;
            var nextNode = node.Next;

            prevNode.Next = nextNode;
            nextNode.Prev = prevNode;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        protected abstract void RemoveNode();
    }
}