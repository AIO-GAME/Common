/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System.IO;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AIO
{
    /// <summary>
    /// Yaml 解析
    /// </summary>
    public static partial class ParseUtils
    {
        /// <summary>
        /// 序列化
        /// </summary>
        public static string YamlSerialize(object data)
        {
            var serializer = new SerializerBuilder().WithIndentedSequences().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            return serializer.Serialize(data);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public static string YamlToJson(string data)
        {
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(new StringReader(data));
            var serializer = new SerializerBuilder().JsonCompatible().Build();
            return serializer.Serialize(yamlObject);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static T YamlDeserialize<T>(string data)
        {
            return new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build().Deserialize<T>(data);
        }
    }
}