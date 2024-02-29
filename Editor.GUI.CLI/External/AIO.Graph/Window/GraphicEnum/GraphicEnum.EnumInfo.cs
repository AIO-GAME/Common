using System;
using System.Collections.Generic;

namespace AIO.UEditor
{
    public partial class GraphicEnum
    {
        /// <summary>
        /// 枚举信息
        /// </summary>
        protected class EnumInfo
        {
            /// <summary>
            /// 枚举信息字典
            /// </summary>
            // public int[] Values { get; private set; }
            public Dictionary<object, string> DescriptionDic;

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// 创建枚举信息
            /// </summary>
            /// <param name="type">类型</param>
            /// <returns><see cref="EnumInfo"/></returns>
            public static EnumInfo Create(in Type type)
            {
                var code = type.FullName.GetHashCode();
                if (!Data.ContainsKey(code))
                {
                    var info = new EnumInfo();
                    info.Name = type.Name;
                    info.DescriptionDic = GetDescription(type);

                    Data.Add(code, info);
                }

                return Data[code];
            }

            /// <summary>
            /// 创建枚举信息
            /// </summary>
            public static EnumInfo Create<T>()
            {
                return Create(typeof(T));
            }
        }
    }
}