namespace AIO
{
    /// <summary>
    /// 转换类型
    /// </summary>
    public enum ConversionType
    {
        /// <summary>
        /// 不可能转换
        /// </summary>
        Impossible,

        /// <summary>
        /// 同等转换
        /// </summary>
        Identity,

        /// <summary>
        /// 向上转换
        /// </summary>
        Upcast,

        /// <summary>
        /// 向下转换
        /// </summary>
        Downcast,

        /// <summary>
        /// 数字隐式
        /// </summary>
        NumericImplicit,

        /// <summary>
        /// 数字显示
        /// </summary>
        NumericExplicit,

        /// <summary>
        /// 用户自定义隐式
        /// </summary>
        UserDefinedImplicit,

        /// <summary>
        /// 用户自定义显式
        /// </summary>
        UserDefinedExplicit,

        /// <summary>
        /// 用户定义的数字隐式
        /// </summary>
        UserDefinedThenNumericImplicit,

        /// <summary>
        /// 用户定义的数字显式
        /// </summary>
        UserDefinedThenNumericExplicit,

        /// <summary>
        /// 集合转化为数组
        /// </summary>
        EnumerableToArray,

        /// <summary>
        /// 集合转化为列表
        /// </summary>
        EnumerableToList,

        /// <summary>
        /// 字符串
        /// </summary>
        ToString
    }
}