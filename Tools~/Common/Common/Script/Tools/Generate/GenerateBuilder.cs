using System;
using System.IO;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 生成器
    /// </summary>
    public class GenerateBuilder
    {
        /// <summary>
        /// 生成器
        /// </summary>
        protected StringBuilder Builder;

        /// <inheritdoc />
        public sealed override string ToString() => Builder.ToString();

        /// <summary>
        ///  缩进
        /// </summary>
        protected string indent { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenerateBuilder()
        {
            Builder = new StringBuilder();
            indent  = string.Empty;
        }

        #region Block

        /// <summary>
        ///    添加一个代码块
        /// </summary>
        public GenerateBuilder IncBlock()
        {
            Builder.Append(indent).Append('{').AppendLine();
            indent = $"{indent}    ";
            return this;
        }

        /// <summary>
        ///   减少一个代码块
        /// </summary>
        public GenerateBuilder DecBlock()
        {
            if (indent.Length >= 4)
            {
                indent = indent.Substring(0, indent.Length - 4);
                Builder.Append(indent);
            }
            else indent = string.Empty;

            Builder.Append('}').AppendLine();
            return this;
        }

        #endregion

        /// <summary>
        /// 写入头部
        /// </summary>
        public GenerateBuilder WriteHeader(string mark)
        {
            Builder.AppendFormat("{0}{1}", mark, Environment.NewLine);
            return this;
        }

        #region WriteLine

        /// <summary>
        ///  写入一行
        /// </summary>
        public GenerateBuilder WriteLine()
        {
            Builder.AppendLine();
            return this;
        }

        /// <summary>
        ///  写入一行
        /// </summary>
        public GenerateBuilder WriteLine(string line)
        {
            Builder.AppendFormat("{0}{1}{2}", indent, line, Environment.NewLine);
            return this;
        }

        /// <summary>
        ///  写入一行
        /// </summary>
        public GenerateBuilder WriteLine(string format, params object[] args)
        {
            Builder.Append(indent).AppendFormat(format, args).Append(Environment.NewLine);
            return this;
        }

        #endregion

        #region Indent

        /// <summary>
        ///   增加缩进
        /// </summary>
        public GenerateBuilder IncIndent()
        {
            indent = $"{indent}    ";
            return this;
        }

        /// <summary>
        ///  减少缩进
        /// </summary>
        public GenerateBuilder DecIndent()
        {
            indent = indent.Length >= 4 ? indent.Substring(0, indent.Length - 4) : string.Empty;
            return this;
        }

        #endregion

        /// <summary>
        ///  保存文件
        /// </summary>
        /// <param name="filePath"> 文件路径 </param>
        /// <param name="encoding"> 编码 </param>
        /// <exception cref="Exception"> 保存文件错误 </exception>
        public void Save(string filePath, Encoding encoding = null)
        {
            try
            {
                var target = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(target)) return;
                if (!Directory.Exists(target)) Directory.CreateDirectory(target);
                File.WriteAllText(filePath, Builder.ToString(), encoding ?? Encoding.UTF8);
            }
            catch (Exception e)
            {
                throw new Exception("Save file error", e);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public virtual void Reset()
        {
            indent = string.Empty;
            Builder.Clear();
        }
    }
}