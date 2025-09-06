/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-07-27
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIO.PY4N;

namespace AIO
{
    /// <summary>
    /// 随机中文姓名生成器
    /// </summary>
    /// <code>
    /// var random = new RandomChineseName();
    /// random.MaxLen = 3;
    /// random.UseRare = true;
    /// random.Rare = 0.3f;
    /// random.AllowRepetition = false;
    /// random.AddNames(xxx);
    /// random.AddRareName(xxx);
    /// string name = random.Random('张');
    /// </code>
    public class RandomChineseName : IDisposable
    {
        #region 属性

        /// <summary>
        /// 常用姓氏（可扩展）
        /// </summary>
        private List<char> ChineseSurnames { get; set; }

        /// <summary>
        /// 常见名字（可扩展）
        /// </summary>
        private List<char> NameChineseChars { get; set; }

        /// <summary>
        /// 生僻字名字库
        /// </summary>
        private List<char> RareNameChineseChars { get; set; }

        /// <summary>
        /// 姓名拼音
        /// </summary>
        private Dictionary<char, (string, int)[]> NamePyChars { get; set; }

        /// <summary>
        /// 生僻字拼音
        /// </summary>
        private Dictionary<char, (string, int)[]> RareNamePyChars { get; set; }

        #endregion

        #region 参数

        /// <summary>
        /// 姓名的最大长度
        /// </summary>
        public int MaxLen { get; set; } = 3;

        /// <summary>
        /// 生僻字使用概率，0-1之间
        /// </summary>
        public float Rare { get; set; } = 0.5f;

        /// <summary>
        /// 是否使用生僻字
        /// </summary>
        public bool UseRare { get; set; } = false;

        /// <summary>
        /// 是否允许重复使用同一个字
        /// </summary>
        public bool Repetition { get; set; } = false;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化随机中文姓名生成器
        /// </summary>
        /// <param name="surname"> 常用姓氏 </param>
        /// <param name="surnames"> 常用姓氏 </param>
        public RandomChineseName(char surname, params char[] surnames)
        {
            ChineseSurnames      = new List<char>(surnames) { surname };
            NameChineseChars     = new List<char>();
            RareNameChineseChars = new List<char>();
            NamePyChars          = new Dictionary<char, (string, int)[]>();
            RareNamePyChars      = new Dictionary<char, (string, int)[]>();
        }

        /// <summary>
        /// 初始化随机中文姓名生成器
        /// </summary>
        public RandomChineseName()
        {
            ChineseSurnames      = new List<char>();
            NameChineseChars     = new List<char>();
            RareNameChineseChars = new List<char>();
            NamePyChars          = new Dictionary<char, (string, int)[]>();
            RareNamePyChars      = new Dictionary<char, (string, int)[]>();
        }

        /// <summary>
        /// 初始化随机中文姓名生成器
        /// </summary>
        /// <param name="surnames"> 常用姓氏 </param>
        public RandomChineseName(IEnumerable<char> surnames)
        {
            ChineseSurnames      = new List<char>(surnames);
            NameChineseChars     = new List<char>();
            RareNameChineseChars = new List<char>();
            NamePyChars          = new Dictionary<char, (string, int)[]>();
            RareNamePyChars      = new Dictionary<char, (string, int)[]>();
        }

        /// <summary>
        /// 初始化随机中文姓名生成器
        /// </summary>
        /// <param name="surnames"> 常用姓氏 </param>
        /// <param name="names"> 常用名字 </param>
        /// <param name="rareName"> 生僻字名字 </param>
        public RandomChineseName(IEnumerable<char> surnames, IEnumerable<char> names, IEnumerable<char> rareName)
        {
            ChineseSurnames      = new List<char>(surnames);
            NameChineseChars     = new List<char>();
            RareNameChineseChars = new List<char>();
            NamePyChars          = new Dictionary<char, (string, int)[]>();
            RareNamePyChars      = new Dictionary<char, (string, int)[]>();

            AddNames(names);
            AddRareNames(rareName);
        }

        #endregion

        #region Surnames

        /// <summary>
        /// 添加常用姓氏
        /// </summary>
        /// <param name="surnames"> 常用姓氏 </param>
        public void AddSurnames(IEnumerable<char> surnames)
        {
            foreach (var s in surnames.Where(s => !ChineseSurnames.Contains(s)))
            {
                ChineseSurnames.Add(s);
            }
        }

        /// <summary>
        /// 添加常用姓氏
        /// </summary>
        /// <param name="surname"> 常用姓氏 </param>
        /// <param name="surnames"> 常用姓氏 </param>
        public void AddSurnames(char surname, params char[] surnames)
        {
            if (!ChineseSurnames.Contains(surname)) ChineseSurnames.Add(surname);
            AddSurnames(surnames);
        }

        /// <summary>
        /// 添加常用姓氏
        /// </summary>
        /// <param name="surname"> 常用姓氏 </param>
        public void AddSurnames(char surname)
        {
            if (!ChineseSurnames.Contains(surname)) ChineseSurnames.Add(surname);
        }

        /// <summary>
        /// 清空常用姓氏
        /// </summary>
        public void ClearSurnames() { ChineseSurnames.Clear(); }

        #endregion

        #region Names

        /// <summary>
        /// 添加常用姓氏
        /// </summary>
        /// <param name="names"> 常用姓氏 </param>
        public void AddNames(IEnumerable<char> names)
        {
            var dictionary = GetNameChars(names.Select(s => s).Where(s => !NameChineseChars.Contains(s)));
            foreach (var kvp in dictionary)
            {
                NameChineseChars.Add(kvp.Key);
                NamePyChars[kvp.Key] = kvp.Value;
            }
        }

        /// <summary>
        /// 添加常用姓氏
        /// </summary>
        /// <param name="name"> 常用姓氏 </param>
        /// <param name="names"> 常用姓氏 </param>
        public void AddNames(char name, params char[] names)
        {
            if (!NameChineseChars.Contains(name))
            {
                NameChineseChars.Add(name);
                NamePyChars[name] = GetNameChars(name);
            }

            AddNames(names);
        }

        /// <summary>
        /// 清空常用名字
        /// </summary>
        public void ClearNames()
        {
            NameChineseChars.Clear();
            NamePyChars.Clear();
        }

        #endregion

        #region RareNames

        /// <summary>
        /// 添加生僻字名字
        /// </summary>
        /// <param name="names"> 生僻字名字 </param>
        public void AddRareNames(IEnumerable<char> names)
        {
            var dictionary = GetNameChars(names.Select(s => s).Where(s => !RareNameChineseChars.Contains(s)));
            foreach (var kvp in dictionary)
            {
                RareNameChineseChars.Add(kvp.Key);
                RareNamePyChars[kvp.Key] = kvp.Value;
            }
        }

        /// <summary>
        /// 添加生僻字名字
        /// </summary>
        /// <param name="name"> 生僻字名字 </param>
        /// <param name="names"> 生僻字名字 </param>
        public void AddRareNames(char name, params char[] names)
        {
            if (!RareNameChineseChars.Contains(name))
            {
                RareNameChineseChars.Add(name);
                RareNamePyChars[name] = GetNameChars(name);
            }

            AddRareNames(names);
        }

        /// <summary>
        /// 清空生僻字名字
        /// </summary>
        public void ClearRareNames()
        {
            RareNameChineseChars.Clear();
            RareNamePyChars.Clear();
        }

        #endregion

        #region GetName

        /// <summary>
        /// 随机获取中文姓名
        /// </summary>
        /// <returns>姓名字符串</returns>
        public string GetName()
        {
            if (ChineseSurnames.Count == 0) throw new Exception("没有可用的姓氏，请先添加常用姓氏");
            var ch = ChineseSurnames[AHelper.Random.random.Next(0, ChineseSurnames.Count - 1)];
            return GetName(ch);
        }

        /// <summary>
        /// 随机获取中文姓名
        /// </summary>
        /// <param name="surnames"> 指定姓氏 </param>
        /// <returns>姓名字符串</returns>
        public string GetName(IList<char> surnames)
        {
            if (surnames == null || surnames.Count == 0) return GetName();
            var random = AHelper.Random.random;
            var ch     = surnames[random.Next(0, surnames.Count - 1)];
            return GetName(ch);
        }

        /// <summary>
        /// 随机获取中文姓名
        /// </summary>
        /// <param name="surname"> 指定姓氏 </param>
        /// <returns>姓名字符串</returns>
        public string GetName(char surname)
        {
            if (MaxLen < 1 || MaxLen > 4) MaxLen = 3; // 限制姓名长度在1-3之间

            var random  = AHelper.Random.random;
            var builder = new StringBuilder().Append(surname);
            if (MaxLen == 1) return builder.ToString();
            var chars = NamePyChars;


            if (Repetition && (MaxLen == 3 || MaxLen == 2))
            {
                var ch = UseRare && random.NextDouble() < Rare ? AHelper.Random.RandArrayValue(RareNamePyChars) : AHelper.Random.RandArrayValue(chars);
                for (int i = 0; i < MaxLen - 1; i++) builder.Append(ch.Key);
                return builder.ToString();
            }

            string last = string.Empty;
            for (int tries = 0; tries < 20; tries++) // --- 非叠字名 ---
            {
                var first  = UseRare && random.NextDouble() < Rare ? AHelper.Random.RandArrayValue(RareNamePyChars) : AHelper.Random.RandArrayValue(chars);
                var second = UseRare && random.NextDouble() < Rare ? AHelper.Random.RandArrayValue(RareNamePyChars) : AHelper.Random.RandArrayValue(chars);
                if (GetPinyinPrefix(first.Value[0].Item1) == GetPinyinPrefix(second.Value[0].Item1)) continue;
                builder.Append(first.Key);
                if (MaxLen == 3)
                {
                    builder.Append(second.Key);
                    last = second.Value[0].Item1;
                }
                else
                {
                    last = first.Value[0].Item1;
                }

                break;
            }

            if (MaxLen == 4)
            {
                for (int tries = 0; tries < 10; tries++)
                {
                    var first = UseRare && random.NextDouble() < Rare ? AHelper.Random.RandArrayValue(RareNamePyChars) : AHelper.Random.RandArrayValue(chars);
                    if (GetPinyinPrefix(last) == GetPinyinPrefix(first.Value[0].Item1)) continue;
                    builder.Append(first.Key);
                    break;
                }
            }

            if (builder.Length != 1) return builder.ToString();


            for (int i = 0; i < MaxLen - 1; i++) // --- 单字名 ---
            {
                builder.Append(UseRare && random.NextDouble() < Rare ? AHelper.Random.RandArrayValue(RareNamePyChars) : AHelper.Random.RandArrayValue(chars));
            }

            return builder.ToString();
        }

        #endregion

        #region 静态函数

        private static readonly string[] initials =
        {
            "zh", "ch", "sh", "b", "p", "m", "f", "d", "t", "n", "l",
            "g", "k", "h", "j", "q", "x", "r", "z", "c", "s", "y", "w"
        };

        private static Dictionary<char, (string, int)[]> GetNameChars(IEnumerable<char> chars)
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

        private static (string, int)[] GetNameChars(char @char)
        {
            var tp     = Pinyin4Net.GetPinyin(@char, PinyinFormat.LOWERCASE);
            var tuples = new (string, int)[tp.Length];
            for (var index = 0; index < tp.Length; index++)
            {
                var item = tp[index];
                tuples[index] = (item, item[item.Length - 1]);
            }

            return tuples;
        }

        private static string GetPinyinPrefix(string pinyin)
        {
            foreach (var ini in initials.OrderByDescending(s => s.Length).Where(pinyin.StartsWith)) return ini;
            return string.Empty;
        }

        #endregion

        /// <inheritdoc />
        public void Dispose()
        {
            ChineseSurnames.Clear();
            NameChineseChars.Clear();
            RareNameChineseChars.Clear();
            NamePyChars.Clear();
            RareNamePyChars.Clear();
            GC.SuppressFinalize(this);
        }
    }
}