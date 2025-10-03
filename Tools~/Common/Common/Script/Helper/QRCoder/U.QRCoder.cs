using System;

namespace AIO
{
    partial class AHelper
    {
        /// <summary>
        /// 二维码生成器
        /// </summary>
        public partial class QRCoder
        {
            /// <summary>
            /// 二维码生成器
            /// </summary>
            private static Lazy<QRCodeGenerator> Generator = new Lazy<QRCodeGenerator>(() => new QRCodeGenerator());

            /// <summary>
            /// 创建二维码
            /// </summary>
            /// <param name="content"> 内容 </param>
            /// <param name="eccLevel"> 容错级别 </param>
            /// <returns> 二维码数据 </returns>
            public static QRCodeData Create(string content, QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M) { return Generator.Value.CreateQrCode(content, eccLevel); }

            /// <summary>
            /// 创建二维码
            /// </summary>
            /// <param name="uri"> 内容 </param>
            /// <param name="eccLevel"> 容错级别 </param>
            /// <returns> 二维码数据 </returns>
            public static QRCodeData Create(Uri uri, QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M) { return Generator.Value.CreateQrCode(uri.ToString(), eccLevel); }
        }
    }
}