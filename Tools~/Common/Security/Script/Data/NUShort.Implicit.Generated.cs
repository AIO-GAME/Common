
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
    /// NUShort is <see cref="ushort"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NUShort
    { 
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NUShort(ushort value) => new NUShort(value);

        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator ushort(NUShort value) => value.Value;


        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NUShort(byte value) => new NUShort(value);

        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NUShort(char value) => new NUShort(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NUShort(NByte value) => new NUShort(value);

        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NUShort(NChar value) => new NUShort(value);

    }
}
