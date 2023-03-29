using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine.SceneManagement;

using UnityObject = UnityEngine.Object;

public static partial class UtilsEngine
{
    /// <summary>
    /// GameObject 工具
    /// </summary>
    public static class GO
    {

        /// <summary>
        /// 真正的相等
        /// </summary>
        public static bool TrulyEqual(in UnityObject a, in UnityObject b)
        {
            if (a != b) return false;
            if ((a == null) != (b == null)) return false;
            return true;
        }

    }
}