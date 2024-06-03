/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace MonoHook
{
    /// <summary>
    /// arm64 远距离跳转
    /// </summary>
    public unsafe class CodePatcher_arm64_far : CodePatcher
    {
        private static readonly byte[] s_jmpCode = new byte[] // 20 bytes(字节数过多，太危险了，不建议使用)
        {
            /*
             * ADR: https://developer.arm.com/documentation/ddi0596/2021-09/Base-Instructions/ADR--Form-PC-relative-address-
             * BR: https://developer.arm.com/documentation/ddi0596/2021-09/Base-Instructions/BR--Branch-to-Register-
             */
            0x6A, 0x00, 0x00, 0x10,                        // ADR X10, #C
            0x4A, 0x01, 0x40, 0xF9,                        // LDR X10, [X10,#0]
            0x40, 0x01, 0x1F, 0xD6,                        // BR X10
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 // $dst
        };

        public CodePatcher_arm64_far(IntPtr target, IntPtr replace, IntPtr proxy, int jmpCodeSize) : base(target, replace, proxy, jmpCodeSize) { }

        protected override byte[] GenJmpCode(void* jmpFrom, void* jmpTo) { throw new NotImplementedException(); }
    }
}