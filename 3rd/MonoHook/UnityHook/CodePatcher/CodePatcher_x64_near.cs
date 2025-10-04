/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace MonoHook
{
    /// <summary>
    /// x64下2G 内的跳转
    /// </summary>
    public class CodePatcher_x64_near : CodePatcher_x86 // x64_near pathcer code is same to x86
    {
        public CodePatcher_x64_near(IntPtr target, IntPtr replace, IntPtr proxy) : base(target, replace, proxy) { }
    }
}