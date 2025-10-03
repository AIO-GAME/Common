using System.Collections;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// Contains the raw data of a QR code, as a matrix of modules (pixels).
    /// 二维码数据
    /// </summary>
    public class QRCodeData
    {
        /// <summary>
        /// The matrix of modules (pixels) of the QR code. True means a dark module, false means a light module.
        /// 数据矩阵。True表示黑色模块，False表示白色模块。
        /// </summary>
        public readonly List<BitArray> ModuleMatrix;

        /// <param name="originalSize">QR code's original width/height value in pixels.</param>
        /// <returns>The multiplier for <paramref name="originalSize"/>. So, if it's 2, the resulting texture will be twice as big. If it's 5, the texture will be 5 times as big.</returns>
        public delegate int SizeMultiplierDelegate(int originalSize);

        /// <summary>
        /// Creates a new instance of the QRCodeData class for a specific version.
        /// </summary>
        /// <param name="version"> The version of the QR code (1-40).</param>
        public QRCodeData(int version)
        {
            int size = 21 + (version - 1) * 4;
            ModuleMatrix = new List<BitArray>(size);
            for (int i = 0; i < size; i++)
                ModuleMatrix.Add(new BitArray(size));
        }

    }
}