#region

using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

#endregion

namespace AIO
{
    /// <summary>
    /// Json 缓冲区
    /// </summary>
    public class BufferJsonDataData : BufferByte, IReadJsonData, IWriteJsonData
    {
        /// <summary>
        /// Json 缓冲区
        /// </summary>
        public BufferJsonDataData() : base() { }

        /// <summary>
        /// Json 缓冲区
        /// </summary>
        public BufferJsonDataData(int capacity) : base(capacity) { }

        /// <summary>
        /// Json 缓冲区
        /// </summary>
        public BufferJsonDataData(byte[] buffer) : base(buffer) { }

        /// <summary>
        /// Json 缓冲区
        /// </summary>
        public BufferJsonDataData(byte[] buffer, int offset, int count) : base(buffer, offset, count) { }

        /// <summary>
        /// Json 缓冲区
        /// </summary>
        public BufferJsonDataData(string str) : base(Encoding.UTF8.GetBytes(str)) { }

        /// <summary>
        /// Json 缓冲区
        /// </summary>
        public BufferJsonDataData(FileSystemInfo info) : base(info) { }

        /// <summary>
        /// Json 默认设置
        /// </summary>
        protected static JsonSerializerSettings JSONNormalSettings { get; } = new JsonSerializerSettings
        {
            //设置区域格式
            Culture = CultureInfo.CurrentCulture,
            //内容处理方式 文件 远端 压缩 多应用程序 复制等
            Context = new StreamingContext(StreamingContextStates.All),
            //[Ignore:默认] [Indented:文本根据 Indentation IndentChar 压缩] 格式化
            Formatting = Formatting.None,
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
            StringEscapeHandling       = StringEscapeHandling.Default,
            PreserveReferencesHandling = PreserveReferencesHandling.None
        };

        /// <inheritdoc/>
        public T ReadData<T>(bool reverse = false) { return AHelper.Json.Deserialize<T>(ReadString(reverse)); }

        /// <inheritdoc />
        public void ReadData<T>(out T value, bool reverse = false) { value = AHelper.Json.Deserialize<T>(ReadString(reverse)); }

        /// <inheritdoc/>
        public void WriteData<T>(T value, bool reverse = false) { WriteString(AHelper.Json.Serialize(value), reverse); }
    }
}