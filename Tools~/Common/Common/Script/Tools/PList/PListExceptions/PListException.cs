using System;

namespace AIO.PList
{
    /// <inheritdoc />
    [Serializable]
    public class PListException : Exception
    {
        /// <inheritdoc />
        public PListException() { }
        /// <inheritdoc />
        public PListException(string message) : base(message) { }
        /// <inheritdoc />
        public PListException(string message, Exception inner) : base(message, inner) { }
        /// <inheritdoc />
        protected PListException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
