/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-03-25
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AIO
{
    /// <summary>
    /// Enum Helpers
    /// </summary>
    /// <typeparam name="T">
    /// The Type Parameter
    /// </typeparam>
    public class EnumHelper<T>
    {
        /// <summary>
        /// Get the description of an Enum
        /// </summary>
        /// <param name="value"> 值 </param>
        /// <param name="inherit"> 是否继承</param>
        /// <returns> 值的描述字符串 </returns>
        public static TH GetAttribute<TH, TK>(TK value, bool inherit = false)
        where TH : Attribute
        {
            if (value == null) return null;
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attributes = fieldInfo.GetCustomAttributes<TH>(inherit);
            return attributes.Length > 0 ? attributes[0] : null;
        }

        /// <summary>
        /// Get the description of an Enum
        /// </summary>
        /// <param name="value"> 值 </param>
        /// <param name="inherit"> 是否继承</param>
        /// <returns> 值的描述字符串 </returns>
        public static string GetDescription(T value, bool inherit = false)
        {
            var attribute = GetAttribute<DescriptionAttribute, T>(value, inherit);
            return attribute?.Description ?? value.ToString();
        }

        /// <summary>
        /// Return a list of all the enum values.
        /// </summary>
        /// <returns>
        /// An Enum Object List
        /// </returns>
        public static IEnumerable<T> GetEnumList() { return Enum.GetValues(typeof(T)).Cast<T>().ToList(); }
    }
}