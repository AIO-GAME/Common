using UObject = UnityEngine.Object;

namespace AIO
{
    partial class RHelper
    {
        /// <summary>
        /// GameObject 工具
        /// </summary>
        public static class GO
        {
            /// <summary>
            /// 真正的相等
            /// </summary>
            public static bool TryEqual(in UObject a, in UObject b)
            {
                if (a != b) return false;
                return a == null == (b == null);
            }
        }
    }
}