/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-03-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
namespace AIO
{
    /// <summary>
    ///
    /// </summary>
    public static class CS
    {
        #region WriteLine

        public static void WriteLine(ConsoleColor color, string format, params object[] messages)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(format, messages);
            Console.ResetColor();
        }

        public static void WriteLine(ConsoleColor color, object message, bool newline = false, bool fill = false)
        {
            Console.ForegroundColor = color;
            if (fill)
            {
                Console.WriteLine(content);
                Console.CursorTop--;
            }

            Console.WriteLine(message);
            if (newline) Console.WriteLine(content);
            Console.ResetColor();
        }

        public static void WriteLine(ConsoleColor color, string message, bool newline = false, bool fill = false)
        {
            Console.ForegroundColor = color;
            if (fill)
            {
                Console.WriteLine(content);
                Console.CursorTop--;
            }

            Console.WriteLine(message);
            if (newline) Console.WriteLine(content);
            Console.ResetColor();
        }

        public static void Write(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        #endregion

        /// <summary>
        /// 清空指定行
        /// </summary>
        /// <param name="cursor"></param>
        public static void ClearIndex(int cursor)
        {
            if (cursor < 0) return;
            if (cursor >= Console.BufferHeight)
            {
                Console.BufferHeight = cursor;
                return;
            }

            Console.SetCursorPosition(0, cursor);
            Console.Write(content);
            Console.SetCursorPosition(0, cursor - 1);
        }

        #region WriteLine

        public static void WriteLine(object message)                         { Console.WriteLine(message); }
        public static void WriteLine(string message)                         { Console.WriteLine(message); }
        public static void WriteLine(string format, params object[] message) { Console.WriteLine(format, message); }

        #endregion

        public static ConsoleColor WarnColor { get; set; } = ConsoleColor.DarkYellow;

        public static void WriteLineWarn(Exception exception)                       { WriteLine(WarnColor, exception.Message); }
        public static void WriteLineWarn(string    message)                         { WriteLine(WarnColor, message); }
        public static void WriteLineWarn(object    message)                         { WriteLine(WarnColor, message); }
        public static void WriteLineWarn(string    format, params object[] message) { WriteLine(WarnColor, format, message); }

        public static ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;

        public static void WriteLineError(Exception exception)                       { WriteLine(ErrorColor, exception.Message); }
        public static void WriteLineError(string    message)                         { WriteLine(ErrorColor, message); }
        public static void WriteLineError(object    message)                         { WriteLine(ErrorColor, message); }
        public static void WriteLineError(string    format, params object[] message) { WriteLine(ErrorColor, format, message); }

        public static ConsoleColor InfoColor { get; set; } = ConsoleColor.Green;

        public static void WriteLineInfo(string message)                         { WriteLine(InfoColor, message); }
        public static void WriteLineInfo(object message)                         { WriteLine(InfoColor, message); }
        public static void WriteLineInfo(string format, params object[] message) { WriteLine(InfoColor, format, message); }

        public static void WriteLineInfo(string message, ConsoleColor color) { WriteLine(color, message); }
        public static void WriteLineInfo(object message, ConsoleColor color) { WriteLine(color, message); }

        private static readonly string content = new string(' ', Console.BufferWidth);

        public static void WriteLineCursor(string message, ConsoleColor color = ConsoleColor.White)
        {
            var currentLineCursor = Console.CursorTop <= 1 ? Console.BufferHeight : Console.CursorTop - 1; //获取当前光标所在行的位置
            Console.SetCursorPosition(0, currentLineCursor);                                               //将光标至于当前行的开始位置
            Console.Write(content);                                                                        //用空格将当前行填满，相当于清除当前行
            Console.SetCursorPosition(0, currentLineCursor - 1);                                           //将光标恢复至开始时的位置
            WriteLine(color, message);                                                                     //输出新的内容
        }

        public static void WriteLineCursor(string message, int cursor, ConsoleColor color = ConsoleColor.White)
        {
            if (cursor < 0) return;
            if (cursor >= Console.BufferHeight)
            {
                Console.BufferHeight = cursor;
                cursor               = Console.BufferHeight - 1;
            }

            try
            {
                Console.SetCursorPosition(0, cursor);
                Console.Write(content);
                Console.SetCursorPosition(0, cursor - 1);
            }
            catch (Exception)
            {
                Console.CursorTop--;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(content);
                Console.CursorTop--;
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            WriteLine(color, message);
        }

        public static void WriteCursor(string message, int cursor, ConsoleColor color = ConsoleColor.White, bool newline = false, bool fill = false)
        {
            if (cursor < 0) return;
            if (string.IsNullOrEmpty(message)) return;

            try
            {
                if (cursor >= Console.BufferHeight) Console.BufferHeight = cursor + 1;
                if (fill) ClearIndex(cursor);
                Console.SetCursorPosition(0, cursor);
            }
            catch
            { // ignored
            }

            message = message.TrimStart('\n', '\r', '\t').TrimEnd(' ', '\n', '\r', '\t');
            if (message.Length >= Console.LargestWindowWidth - 1)
                message = message.Substring(0, Console.LargestWindowWidth - 5) + "...";
            else if (message.Length >= Console.BufferWidth - 1)
                message = message.Substring(0, Console.BufferWidth - 5) + "...";
            else if (message.Length >= Console.WindowWidth - 1)
                message = message.Substring(0, Console.WindowWidth - 5) + "...";
            Write(color, message);

            Console.SetCursorPosition(0, cursor - 1);
            if (newline) Write(color, "\n");
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(content);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释