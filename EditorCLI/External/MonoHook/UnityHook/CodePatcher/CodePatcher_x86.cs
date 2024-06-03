/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace MonoHook
{
    public unsafe class CodePatcher_x86 : CodePatcher
    {
        protected static readonly byte[] s_jmpCode = new byte[] // 5 bytes
        {
            0xE9, 0x00, 0x00, 0x00, 0x00, // jmp $val   ; $val = $dst - $src - 5 
        };

        public CodePatcher_x86(IntPtr target, IntPtr replace, IntPtr proxy) : base(target, replace, proxy,
            s_jmpCode.Length)
        {
        }

        protected override byte[] GenJmpCode(void* jmpFrom, void* jmpTo)
        {
            byte[] ret = new byte[s_jmpCode.Length];
            int val = (int)jmpTo - (int)jmpFrom - 5;

            fixed (void* p = &ret[0])
            {
                byte* ptr = (byte*)p;
                *ptr = 0xE9;
                int* pOffset = (int*)(ptr + 1);
                *pOffset = val;
            }

            return ret;
        }
    }
}