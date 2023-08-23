namespace AIO
{
    using System.Diagnostics;

    /// <summary>
    /// 调试片段
    /// </summary>
    public class ProfiledSegment
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parent">父节点片段</param>
        /// <param name="name">名称</param>
        public ProfiledSegment(in ProfiledSegment parent, in string name)
        {
            Parent = parent;
            Name = name;
            Stopwatch = new Stopwatch();
            Children = new ProfiledSegmentCollection();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 精度计时器
        /// </summary>
        public Stopwatch Stopwatch { get; private set; }

        /// <summary>
        /// 调用次数
        /// </summary>
        public long Calls { get; set; }

        /// <summary>
        /// 父片段
        /// </summary>
        public ProfiledSegment Parent { get; private set; }

        /// <summary>
        /// 子片段集合
        /// </summary>
        public ProfiledSegmentCollection Children { get; private set; }
    }
}
