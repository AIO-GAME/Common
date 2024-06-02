﻿// <auto-generated> This code was generated by a tool. </auto-generated>
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

namespace AIO.Security
{
    partial struct NSByte
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NSByte a => Value.Equals(a.Value),
            sbyte b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);
    }
}
