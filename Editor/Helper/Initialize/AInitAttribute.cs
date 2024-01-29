/*|============|*|
|*|Author:     |*| star fire
|*|Date:       |*| 2024-01-29
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;

namespace AIO.UEditor
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class AInitAttribute : Attribute
    {
        public AInitAttribute(int order = 0)
        {
            Order = order;
        }

        public AInitAttribute(EInitAttrMode mode, int order = 0)
        {
            Mode = mode;
            Order = order;
        }

        public EInitAttrMode Mode { get; set; } = EInitAttrMode.Editor;

        public int Order { get; }
    }
}