/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-07-27
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIO.PY4N;

namespace AIO
{
    partial class AHelper
    {
        partial class Random
        {
            #region Nested type: Random

            /// <summary>
            /// 常用姓氏（可扩展）
            /// </summary>
            public static readonly List<char> ChineseSurnames = new List<char>
            {
                '赵', '钱', '孙', '李', '周', '吴', '郑', '王', '冯', '陈', '褚', '卫', '蒋', '沈', '韩', '杨', '朱', '秦', '尤', '许', '何', '吕', '施', '张', '孔', '曹', '严', '华', '金', '魏',
                '陶', '姜', '戚', '谢', '邹', '喻', '柏', '水', '窦', '章', '云', '苏', '潘', '葛', '奚', '范', '彭', '郎', '鲁', '韦', '昌', '马', '苗', '凤', '花', '方', '俞', '任', '袁', '柳',
                '酆', '鲍', '史', '唐', '费', '廉', '岑', '薛', '雷', '贺', '倪', '汤', '滕', '殷', '罗', '毕', '郝', '邬', '安', '常', '乐', '于', '时', '傅', '皮', '卞', '齐', '康', '伍', '余',
                '元', '卜', '顾', '孟', '平', '黄', '和', '穆', '萧', '尹', '姚', '邵', '湛', '汪', '祁', '毛', '禹', '狄', '米', '贝', '明', '臧', '计', '伏', '成', '戴', '谈', '宋', '茅', '庞',
                '熊', '纪', '舒', '屈', '项', '祝', '董', '梁', '杜', '阮', '蓝', '闵', '席', '季', '麻', '强', '贾', '路', '娄', '危', '江', '童', '颜', '郭', '梅', '盛', '林', '刁', '钟', '徐',
                '邱', '骆', '高', '夏', '蔡', '田', '樊', '胡', '凌', '霍', '虞', '万', '支', '柯', '昝', '管', '卢', '莫', '经', '房', '裘', '缪', '干', '解', '应', '宗', '丁', '宣', '贲', '邓',
                '郁', '单', '杭', '洪', '包', '诸', '左', '石', '崔', '吉', '钮', '龚', '程', '嵇', '邢', '滑', '裴', '陆', '荣', '翁', '荀', '羊', '於', '惠', '甄', '曲', '家', '封',
            };

            /// <summary>
            /// 常见男性名字字（可扩展）
            /// </summary>
            public static readonly List<char> ChineseMaleNames = new List<char>
            {
                '强', '刚', '勇', '军', '磊', '涛', '超', '波', '辉', '杰',
                '宇', '明', '龙', '鑫', '飞', '旭', '峰', '凡', '楠', '雷',
                '浩', '鹏', '健', '文', '志', '建', '义', '良', '兴', '海',
                '成', '林', '东', '胜', '亮', '斌', '凯', '洋', '翔', '辰',
                '思', '哲', '辰', '昊', '瑞', '博', '阳', '安', '天', '鸣',
                '鸿', '昊', '瑞', '博', '阳', '安', '念', '鸣', '泽', '航',
            };

            /// <summary>
            /// 常见女性名字字（可扩展）
            /// </summary>
            public static readonly List<char> ChineseFemaleNames = new List<char>
            {
                '芳', '娜', '静', '丽', '敏', '燕', '娟', '雪', '艳', '梅',
                '婷', '慧', '琳', '欣', '佳', '玉', '彤', '怡', '诗', '露',
                "宝", "璐", "璇", "珊", "莹", "瑶", "晴", "倩", '璇', '珊',
                '蓉', '晴', '婧', '妍'
            };

            /// <summary>
            /// 生僻字名字库（适用于男女）
            /// </summary>
            public static readonly List<char> ChineseRareNameChars = new List<char>
            {
                '昉', '珧', '颉', '祎', '潞', '泷', '沣', '骐', '珺', '琛',
                '峤', '飏', '忻', '杼', '晗', '卿', '煦', '翊', '嵘', '濡',
                '槿', '荻', '澹', '蓁', '忪', '郗', '茈', '暄', '翎', '羲'
            };

            private static Dictionary<char, (string, int)[]> maleNames = new Dictionary<char, (string, int)[]>();

            private static Dictionary<char, (string, int)[]> femaleNames = new Dictionary<char, (string, int)[]>();

            private static Dictionary<char, (string, int)[]> rareNameChars = new Dictionary<char, (string, int)[]>();

            /// <summary>
            ///
            /// </summary>
            public static void Init()
            {
                maleNames     = GetNameChars(ChineseMaleNames);
                femaleNames   = GetNameChars(ChineseFemaleNames);
                rareNameChars = GetNameChars(ChineseRareNameChars);
            }

            private static Dictionary<char, (string, int)[]> GetNameChars(List<char> chars)
            {
                var nameChars = new Dictionary<char, (string, int)[]>();
                foreach (var variable in chars)
                {
                    var tp = Pinyin4Net.GetPinyin(variable, PinyinFormat.LOWERCASE);
                    nameChars[variable] = new (string, int)[tp.Length];
                    for (var index = 0; index < tp.Length; index++)
                    {
                        var item = tp[index];
                        nameChars[variable][index] = (item, item[item.Length - 1]);
                    }
                }

                return nameChars;
            }

            static readonly string[] initials =
            {
                "zh", "ch", "sh", "b", "p", "m", "f", "d", "t", "n", "l",
                "g", "k", "h", "j", "q", "x", "r", "z", "c", "s", "y", "w"
            };

            static string GetPinyinPrefix(string pinyin)
            {
                foreach (var ini in initials.OrderByDescending(s => s.Length).Where(pinyin.StartsWith)) return ini;
                return string.Empty;
            }

            /// <summary>
            /// 生成随机中文姓名
            /// </summary>
            /// <param name="isMale">是否为男性</param>
            /// <param name="useRare">是否使用生僻字</param>
            /// <param name="allowRepetition">是否允许重复使用同一个字</param>
            /// <param name="maxLen">姓名的最大长度</param>
            /// <param name="rare">生僻字使用概率，0-1之间</param>
            /// <returns>姓名字符串</returns>
            public static string RandomChineseName(
                bool  isMale          = true,
                bool  useRare         = false,
                bool  allowRepetition = false,
                int   maxLen          = 3,
                float rare            = 0.5f
            )
            {
                if (maleNames.Count == 0) Init();
                if (maxLen < 1 || maxLen > 4) maxLen = 3; // 限制姓名长度在1-3之间

                var str = new StringBuilder();
                str.Append(ChineseSurnames[random.Next(0, ChineseSurnames.Count - 1)]);
                if (maxLen == 1) return str.ToString();
                var chars = isMale ? maleNames : femaleNames;


                if (allowRepetition && (maxLen == 3 || maxLen == 2))
                {
                    var ch = useRare && random.NextDouble() < rare ? RandArrayValue(rareNameChars) : RandArrayValue(chars);
                    for (int i = 0; i < maxLen - 1; i++) str.Append(ch.Key);
                    return str.ToString();
                }

                string last = string.Empty;
                for (int tries = 0; tries < 20; tries++) // --- 非叠字名 ---
                {
                    var first  = useRare && random.NextDouble() < rare ? RandArrayValue(rareNameChars) : RandArrayValue(chars);
                    var second = useRare && random.NextDouble() < rare ? RandArrayValue(rareNameChars) : RandArrayValue(chars);
                    if (GetPinyinPrefix(first.Value[0].Item1) == GetPinyinPrefix(second.Value[0].Item1)) continue;
                    str.Append(first.Key);
                    if (maxLen == 3)
                    {
                        str.Append(second.Key);
                        last = second.Value[0].Item1;
                    }
                    else
                    {
                        last = first.Value[0].Item1;
                    }

                    break;
                }

                if (maxLen == 4)
                {
                    for (int tries = 0; tries < 10; tries++)
                    {
                        var first = useRare && random.NextDouble() < rare ? RandArrayValue(rareNameChars) : RandArrayValue(chars);
                        if (GetPinyinPrefix(last) == GetPinyinPrefix(first.Value[0].Item1)) continue;
                        str.Append(first.Key);
                        break;
                    }
                }

                if (str.Length != 1) return str.ToString();


                for (int i = 0; i < maxLen - 1; i++) // --- 单字名 ---
                {
                    str.Append(useRare && random.NextDouble() < rare ? RandArrayValue(rareNameChars) : RandArrayValue(chars));
                }

                return str.ToString();
            }

            #endregion
        }
    }
}