using System;

namespace AIO
{
    /// <summary>
    /// 数据过长异常
    /// </summary>
    public class DataTooLongException : Exception
    {
        /// <summary>
        /// 数据过长异常
        /// </summary>
        /// <param name="eccLevel"> eccLevel </param>
        /// <param name="encodingMode"> encodingMode </param>
        /// <param name="maxSizeByte"> maxSizeByte </param>
        public DataTooLongException(
            string eccLevel,
            string encodingMode,
            int    maxSizeByte
        ) : base($"The given payload exceeds the maximum size of the QR code standard. The maximum size allowed for the choosen paramters (ECC level={eccLevel}, EncodingMode={encodingMode}) is {maxSizeByte} byte."
                ) { }

        /// <summary>
        /// 数据过长异常
        /// </summary>
        /// <param name="eccLevel"> eccLevel </param>
        /// <param name="encodingMode"> encodingMode </param>
        /// <param name="version"> version </param>
        /// <param name="maxSizeByte"> maxSizeByte </param>
        public DataTooLongException(
            string eccLevel,
            string encodingMode,
            int    version,
            int    maxSizeByte
        ) : base($"The given payload exceeds the maximum size of the QR code standard. The maximum size allowed for the choosen paramters (ECC level={eccLevel}, EncodingMode={encodingMode}, FixedVersion={version}) is {maxSizeByte} byte."
                ) { }
    }
}