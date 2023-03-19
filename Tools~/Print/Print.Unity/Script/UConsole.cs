/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2023-01-08                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Unity 控制台输出
    /// </summary>
    internal static class UnityConsole
    {
        #region :: FIELDS

        /// <summary>
        /// Unity Log 输出
        /// </summary>
        private static UnityTextWriter WriterOut;

        /// <summary>
        /// Unity Error 输出
        /// </summary>
        private static UnityTextWriter WriterError;

        /// <summary>
        /// 源 输出
        /// </summary>
        private static TextWriter OriginalOut;

        /// <summary>
        /// 源 输出
        /// </summary>
        private static TextWriter OriginalError;

        #endregion

        /// <summary>
        /// 关闭 LOG 输出
        /// </summary>
        internal static void EnabledLog()
        {
            if (WriterOut == null)
            {
                OriginalOut = Console.Out;
                WriterOut = new UnityTextWriter(UnityConsoleType.Log);
                Console.SetOut(WriterOut);
            }
        }

        /// <summary>
        /// 关闭 LOG 输出
        /// </summary>
        internal static void EnabledError()
        {
            if (WriterError == null)
            {
                OriginalError = Console.Error;
                WriterError = new UnityTextWriter(UnityConsoleType.Error);
                Console.SetError(WriterError);
            }
        }

        /// <summary>
        /// 关闭 LOG 输出
        /// </summary>
        internal static void ENABLED_ALL_NO_CONDITION()
        {
            EnabledLog();
            EnabledError();
        }

        #region :: DISABLE

        /// <summary>
        /// 关闭 全部输出
        /// </summary>
        internal static void DisableALL()
        {
            DisableLog();
            DisableError();
        }

        /// <summary>
        /// 关闭 LOG 输出
        /// </summary>
        internal static void DisableLog()
        {
            if (WriterOut != null)
            {
                WriterOut.SetPrint(false);
                WriterOut.Close();
                WriterOut.Dispose();
                WriterOut = null;
                if (OriginalOut != null) Console.SetOut(OriginalOut);
                Console.Clear();
            }
        }

        /// <summary>
        /// 关闭 ERROR 输出
        /// </summary>
        internal static void DisableError()
        {
            if (WriterError != null)
            {
                WriterError.SetPrint(false);
                WriterError.Close();
                WriterError.Dispose();
                WriterError = null;
                if (OriginalError != null) Console.SetOut(OriginalError);
                Console.Clear();
            }
        }

        #endregion

        internal enum UnityConsoleType
        {
            /// <summary>
            /// 正常输出
            /// </summary>
            Log,
            /// <summary>
            /// 错误输出
            /// </summary>
            Error,
        }

        /// <summary>
        /// Unity 重置定向 输出
        /// Redirects writes to System.Console to Unity3D's Debug.Log.
        /// </summary>
        /// <see url="https://www.jacksondunstan.com/articles/2986"/>
        /// <author name="Jackson Dunstan"/>
        private class UnityTextWriter : TextWriter
        {
            #region :: FIELDS

            private const string TAG_LOG = "<color=#E47833><b>[LOG]</b></color> ";
            private const string TAG_ERROR = "<color=#E47833><b>[ERROR]</b></color> ";
            private bool IsPrint;
            private StringBuilder Buffer { get; }
            private UnityConsoleType Type { get; }

            #endregion

            #region :: FUNCTION

            public UnityTextWriter(UnityConsoleType type)
            {
                Buffer = new StringBuilder();
                Type = type;
                SetPrint(true);
            }

            internal void SetPrint(bool value)
            {
                IsPrint = value;
                UnityEngine.Debug.Log(string.Concat(TAG_LOG, value ? "Enabled" : "Disable", " Success : ", Type.ToString()));
            }

            private void Print(string message)
            {
                if (!IsPrint) return;
                if (string.IsNullOrEmpty(message)) return;
                switch (Type)
                {
                    case UnityConsoleType.Error:
                        UnityEngine.Debug.LogError(string.Concat(TAG_ERROR, message));
                        break;
                    case UnityConsoleType.Log:
                    default:
                        UnityEngine.Debug.Log(string.Concat(TAG_LOG, message));
                        break;
                }
            }

            private void FlushNoLock()
            {
                var str = Buffer.ToString();
                Buffer.Length = 0;
                Print(str);
            }

            /// <summary>
            /// 刷新
            /// </summary>
            public override void Flush()
            {
                string str;
                lock (this)
                {
                    str = Buffer.ToString();
                    Buffer.Length = 0;
                }
                Print(str);
            }

            /// <summary>
            /// 写入
            /// </summary>
            public override void Write(string value)
            {
                if (value != null)
                {

                    var len = value.Length;
                    if (len > 0)
                    {
                        lock (this)
                        {
                            var lastChar = value[len - 1];
                            if (lastChar == '\n')
                            {
                                if (len > 1)
                                    Buffer.Append(value, 0, len - 1);
                                FlushNoLock();
                            }
                            else
                            {
                                Buffer.Append(value, 0, len);
                            }
                        }
                    }
                }
            }

            /// <summary>
            ///
            /// </summary>
            public override void Write(char value)
            {
                lock (this)
                {
                    if (value == '\n')
                    {
                        FlushNoLock();
                    }
                    else
                    {
                        Buffer.Append(value);
                    }
                }
            }

            /// <summary>
            ///
            /// </summary>
            public override void Write(char[] value, int index, int count)
            {
                if (count > 0)
                {
                    lock (this)
                    {
                        var lastChar = value[index + count - 1];
                        if (lastChar == '\n')
                        {
                            if (count > 1)
                                Buffer.Append(value, index, count - 1);
                            FlushNoLock();
                        }
                        else
                        {
                            Buffer.Append(value, index, count);
                        }
                    }
                }
            }

            /// <summary>
            ///
            /// </summary>
            public override Encoding Encoding
            {
                get { return Encoding.Default; }
            }

            #endregion
        }
    }
}