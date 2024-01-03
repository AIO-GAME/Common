/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Debug = UnityEngine.Debug;

namespace AIO.UEditor
{
    public static partial class EHelper
    {
        /// <summary>
        /// 程序设置
        /// </summary>
        public class Program
        {
#if UNITY_EDITOR_WIN

            #region WIN32API

            private delegate bool EnumWindowsCallBack(IntPtr hwnd, IntPtr lParam);

            [DllImport("user32", CharSet = CharSet.Unicode)]
            private static extern bool SetWindowTextW(IntPtr hwnd, string title);

            //回调指针，值
            [DllImport("user32")]
            private static extern int EnumWindows(EnumWindowsCallBack lpEnumFunc, IntPtr lParam);

            [DllImport("user32")]
            private static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref IntPtr lpdwProcessId);

            [DllImport("user32")]
            private static extern bool CloseWindow(IntPtr hwnd);

            [DllImport("user32")]
            private static extern int SendMessageA(IntPtr hWnd, int wMsg, int wParam, IntPtr lParam);

            [DllImport("shell32")]
            private static extern IntPtr ExtractIcon(int hInst, string lpszExeFileName, int nIconIndex);

            #endregion

#endif

            /// <summary>
            /// 当前窗口句柄
            /// </summary>
            private static IntPtr myWindowHandle;

#if UNITY_EDITOR_WIN
            static Program()
            {
                var handle = (IntPtr)Process.GetCurrentProcess().Id; //获取进程ID
                EnumWindows(EnumWindCallback, handle); //枚举查找本窗口
            }
#endif

            /// <summary>
            /// 改变窗口标题
            /// </summary>
            /// <param name="title"></param>
            public static void ChangeTitle(string title)
            {
#if UNITY_EDITOR_WIN
                SetWindowTextW(myWindowHandle, title); //设置窗口标题
#endif
            }

            /// <summary>
            /// 更改窗口图标
            /// </summary>
            /// <param name="icon">图标路径</param>
            public static void ChangeICON(string icon)
            {
                if (!File.Exists(icon)) throw new IOException(icon);
#if UNITY_EDITOR_WIN
                var result = ExtractIcon(0, icon.Replace('\\', '/'), 0);
                try
                {
                    if (result != IntPtr.Zero) SendMessageA(myWindowHandle, 0x80, 0, result);
                    else Debug.LogError("设置 进程 ICON 失败");
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
#endif
            }

            private static bool EnumWindCallback(IntPtr hwnd, IntPtr lParam)
            {
#if UNITY_EDITOR_WIN
                var pid = IntPtr.Zero;
                GetWindowThreadProcessId(hwnd, ref pid);
                if (pid == lParam) //判断当前窗口是否属于本进程
                {
                    myWindowHandle = hwnd;
                    return false;
                }

                return true;
#else
            return false;
#endif
            }
        }
    }
}