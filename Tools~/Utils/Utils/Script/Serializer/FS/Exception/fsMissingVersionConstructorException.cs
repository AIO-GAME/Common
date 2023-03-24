namespace AIO
{
    using System;

    /// <summary>
    /// 缺少一个数据的构造类型
    /// </summary>
    public sealed class fsMissingVersionConstructorException : Exception
    {

        /// <summary>
        /// 缺少一个数据的构造类型
        /// </summary>
        public fsMissingVersionConstructorException(in Type versionedType, in Type constructorType) :
            base(string.Concat(versionedType, " is missing a constructor for previous model type ", constructorType))
        {
        }
    }
}
