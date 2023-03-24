using System.Reflection;

namespace AIO
{
    /// <summary>
    /// 字段信息扩展
    /// </summary>
    public static  partial class FieldInfoExtend
    {
        /// <summary>
        /// 是否能写入
        /// </summary>
        public static bool CanWrite(this FieldInfo fieldInfo)
        {
            return !(fieldInfo.IsInitOnly || fieldInfo.IsLiteral);
        }
    }
}