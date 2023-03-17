/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System;

    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static partial class EnumExtend
    {
        /// <summary>
        /// 获取属性值
        /// </summary>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            if (value == null) { throw new ArgumentException("value"); }
            var description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes = (T[])fieldInfo.GetCustomAttributes(typeof(T), false);
            if (attributes != null && attributes.Length > 0) return attributes[0];
            else return null;
        }
    }
}
