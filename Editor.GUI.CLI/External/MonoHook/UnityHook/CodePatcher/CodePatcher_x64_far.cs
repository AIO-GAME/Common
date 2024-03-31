/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace MonoHook
{
    /// <summary>
    /// x64下距离超过2G的跳转
    /// </summary>
    public unsafe class CodePatcher_x64_far : CodePatcher
    {
        protected static readonly byte[] s_jmpCode = new byte[] // 12 bytes
        {
            // 由于 rax 会被函数作为返回值修改，并且不会被做为参数使用，因此修改是安全的
            0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // mov rax, <jmpTo>
            0x50, // push rax
            0xC3 // ret
        };

        //protected static readonly byte[] s_jmpCode2 = new byte[] // 14 bytes
        //{
        //    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,       // <jmpTo>
        //    0xFF, 0x25, 0xF2, 0xFF, 0xFF, 0xFF                    // jmp [rip - 0xe]
        //};

        public CodePatcher_x64_far(IntPtr target, IntPtr replace, IntPtr proxy) : base(target, replace, proxy,
            s_jmpCode.Length)
        {
        }

        protected override byte[] GenJmpCode(void* jmpFrom, void* jmpTo)
        {
            byte[] ret = new byte[s_jmpCode.Length];

            fixed (void* p = &ret[0])
            {
                byte* ptr = (byte*)p;
                *ptr++ = 0x48;
                *ptr++ = 0xB8;
                *(long*)ptr = (long)jmpTo;
                ptr += 8;
                *ptr++ = 0x50;
                *ptr++ = 0xC3;
            }

            return ret;
        }
    }
}