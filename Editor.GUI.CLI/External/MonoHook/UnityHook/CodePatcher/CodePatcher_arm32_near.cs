/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace MonoHook
{
    public unsafe class CodePatcher_arm32_near : CodePatcher
    {
        private static readonly byte[] s_jmpCode = new byte[] // 4 bytes
        {
            0x00, 0x00, 0x00, 0xEA, // B $val   ; $val = (($dst - $src) / 4 - 2) & 0x1FFFFFF
        };

        public CodePatcher_arm32_near(IntPtr target, IntPtr replace, IntPtr proxy) : base(target, replace, proxy,
            s_jmpCode.Length)
        {
            if (Math.Abs((long)target - (long)replace) >= ((1 << 25) - 1))
                throw new ArgumentException("address offset of target and replace must less than ((1 << 25) - 1)");

#if ENABLE_HOOK_DEBUG
            Debug.Log($"CodePatcher_arm32_near: {PrintAddrs()}");
#endif
        }

        protected override byte[] GenJmpCode(void* jmpFrom, void* jmpTo)
        {
            var ret = new byte[s_jmpCode.Length];
            var val = ((int)jmpTo - (int)jmpFrom) / 4 - 2;

            fixed (void* p = &ret[0])
            {
                byte* ptr = (byte*)p;
                *ptr++ = (byte)val;
                *ptr++ = (byte)(val >> 8);
                *ptr++ = (byte)(val >> 16);
                *ptr++ = 0xEA;
            }

            return ret;
        }
    }
}