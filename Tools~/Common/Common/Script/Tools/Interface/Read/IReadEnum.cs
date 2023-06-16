using System;

namespace AIO
{
    /// <summary>
    /// 读取数据
    /// </summary>
    public interface IReadEnum
    {
        /// <summary>
        /// 读取 枚举 数据类型
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举值</returns>
        T ReadEnum<T>() where T : struct, Enum;

        /// <summary>
        /// 读取 枚举数组 数据类型
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举值数组</returns>
        T[] ReadEnumArray<T>() where T : struct, Enum;
    }
}