#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: Json

        /// <summary>
        /// Json 工具类
        /// </summary>
        [Description("https://www.newtonsoft.com/json/help/html/N_Newtonsoft_Json.htm")]
        public class Json
        {
            static Json()
            {
                JSONNormalSettings = new JsonSerializerSettings
                {
                    //设置区域格式
                    Culture = CultureInfo.CurrentCulture,
                    //内容处理方式 文件 远端 压缩 多应用程序 复制等
                    Context = new StreamingContext(StreamingContextStates.All),
                    //[Ignore:默认] [Indented:文本根据 Indentation IndentChar 压缩] 格式化 
                    Formatting = Formatting.Indented,
                    //[Ignore:默认] [Include:包含]     空值
                    NullValueHandling = NullValueHandling.Ignore,
                    //[Ignore:默认] [DateTime:解析为DateTime] [DateTimeOffset:解析为DateTimeOffset]
                    DateParseHandling = DateParseHandling.DateTime,
                    //[Ignore:在序列化对象时,忽略成员值与成员默认值相同的成员,这样它就不会被写入JSON.该选项将忽略所有默认值(例如对象和可空类型为null;0表示整数、小数和浮点数;布尔值为假).可以通过在属性上放置DefaultValueAttribute来更改忽略的默认值.]
                    //[Include:在序列化对象时,包含成员值与成员默认值相同的成员.包含的成员被写入JSON.反序列化时没有效果.]
                    //[Populate:在反序列化时,具有默认值但没有JSON的成员将被设置为默认值.]
                    //[IgnoreAndPopulate:在序列化对象时忽略成员值与成员默认值相同的成员,在反序列化时将成员设置为成员的默认值.]
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    //[Ignore:忽略一个缺失的成员,不要试图反序列化它.] [Error:在反序列化过程中遇到缺少成员时抛出JsonSerializationException.]  
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    //[Default:首先尝试使用公共默认构造函数,然后使用单个参数化构造函数,然后使用非公共默认构造函数.] 
                    //[AllowNonPublicDefaultConstructor:Json.NET将在回落到参数化构造函数之前使用非公共的默认构造函数.] 
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    //[True:反序列化对象后将检查附加内容 False:反之]
                    CheckAdditionalContent = false,
                    //[Ignore:忽略循环引用 不要序列化] [Error:当遇到循环时抛出JsonSerializationException] [Serialize:序列化循环引用.]
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //[Default:只有控制字符(例如换行符)被转义]
                    //[EscapeNonAscii:所有非ascii和控制字符(例如换行符)都会转义。]
                    //[EscapeHtml:HTML(<， >， &， '， ")和控制字符(例如换行符)被转义.]
                    StringEscapeHandling       = StringEscapeHandling.EscapeNonAscii,
                    PreserveReferencesHandling = PreserveReferencesHandling.None
                };
            }

            /// <summary>
            /// Json默认设置
            /// </summary>
            public static JsonSerializerSettings JSONNormalSettings { get; }

            #region Serialize

            /// <summary>
            /// 序列化
            /// </summary>
            public static string Serialize(in object data)
            {
                return JsonConvert.SerializeObject(data, JSONNormalSettings);
            }

            /// <summary>
            /// 序列化
            /// </summary>
            public static string Serialize(in object data, in JsonSerializerSettings settings)
            {
                return JsonConvert.SerializeObject(data, settings);
            }

            /// <summary>
            /// 序列化
            /// </summary>
            public static string Serialize<T>(in T data, in JsonSerializerSettings settings)
            {
                return JsonConvert.SerializeObject(data, typeof(T), settings);
            }

            /// <summary>
            /// 序列化
            /// </summary>
            public static string Serialize<T>(in T data)
            {
                return JsonConvert.SerializeObject(data, typeof(T), JSONNormalSettings);
            }

            #endregion

            #region 反序列化

            /// <summary>
            /// 反序列化 HashTable
            /// </summary>
            public static Hashtable ToHashTable(in string data)
            {
                return JsonConvert.DeserializeObject<Hashtable>(data, JSONNormalSettings);
            }

            /// <summary>
            /// 反序列化
            /// </summary>
            public static JObject Deserialize(in string data, in JsonSerializerSettings settings)
            {
                return JsonConvert.DeserializeObject<JObject>(data, settings);
            }

            /// <summary>
            /// 反序列化
            /// </summary>
            public static JObject Deserialize(in string data)
            {
                return JsonConvert.DeserializeObject<JObject>(data, JSONNormalSettings);
            }

            /// <summary>
            /// 反序列化
            /// </summary>
            public static T Deserialize<T>(in string data, in JsonSerializerSettings settings)
            {
                return JsonConvert.DeserializeObject<T>(data, settings);
            }

            /// <summary>
            /// 反序列化
            /// </summary>
            public static T Deserialize<T>(in string data)
            {
                return JsonConvert.DeserializeObject<T>(data, JSONNormalSettings);
            }

            /// <summary>
            /// 反序列化
            /// </summary>
            public static object Deserialize(in string data, in Type type, in JsonSerializerSettings settings)
            {
                return JsonConvert.DeserializeObject(data, type, settings);
            }

            /// <summary>
            /// 反序列化
            /// </summary>
            public static object Deserialize(in string data, in Type type)
            {
                return JsonConvert.DeserializeObject(data, type, JSONNormalSettings);
            }

            #endregion

            #region Populate

            /// <summary>
            /// 反序列化 填充对象
            /// </summary>
            public static void PopulateObject<T>(string data, T TargetObj, in JsonSerializerSettings settings)
            {
                JsonConvert.PopulateObject(data, TargetObj, settings);
            }

            /// <summary>
            /// 反序列化 填充对象
            /// </summary>
            public static void PopulateObject<T>(string data, T TargetObj)
            {
                JsonConvert.PopulateObject(data, TargetObj, JSONNormalSettings);
            }

            /// <summary>
            /// 反序列化 填充对象
            /// </summary>
            public static void PopulateObject(string data, object TargetObj, in JsonSerializerSettings settings)
            {
                JsonConvert.PopulateObject(data, TargetObj, settings);
            }

            /// <summary>
            /// 反序列化 填充对象
            /// </summary>
            public static void PopulateObject(string data, object TargetObj)
            {
                JsonConvert.PopulateObject(data, TargetObj, JSONNormalSettings);
            }

            #endregion
        }

        #endregion
    }


    // public class LimitPropsContractResolver : DefaultContractResolver
    // {
    //     private ICollection<string> props { get; }
    //
    //     private bool retain { get; }
    //
    //     /// <summary>
    //     /// 构造函数
    //     /// </summary>
    //     /// <param name="props">传入的属性数组</param>
    //     /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
    //     public LimitPropsContractResolver(ICollection<string> props, bool retain = true)
    //     {
    //         //指定要序列化属性的清单
    //         this.props = props;
    //         this.retain = retain;
    //     }
    //
    //     protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    //     {
    //         IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
    //         //只保留清单有列出的属性
    //         return list.Where(p =>
    //         {
    //             if (retain)
    //             {
    //                 return props.Contains(p.PropertyName);
    //             }
    //             else
    //             {
    //                 return !props.Contains(p.PropertyName);
    //             }
    //         }).ToList();
    //     }
    // }
}