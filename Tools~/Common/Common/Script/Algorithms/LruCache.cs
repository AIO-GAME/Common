/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-08-24
|||✩ Document: ||| ->
|||✩ - - - - - |*/

namespace AIO
{
    /*
     * 例子:
     * 安卓手机都可以把软件放到后台运行，
     * 比如我先后打开了「设置」「手机管家」「日历」，那么现在他们在后台排列的顺序是这样的：「日历」「手机管家」「设置」。
     * 但是这时候如果我访问了一下「设置」界面，那么「设置」就会被提前到第一个，变成这样：「设置」「日历」「手机管家」。
     * 假设我的手机只允许我同时开 3 个应用程序，现在已经满了。
     * 那么如果我新开了一个应用「时钟」，就必须关闭一个应用为「时钟」腾出一个位置，关那个呢？
     * 按照 LRU 的策略，就关最底下的「手机管家」，因为那是最久未使用的，然后把新开的应用放到最上面：「时钟」「设置」「日历」。
     * 还有其他缓存淘汰策略，比如不要按访问的时序来淘汰，而是按访问频率（LFU 策略）来淘汰等等。
     */

    /// <summary>
    /// LRU 缓存淘汰算法 (Least Recently Used)
    /// </summary>
    public class LruCache
    {
        /// <summary>
        /// 节点信息
        /// </summary>
        public class NodeInfo
        {
            /// <summary>
            /// 节点ID
            /// </summary>
            public int id = 0;

            /// <summary>
            /// 下一个节点
            /// </summary>
            public NodeInfo Next { get; set; }

            /// <summary>
            /// 上一个节点
            /// </summary>
            public NodeInfo Prev { get; set; }
        }

        private NodeInfo[] allNodes;
        private NodeInfo head = null;
        private NodeInfo tail = null;

        /// <summary>
        /// 第一个节点
        /// </summary>
        public NodeInfo First
        {
            get { return head; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="count">节点数量</param>
        public void Init(uint count)
        {
            allNodes = new NodeInfo[count];
            for (var i = 0; i < count; i++)
            {
                allNodes[i] = new NodeInfo
                {
                    id = i,
                };
            }

            for (var i = 0; i < count; i++)
            {
                allNodes[i].Next = (i + 1 < count) ? allNodes[i + 1] : null;
                allNodes[i].Prev = (i != 0) ? allNodes[i - 1] : null;
            }

            head = allNodes[0];
            tail = allNodes[count - 1];
        }

        /// <summary>
        /// 设置节点为最新
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <returns>是否设置成功</returns>
        public bool SetActive(int id)
        {
            if (id < 0 || id >= allNodes.Length)
                return false;

            var node = allNodes[id];
            if (node == tail) return true;

            Remove(node);
            AddLast(node);
            return true;
        }

        private void AddLast(NodeInfo node)
        {
            var lastTail = tail;
            lastTail.Next = node;
            tail = node;
            node.Prev = lastTail;
        }

        private void Remove(NodeInfo node)
        {
            if (head == node)
            {
                head = node.Next;
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }
        }

        private NodeInfo RemoveLast()
        {
            var temp = allNodes[tail.id];
            allNodes[tail.id] = null;
            tail = allNodes[temp.id - 1];
            return temp;
        }
    }
}
