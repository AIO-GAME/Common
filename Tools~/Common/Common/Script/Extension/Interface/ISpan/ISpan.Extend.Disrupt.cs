/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-02-28
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// 随机打乱数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array">数组</param>
        public static void Disrupt<T>(this T[] array)
        {
            T tmp;
            for (int i = 0, index; i < array.Length; i++)
            {
                index = AHelper.Random.RandInt32(0, array.Length);
                if (index == i) continue;
                tmp = array[i];
                array[i] = array[index];
                array[index] = tmp;
            }
        }
    }
}