using System.Collections.Generic;

namespace AIO
{
    public partial class BufferByte
    {
        /// <inheritdoc/> 
        public double[] ReadDoubleArray(in bool all, in bool reverse = false)
        {
            return Arrays.GetDoubleArray(ref ReadIndex, all, reverse);
        }

        /// <inheritdoc/> 
        public void WriteDoubleArray(in ICollection<double> value, in bool all, in bool reverse = false)
        {
            WriteLen(value.Count);
            foreach (var item in value) WriteDouble(item, all, reverse);
        }
    }

    public partial interface IWrite
    {
        void WriteDoubleArray(in ICollection<double> value, in bool all, in bool reverse = false);
    }

    public partial interface IRead
    {
        double[] ReadDoubleArray(in bool all, in bool reverse = false);
    }
}