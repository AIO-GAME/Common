
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
    /// NShort is <see cref="short"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NShort
    { 
        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NShort(short value) => new NShort(value);

        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator short(NShort value) => value.Value;


        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NShort(byte value) => new NShort(value);

        /// <param name="value"> <see cref="sbyte"/> </param>
        public static implicit operator NShort(sbyte value) => new NShort(value);

        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NShort(char value) => new NShort(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NShort(NByte value) => new NShort(value);

        /// <param name="value"> <see cref="NSByte"/> </param>
        public static implicit operator NShort(NSByte value) => new NShort(value);

        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NShort(NChar value) => new NShort(value);

    }
}
