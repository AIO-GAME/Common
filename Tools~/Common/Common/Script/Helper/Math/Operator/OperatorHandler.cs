namespace AIO
{
    /// <summary>
    /// 运算符操作
    /// </summary>
    public abstract class OperatorHandler
    {
        /// <summary>
        /// 运算符操作
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="verb">动词</param>
        /// <param name="symbol">标志</param>
        /// <param name="customMethodName">定制方法名</param>
        protected OperatorHandler(
            in string name,
            in string verb,
            in string symbol,
            in string customMethodName)
        {
            Ensure.That(nameof(name)).IsNotNull(name);
            Ensure.That(nameof(verb)).IsNotNull(verb);
            Ensure.That(nameof(symbol)).IsNotNull(symbol);

            Name = name;
            Verb = verb;
            Symbol = symbol;
            CustomMethodName = customMethodName;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 动词
        /// </summary>
        public string Verb { get; }

        /// <summary>
        /// 标志
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// 自定义函数名
        /// </summary>
        public string CustomMethodName { get; }
    }
}