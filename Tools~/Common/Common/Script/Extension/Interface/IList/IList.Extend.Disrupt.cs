/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-02-28
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System.Collections.Generic;

namespace AIO
{
    partial class ExtendIList
    {
        /// <summary>
        /// 随机打乱集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="array">集合</param>
        public static void Disrupt<T>(this IList<T> array)
        {
            T tmp;
            for (int i = 0, index; i < array.Count; i++)
            {
                index = AHelper.Random.RandInt32(0, array.Count);
                if (index == i) continue;
                tmp = array[i];
                array[i] = array[index];
                array[index] = tmp;
            }
        }
    }
}