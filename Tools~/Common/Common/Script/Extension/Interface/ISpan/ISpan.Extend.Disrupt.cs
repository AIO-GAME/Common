namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// 随机打乱数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array">数组</param>
        /// <remarks>
        /// 不重新分配内存 source.<see cref="MRandom{T}"/>
        /// </remarks>
        public static T[] MRandom<T>(this T[] array)
        {
            for (int i = 0, index = AHelper.Random.RandInt32(0, array.Length);
                 i < array.Length / 2;
                 i++)
            {
                if (index == i) continue;
                (array[i], array[index]) = (array[index], array[i]);
            }

            return array;
        }

        /// <summary>
        /// 随机打乱数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array">数组</param>
        /// <remarks>
        /// 并重新分配内存 source = source.<see cref="Random{T}(T[])"/>
        /// </remarks>
        public static T[] Random<T>(this T[] array)
        {
            var newArray = new T[array.Length];
            for (int i = 0, index = AHelper.Random.RandInt32(0, array.Length);
                 i < array.Length / 2;
                 i++) newArray[i] = index == i ? array[i] : array[index];

            return newArray;
        }
    }
}