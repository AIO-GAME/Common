#region

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// 字符串区块 字符单位
    /// </summary>
    internal class StringBlock
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="units"></param>
        /// <param name="width"></param>
        /// <param name="ignoreChinese"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public StringBlock(IList<char> units, int width = 200, bool ignoreChinese = false)
        {
            if (units == null) throw new ArgumentNullException(nameof(units));
            if (units.Count != 8) throw new ArgumentNullException(nameof(units), "u arg array count equal to 8");
            if (width <= 3) throw new ArgumentNullException(nameof(width), "arg width value can't less 3");

            Units         = units;
            Width         = width;
            IgnoreChinese = ignoreChinese;

            var format = string.Concat("{0:d}", "{1:d}", "{2:d}");
            Top    = string.Format(format, Units[0], Units[6].Repeat(Width - 2), Units[1]);
            Bottom = string.Format(format, Units[3], Units[6].Repeat(Width - 2), Units[4]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="units"></param>
        /// <param name="width"></param>
        /// <param name="ignoreChinese"></param>
        public StringBlock(string units, int width = 200, bool ignoreChinese = false) : this(units.ToCharArray(), width,
                                                                                             ignoreChinese) { }

        private IList<char> Units { get; }

        private int Width { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Top { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Bottom { get; }

        /// <summary>
        /// 忽略中文
        /// </summary>
        public bool IgnoreChinese { get; private set; }

        private string Context(string value)
        {
            string fomat;
            if (IgnoreChinese)
            {
                var dcount = 0;
                var coding = Encoding.UTF8;
                foreach (var ch in value.ToCharArray())
                    if (coding.GetByteCount(ch.ToString()) >= 2)
                        dcount++;

                fomat = string.Concat("{0} {1}{2,", Width - 2 - value.Length - dcount, "}");
            }
            else
            {
                fomat = string.Concat("{0} {1}{2,", Width - 2 - value.Length, "}");
            }

            return string.Format(fomat, Units[2], value, Units[7]);
        }

        #region Convert

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Convert(params string[] context)
        {
            var builder = new StringBuilder(Top).AppendLine();
            foreach (var info in context) builder.Append(Context(info)).AppendLine();
            builder.Append(Bottom);
            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Convert(IEnumerable<string> context)
        {
            var builder = new StringBuilder(Top).AppendLine();
            foreach (var info in context) builder.Append(Context(info)).AppendLine();
            builder.Append(Bottom);
            return builder.ToString();
        }

        #endregion
    }
}