using System;
using System.IO;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 控制台输出复制到文件
    /// </summary>
    public class ConsoleCopy : IDisposable
    {
        private FileStream m_FileStream;
        private StreamWriter m_FileWriter;

        private readonly TextWriter m_DoubleWriter;
        private readonly TextWriter m_OldOut;

        private class DoubleWriter : TextWriter, IDisposable
        {
            private TextWriter _mOne;
            private TextWriter _mTwo;

            public DoubleWriter(TextWriter one, TextWriter two)
            {
                _mOne = one;
                _mTwo = two;
            }

            public override Encoding Encoding => _mOne.Encoding;

            public override void Flush()
            {
                _mOne.Flush();
                _mTwo.Flush();
            }

            public override void Write(char value)
            {
                _mOne.Write(value);
                _mTwo.Write(value);
            }

            /// <summary>
            /// 释放
            /// </summary>
            void IDisposable.Dispose()
            {
                _mOne = null;
                _mTwo = null;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path"> 文件路径 </param>
        public ConsoleCopy(string path)
        {
            m_OldOut = Console.Out;

            try
            {
                m_FileStream = File.Create(path);

                m_FileWriter = new StreamWriter(m_FileStream)
                {
                    AutoFlush = true
                };

                m_DoubleWriter = new DoubleWriter(m_FileWriter, m_OldOut);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open file for writing");
                Console.WriteLine(e.Message);
                return;
            }

            Console.SetOut(m_DoubleWriter);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            Console.SetOut(m_OldOut);

            if (m_FileWriter != null)
            {
                m_FileWriter.Flush();
                m_FileWriter.Close();
                m_FileWriter = null;
            }

            if (m_FileStream != null)
            {
                m_FileStream.Close();
                m_FileStream = null;
            }
        }
    }
}