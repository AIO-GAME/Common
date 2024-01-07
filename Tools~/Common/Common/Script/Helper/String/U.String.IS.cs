using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIO
{
    public partial class AHelper
    {
        public partial class String
        {
            /// <summary>
            /// 验证一年的12个月
            /// </summary>
            public static bool IsValidateMonth(in string value)
            {
                return Regex.IsMatch(value, @"^(0?[[1-9]|1[0-2])$");
            }

            /// <summary>
            /// 验证输入汉字
            /// </summary>
            public static bool IsValidateChinese(in string value)
            {
                return Regex.IsMatch(value, $@"^[/u4e00-/u9fa5],{{0,}}$");
            }

            /// <summary>
            /// 验证输入字符串 是否满足指定个数
            /// </summary>
            public static bool IsValidateLength(in string value, int number)
            {
                return Regex.IsMatch(value, string.Concat("^.{", number, "}$"));
            }

            /// <summary>
            /// 验证当前字符串是否在指定长度范围内
            /// </summary>
            public static bool IsValidateMinMaxLength(in string value, int min, int max)
            {
                return Regex.IsMatch(value, string.Concat("^/d{", min, ',', max, "}$"));
            }

            /// <summary>
            /// 验证一个月的31天
            /// </summary>
            public static bool IsValidateDay(in string value)
            {
                return Regex.IsMatch(value, @"^((0?[1-9])|((1|2)[0-9])|30|31)$");
            }

            /// <summary>
            /// 验证两位小数
            /// </summary>
            public static bool IsValidate2Decimal(in string value)
            {
                return Regex.IsMatch(value, @"^[0-9]*[.]{1}[0-9]{2}$");
            }

            /// <summary> 
            /// 判断字符是否为空 
            /// </summary>
            public static bool IsNullOrEmpty(in string value)
            {
                return string.IsNullOrEmpty(value);
            }

            /// <summary>
            /// 是否为Numeric
            /// </summary>
            public static bool IsValidateNumeric(in string value)
            {
                return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
            }

            /// <summary>
            /// 是否为Int
            /// </summary>
            public static bool IsValidateInt(in string value)
            {
                return Regex.IsMatch(value, "^-?\\d+$");
            }

            /// <summary>
            /// 是否为Bool
            /// </summary>
            public static bool IsValidateBool(in string value)
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
            public static bool IsValidateDate(in string value)
            {
                var bValids = Regex.IsMatch(value,
                    @"^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$");
                return (bValids && string.Compare(value, "1753-01-01", StringComparison.InvariantCulture) >= 0);
            }

            /// <summary>
            /// 判断字符串是否是yy-mm-dd字符串
            /// </summary>
            public static bool IsValidateDateString(in string value)
            {
                return Regex.IsMatch(value, @"（\d{4}）-（\d{1,2}）-（\d{1,2}）");
            }

            /// <summary>
            /// 验证手机号码
            /// </summary>
            public static bool IsValidatePhone(in string value)
            {
                return Regex.IsMatch(value, @"^1(?:3\d|4[4-9]|5[0-35-9]|6[67]|7[013-8]|8\d|9\d)\d{8}$");
            }

            /// <summary>
            /// 是否为IP分段
            /// </summary>
            public static bool IsValidateIPSect(in string value)
            {
                return Regex.IsMatch(value,
                    @"^（（2[0-4]\d|25[0-5]|[01]?\d\d?）\.）{2}（（2[0-4]\d|25[0-5]|[01]?\d\d?|\*）\.）（2[0-4]\d|25[0-5]|[01]?\d\d?|\*）$");
            }

            /// <summary>
            /// 是否电子邮件
            /// </summary>
            public static bool IsValidateEmail(in string value)
            {
                return Regex.IsMatch(value,
                    @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            }

            /// <summary>
            /// 验证邮编
            /// </summary>
            public static bool IsPostalcode(in string value)
            {
                return Regex.IsMatch(value, @"^\d{6}$");
            }

            /// <summary>
            /// 验证身份证号
            /// </summary>
            public static bool IsValidateIDcard(in string value)
            {
                switch (value.Length)
                {
                    case 18: return IsValidateIDcard18(value);
                    case 15: return IsValidateIDcard15(value);
                    default: return false;
                }
            }

            /// <summary>
            /// 验证中国身份证号18
            /// </summary>
            public static bool IsValidateIDcard18(in string value)
            {
                //数字验证
                if (long.TryParse(value.Remove(17), out var n) == false ||
                    n < System.Math.Pow(10, 16) ||
                    long.TryParse(value.Replace('x', '0').Replace('X', '0'), out n) == false)
                    return false;

                //省份验证
                const string address =
                    "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(value.Remove(2), StringComparison.CurrentCulture) == -1) return false;

                var birth = value.Substring(6, 8).Insert(6, "-").Insert(4, "-");

                //生日验证
                if (DateTime.TryParse(birth, out _) == false) return false;

                var arrVerifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                var Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                var Ai = value.Remove(17).ToCharArray();
                var sum = 0;
                for (var i = 0; i < 17; i++) sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());

                System.Math.DivRem(sum, 11, out var y);
                //校验码验证
                if (arrVerifyCode[y] != value.Substring(17, 1).ToLower()) return false;

                return true; //符合GB11643-1999标准
            }

            /// <summary>
            /// 验证身份证号15
            /// </summary>
            public static bool IsValidateIDcard15(in string value)
            {
                //数字验证
                if (long.TryParse(value, out var n) == false || n < System.Math.Pow(10, 14)) return false;

                const string address =
                    "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                //省份验证
                if (address.IndexOf(value.Remove(2), StringComparison.CurrentCulture) == -1) return false;

                var birth = value.Substring(6, 6).Insert(4, "-").Insert(2, "-");

                //生日验证
                if (DateTime.TryParse(birth, out _) == false) return false;

                return true; //符合15位身份证标准
            }

            /// <summary>
            /// 15位省份证号码升18位
            /// </summary>
            public static string IDcard15To18(in string value)
            {
                // 不是15位身份证号码，直接返回原字符串
                if (value.Length != 15) return value;
                var iS = 0;
                // 加权因子常数
                var iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
                // 校验码常数
                const string LastCode = "10X98765432";
                // 新身份证号
                var perIDNew = value.Substring(0, 6) + "19" + value.Substring(6, 9);

                if (DateTime.TryParseExact(perIDNew.Substring(6, 8), "yyMMdd", null, DateTimeStyles.None, out var dt))
                {
                    perIDNew = perIDNew.Substring(0, 6) + dt.ToString("yyyyMMdd") + perIDNew.Substring(14);
                } // 出生日期无效，直接返回原字符串
                else return value;

                // 进行加权求和
                for (var i = 0; i < 17; i++) iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];

                // 取模运算，得到模值
                var iY = iS % 11;
                // 从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。
                perIDNew += LastCode.Substring(iY, 1);
                return perIDNew;
            }

            /// <summary>
            /// 获取Http主机名 如果有端口 包含端口
            /// </summary>
            public static string GetHttpHost(in string value)
            {
                if (Uri.TryCreate(value, UriKind.Absolute, out var uriResult) && uriResult.Scheme == Uri.UriSchemeHttp)
                {
                    return uriResult.Host + (uriResult.IsDefaultPort ? "" : ":" + uriResult.Port);
                }

                return "";
            }

            /// <summary>
            /// 根据正则表达式 获取对应内容
            /// </summary>
            public static MatchCollection GetRegexMatches(in string value, string Regex)
            {
                return new Regex(@Regex).Matches(value);
            }


            /// <summary>
            /// 验证URL是否有效
            /// </summary>
            public static async Task<bool> IsValidateUrl(string url, string unit = "http://")
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out _))
                {
                    url = string.Concat(unit + url);
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(5);

                    try
                    {
                        var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                        return (response.StatusCode == HttpStatusCode.OK);
                    }
                    catch (HttpRequestException ex) when (ex.InnerException is OperationCanceledException)
                    {
                        return false; // 超时异常，认为URL无效
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}