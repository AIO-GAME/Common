/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public partial class Utils
{
    /// <summary>
    /// XML 工具类
    /// </summary>
    public static partial class Xml
    {
        /// <summary>
        /// 序列化
        /// </summary>
        public static string Serialize<T>(
            in T sourceObj,
            in string xmlRootName = null,
            in Encoding encoding = null)
        {
            if (sourceObj == null) return string.Empty;
            try
            {
                var xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName)
                    ? new XmlSerializer(typeof(T))
                    : new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                using (var stream = new MemoryStream())
                {
                    using (var xmlWriter = new XmlTextWriter(stream, Encoding.UTF8))
                    {
                        xmlWriter.Formatting = Formatting.Indented;
                        xmlSerializer.Serialize(xmlWriter, sourceObj, ns);
                        xmlWriter.Flush();
                    }

                    stream.Flush();
                    return (encoding ?? Encoding.Default).GetString(stream.ToArray());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return string.Empty;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static T Deserialize<T>(
            in string value,
            in Encoding encoding = null)
        {
            var data = (encoding ?? Encoding.Default).GetBytes(value.Trim());
            using (var stream = new MemoryStream(data))
            {
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(stream);
            }
        }
    }
}
