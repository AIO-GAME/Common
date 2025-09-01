using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO
{
    /// <summary>
    /// Lfu 对象池 (Least Frequently Used，最少使用频率算法) 辅助算法(LRU的改进版)
    /// </summary>
    /// <typeparam name="T"> 对象类型，必须实现ILru接口 </typeparam>
    public sealed class ObjPoolLfu<T> : ObjPool<T>
    where T : IPoolItem
    {
        #region class

        private class ItemNode : Node
        {
            /// <summary>
            /// 访问频率
            /// </summary>
            public int Frequency;

            /// <summary>
            /// 上次使用时间
            /// </summary>
            public long LastUsedTime;

            public ItemNode(T value) : base(value)
            {
                Frequency    = 0;
                LastUsedTime = DateTime.Now.Ticks;
            }

            public sealed override string ToString() { return $"UUID : {Value.UUID} (值: {Value} 访问频率: {Frequency}, 使用时间: {new DateTime(LastUsedTime)})"; }
        }

        #endregion

        /// <summary>
        /// 存储最近的调用记录（环形缓冲区，最多记录<see cref="MaxCalls"/>次）
        /// </summary>
        private Queue<int> _recentCalls;

        /// <summary>
        /// 最近的调用记录（环形缓冲区，最多记录<see cref="MaxCalls"/>次）
        /// </summary>
        public int[] RecentCalls => _recentCalls.ToArray();

        /// <summary>
        /// 最大调用记录数
        /// </summary>
        public int MaxCalls
        {
            get => _maxCalls;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "MaxCalls must be greater than 0.");
                _maxCalls = value;
                while (_recentCalls.Count > MaxCalls)
                {
                    var id                                          = _recentCalls.Dequeue(); // 移除最老的调用记录
                    if (_cache[id] is ItemNode node) node.Frequency = GetFrequencyForRecentCalls(id);
                }
            }
        }

        private int _maxCalls = 100;

        public ObjPoolLfu(int capacity = 8) : base(capacity) { _recentCalls = new Queue<int>(); }

        public override T Get(int key)
        {
            if (_cache.TryGetValue(key, out var node))
            {
                if (node is ItemNode value) // 访问过的对象移动到头部
                {
                    RecordCall(key);                         // 记录调用
                    value.LastUsedTime = DateTime.Now.Ticks; // 更新时间
                    MoveToHead(value);
                    return node.Value;
                }
            }

            // 对象不存在，创建新对象
            var newObj = InvokeOnCreate(key);
            Add(newObj);
            return newObj;
        }

        public override void Add(T value)
        {
            if (_head == null)
            {
                _head      = new ItemNode(value);
                _tail      = new ItemNode(value);
                _head.Next = _tail;
                _tail.Prev = _head;
            }

            var key = value.UUID;
            if (_cache.ContainsKey(key))
            {
                var node = _cache[key];
                node.Value = value;
                RecordCall(key);                                                         // 记录调用
                if (node is ItemNode lruNode) lruNode.LastUsedTime = DateTime.Now.Ticks; // 更新时间
                MoveToHead(node);
            }
            else // 新增对象
            {
                var newNode = new ItemNode(value);
                _cache[key] = newNode;
                RecordCall(key); // 记录调用
                AddNode(newNode);
                while (_cache.Count > Capacity) RemoveNode(); // 池子容量已满，移除最久未使用的对象
            }
        }

        protected override void RemoveNode()
        {
            // 找到最少使用的节点（频率最小），如果有多个相同频率的节点，则选择最久未使用的节点
            ItemNode leastUsedNode = null;
            foreach (var node in _cache.Values)
            {
                if (node is ItemNode lruNode)
                {
                    if (leastUsedNode == null ||
                        lruNode.Frequency < leastUsedNode.Frequency ||
                        (lruNode.Frequency == leastUsedNode.Frequency && lruNode.LastUsedTime < leastUsedNode.LastUsedTime)
                       ) leastUsedNode = lruNode;
                }
            }

            if (leastUsedNode == null) return;


            var calls = _recentCalls.ToList(); // 给调用记录一并移除
            calls.RemoveAll(id => id == leastUsedNode.Value.UUID);
            _recentCalls = new Queue<int>(calls);

            RemoveNode(leastUsedNode);
            _cache.Remove(leastUsedNode.Value.UUID); // 唯一标识
            InvokeOnRelease(leastUsedNode.Value);    // 触发回调函数
            leastUsedNode.Value?.Dispose();
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine($"{nameof(ObjPool<T>)} (容量: {Capacity}, 数据: {_cache.Count}, 当前调用记录数: {_recentCalls.Count})");
            str.AppendLine("节点:");
            var current = _head.Next;
            while (current != null && current != _tail)
            {
                str.AppendLine(current.ToString());
                current = current.Next;
            }

            return str.ToString();
        }

        /// <summary>
        /// 获取对象的频率，基于最近 <see cref="MaxCalls"/> 次调用
        /// </summary>
        /// <param name="key"> 对象唯一标识 </param>
        /// <returns> 访问频率 </returns>
        private int GetFrequencyForRecentCalls(int key) { return _recentCalls.Count(item => item == key); }

        /// <summary>
        /// 记录调用
        /// </summary>
        /// <param name="key"></param>
        private void RecordCall(int key)
        {
            _recentCalls.Enqueue(key); // 记录当前调用的对象
            if (_cache[key] is ItemNode item) item.Frequency = GetFrequencyForRecentCalls(key);

            while (_recentCalls.Count > MaxCalls)
            {
                var id                                          = _recentCalls.Dequeue(); // 移除最老的调用记录
                if (_cache[id] is ItemNode node) node.Frequency = GetFrequencyForRecentCalls(id);
            }
        }
    }
}