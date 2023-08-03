namespace AIO
{
    using System;

    /// <summary>
    /// 出现多个相同类型
    /// </summary>
    public sealed class fsDuplicateVersionNameException : Exception
    {
        /// <summary>
        /// 出现多个相同类型
        /// </summary>
        public fsDuplicateVersionNameException(Type typeA, Type typeB, string version) :
            base(typeA + " and " + typeB + " have the same version string (" + version + "); please change one of them.")
        { }
    }
}
