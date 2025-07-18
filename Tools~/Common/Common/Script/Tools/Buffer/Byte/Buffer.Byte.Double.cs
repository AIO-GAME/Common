using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AIO
{
    partial class BufferByte : IReadDouble, IWriteDouble
    {
        /// <inheritdoc/> 
        public double ReadDouble(bool reverse = false) { return Arrays.GetDouble(ref ReadIndex, true, reverse); }

        /// <inheritdoc/>
        public void WriteDouble(double value, bool reverse = false)
        {
            var bytes = Encoding.UTF8.GetBytes(value.ToString(NumberFormatInfo.CurrentInfo));
            WriteByteArray(bytes, reverse);
        }

        /// <inheritdoc/>
        public double[] ReadDoubleArray(bool reverse = false)
        {
            var len = ReadLen();
            var doubles = new double[len];
            for (var i = 0; i < len; i++)
            {
                var bytes = Arrays.GetByteArray(ref ReadIndex, reverse);
                doubles[i] = double.Parse(Encoding.UTF8.GetString(bytes), NumberFormatInfo.CurrentInfo);
            }

            return doubles;
        }

        /// <inheritdoc/>
        public void WriteDoubleArray(ICollection<double> value, bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value)
            {
                var bytes = Encoding.UTF8.GetBytes(item.ToString(NumberFormatInfo.CurrentInfo));
                WriteByteArray(bytes, reverse);
            }
        }
    }
}