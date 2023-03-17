/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text.RegularExpressions;

    /* 常用元字符
      .                             匹配除换行符以外的任意字符。
      \w                            匹配字母或数字或下划线或汉字。
      \s                            匹配任意的空白符。
      \d                            匹配数字。
      \b                            匹配单词的开始或结束。
      [ck]                          匹配包含括号内元素的字符
      ^                             匹配行的开始。
      $                             匹配行的结束。
      \                             对下一个字符转义。比如$是个特殊的字符。要匹配$的话就得用\$
    |*|                             分支条件，如：x|y匹配 x 或 y。

      反义元字符
      \W                            匹配任意不是字母，数字，下划线，汉字的字符。
      \S                            匹配任意不是空白符的字符。等价于 [^ \f\n\r\t\v]。
      \D                            匹配任意非数字的字符。等价于 [^0-9]。
      \B                            匹配不是单词开头或结束的位置。
      [^CK]                         匹配除了CK以外的任意字符。

      特殊元字符
      \f                            匹配一个换页符。等价于 \x0c 和 \cL。
      \n                            匹配一个换行符。等价于 \x0a 和 \cJ。
      \r                            匹配一个回车符。等价于 \x0d 和 \cM。
      \t                            匹配一个制表符。等价于 \x09 和 \cI。
      \v                            匹配一个垂直制表符。等价于 \x0b 和 \cK。

      限定符
      *                             匹配前面的子表达式零次或多次。
      +                             匹配前面的子表达式一次或多次。
      ?                             匹配前面的子表达式零次或一次。
      {n}                           n 是一个非负整数。匹配确定的 n 次。
      {n,}                          n 是一个非负整数。至少匹配n 次。
      {n,m}                         m 和 n 均为非负整数，其中n <= m。最少匹配 n 次且最多匹配 m 次。

      懒惰限定符
      *?                            重复任意次，但尽可能少重复。如 "acbacb"  正则  "a.*?b" 只会取到第一个"acb" 原本可以全部取到但加了限定符后，只会匹配尽可能少的字符 ，而"acbacb"最少字符的结果就是"acb" 。
      +?                            重复1次或更多次，但尽可能少重复。与上面一样，只是至少要重复1次。
      ??                            重复0次或1次，但尽可能少重复。如 "aaacb" 正则 "a.??b" 只会取到最后的三个字符"acb"。
      {n,m}?                        重复n到m次，但尽可能少重复。如 "aaaaaaaa"  正则 "a{0,m}" 因为最少是0次所以取到结果为空。
      {n,}?                         重复n次以上，但尽可能少重复。如 "aaaaaaa"  正则 "a{1,}" 最少是1次所以取到结果为 "a"。

      捕获分组
      (exp)                         匹配exp,并捕获文本到自动命名的组里。
      (?<name>exp)                  匹配exp,并捕获文本到名称为name的组里。
      (?:exp)                       匹配exp,不捕获匹配的文本，也不给此分组分配组号以下为零宽断言。
      (?=exp)                       匹配exp前面的位置。如 "How are you doing" 正则"(?<txt>.+(?=ing))" 这里取ing前所有的字符，并定义了一个捕获分组名字为 "txt" 而"txt"这个组里的值为"How are you do";
      (?<=exp)                      匹配exp后面的位置。如 "How are you doing" 正则"(?<txt>(?<=How).+)" 这里取"How"之后所有的字符，并定义了一个捕获分组名字为 "txt" 而"txt"这个组里的值为" are you doing";
      (?!exp)                       匹配后面跟的不是exp的位置。如 "123abc" 正则 "\d{3}(?!\d)"匹配3位数字后非数字的结果
      (?<!exp)                      匹配前面不是exp的位置。如 "abc123 " 正则 "(?<![0-9])123" 匹配"123"前面是非数字的结果也可写成"(?!<\d)123"

      常用方法
      IsMatch(String, String)       指示 Regex 构造函数中指定的正则表达式在指定的输入字符串中是否找到了匹配项。
      Match(String, String)         在指定的输入字符串中搜索 Regex 构造函数中指定的正则表达式的第一个匹配项。
      Matches(String, String)       在指定的输入字符串中搜索正则表达式的所有匹配项。
      Replace(String, String)       在指定的输入字符串内，使用指定的替换字符串替换与某个正则表达式模式匹配的所有字符串。
      Split(String, String)         在由 Regex 构造函数指定的正则表达式模式所定义的位置，拆分指定的输入字符串。
    */

    public static partial class StringExtend
    {
        #region IsValidate

        /// <summary>
        /// 判断路径是是否包含指定字符
        /// </summary>
        /// <returns>Ture:符合 False:不符合</returns>
        public static bool IsValidatePath(this string value, int[] list)
        {
            if (list == null || list.Length == 0) return false;
            for (int i = 0; i < value.Length; i++)
            {
                if (!list.Contain(value[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// 判断路径是是否包含指定字符
        /// </summary>
        /// <returns>Ture:符合 False:不符合</returns>
        public static bool IsValidatePath(this string value, List<int> list)
        {
            if (list == null || list.Count == 0) return false;
            for (int i = 0; i < value.Length; i++)
            {
                if (!list.Contains(value[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// 判断路径是是否包含指定字符
        /// </summary>
        /// <returns>Ture:符合 False:不符合</returns>
        public static bool IsValidatePath(this string value, int start, int end)
        {
            for (int i = 0; i < value.Length; i++)
            {
                var c = value[i];
                if (!(c >= start && c <= end)) return false;
            }

            return true;
        }

        /// <summary>
        /// 验证一年的12个月
        /// </summary>
        public static bool IsValidateMonth(this string value)
        {
            return Regex.IsMatch(value, @"^(0?[[1-9]|1[0-2])$");
        }

        /// <summary>
        /// 验证输入汉字
        /// </summary>
        public static bool IsValidateChinese(this string value)
        {
            return Regex.IsMatch(value, $@"^[/u4e00-/u9fa5],{{0,}}$");
        }

        /// <summary>
        /// 验证输入字符串 是否满足指定个数
        /// </summary>
        public static bool IsValidateLength(this string value, int number)
        {
            return Regex.IsMatch(value, string.Concat("^.{", number, "}$"));
        }

        /// <summary>
        /// 验证当前字符串是否在指定长度范围内
        /// </summary>
        public static bool IsValidateMinMaxLength(this string value, int min, int max)
        {
            return Regex.IsMatch(value, string.Concat("^/d{", min, ',', max, "}$"));
        }

        /// <summary>
        /// 验证一个月的31天
        /// </summary>
        public static bool IsValidateDay(this string value)
        {
            return Regex.IsMatch(value, @"^((0?[1-9])|((1|2)[0-9])|30|31)$");
        }

        /// <summary>
        /// 验证两位小数
        /// </summary>
        public static bool IsValidate2Decimal(this string value)
        {
            return Regex.IsMatch(value, @"^[0-9]*[.]{1}[0-9]{2}$");
        }

        /// <summary> 判断字符是否为空 </summary>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 是否为Numeric
        /// </summary>
        public static bool IsValidateNumeric(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 是否为Int
        /// </summary>
        public static bool IsValidateInt(this string value)
        {
            return Regex.IsMatch(value, "^-?\\d+$");
        }

        /// <summary>
        /// 是否为Bool
        /// </summary>
        public static bool IsValidateBool(this string value)
        {
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// 是否为整数
        /// </summary>
        public static bool IsValidateNum(string strNum)
        {
            return Regex.IsMatch(strNum, "^[0-9]*$");
        }

        /// <summary>
        /// 是否为Unsign
        /// </summary>
        public static bool IsValidateUnsign(string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }

        /// <summary>
        /// 验证是否为 YYYY MM DD 格式 包含闰年 平年
        /// </summary>
        public static bool IsValidateDate(this string value)
        {
            bool bValids = Regex.IsMatch(value,
                @"^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$");
            return (bValids && value.CompareTo("1753-01-01") >= 0);
        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        public static bool IsValidateDateString(this string value)
        {
            return Regex.IsMatch(value, @"（\d{4}）-（\d{1,2}）-（\d{1,2}）");
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        public static bool IsValidatePhone(this string value)
        {
            return Regex.IsMatch(value, @"^1(?:3\d|4[4-9]|5[0-35-9]|6[67]|7[013-8]|8\d|9\d)\d{8}$");
        }

        /// <summary>
        /// 是否为IP分段
        /// </summary>
        public static bool IsValidateIPSect(this string value)
        {
            return Regex.IsMatch(value, @"^（（2[0-4]\d|25[0-5]|[01]?\d\d?）\.）{2}（（2[0-4]\d|25[0-5]|[01]?\d\d?|\*）\.）（2[0-4]\d|25[0-5]|[01]?\d\d?|\*）$");
        }

        /// <summary>
        /// 是否电子邮件
        /// </summary>
        public static bool IsValidateEmail(this string value)
        {
            return Regex.IsMatch(value, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 验证邮编
        /// </summary>
        public static bool IsPostalcode(this string value)
        {
            return Regex.IsMatch(value, @"^\d{6}$");
        }

        /// <summary>
        /// 验证身份证号
        /// </summary>
        public static bool IsValidateIDcard(this string value)
        {
            switch (value.Length)
            {
                case 18: return IsValidateIDcard18(value);
                case 15: return IsValidateIDcard15(value);
                default: return false;
            }
        }

        /// <summary>
        /// 验证身份证号18
        /// </summary>
        public static bool IsValidateIDcard18(this string value)
        {
            long n = 0;
            if (long.TryParse(value.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(value.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false; //数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(value.Remove(2)) == -1)
            {
                return false; //省份验证
            }

            string birth = value.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; //生日验证
            }

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = value.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }

            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != value.Substring(17, 1).ToLower())
            {
                return false; //校验码验证
            }

            return true; //符合GB11643-1999标准
        }

        /// <summary>
        /// 验证身份证号15
        /// </summary>
        public static bool IsValidateIDcard15(this string value)
        {
            long n = 0;
            if (long.TryParse(value, out n) == false || n < Math.Pow(10, 14))
            {
                return false; //数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(value.Remove(2)) == -1)
            {
                return false; //省份验证
            }

            string birth = value.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; //生日验证
            }

            return true; //符合15位身份证标准
        }

        /// <summary>
        /// 15位省份证号码升18位
        /// </summary>
        public static string IDcard15To18(this string value)
        {
            int iS = 0;
            //加权因子常数
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //校验码常数
            string LastCode = "10X98765432";
            //新身份证号
            string perIDNew;
            perIDNew = value.Substring(0, 6);
            //填在第6位及第7位上填上‘1’，‘9’两个数字
            perIDNew += "19";
            perIDNew += value.Substring(6, 9);
            //进行加权求和
            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }

            //取模运算，得到模值
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。
            perIDNew += LastCode.Substring(iY, 1);
            return perIDNew;
        }

        /// <summary>
        /// 获取Http主机名 如果有端口 包含端口
        /// </summary>
        public static string GetHttpHost(this string value)
        {
            Regex re = new Regex(@"(((?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(:[0-9]+)?|(?:ww‌​w.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?‌​(?:[\w]*))?)");
            return re.Matches(value)[1].ToString();
        }

        /// <summary>
        /// 根据正则表达式 获取对应内容
        /// </summary>
        public static MatchCollection GetRegexMatches(this string value, string Regex)
        {
            return new Regex(@Regex).Matches(value);
        }


        /// <summary>
        /// 验证URL是否有效
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidateUrl(this string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                url = "http://" + url;
            }

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                request.Timeout = 5000; // 5 seconds

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return (response.StatusCode == HttpStatusCode.OK);
                }
            }
            catch (WebException ex) when (ex.Response is HttpWebResponse response)
            {
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}