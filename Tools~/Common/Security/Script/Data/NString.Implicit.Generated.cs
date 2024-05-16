
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
    /// NString is <see cref="string"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NString
    { 
        /// <param name="value"> <see cref="string"/> </param>
        public static implicit operator NString(string value) => new NString(value);

        /// <param name="value"> <see cref="NString"/> </param>
        public static implicit operator string(NString value) => value.Value;


    }
}
