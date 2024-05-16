
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
    /// NFloat is <see cref="float"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NFloat
    { 
        /// <param name="value"> <see cref="float"/> </param>
        public static implicit operator NFloat(float value) => new NFloat(value);

        /// <param name="value"> <see cref="NFloat"/> </param>
        public static implicit operator float(NFloat value) => value.Value;


        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NFloat(byte value) => new NFloat(value);

        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NFloat(short value) => new NFloat(value);

        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NFloat(ushort value) => new NFloat(value);

        /// <param name="value"> <see cref="int"/> </param>
        public static implicit operator NFloat(int value) => new NFloat(value);

        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NFloat(uint value) => new NFloat(value);

        /// <param name="value"> <see cref="long"/> </param>
        public static implicit operator NFloat(long value) => new NFloat(value);

        /// <param name="value"> <see cref="ulong"/> </param>
        public static implicit operator NFloat(ulong value) => new NFloat(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NFloat(NByte value) => new NFloat(value);

        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator NFloat(NShort value) => new NFloat(value);

        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NFloat(NUShort value) => new NFloat(value);

        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator NFloat(NInt value) => new NFloat(value);

        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NFloat(NUInt value) => new NFloat(value);

        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator NFloat(NLong value) => new NFloat(value);

        /// <param name="value"> <see cref="NULong"/> </param>
        public static implicit operator NFloat(NULong value) => new NFloat(value);

    }
}
