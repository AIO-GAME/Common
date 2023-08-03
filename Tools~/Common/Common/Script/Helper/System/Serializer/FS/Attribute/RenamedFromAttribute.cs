using System;

namespace AIO
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public sealed class RenamedFromAttribute : Attribute
    {
        public RenamedFromAttribute(string previousName)
        {
            this.previousName = previousName;
        }

        public string previousName { get; }
    }
}