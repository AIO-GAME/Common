namespace AIO
{
    partial class RHelper
    {
        /// <summary>
        /// 平台工具类
        /// </summary>
        public static class Platform
        {
            /// <summary>
            /// 支持jit
            /// </summary>
            public static readonly bool supportsJit;

            static Platform()
            {
                supportsJit = CheckJitSupport();
            }

            private static bool CheckJitSupport()
            {
                // Temporary hotfix
                // Generally it seems like JIT is becoming more and more unreliable
                // And some of the generated IL we were using crashes in some cases, but it's hard to isolate
                // Because the delegate approach is very close in speed, we'll just disable it altogether until Bolt 2
                // generates full C# scripts.
                // https://forum.unity.com/threads/is-jit-no-longer-supported-on-standalone-mono.671572/
                // https://support.ludiq.io/communities/5/topics/3129-bolt-143-runtime-broken
                // https://support.ludiq.io/communities/5/topics/4013-unity-crash-randomly-after-hit-play
                return false;
            }
        }
    }
}