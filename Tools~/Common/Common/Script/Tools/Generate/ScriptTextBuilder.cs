using System;
using static System.String;
using static System.Environment;

namespace AIO
{
    /// <summary>
    ///    代码生成器
    /// </summary>
    public class ScriptTextBuilder : GenerateBuilder
    {
        #region Namespace

        private bool IsNamespace;

        /// <summary>
        /// 重置
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            IsNamespace = false;
        }

        /// <summary>
        ///   添加命名空间
        /// </summary>
        /// <param name="ns"> 命名空间 </param>
        /// <exception cref="Exception">命名空间已经开始</exception>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        public ScriptTextBuilder IncNamespace(string ns)
        {
            if (IsNamespace) throw new Exception("Namespace already started");
            if (IsNullOrEmpty(ns)) throw new ArgumentNullException(nameof(ns));
            IsNamespace = true;
            Builder.Append("namespace ").Append(ns).AppendLine();
            IncBlock();
            return this;
        }

        /// <summary>
        ///  减少命名空间
        /// </summary>
        /// <exception cref="Exception">命名空间未开始</exception>
        public ScriptTextBuilder DecNamespace()
        {
            if (!IsNamespace) throw new Exception("Namespace not started");
            DecBlock();
            IsNamespace = false;
            return this;
        }

        #endregion

        #region Annot

        /// <summary>
        /// summary 注释
        /// </summary>
        /// <param name="content"> 注释内容 </param>
        public ScriptTextBuilder AnnotCode(string content)
        {
            if (IsNullOrEmpty(content)) return this;
            Builder.AppendFormat("{0}/// <code>{1}", indent, NewLine);
            Builder.AppendFormat("{0}/// {1}{2}", indent, content, NewLine);
            Builder.AppendFormat("{0}/// </code>{1}", indent, NewLine);
            return this;
        }

        /// <summary>
        /// summary 注释
        /// </summary>
        /// <param name="contents"> 注释内容 </param>
        public ScriptTextBuilder AnnotCode(params string[] contents)
        {
            Builder.AppendFormat("{0}/// <code>{1}", indent, NewLine);
            for (var i = 1; i < contents.Length; i++)
            {
                if (IsNullOrEmpty(contents[i])) continue;
                Builder.AppendFormat("{0}/// {1}{2}", indent, contents[i], NewLine);
            }

            Builder.AppendFormat("{0}/// </code>{1}", indent, NewLine);
            return this;
        }

        /// <summary>
        /// summary 注释
        /// </summary>
        /// <param name="content"> 注释内容 </param>
        public ScriptTextBuilder AnnotReturns(string content)
        {
            if (IsNullOrEmpty(content)) return this;
            Builder.AppendFormat("{0}/// <returns>{1}</returns>{2}", indent, content, NewLine);
            return this;
        }

        /// <summary>
        /// summary 注释
        /// </summary>
        /// <param name="cref"> cref </param>
        /// <param name="content"> 注释内容 </param>
        public ScriptTextBuilder AnnotExpressionCref(string cref, string content)
        {
            if (IsNullOrEmpty(content)) return this;
            Builder.AppendFormat("{0}/// <expression cref=\"{1}\">{2}</expression>{3}", indent, cref, content, NewLine);
            return this;
        }

        /// <summary>
        /// summary 注释
        /// </summary>
        /// <param name="name"> 参数名 </param>
        /// <param name="content"> 注释内容 </param>
        public ScriptTextBuilder AnnotTypeParam(string name, string content)
        {
            if (IsNullOrEmpty(content)) return this;
            Builder.AppendFormat("{0}/// <typeparam name=\"{1}\">{2}</typeparam>{3}", indent, name, content, NewLine);
            return this;
        }

        /// <summary>
        /// summary 注释
        /// </summary>
        /// <param name="content"> 注释内容 </param>
        public ScriptTextBuilder AnnotC(string content)
        {
            if (IsNullOrEmpty(content)) return this;
            Builder.AppendFormat("{0}/// <c>{1}</c>{2}", indent, content, NewLine);
            return this;
        }

        /// <summary>
        /// summary 注释
        /// </summary>
        /// <param name="param"> 参数名 </param>
        /// <param name="content"> 内容 </param>
        public ScriptTextBuilder AnnotParam(string param, string content)
        {
            if (IsNullOrEmpty(param)) return this;
            Builder.AppendFormat("{0}/// <param name=\"{1}\">{2}</param>{3}", indent, param, content, NewLine);
            return this;
        }

        /// <summary>
        /// summary 注释
        /// </summary>
        /// <param name="value"> 注释内容 </param>
        public ScriptTextBuilder AnnotSummary(string value)
        {
            if (IsNullOrEmpty(value)) return this;
            Builder.AppendFormat("{0}/// <summary>{1}", indent, NewLine);
            Builder.AppendFormat("{0}/// {1}{2}", indent, value, NewLine);
            Builder.AppendFormat("{0}/// </summary>{1}", indent, NewLine);
            return this;
        }

        /// <summary>
        /// summary 注释
        /// </summary>
        /// <param name="values"> 注释内容 </param>
        public ScriptTextBuilder AnnotSummary(params string[] values)
        {
            Builder.AppendFormat("{0}/// <summary>{1}", indent, NewLine);
            foreach (var value in values)
            {
                if (IsNullOrEmpty(value)) continue;
                Builder.AppendFormat("{0}/// {1}{2}", indent, value, NewLine);
            }

            Builder.AppendFormat("{0}/// </summary>{1}", indent, NewLine);
            return this;
        }

        /// <summary>
        /// date 注释
        /// </summary>
        /// <returns></returns>
        public ScriptTextBuilder AnnotDate()
        {
            Builder.AppendFormat("{0}/// <date> {1:yyyy-MM-dd} </date>{2}", indent, DateTime.Now, NewLine);
            return this;
        }

        /// <summary>
        /// date 注释
        /// </summary>
        /// <param name="value"> 时间 </param>
        /// <returns></returns>
        public ScriptTextBuilder AnnotDate(DateTime value)
        {
            Builder.AppendFormat("{0}/// <date> {1:yyyy-MM-dd} </date>{2}", indent, value, NewLine);
            return this;
        }

        /// <summary>
        /// email 注释
        /// </summary>
        /// <param name="value"> 邮箱 </param>
        /// <returns></returns>
        public ScriptTextBuilder AnnotEmail(string value)
        {
            if (IsNullOrEmpty(value)) return this;
            Builder.AppendFormat("{0}/// <email> {1} </email>{2}", indent, value, NewLine);
            return this;
        }

        /// <summary>
        /// author 注释
        /// </summary>
        /// <param name="value"> 作者 </param>
        /// <returns></returns>
        public ScriptTextBuilder AnnotAuthor(string value)
        {
            if (IsNullOrEmpty(value)) return this;
            Builder.AppendFormat("{0}/// <author> {1} </author>{2}", indent, value, NewLine);
            return this;
        }

        /// <summary>
        /// see 注释
        /// </summary>
        /// <param name="value"> 参考 </param>
        /// <returns></returns>
        public ScriptTextBuilder AnnotSee(string value)
        {
            if (IsNullOrEmpty(value)) return this;
            Builder.AppendFormat("{0}/// <see> {1} </see>{2}", indent, value, NewLine);
            return this;
        }

        /// <summary>
        /// href 注释
        /// </summary>
        /// <param name="value"> 链接 </param>
        /// <returns></returns>
        public ScriptTextBuilder AnnotHref(string value)
        {
            if (IsNullOrEmpty(value)) return this;
            Builder.AppendFormat("{0}/// <href> {1} </href>{2}", indent, value, NewLine);
            return this;
        }

        /// <summary>
        /// remarks 注释
        /// </summary>
        /// <param name="value"> 备注 </param>
        /// <returns></returns>
        public ScriptTextBuilder AnnotRemarks(string value)
        {
            if (IsNullOrEmpty(value)) return this;
            Builder.AppendFormat("{0}/// <remarks> {1} </remarks>{2}", indent, value, NewLine);
            return this;
        }

        #endregion

        /// <summary>
        /// 写入 goto
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public ScriptTextBuilder WriteGoto(string tag)
        {
            if (IsNullOrEmpty(tag)) return this;
            Builder.AppendFormat("{0}goto {1};{2}", indent, tag, NewLine);
            return this;
        }

        /// <summary>
        /// 写入 using
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        public ScriptTextBuilder WriteUsing(string ns)
        {
            if (IsNullOrEmpty(ns)) return this;
            Builder.AppendFormat("{0}using {1};{2}", indent, ns, NewLine);
            return this;
        }

        /// <summary>
        /// 写入 using
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        public ScriptTextBuilder WriteUsing(params string[] ns)
        {
            foreach (var n in ns)
            {
                if (IsNullOrEmpty(n)) continue;
                Builder.AppendFormat("{0}using {1};{2}", indent, n, NewLine);
            }

            return this;
        }

        #region Macro

        /// <summary>
        /// 写入 宏开始
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public ScriptTextBuilder MacroRegion(string region)
        {
            Builder.AppendFormat("{0}#region {1}{2}", indent, region, NewLine);
            return this;
        }

        /// <summary>
        /// 写入 宏开始
        /// </summary>
        /// <returns></returns>
        public ScriptTextBuilder MacroEndRegion()
        {
            Builder.AppendFormat("{0}#endregion{1}", indent, NewLine);
            return this;
        }

        /// <summary>
        /// 写入 宏define
        /// </summary>
        /// <param name="define"></param>
        /// <returns></returns>
        public ScriptTextBuilder MacroDefine(string define)
        {
            Builder.AppendFormat("{0}#define {1}{2}", indent, define, NewLine);
            return this;
        }

        /// <summary>
        /// 写入 宏undef
        /// </summary>
        /// <returns></returns>
        public ScriptTextBuilder MacroUnDefine()
        {
            Builder.AppendFormat("{0}#undef {1}", indent, NewLine);
            return this;
        }

        /// <summary>
        /// 写入 宏if
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ScriptTextBuilder MacroIf(string condition)
        {
            Builder.AppendFormat("{0}#if {1}{2}", indent, condition, NewLine);
            return this;
        }

        /// <summary>
        /// 写入 宏else if
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ScriptTextBuilder MacroElseIf(string condition)
        {
            Builder.AppendFormat("{0}#elif {1}{2}", indent, condition, NewLine);
            return this;
        }

        /// <summary>
        /// 写入 宏else
        /// </summary>
        /// <returns></returns>
        public ScriptTextBuilder MacroElse()
        {
            Builder.AppendFormat("{0}#else{1}", indent, NewLine);
            return this;
        }

        /// <summary>
        /// 写入 宏结束
        /// </summary>
        /// <returns></returns>
        public ScriptTextBuilder MacroEndIf()
        {
            Builder.AppendFormat("{0}#endif{1}", indent, NewLine);
            return this;
        }

        /// <summary>
        /// 写入错误
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public ScriptTextBuilder MacroError(string error)
        {
            Builder.AppendFormat("{0}#error {1}{2}", indent, error, NewLine);
            return this;
        }

        /// <summary>
        /// 写入警告
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        public ScriptTextBuilder MacroWarning(string warning)
        {
            Builder.AppendFormat("{0}#warning {1}{2}", indent, warning, NewLine);
            return this;
        }

        /// <summary>
        ///  写入标记
        /// </summary>
        /// <param name="pragma"></param>
        /// <returns></returns>
        public ScriptTextBuilder MacroPragma(string pragma)
        {
            Builder.AppendFormat("{0}#pragma {1}{2}", indent, pragma, NewLine);
            return this;
        }

        /// <summary>
        ///   写入标记
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        public ScriptTextBuilder MacroPragmaWarning(string warning)
        {
            Builder.AppendFormat("{0}#pragma warning {1}{2}", indent, warning, NewLine);
            return this;
        }

        #endregion
    }
}