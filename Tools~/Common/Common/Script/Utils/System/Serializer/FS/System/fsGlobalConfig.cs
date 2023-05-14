namespace AIO
{
    /// <summary>
    /// Global configuration options.
    /// </summary>
    public static class fsGlobalConfig
    {
        /// <summary>
        /// Should deserialization be case sensitive? If this is false and the
        /// JSON has multiple members with the same keys only separated by case,
        /// then this results in undefined behavior.
        /// </summary>
        public static bool IsCaseSensitive = true;

        /// <summary>
        /// If exceptions are allowed internally, then additional date formats
        /// can be deserialized. Note that the Full Serializer public API will
        /// *not* throw exceptions with this enabled; errors will still be
        /// returned in a fsResult instance.
        /// </summary>
        public static bool AllowInternalExceptions = true;

        /// <summary>
        /// This string will be used to prefix fields used internally by
        /// FullSerializer.
        /// </summary>
        public static string InternalFieldPrefix = "$";
    }
}
