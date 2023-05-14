using System;

namespace AIO
{
    /// <summary>
    /// This allows you to forward serialization of an object to one of its
    /// members. For example,
    /// [fsForward("Values")]
    /// struct Wrapper {
    /// public int[] Values;
    /// }
    /// Then `Wrapper` will be serialized into a JSON array of integers. It will
    /// be as if `Wrapper` doesn't exist.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public sealed class fsForwardAttribute : Attribute
    {
        /// <summary>
        /// Forward object serialization to an instance member. See class
        /// comment.
        /// </summary>
        /// <param name="memberName">
        /// The name of the member that we should serialize this object as.
        /// </param>
        public fsForwardAttribute(string memberName)
        {
            MemberName = memberName;
        }

        /// <summary>
        /// The name of the member we should serialize as.
        /// </summary>
        public string MemberName;
    }
}