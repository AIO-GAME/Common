using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

public partial class AHelper
{
    /// <summary>
    /// Http 工具类
    /// </summary>
    public static class Http
    {
        /// <summary>
        /// Model对象转换为uri网址参数形式
        /// </summary>
        /// <param name="obj">Model对象</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string UriParamSerialize(in object obj)
        {
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var sb = new StringBuilder();
            sb.Append("?");
            foreach (var p in properties)
            {
                var v = p.GetValue(obj, null) ?? "";

                sb.Append(p.Name);
                sb.Append("=");
                sb.Append(Uri.EscapeDataString(v.ToString())); //将字符串转换为它的转义表示形式，HttpUtility.UrlEncode是小写
                sb.Append("&");
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
