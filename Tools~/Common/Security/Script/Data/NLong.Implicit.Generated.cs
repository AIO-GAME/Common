
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
    /// NLong is <see cref="long"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NLong
    { 
        /// <param name="value"> <see cref="long"/> </param>
        public static implicit operator NLong(long value) => new NLong(value);

        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator long(NLong value) => value.Value;


        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NLong(byte value) => new NLong(value);

        /// <param name="value"> <see cref="sbyte"/> </param>
        public static implicit operator NLong(sbyte value) => new NLong(value);

        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NLong(char value) => new NLong(value);

        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NLong(short value) => new NLong(value);

        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NLong(ushort value) => new NLong(value);

        /// <param name="value"> <see cref="int"/> </param>
        public static implicit operator NLong(int value) => new NLong(value);

        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NLong(uint value) => new NLong(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NLong(NByte value) => new NLong(value);

        /// <param name="value"> <see cref="NSByte"/> </param>
        public static implicit operator NLong(NSByte value) => new NLong(value);

        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NLong(NChar value) => new NLong(value);

        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator NLong(NShort value) => new NLong(value);

        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NLong(NUShort value) => new NLong(value);

        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator NLong(NInt value) => new NLong(value);

        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NLong(NUInt value) => new NLong(value);

    }
}
