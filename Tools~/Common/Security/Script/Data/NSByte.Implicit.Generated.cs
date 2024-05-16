
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
    /// NSByte is <see cref="sbyte"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NSByte
    { 
        /// <param name="value"> <see cref="sbyte"/> </param>
        public static implicit operator NSByte(sbyte value) => new NSByte(value);

        /// <param name="value"> <see cref="NSByte"/> </param>
        public static implicit operator sbyte(NSByte value) => value.Value;


    }
}
