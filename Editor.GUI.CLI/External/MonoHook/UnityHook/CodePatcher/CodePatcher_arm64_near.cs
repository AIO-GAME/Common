/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace MonoHook
{
    /// <summary>
    /// arm64 下 ±128MB 范围内的跳转
    /// </summary>
    public unsafe class CodePatcher_arm64_near : CodePatcher
    {
        private static readonly byte[] s_jmpCode = new byte[] // 4 bytes
        {
            /*
             * from 0x14 to 0x17 is B opcode
             * offset bits is 26
             * https://developer.arm.com/documentation/ddi0596/2021-09/Base-Instructions/B--Branch-
             */
            0x00, 0x00, 0x00, 0x14, //  B $val   ; $val = (($dst - $src)/4) & 7FFFFFF
        };

        public CodePatcher_arm64_near(IntPtr target, IntPtr replace, IntPtr proxy) : base(target, replace, proxy,
            s_jmpCode.Length)
        {
            if (Math.Abs((long)target - (long)replace) >= ((1 << 26) - 1) * 4)
                throw new ArgumentException("address offset of target and replace must less than (1 << 26) - 1) * 4");

#if ENABLE_HOOK_DEBUG
            Debug.Log($"CodePatcher_arm64: {PrintAddrs()}");
#endif
        }

        protected override byte[] GenJmpCode(void* jmpFrom, void* jmpTo)
        {
            byte[] ret = new byte[s_jmpCode.Length];
            int val = (int)((long)jmpTo - (long)jmpFrom) / 4;

            fixed (void* p = &ret[0])
            {
                byte* ptr = (byte*)p;
                *ptr++ = (byte)val;
                *ptr++ = (byte)(val >> 8);
                *ptr++ = (byte)(val >> 16);

                byte last = (byte)(val >> 24);
                last &= 0b11;
                last |= 0x14;

                *ptr = last;
            }

            return ret;
        }
    }
}