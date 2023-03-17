using System;

namespace AIO.PList
{

    /// <inheritdoc />
    [global::System.Serializable]
    public class PListFormatException : PListException
    {
        /// <inheritdoc />
        public PListFormatException() { }
     
        /// <inheritdoc />
        public PListFormatException(string message) : base(message) { }

        /// <inheritdoc />
        public PListFormatException(string message, Exception inner) : base(message, inner) { }

        /// <inheritdoc />
        protected PListFormatException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
