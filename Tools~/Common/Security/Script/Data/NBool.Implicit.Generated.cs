
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
    /// NBool is <see cref="bool"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NBool
    { 
        /// <param name="value"> <see cref="bool"/> </param>
        public static implicit operator NBool(bool value) => new NBool(value);

        /// <param name="value"> <see cref="NBool"/> </param>
        public static implicit operator bool(NBool value) => value.Value;


    }
}
