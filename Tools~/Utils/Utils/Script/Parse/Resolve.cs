/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System;
    using System.Reflection;
    using System.Text;

    public static partial class ParseUtils
    {
        /// <summary>
        /// Model对象转换为uri网址参数形式
        /// </summary>
        /// <param name="obj">Model对象</param>
        public static string UriParamSerialize(object obj)
        {
            var propertis = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var sb = new StringBuilder();
            sb.Append("?");
            foreach (var p in propertis)
            {
                var v = p.GetValue(obj, null);
                if (v == null) v = "";

                sb.Append(p.Name);
                sb.Append("=");
                sb.Append(Uri.EscapeDataString(v.ToString()));//将字符串转换为它的转义表示形式，HttpUtility.UrlEncode是小写
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
