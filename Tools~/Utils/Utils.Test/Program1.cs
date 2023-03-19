namespace Utils
{
    using AIO;

    using HtmlAgilityPack;

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using static System.Net.Mime.MediaTypeNames;

    class Program1
    {
        static void Main(string[] args)
        {
            Test();
            Console.Read();
        }

        public const string FileName = "ver.json";

        public async static void Test()
        {
            //MoveFiles(@"G:\VIDEO\TEMP", @"G:\VIDEO\R18", "(V)");
            MoveFiles(@"G:\IMAGE\TEMP", @"G:\IMAGE\R18", "(I)");

            //DownLoad();

            //DownLoadCaribbeancomHead();
        }

        /// <summary>
        /// 请求头
        /// </summary>
        public async static void DownLoadCaribbeancomHead()
        {
            await Console.Out.WriteLineAsync(" ----------------- - 开启下载 - ----------------- ");
            var htmlstr = GetHTML("https://www.caribbeancom.com/category.htm#cat-series");
            var list = HTML_Caribbeancom_GetSeries(htmlstr);
            await Console.Out.WriteLineAsync(" ----------------- - 下载完成 - ----------------- ");
        }

        //public class CaribbeanInfo { 
        //}

        public static List<string> HTML_Caribbeancom_GetSeries(string content)
        {
            var list = new List<string>();
            if (content == null) return list;
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var xpath = new StringBuilder();
                xpath.Append("//body");
                xpath.Append("//div[@id='container']");
                xpath.Append("//div[@id='main']");
                xpath.Append("//div");
                xpath.Append("//div[@class='container']");

                var nodeList = doc.DocumentNode.SelectNodes(xpath.ToString());
                if (nodeList == null) { return null; }
          
                xpath.Clear();
                xpath.Append("child::");
                xpath.Append("div[@id='cat-series']");
                xpath.Append("/ul[@class='grid is-button-list']");
                xpath.Append("/li[@class='grid-item']");
                xpath.Append("/li[@class='grid-item']");
                xpath.Append("/a[@class='list-button']");
                var seriesList = nodeList[0].SelectNodes(xpath.ToString());
                if (nodeList == null) { return null; }
              
                xpath.Clear();
                xpath.Append("./child::");
                xpath.Append("a[@class='list-button']");

                foreach (var item in seriesList.Elements())
                {
                    //var seriesValue = item.SelectSingleNode(xpath.ToString());
                    Console.WriteLine(item.InnerText);
                    if (item == null) continue;
                    var href = item.GetAttributeValue("href","null");
                    Console.WriteLine(href);
                    list.Add(href);
                    break;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return list;
        }

        public async static void DownLoadCaribbeancom()
        {
            //    const int MAX = 10;
            //    const string PATH = @"G:\IMAGE\R18\(I)";
            //    var Dic = new Dictionary<string, PBInfo>();
            //    var JsonPath = Path.Combine(PATH, URL.Replace(':', '_').Replace('/', '_').Replace('?', '_').Replace('=', '_') + Number + ".json");
            //Agin:
            //    if (IOUtils.FileExists(JsonPath))
            //    {
            //        try
            //        {
            //            Dic = IOUtils.ReadJsonUTF8<Dictionary<string, PBInfo>>(JsonPath);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex);
            //            File.Delete(JsonPath);
            //            Console.WriteLine("自动删除文件 重新拉取新数据");
            //            goto Agin;
            //        }
            //        foreach (var item in Dic)
            //        {
            //            Queue.Enqueue(item.Key);
            //        }
            //    }
            //    else
            //    {
            //        if (Number <= 0) NQueue.Enqueue(URL);
            //        else for (int i = 1; i <= Number; i++) NQueue.Enqueue(string.Concat(URL, i));

            //        var NT = NQueue.Count <= MAX ? NQueue.Count : MAX;
            //        var NList = new List<Task>(NT);
            //        for (int i = 0; i < NT; i++)
            //        {
            //            NList.Add(Task.Run(() =>
            //            {
            //                while (NQueue.Count > 0)
            //                {
            //                    if (!NQueue.TryDequeue(out var url))
            //                    {
            //                        if (NQueue.Count == 0) return;
            //                        continue;
            //                    }
            //                    if (string.IsNullOrEmpty(url)) return;
            //                    var list = HTML_PB_GetSeries(GetHTML(url));
            //                    if (list == null)
            //                    {
            //                        Console.WriteLine("获取列表失败 {0}", url);
            //                        continue;
            //                    }
            //                    foreach (var item in list)
            //                    {
            //                        if (!string.IsNullOrEmpty(item))
            //                        {
            //                            Queue.Enqueue(item);
            //                            Console.WriteLine(item);
            //                        }
            //                    }
            //                }
            //            }));
            //        }
            //        Console.WriteLine(" ----------------- -开始获取列表- ----------------- ");
            //        try { await Task.WhenAll(NList); }
            //        catch (Exception ex) { Console.WriteLine(ex); }

            //        var MT = Queue.Count <= MAX ? Queue.Count : MAX;
            //        var MList = new List<Task>(MT);
            //        for (int i = 0; i < MT; i++)
            //        {
            //            MList.Add(Task.Run(() =>
            //            {
            //                while (Queue.Count > 0)
            //                {
            //                    if (!Queue.TryDequeue(out var item))
            //                    {
            //                        if (Queue.Count == 0) return;
            //                        continue;
            //                    }
            //                    var info = HTML_PB_GetProduct(GetHTML(item));
            //                    if (info == null)
            //                    {
            //                        Console.WriteLine("获取信息失败 => {0}", item);
            //                        continue;
            //                    }
            //                    if (string.IsNullOrEmpty(info.Name) || string.IsNullOrEmpty(info.URL) || string.IsNullOrEmpty(info.ID))
            //                    {
            //                        Console.WriteLine("获取信息失败 {1} |=>> {0}", item, info.ToString());
            //                        continue;
            //                    }
            //                    Dic.Add(item, info);
            //                    Console.WriteLine(info.ToString());

            //                }
            //            }));
            //        }
            //        Console.WriteLine(" ----------------- -开始获取信息- -----------------");
            //        try { await Task.WhenAll(MList); }
            //        catch (Exception ex) { Console.WriteLine(ex); }
            //        IOUtils.WriteJson(JsonPath, Dic);
            //    }

            //    foreach (var item in Dic) Queue.Enqueue(item.Key);
            //    var max = Queue.Count <= MAX ? Queue.Count : MAX;

            //    var downList = new List<Task>(max);
            //    for (int i = 0; i < max; i++)
            //    {
            //        downList.Add(Task.Run(() =>
            //        {
            //            while (Queue.Count > 0)
            //            {
            //                if (!Queue.TryDequeue(out var itme))
            //                {
            //                    if (Queue.Count == 0) return;
            //                    continue;
            //                }
            //                if (!Dic.TryGetValue(itme, out var info)) continue;

            //                if (info.Name.Contains('|')) info.Name = info.Name.Replace("|", "】【");

            //                var ONE = string.Concat(PATH + info.ID[0]);
            //                string TWO;
            //                if (info.ID.Contains('-')) TWO = info.ID.Split('-')[0];
            //                else if (info.ID.Contains('_')) TWO = info.ID.Split('_')[0];
            //                else if (info.ID.Contains(' ')) TWO = info.ID.Split(' ')[0];
            //                else TWO = "";

            //                var ext = Path.GetExtension(info.URL);
            //                if (ext.Contains("webp")) ext = ext.Replace("webp", "jpg");

            //                var index = 0;
            //                foreach (var item in info.Name) if (item == '【') index++;
            //                if (index >= 15) info.Name = "ALL";

            //                DownImage(info.URL, Path.Combine(ONE, TWO, string.Format("{0}【{1}】{2}", info.ID, info.Name, ext)));
            //            }
            //        }));
            //    }

            //    Console.WriteLine("Count -> {0}", Queue.Count);
            //    try { await Task.WhenAll(downList); }
            //    catch (Exception ex) { Console.WriteLine(ex); }

            //    Console.WriteLine(" ....... 下载全部完成 ....... ");
        }

        public async static void Starts()
        {
            var dir = new DirectoryInfo(@"G:\IMAGE\R18\(I)S\STAR");
            const string TITLE = "STAR-";
            var filePath = Path.Combine(dir.FullName, TITLE);
            var dic = new Dictionary<string, string>();
            foreach (var item in dir.GetFiles("*.jpg", SearchOption.AllDirectories))
            {
                if (item.Name.Contains("【=")) continue;
                if (item.Name.Contains("【"))
                {
                    var key = item.Name.Split('【')[0];
                    if (!dic.ContainsKey(key)) dic.Add(key, item.Name);

                }
                else
                {
                    var key = item.Name.Split('.')[0];
                    if (!dic.ContainsKey(key)) dic.Add(key, item.Name);
                }
            }
            var UrlFormat = "https://pics.dmm.co.jp/mono/movie/adult/1star{0}/1star{0}pl.jpg";
            for (int i = 1; i < 999; i++)
            {
                var id = string.Concat(TITLE, i.ToString("000"));
                if (!dic.ContainsKey(id))
                {
                    var URL = string.Format(UrlFormat, i.ToString("000")) + "|" + Path.Combine(dir.FullName, string.Concat(TITLE, i.ToString("000"), ".jpg"));
                    Queue.Enqueue(URL);
                }
            }
            const int MAX = 10;
            var max = Queue.Count <= MAX ? Queue.Count : MAX;

            var List = new List<Task>(max);
            for (int i = 0; i < max; i++)
            {
                List.Add(Task.Run(() =>
                {
                    while (Queue.Count > 0)
                    {
                        if (!Queue.TryDequeue(out var item))
                        {
                            if (Queue.Count == 0) return;
                            continue;
                        }
                        var v = item.Split('|');
                        DownImage(v[0], v[1]);
                    }
                }));
            }

            Console.WriteLine("Count -> {0}", Queue.Count);
            try
            {
                await Task.WhenAll(List);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine(" ....... 下载全部完成 ....... ");
        }

        private static async void DownLoad()
        {
            var list = new List<(string, int)>
            {
("https://www.aventertainments.com/29/239/2/subdept_products", 13),
("https://www.aventertainments.com/29/525/2/subdept_products", 25),
("https://www.aventertainments.com/29/27/2/subdept_products", 178),
("https://www.aventertainments.com/29/27/2/subdept_products", 67),
("https://www.aventertainments.com/29/375/2/subdept_products", 22),
("https://www.aventertainments.com/29/369/2/subdept_products", 61),
("https://www.aventertainments.com/29/358/2/subdept_products", 85),
("https://www.aventertainments.com/29/121/2/subdept_products", 29),
            };
            foreach (var item in list) await PBDownLoad(item.Item1 + "?countpage=", item.Item2);
            Console.WriteLine("---------------------------------------------------------");
            list = new List<(string, int)>
            {
("https://www.aventertainments.com/search_products.aspx?Dept_ID=29&keyword=LAF&whichOne=all&languageID=2&Rows=1", 8),
("https://www.aventertainments.com/search_products.aspx?Dept_ID=29&keyword=CWP&whichOne=all&languageID=2&Rows=1", 9),
("https://www.aventertainments.com/search_products.aspx?Dept_ID=29&keyword=HEY&whichOne=all&languageID=2&Rows=1", 13),
            };
            foreach (var item in list) await PBDownLoad(item.Item1 + "&CountPage=", item.Item2);

            await PBDownLoad("https://www.aventertainments.com/search_Products.aspx?languageID=2&dept_id=29&keyword=SSKP&searchby=keyword#");
        }

        private static readonly ConcurrentQueue<string> NQueue = new ConcurrentQueue<string>();
        private static readonly ConcurrentQueue<string> Queue = new ConcurrentQueue<string>();

        public async static Task PBDownLoad(string URL, int Number = 0)
        {
            const int MAX = 10;
            const string PATH = @"G:\IMAGE\R18\(I)";
            var Dic = new Dictionary<string, PBInfo>();
            var JsonPath = Path.Combine(PATH, URL.Replace(':', '_').Replace('/', '_').Replace('?', '_').Replace('=', '_') + Number + ".json");
        Agin:
            if (IOUtils.FileExists(JsonPath))
            {
                try
                {
                    Dic = IOUtils.ReadJsonUTF8<Dictionary<string, PBInfo>>(JsonPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    File.Delete(JsonPath);
                    Console.WriteLine("自动删除文件 重新拉取新数据");
                    goto Agin;
                }
                foreach (var item in Dic)
                {
                    Queue.Enqueue(item.Key);
                }
            }
            else
            {
                if (Number <= 0) NQueue.Enqueue(URL);
                else for (int i = 1; i <= Number; i++) NQueue.Enqueue(string.Concat(URL, i));

                var NT = NQueue.Count <= MAX ? NQueue.Count : MAX;
                var NList = new List<Task>(NT);
                for (int i = 0; i < NT; i++)
                {
                    NList.Add(Task.Run(() =>
                    {
                        while (NQueue.Count > 0)
                        {
                            if (!NQueue.TryDequeue(out var url))
                            {
                                if (NQueue.Count == 0) return;
                                continue;
                            }
                            if (string.IsNullOrEmpty(url)) return;
                            var list = HTML_PB_GetSeries(GetHTML(url));
                            if (list == null)
                            {
                                Console.WriteLine("获取列表失败 {0}", url);
                                continue;
                            }
                            foreach (var item in list)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    Queue.Enqueue(item);
                                    Console.WriteLine(item);
                                }
                            }
                        }
                    }));
                }
                Console.WriteLine(" ----------------- -开始获取列表- ----------------- ");
                try { await Task.WhenAll(NList); }
                catch (Exception ex) { Console.WriteLine(ex); }

                var MT = Queue.Count <= MAX ? Queue.Count : MAX;
                var MList = new List<Task>(MT);
                for (int i = 0; i < MT; i++)
                {
                    MList.Add(Task.Run(() =>
                    {
                        while (Queue.Count > 0)
                        {
                            if (!Queue.TryDequeue(out var item))
                            {
                                if (Queue.Count == 0) return;
                                continue;
                            }
                            var info = HTML_PB_GetProduct(GetHTML(item));
                            if (info == null)
                            {
                                Console.WriteLine("获取信息失败 => {0}", item);
                                continue;
                            }
                            if (string.IsNullOrEmpty(info.Name) || string.IsNullOrEmpty(info.URL) || string.IsNullOrEmpty(info.ID))
                            {
                                Console.WriteLine("获取信息失败 {1} |=>> {0}", item, info.ToString());
                                continue;
                            }
                            Dic.Add(item, info);
                            Console.WriteLine(info.ToString());

                        }
                    }));
                }
                Console.WriteLine(" ----------------- -开始获取信息- -----------------");
                try { await Task.WhenAll(MList); }
                catch (Exception ex) { Console.WriteLine(ex); }
                IOUtils.WriteJson(JsonPath, Dic);
            }

            foreach (var item in Dic) Queue.Enqueue(item.Key);
            var max = Queue.Count <= MAX ? Queue.Count : MAX;

            var downList = new List<Task>(max);
            for (int i = 0; i < max; i++)
            {
                downList.Add(Task.Run(() =>
                {
                    while (Queue.Count > 0)
                    {
                        if (!Queue.TryDequeue(out var itme))
                        {
                            if (Queue.Count == 0) return;
                            continue;
                        }
                        if (!Dic.TryGetValue(itme, out var info)) continue;

                        if (info.Name.Contains('|')) info.Name = info.Name.Replace("|", "】【");

                        var ONE = string.Concat(PATH + info.ID[0]);
                        string TWO;
                        if (info.ID.Contains('-')) TWO = info.ID.Split('-')[0];
                        else if (info.ID.Contains('_')) TWO = info.ID.Split('_')[0];
                        else if (info.ID.Contains(' ')) TWO = info.ID.Split(' ')[0];
                        else TWO = "";

                        var ext = Path.GetExtension(info.URL);
                        if (ext.Contains("webp")) ext = ext.Replace("webp", "jpg");

                        var index = 0;
                        foreach (var item in info.Name) if (item == '【') index++;
                        if (index >= 15) info.Name = "ALL";

                        DownImage(info.URL, Path.Combine(ONE, TWO, string.Format("{0}【{1}】{2}", info.ID, info.Name, ext)));
                    }
                }));
            }

            Console.WriteLine("Count -> {0}", Queue.Count);
            try { await Task.WhenAll(downList); }
            catch (Exception ex) { Console.WriteLine(ex); }

            Console.WriteLine(" ....... 下载全部完成 ....... ");
        }

        public static string GetHTML(string URL)
        {
            var stream = "";
            try
            {
                //创建HttpWebRequest 
                var myrq = WebRequest.CreateHttp(URL);
                myrq.KeepAlive = false;
                //超时时间
                myrq.Timeout = 10 * 1000;
                //请求方式 
                myrq.Method = "Get";

                myrq.Proxy = new WebProxy("127.0.0.1:1080");

                myrq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                //来源
                //myrq.Host = "baike.baidu.com";

                //定义浏览器代理
                myrq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.88 Safari/537.36";

                //请求网页
                using var myrp = (HttpWebResponse)myrq.GetResponse();

                //判断请求状态
                if (myrp.StatusCode != HttpStatusCode.OK) return null;
                using var sr = new StreamReader(myrp.GetResponseStream());
                stream = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return stream;
        }

        public class PBInfo
        {
            /// <summary>
            /// 地址
            /// </summary>
            public string URL;

            /// <summary>
            /// 人物名
            /// </summary>
            public string Name;

            /// <summary>
            /// 番号
            /// </summary>
            public string ID;

            /// <summary>
            /// 更多信息
            /// </summary>
            public string More;

            public override string ToString()
            {
                return string.Format("ID : {0} Name : {1} URL : {2}", ID ?? "NULL", Name ?? "NULL", URL ?? "NULL");
            }
        }

        public static PBInfo HTML_PB_GetProduct(string content)
        {
            var info = new PBInfo();
            if (content == null) return info;
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var xpath = new StringBuilder();
                xpath.Append("//body[@id='MyBody']");
                xpath.Append("//div[@class='page-wrapper-reds']");
                xpath.Append("//div");
                xpath.Append("//div[@class='product-details-area mb-50']");
                xpath.Append("//div[@class='container mt-30']");
                xpath.Append("//div[@class='row']");
                xpath.Append("//div[@class='col-lg-10']");
                xpath.Append("//div[@id='PlayerCover']");
                xpath.Append("//img");

                var IMGNode = doc.DocumentNode.SelectSingleNode(xpath.ToString());
                if (IMGNode != null) info.URL = IMGNode.GetAttributeValue("src", null);
                else
                {
                    xpath.Clear();
                    xpath.Append("//body[@id='MyBody']");
                    xpath.Append("//div[@class='page-wrapper-reds']");
                    xpath.Append("//div");
                    xpath.Append("//div[@class='product-details-area mb-50']");
                    xpath.Append("//div[@class='container mt-30']");
                    xpath.Append("//div[@class='row']");
                    xpath.Append("//div[@class='col-lg-10']");
                    xpath.Append("//div[@class='col-12']");
                    xpath.Append("//img");
                    IMGNode = doc.DocumentNode.SelectSingleNode(xpath.ToString());
                    if (IMGNode != null) info.URL = IMGNode.GetAttributeValue("src", null);
                }


                xpath.Clear();
                xpath.Append("//body[@id='MyBody']");
                xpath.Append("//div[@class='page-wrapper-reds']");
                xpath.Append("//div");
                xpath.Append("//div[@class='product-details-area mb-50']");
                xpath.Append("//div[@class='container mt-30']");
                xpath.Append("//div[@class='row']");
                xpath.Append("//div[@class='col-sm-12 col-md-12 col-lg-9 mt-30']");
                xpath.Append("//div[@class='product-info-block-rev mt-20']");
                xpath.Append("//div[@class='single-info']");

                var INFONode = doc.DocumentNode.SelectNodes(xpath.ToString());
                if (INFONode != null)
                {
                    var IDNode = INFONode[0].SelectSingleNode("./child::span[@class='tag-title']");
                    if (IDNode != null) info.ID = IDNode.InnerText;

                    var NameNode = INFONode[1].SelectNodes("./child::span[@class='value']/a");
                    if (NameNode != null)
                    {
                        xpath.Clear();
                        foreach (var item in NameNode)
                        {
                            if (item == null) continue;
                            xpath.Append(item.InnerText).Append('|');
                        }
                        info.Name = xpath.ToString().Substring(0, xpath.Length - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return info;
        }

        public static List<string> HTML_PB_GetSeries(string content)
        {
            var list = new List<string>();
            if (content == null) return list;
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var xpath = new StringBuilder();
                xpath.Append("//body[@id='MyBody']");
                xpath.Append("//div[@class='page-wrapper-reds']");
                xpath.Append("//div");
                xpath.Append("//div");
                xpath.Append("//div[@class='container']");
                xpath.Append("//div[@class='row']");
                xpath.Append("//div");
                xpath.Append("//div[@class='row shop-product-wrap grid mb-10']");

                var nodeList = doc.DocumentNode.SelectNodes(xpath.ToString());
                if (nodeList == null) { return null; }

                xpath.Clear();
                xpath.Append("child::");
                xpath.Append("div[@class='single-slider-product grid-view-product']");
                xpath.Append("/div[@class='single-slider-product__content']");
                xpath.Append("/p[@class='product-title']");
                xpath.Append("/a");
                var XPathHref = xpath.ToString();
                foreach (var item in nodeList.Elements())
                {
                    var node = item.SelectSingleNode(XPathHref);
                    if (node != null) list.Add(node.GetAttributeValue("href", null));
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return list;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public static void DownImage(string URL, string SavePath)
        {
            SavePath = SavePath.Replace(" 】", "】").Replace(" 【", "【").Replace("】 ", "】").Replace("【 ", "【");
            if (File.Exists(SavePath)) return;
            SavePath = SavePath.Replace(".webp ", ".jpg");
            if (File.Exists(SavePath)) return;

            var myrq = (HttpWebRequest)WebRequest.Create(URL);
            myrq.KeepAlive = false;
            myrq.Timeout = 30 * 1000; //超时时间
            myrq.Method = "Get";  //请求方式 
            myrq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 UBrowser/6.2.4098.3 Safari/537.36";

            try
            {
                using var myrp = (HttpWebResponse)myrq.GetResponse();
                Console.WriteLine("{0} - {1} -> {2}", myrp.StatusCode, URL, SavePath);
                if (myrp.StatusCode != HttpStatusCode.OK) return;

                var dir = new DirectoryInfo(Path.GetDirectoryName(SavePath));
                if (!dir.Exists) dir.Create();

                //保存图片
                using var fs = new FileStream(SavePath, File.Exists(SavePath) ? FileMode.Open : FileMode.CreateNew);
                myrp.GetResponseStream().CopyTo(fs);
                myrp.Close();
                myrp.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("{0} - {1} -> {2} \n {3}", "ERROR", URL, SavePath, ex);
            }
        }


        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="source">源路径</param>
        /// <param name="target">目标路径</param>
        public static void MoveFiles(in string source, in string target, in string UNIT = "(I)")
        {
            var IMG = new DirectoryInfo(source);
            var TARGET = new DirectoryInfo(target);
            var dir = new Dictionary<char, DirectoryInfo>();
            for (int i = CharUnit.EA; i <= CharUnit.EZ; i++)
            {
                char item = (char)i;
                dir.Add(item, new DirectoryInfo(Path.Combine(TARGET.FullName, UNIT + item)));
                if (!dir[item].Exists) dir[item].Create();
            }

            foreach (var file in IMG.GetFiles("*", SearchOption.AllDirectories))
            {
                if (!file.Name.Contains("-")) continue;
                if (!dir.TryGetValue(char.ToUpper(file.Name[0]), out var directory)) continue;
                var folder = file.Name.Split("-")[0];
                var subdir = new DirectoryInfo(Path.Combine(directory.FullName, folder.ToUpper()));
                if (!subdir.Exists) subdir.Create();
                var des = Path.Combine(subdir.FullName, file.Name);
                if (File.Exists(des)) continue;
                file.MoveTo(des);
                Console.WriteLine(des);
            }
        }
    }
}
