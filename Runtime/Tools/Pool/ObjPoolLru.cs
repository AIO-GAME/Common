using System;

namespace AIO
{
    /// <summary>
    /// LRU 对象池 (Least Recently Used，最近最少使用算法)
    /// </summary>
    /// <typeparam name="T"> 对象类型，必须实现ILru接口 </typeparam>
    public class ObjPoolLru<T> : ObjPool<T>
    where T : IPoolItem
    {
        #region class

        private class ItemNode : Node
        {
            /// <summary>
            /// 上次使用时间
            /// </summary>
            public long LastUsedTime;

            public ItemNode(T value) : base(value) { LastUsedTime = DateTime.Now.Ticks; }

            public sealed override string ToString() { return $"UUID : {Value.UUID} (值: {Value}, 使用时间: {new DateTime(LastUsedTime)})"; }
        }

        #endregion

        public ObjPoolLru(int capacity = 8) : base(capacity) { }

        public override T Get(int key)
        {
            if (_cache.TryGetValue(key, out var node))
            {
                if (node is ItemNode value)
                {
                    value.LastUsedTime = DateTime.Now.Ticks;
                    MoveToHead(value);
                    return node.Value;
                }
            }

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
                if (node is ItemNode lruNode) lruNode.LastUsedTime = DateTime.Now.Ticks; // 更新时间
                MoveToHead(node);
            }
            else // 新增对象
            {
                var newNode = new ItemNode(value);
                _cache[key] = newNode;
                AddNode(newNode);
                while (_cache.Count > Capacity) RemoveNode(); // 池子容量已满，移除最久未使用的对象
            }
        }

        protected override void RemoveNode()
        {
            // 找到最久未使用的节点
            ItemNode leastUsedNode = null;
            foreach (var node in _cache.Values)
            {
                if (node is ItemNode lruNode)
                {
                    if (leastUsedNode == null ||
                        lruNode.LastUsedTime < leastUsedNode.LastUsedTime
                       ) leastUsedNode = lruNode;
                }
            }

            if (leastUsedNode == null) return;

            RemoveNode(leastUsedNode);
            _cache.Remove(leastUsedNode.Value.UUID); // 唯一标识
            InvokeOnRelease(leastUsedNode.Value);    // 触发回调函数
            leastUsedNode.Value?.Dispose();
        }
    }
}