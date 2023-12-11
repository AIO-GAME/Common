namespace AIO
{
    public static partial class ExtendType
    {
        /// <summary>
        /// 使用 as 强转目标
        /// </summary>
        /// <typeparam name="T">强转的类型</typeparam>
        /// <param name="target">强转的对象</param>
        /// <returns>转换后的对象</returns>
        public static T To<T>(this object target) where T : class
        {
            return target as T;
        }
    }
}