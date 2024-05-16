
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
    /// NByte is <see cref="byte"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NByte
    { 
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NByte(byte value) => new NByte(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator byte(NByte value) => value.Value;


    }
}
