/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using AIO.PList;

    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IO 写入
    /// </summary>
    public static partial class IOUtils
    {
        #region Unicode

        /// <summary>
        /// 按照UTF-8读取文本文件
        /// </summary>
        public static string ReadUnicode(string Path)
        {
            return ReadText(Path, "unicode");
        }

        /// <summary>
        /// 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Text">内容</param>
        /// <param name="Concat">Ture:追加 False:覆盖</param>
        public static bool WriteUnicode(string Path, string Text, bool Concat = false)
        {
            return WriteText(Path, Text, "unicode", Concat);
        }

        /// <summary>
        /// 按照指定编码读取文本文件
        /// </summary>
        public static string ReadText(string Path, string Charset)
        {
            return Encoding.GetEncoding(Charset).GetString(ReadFile(Path));
        }

        /// <summary>
        /// 异步 按照UTF-8读取文本文件
        /// </summary>
        public static async Task<string> ReadUnicodeAsync(string Path)
        {
            return await ReadTextAsync(Path, "unicode");
        }

        /// <summary>
        /// 异步 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Text">内容</param>
        /// <param name="Concat">Ture:追加 False:覆盖</param>
        public static async Task<bool> WriteUnicodeAsync(string Path, string Text, bool Concat = false)
        {
            return await WriteTextAsync(Path, Text, "unicode", Concat);
        }

        /// <summary>
        /// 将字符串按照指定编码写入文件,是否追加到文件尾
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Text">内容</param>
        /// <param name="Charset">保存文本格式</param>
        /// <param name="Concat">true:拼接 | false:覆盖</param>
        public static bool WriteText(string Path, string Text, string Charset, bool Concat = false)
        {
            var b = Encoding.GetEncoding(Charset).GetBytes(Text);
            return Write(Path, b, 0, b.Length, Concat);
        }

        #endregion

        #region Text

        /// <summary>
        /// 异步 将字符串按照指定编码写入文件,是否追加到文件尾
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Text">内容</param>
        /// <param name="Charset">保存文本格式</param>
        /// <param name="Concat">true:拼接 | false:覆盖</param>
        public static async Task<bool> WriteTextAsync(string Path, string Text, string Charset, bool Concat = false)
        {
            var b = Encoding.GetEncoding(Charset).GetBytes(Text);
            return await WriteAsync(Path, b, 0, b.Length, Concat);
        }

        /// <summary>
        /// 异步 按照指定编码读取文本文件
        /// </summary>
        public static async Task<string> ReadTextAsync(string Path, string Charset)
        {
            return Encoding.GetEncoding(Charset).GetString(await ReadFileAsync(Path));
        }

        #endregion

        #region UTF-8


        /// <summary>
        /// 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Text">内容</param>
        /// <param name="Concat">Ture:追加 False:覆盖</param>
        public static bool WriteUTF8(string Path, string Text, bool Concat = false)
        {
            return WriteText(Path, Text, "utf-8", Concat);
        }

        /// <summary>
        /// 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Text">内容</param>
        /// <param name="Concat">Ture:追加 False:覆盖</param>
        public static bool WriteUTF8(string Path, StringBuilder Text, bool Concat = false)
        {
            return WriteText(Path, Text.ToString(), "utf-8", Concat);
        }

        /// <summary>
        /// 按照UTF-8读取文本文件
        /// </summary>
        public static string ReadUTF8(string Path)
        {
            return ReadText(Path, "utf-8");
        }

        /// <summary>
        /// 异步 按照UTF-8读取文本文件
        /// </summary>
        public static async Task<string> ReadUTF8Async(string Path)
        {
            return await ReadTextAsync(Path, "utf-8");
        }

        /// <summary>
        /// 异步 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Text">内容</param>
        /// <param name="Concat">Ture:追加 False:覆盖</param>
        public static async Task<bool> WriteUTF8Async(string Path, string Text, bool Concat = false)
        {
            return await WriteTextAsync(Path, Text, "utf-8", Concat);
        }

        /// <summary>
        /// 异步 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="Text">内容</param>
        /// <param name="Concat">Ture:追加 False:覆盖</param>
        public static async Task<bool> WriteUTF8Async(string Path, StringBuilder Text, bool Concat = false)
        {
            return await WriteTextAsync(Path, Text.ToString(), "utf-8", Concat);
        }

        #endregion

        #region Json

        /// <summary>
        /// 读取Json文件 根据编码
        /// </summary>
        public static T ReadJson<T>(string Path, string Charset = "utf-8")
        {
            return ParseUtils.JsonDeserialize<T>(ReadText(Path, Charset));
        }

        /// <summary>
        /// 读取Json文件 编码utf-8
        /// </summary>
        public static async Task<T> ReadJsonAsync<T>(string Path, string Charset = "utf-8")
        {
            var Content = await ReadTextAsync(Path, Charset);
            return ParseUtils.JsonDeserialize<T>(Content);
        }

        /// <summary>
        /// 读取Json文件 编码utf-8
        /// </summary>
        public static T ReadJsonUTF8<T>(string Path)
        {
            return ParseUtils.JsonDeserialize<T>(ReadUTF8(Path));
        }

        /// <summary>
        /// 读取Json文件 编码utf-8
        /// </summary>
        public static async Task<T> ReadJsonUTF8Async<T>(string Path)
        {
            var Content = await ReadUTF8Async(Path);
            return ParseUtils.JsonDeserialize<T>(Content);
        }

        /// <summary>
        /// 写入Json文件 根据编码
        /// </summary>
        public static void WriteJson<T>(string Path, T Value, bool concat = false, string Charset = "utf-8")
        {
            if (Value == null) return;
            WriteText(Path, ParseUtils.JsonSerialize(Value), Charset, concat);
        }

        /// <summary>
        /// 写入Json文件 编码utf-8
        /// </summary>
        public static async Task<bool> WriteJsonAsync<T>(string Path, T Value, bool concat = false, string Charset = "utf-8")
        {
            if (Value == null) return false;
            return await WriteTextAsync(Path, ParseUtils.JsonSerialize(Value), Charset, concat);
        }

        /// <summary>
        /// 写入Json文件 编码utf-8
        /// </summary>
        public static void WriteJsonUTF8<T>(string Path, T Text, bool concat = false)
        {
            if (Text == null) return;
            WriteUTF8(Path, ParseUtils.JsonSerialize(Text), concat);
        }

        /// <summary>
        /// 写入Json文件 编码utf-8
        /// </summary>
        public static async Task<bool> WriteJsonUTF8Async<T>(string Path, T Text, bool concat = false)
        {
            if (Text == null) return false;
            return await WriteUTF8Async(Path, ParseUtils.JsonSerialize(Text), concat);
        }

        #endregion

        #region XML

        /// <summary>
        /// 读取Json文件 根据编码
        /// </summary>
        public static T ReadXml<T>(string Path, string Charset = "utf-8")
        {
            return ParseUtils.XmlDeserialize<T>(ReadText(Path, Charset));
        }

        /// <summary>
        /// 读取Json文件 编码utf-8
        /// </summary>
        public static async Task<T> ReadXmlAsync<T>(string Path, string Charset = "utf-8")
        {
            var Content = await ReadTextAsync(Path, Charset);
            return ParseUtils.XmlDeserialize<T>(Content);
        }

        /// <summary>
        /// 读取Json文件 编码utf-8
        /// </summary>
        public static T ReadXmlUTF8<T>(string Path)
        {
            return ParseUtils.XmlDeserialize<T>(ReadUTF8(Path));
        }

        /// <summary>
        /// 读取Json文件 编码utf-8
        /// </summary>
        public static async Task<T> ReadXmlUTF8Async<T>(string Path)
        {
            var Content = await ReadUTF8Async(Path);
            return ParseUtils.XmlDeserialize<T>(Content);
        }

        /// <summary>
        /// 写入Json文件 根据编码
        /// </summary>
        public static void WriteXml<T>(string Path, T Value, string Charset = "utf-8")
        {
            if (Value == null) return;
            WriteText(Path, ParseUtils.XmlSerialize(Value), Charset, false);
        }

        /// <summary>
        /// 写入Json文件 编码utf-8
        /// </summary>
        public static async Task<bool> WriteXmlAsync<T>(string Path, T Value, string Charset = "utf-8")
        {
            if (Value == null) return false;
            return await WriteTextAsync(Path, ParseUtils.XmlSerialize(Value), Charset, false);
        }

        /// <summary>
        /// 写入Json文件 编码utf-8
        /// </summary>
        public static void WriteXmlUTF8<T>(string Path, T Text)
        {
            if (Text == null) return;
            WriteUTF8(Path, ParseUtils.XmlSerialize(Text), false);
        }

        /// <summary>
        /// 写入Json文件 编码utf-8
        /// </summary>
        public static async Task<bool> WriteXmlUTF8Async<T>(string Path, T Text)
        {
            if (Text == null) return false;
            return await WriteUTF8Async(Path, ParseUtils.XmlSerialize(Text), false);
        }

        #endregion

        #region PList

        /// <summary>
        /// 读取 Plist
        /// </summary>
        public static PListRoot ReadPList(string Path)
        {
            return PListRoot.Load(Path);
        }

        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        public static void WritePList(string Path, PListRoot value)
        {
            value.Save(Path, PListFormat.Xml);
        }

        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        public static void WritePList(string Path, PListDict value)
        {
            var root = new PListRoot();
            root.Root = value;
            root.Save(Path, PListFormat.Xml);
        }

        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        public static async Task WritePListAsync(string Path, PListRoot value)
        {
            await Task.Factory.StartNew(() =>
            {
                value.Save(Path, PListFormat.Xml);
            });
        }

        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        public static async Task WritePListAsync(string Path, PListDict value)
        {
            await Task.Factory.StartNew(() =>
            {
                var root = new PListRoot();
                root.Root = value;
                root.Save(Path, PListFormat.Xml);
            });
        }

        /// <summary>
        /// 写入 Plist 二进制
        /// </summary>
        public static void WritePListBinary(string Path, PListRoot value)
        {
            value.Save(Path, PListFormat.Binary);
        }

        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        public static void WritePListBinary(string Path, PListDict value)
        {
            var root = new PListRoot();
            root.Root = value;
            root.Save(Path, PListFormat.Binary);
        }

        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        public static async Task WritePListBinaryAsync(string Path, PListRoot value)
        {
            await Task.Factory.StartNew(() =>
            {
                value.Save(Path, PListFormat.Binary);
            });
        }

        /// <summary>
        /// 写入 Plist XML
        /// </summary>
        public static async Task WritePListBinaryAsync(string Path, PListDict value)
        {
            await Task.Factory.StartNew(() =>
            {
                var root = new PListRoot();
                root.Root = value;
                root.Save(Path, PListFormat.Binary);
            });
        }

        #endregion
    }
}
