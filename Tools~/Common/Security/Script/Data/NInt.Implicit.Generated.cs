
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
    /// NInt is <see cref="int"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NInt
    { 
        /// <param name="value"> <see cref="int"/> </param>
        public static implicit operator NInt(int value) => new NInt(value);

        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator int(NInt value) => value.Value;

        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator char(NInt value) => (char)value.Value;

        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator long(NInt value) => (long)value.Value;

        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NChar(NInt value) => new NChar(value.Value);

        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator NLong(NInt value) => new NLong(value.Value);


        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NInt(byte value) => new NInt(value);

        /// <param name="value"> <see cref="sbyte"/> </param>
        public static implicit operator NInt(sbyte value) => new NInt(value);

        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NInt(char value) => new NInt(value);

        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NInt(short value) => new NInt(value);

        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NInt(ushort value) => new NInt(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NInt(NByte value) => new NInt(value);

        /// <param name="value"> <see cref="NSByte"/> </param>
        public static implicit operator NInt(NSByte value) => new NInt(value);

        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NInt(NChar value) => new NInt(value);

        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator NInt(NShort value) => new NInt(value);

        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NInt(NUShort value) => new NInt(value);

    }
}
