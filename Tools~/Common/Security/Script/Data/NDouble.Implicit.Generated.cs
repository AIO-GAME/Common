
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.InteropServices;


namespace AIO.Security
{
    /// <summary>
    /// NDouble is <see cref="double"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NDouble
    { 
        /// <param name="value"> <see cref="double"/> </param>
        public static implicit operator NDouble(double value) => new NDouble(value);

        /// <param name="value"> <see cref="NDouble"/> </param>
        public static implicit operator double(NDouble value) => value.Value;


        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NDouble(byte value) => new NDouble(value);

        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NDouble(short value) => new NDouble(value);

        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NDouble(ushort value) => new NDouble(value);

        /// <param name="value"> <see cref="int"/> </param>
        public static implicit operator NDouble(int value) => new NDouble(value);

        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NDouble(uint value) => new NDouble(value);

        /// <param name="value"> <see cref="long"/> </param>
        public static implicit operator NDouble(long value) => new NDouble(value);

        /// <param name="value"> <see cref="ulong"/> </param>
        public static implicit operator NDouble(ulong value) => new NDouble(value);

        /// <param name="value"> <see cref="float"/> </param>
        public static implicit operator NDouble(float value) => new NDouble(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NDouble(NByte value) => new NDouble(value);

        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator NDouble(NShort value) => new NDouble(value);

        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NDouble(NUShort value) => new NDouble(value);

        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator NDouble(NInt value) => new NDouble(value);

        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NDouble(NUInt value) => new NDouble(value);

        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator NDouble(NLong value) => new NDouble(value);

        /// <param name="value"> <see cref="NULong"/> </param>
        public static implicit operator NDouble(NULong value) => new NDouble(value);

        /// <param name="value"> <see cref="NFloat"/> </param>
        public static implicit operator NDouble(NFloat value) => new NDouble(value);

    }
}
