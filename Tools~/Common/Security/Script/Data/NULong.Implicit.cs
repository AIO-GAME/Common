﻿// <auto-generated> This code was generated by a tool. </auto-generated>
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Runtime.InteropServices;

namespace AIO.Security
{
    [Serializable, ComVisible(true)]
    partial struct NULong
    {
        /// <param name="value"> <see cref="ulong"/> </param>
        public static implicit operator NULong(ulong value) => new NULong(value);

        /// <param name="value"> <see cref="NULong"/> </param>
        public static implicit operator ulong(NULong value) => value.Value;
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NULong(byte value) => new NULong(value);
      
        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NULong(char value) => new NULong(value);
      
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NULong(ushort value) => new NULong(value);
      
        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NULong(uint value) => new NULong(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NULong(NByte value) => new NULong(value);
      
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NULong(NChar value) => new NULong(value);
      
        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NULong(NUShort value) => new NULong(value);
      
        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NULong(NUInt value) => new NULong(value);
    }
}
