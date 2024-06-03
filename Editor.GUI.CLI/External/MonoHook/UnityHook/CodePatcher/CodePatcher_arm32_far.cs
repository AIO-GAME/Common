/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace MonoHook
{
    public unsafe class CodePatcher_arm32_far : CodePatcher
    {
        private static readonly byte[] s_jmpCode = new byte[] // 8 bytes
        {
            0x04, 0xF0, 0x1F, 0xE5, // LDR PC, [PC, #-4]
            0x00, 0x00, 0x00, 0x00, // $val
        };

        public CodePatcher_arm32_far(IntPtr target, IntPtr replace, IntPtr proxy) : base(target, replace, proxy,
            s_jmpCode.Length)
        {
            if (Math.Abs((long)target - (long)replace) < ((1 << 25) - 1))
                throw new ArgumentException(
                    "address offset of target and replace must larger than ((1 << 25) - 1), please use InstructionModifier_arm32_near instead");

#if ENABLE_HOOK_DEBUG
            Debug.Log($"CodePatcher_arm32_far: {PrintAddrs()}");
#endif
        }

        protected override byte[] GenJmpCode(void* jmpFrom, void* jmpTo)
        {
            byte[] ret = new byte[s_jmpCode.Length];

            fixed (void* p = &ret[0])
            {
                uint* ptr = (uint*)p;
                *ptr++ = 0xE51FF004;
                *ptr = (uint)jmpTo;
            }

            return ret;
        }
    }
}